using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using adabtek.IPsecLite.Constants;
using System.Linq;
using adabtek.IPsecLite.Cryptography;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.IKEv2;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.SPDB;
namespace adabtek.IPsecLite.SADB
{
    public enum SA_STATUS : byte
    {
        INITi_SENT = 10,
        AUTHi_SENT = 11,
        INITr_SENT = 20,
        AUTHr_SENT = 21,
        ESTABLISHED = 100
    }
    public struct KEYS_TYPE
    {
        public byte[] SK_d;
        public byte[] SK_ai;
        public byte[] SK_ei;
        public byte[] SK_ar;
        public byte[] SK_er;
        public byte[] SK_pi;
        public byte[] SK_pr;
    }
    public struct CRYPTO_ALGS_TYPE
    {
        public IKE_ENCR_ALGS EncrAlg;
        public short EncrKeyLength;
        public short EncrBlockSize;
        public IKE_INTEG_ALGS IntegAlg;
        public short IntegAlgKeyLength;
    }
    public struct AUTH_METHOD
    {
        public IKE_AUTH_METHODS AuthMethod;
        public IKE_INTEG_ALGS AuthAlg;
    }
    public struct TRAFFIC_STATS
    {
        public int ProcessedBytes;
        public long ProcessingCycles;
        public double ProcessingTime;
    }
    public class CHILD_SA_TYPE
    {
        public struct REPLAY_WINDOW_TYPE
        {
            public ushort replayWindow;
            public int left;
            public int right;
        }

        //Key fields
        string sourceIP;
        byte[] peerIP;
        int spi;
        IKE_MODE mode;
        string childSAKey;
        //End Key fields
        public byte[] SK_a;
        public byte[] SK_e;
        public CRYPTO_ALGS_TYPE cryptoAlgs;
        IKE_PROTOCOLS protocol;
        REPLAY_WINDOW_TYPE currentWindow;
        REPLAY_WINDOW_TYPE prevWindow;
        public CHILD_SA_TYPE(int SPI, IKE_MODE Mode, string SourceIP, byte[] PeerIP, IKE_PROTOCOLS Protocol)
        {
            this.sourceIP = SourceIP;
            this.peerIP = PeerIP;
            this.spi = SPI;
            this.mode = Mode;
            this.childSAKey = this.sourceIP + ":" + this.spi.ToString();
            trafficStatistics.ProcessedBytes = 0;
            trafficStatistics.ProcessingCycles = 0;
            trafficStatistics.ProcessingTime = 0;
            this.protocol = Protocol;
            this.sequenceNumber = 0;
            this.currentWindow.replayWindow = 0;
            this.currentWindow.left = APP_CONST.ANTI_REPLAY_WINDOW_SIZE;
            this.currentWindow.right = 1;
        }
        public REPLAY_WINDOW_TYPE ReplayWindow
        {
            get { return this.currentWindow; }
        }
        public byte[] PeerIP 
        {
            get 
            {
                if (this.peerIP != null)
                    return this.peerIP;
                else
                    return null;
            } 
        }
        public string SourceIP
        {
            get
            {
                if (this.sourceIP != null)
                    return this.sourceIP;
                else
                    return "";
            }
        }
        public int SPI 
        { 
            get { return this.spi; } 
        }
        public IKE_MODE Mode
        {
            get { return this.mode; }
        }
        public string ChildSAKey 
        { 
            get { return this.childSAKey; } 
        }
        public IKE_PROTOCOLS Protocol
        {
            get {return this.protocol;}
        }
        int sequenceNumber;
        List<int> delayedPackets = new List<int>(16);
        Random rnd = new Random();
        TRAFFIC_STATS trafficStatistics;
        public TRAFFIC_STATS TrafficStatistics
        {
            get { return trafficStatistics; }
        }

        public void UpdateTrafficStatistics(int ProcessedBytes, long ProcessingCycles, double ProcessingTime)
        {
            trafficStatistics.ProcessedBytes += ProcessedBytes;
            trafficStatistics.ProcessingCycles += ProcessingCycles;
            trafficStatistics.ProcessingTime += ProcessingTime;
        }
        public int SequenceNumber
        {
            get { return this.sequenceNumber; }
        }
        public int GetNextSequenceNumber()
        {
            int sn = 0;
            if (APP_CONFIG.SEND_OUT_OF_SEQUENCE_PERCENT > 0)
            {
                int r = rnd.Next(0, 100);
                if (r < APP_CONFIG.SEND_OUT_OF_SEQUENCE_PERCENT)
                {
                    delayedPackets.Add(++this.sequenceNumber);
                    sn = ++this.sequenceNumber;
                }
                else 
                    if (delayedPackets.Count > 0)
                    {
                        sn = delayedPackets.First();
                        delayedPackets.Remove(sn);
                    }
                    else
                        sn = ++this.sequenceNumber;
            }
            else
                sn = ++this.sequenceNumber;

            return sn;
        }
        public PROTECTION_RESULTS IsSequenceNumberValid(int SequenceNumber)
        {
            PROTECTION_RESULTS result = PROTECTION_RESULTS.OK;
            if (SequenceNumber < this.currentWindow.right)
            {
                result = PROTECTION_RESULTS.TOO_OLD;
                return result;
            }

            this.prevWindow = this.currentWindow;

            if (SequenceNumber > this.currentWindow.left)
            {
                this.currentWindow.replayWindow >>= (SequenceNumber - this.currentWindow.left);
                this.currentWindow.left = SequenceNumber;
                this.currentWindow.right = this.currentWindow.left - APP_CONST.ANTI_REPLAY_WINDOW_SIZE + 1;
                this.currentWindow.replayWindow |= 0x8000;
                return result;
            }

            byte bitNumber = (byte)((SequenceNumber - this.currentWindow.right + 1) % APP_CONST.ANTI_REPLAY_WINDOW_SIZE);
            if (bitNumber == 0)
                bitNumber = APP_CONST.ANTI_REPLAY_WINDOW_SIZE;
            if ((this.currentWindow.replayWindow & (0x0001 << (bitNumber - 1))) != 0)
            {
                result = PROTECTION_RESULTS.REPLAYED;
                return result;
            }
            else
            {
                this.currentWindow.replayWindow |= (ushort) (0x0001 << (bitNumber - 1));
                return result;
            }
        }
        public void RollbackReplayWindow()
        {
            this.currentWindow = this.prevWindow;
        }
    }

    public class SA
    {

        public delegate void ChildSACreatedHandler(object sender, ChildSACreatedEventArgs e1, ChildSACreatedEventArgs e2);
        public delegate void ChildSADeletedHandler(object sender, ChildSADeletedEventArgs e1, ChildSADeletedEventArgs e2);
        public delegate void SADeletedHandler(object sender, long SAKey);
        public static event ChildSACreatedHandler ChildSACreated;
        public static event ChildSADeletedHandler ChildSADeleted;
        public static event SADeletedHandler SADeleted;

        //Key is the SPI generated by this host
        static Dictionary<long, SA> saDB = new Dictionary<long, SA>();

        //TODO: For now we assume SPIs are unique
        bool isInitiatorSA;
        long initiatorSPI;
        byte[] initiatorIP;
        long responderSPI;
        byte[] responderIP;
        SA_STATUS status;
        KEYS_TYPE keys;
        IKE_DH_GROUPS dhGroup;
        IKE_PRFS prf;
        short prfOutputLength;
        AUTH_METHOD authMethod;
        CRYPTO_ALGS_TYPE cryptoAlgs;
        IKE_Proposal[] initiatorChildSAProposal;
        IKE_Proposal[] responderChildSAProposal;

        IKE_MODE mode;
        
        string[] childSAKeys = new string[2];

        public static Dictionary<long, SA> SADB
        {
            get { return saDB; }
        }

        public IKE_MODE Mode
        {
            set { this.mode = value; }
            get { return this.mode; }
        }
        static Dictionary<string, CHILD_SA_TYPE> childSAs = new Dictionary<string,CHILD_SA_TYPE>();

        IKE ikeINITi;
        IKE ikeINITr;

        public static ChildSACreatedEventArgs[] GetChildSAEvents(long SAKey)
        {
            SA sa = null;
            saDB.TryGetValue(SAKey, out sa);
            if (sa != null)
            {
                ChildSACreatedEventArgs[] e = new ChildSACreatedEventArgs[2];
                e[0] = new ChildSACreatedEventArgs(SAKey, sa.childSAKeys[0], sa.initiatorIP, sa.responderIP);
                e[1] = new ChildSACreatedEventArgs(SAKey, sa.childSAKeys[1], sa.responderIP, sa.initiatorIP);
                return e;
            }
            else
                return null;
        }
        public byte[] InitiatorIP 
        { 
            get { return this.initiatorIP; } 
        }
        public byte[] ResponderIP 
        { 
            get {return this.responderIP;}  
        }
        public string SPIiToHex 
        { 
            get { return this.ikeINITi.InitiatorSPIToHex; } 
        }
        public string SPIrToHex 
        { 
            get { if (this.ikeINITr != null) return this.ikeINITr.ResponderSPIToHex; else return ""; } 
        }
        public long InitiatorSPI 
        { 
            get { return this.initiatorSPI; } 
        }
        public long ResponderSPI 
        { 
            get { return this.responderSPI; } 
        }
        public bool IsInitiatorSA 
        { 
            get { return this.isInitiatorSA; } 
        }
        public SA_STATUS Status 
        {
            set { this.status = value; }
            get { return this.status; } 
        }

        private byte[] PeerIPAddress()
        {
            if (this.isInitiatorSA)
                return this.responderIP;
            else
                return this.initiatorIP;
        }
        public long SAKey
        {
            get
            {
                if (this.isInitiatorSA)
                    return this.initiatorSPI;
                else
                    return this.responderSPI;
            }
        }
        public SA(IKE ikeINITi, byte[] InitiatorIP, byte[] ResponderIP, IKE_MODE Mode)
        {
            this.isInitiatorSA = true;
            this.mode = Mode;
            this.initiatorSPI = ikeINITi.InitiatorSPI;
            this.initiatorIP = InitiatorIP;
            this.ikeINITi = ikeINITi;
            this.responderIP = ResponderIP;

            //TODO: This needs to become dynamic
            this.authMethod.AuthMethod = IKE_AUTH_METHODS.SHARED_KEY_MIC;
            foreach (IKE_Transform t in ikeINITi.SAPayload.Proposals[0].Transforms)
                if (t.TransformType == IKE_TRANSFORM_TYPES.INTEG)
                {
                    this.authMethod.AuthAlg = (IKE_INTEG_ALGS) t.TransformID;
                    break;
                }
            saDB.Add(ikeINITi.InitiatorSPI, this);
        }
        public SA(IKE ikeINITi, byte[] InitiatorIP, IKE ikeINITr, byte[] ResponderIP)
        {
            this.isInitiatorSA = false;
            this.mode = IKE_MODE.TUNNEL; //Default
            this.initiatorSPI = ikeINITr.InitiatorSPI;
            this.initiatorIP = InitiatorIP;
            this.responderSPI = ikeINITr.ResponderSPI;
            this.responderIP = ResponderIP;
            this.ikeINITi = ikeINITi;
            this.ikeINITr = ikeINITr;
            //TODO: This needs to become dynamic
            this.authMethod.AuthMethod = IKE_AUTH_METHODS.SHARED_KEY_MIC;
            foreach (IKE_Transform t in ikeINITi.SAPayload.Proposals[0].Transforms)
                if (t.TransformType == IKE_TRANSFORM_TYPES.INTEG)
                {
                    this.authMethod.AuthAlg = (IKE_INTEG_ALGS)t.TransformID;
                    break;
                }

            saDB.Add(ikeINITr.ResponderSPI, this);
            ProcessINIT(this.ikeINITi, this.ikeINITr.ResponderSPI);
            
        }

        public void AddChildSAProposal(IKE_Proposal[] Proposal)
        {
            if (this.isInitiatorSA)
                this.initiatorChildSAProposal = Proposal;
            else
                this.responderChildSAProposal = Proposal;
        }

        public static SA ProcessINIT(IKE ikeINIT, long SAKey)
        {
            SA sa;
            bool saExists;
            saExists = saDB.TryGetValue(SAKey, out sa);
            if (saExists)
            {
                //The message is from the responder
                if (ikeINIT.Flags == 0x20)
                {
                    sa.ikeINITr = ikeINIT;
                    sa.responderSPI = ikeINIT.ResponderSPI;
                }
                //TODO: Process of selecting a proposal needs to be double checked
                IKE_Proposal proposal = ikeINIT.SAPayload.Proposals.First();
                foreach (IKE_Transform transform in proposal.Transforms)
                {
                    //TODO: Attributes to be revisited
                    switch (transform.TransformType)
                    {
                        case IKE_TRANSFORM_TYPES.ENCR:
                            sa.cryptoAlgs.EncrAlg = (IKE_ENCR_ALGS) transform.TransformID;

                            switch (sa.cryptoAlgs.EncrAlg)
                            //TODO: Include other encryption algorithms
                            {
                                case IKE_ENCR_ALGS.ENCR_AES_CBC:
                                    {
                                        IKE_Transform_Attribute attribute = transform.Attributes.First();
                                        switch ((IKE_ATTRIBUTE_TYPES)attribute.AttributeType)
                                        {
                                            case IKE_ATTRIBUTE_TYPES.KEY_LENGTH:
                                                sa.cryptoAlgs.EncrKeyLength = (short)(Utils.BytesToShort(attribute.Value[0], attribute.Value[1]) / 8);
                                                sa.cryptoAlgs.EncrBlockSize = sa.cryptoAlgs.EncrKeyLength;
                                                break;
                                        }
                                        break;
                                    }
                                case IKE_ENCR_ALGS.ENCR_DES:
                                    {
                                        IKE_Transform_Attribute attribute = transform.Attributes.First();
                                        switch ((IKE_ATTRIBUTE_TYPES)attribute.AttributeType)
                                        {
                                            case IKE_ATTRIBUTE_TYPES.KEY_LENGTH:
                                                sa.cryptoAlgs.EncrKeyLength = (short)(Utils.BytesToShort(attribute.Value[0], attribute.Value[1]) / 8);
                                                sa.cryptoAlgs.EncrBlockSize = sa.cryptoAlgs.EncrKeyLength;
                                                break;
                                        }
                                        break;
                                    }
                                case IKE_ENCR_ALGS.ENCR_DES3:
                                    {
                                        foreach (IKE_Transform_Attribute attribute in transform.Attributes)
                                        {
                                            switch ((IKE_ATTRIBUTE_TYPES)attribute.AttributeType)
                                            {
                                                case IKE_ATTRIBUTE_TYPES.KEY_LENGTH:
                                                    sa.cryptoAlgs.EncrKeyLength = (short)(Utils.BytesToShort(attribute.Value[0], attribute.Value[1]) / 8);
                                                    break;
                                                case IKE_ATTRIBUTE_TYPES.BLOCK_SIZE:
                                                    sa.cryptoAlgs.EncrBlockSize = (short)(Utils.BytesToShort(attribute.Value[0], attribute.Value[1]) / 8);
                                                    break;
                                            }
                                        }
                                        break;
                                    }
                            }
                            break;
                        case IKE_TRANSFORM_TYPES.INTEG:
                            sa.cryptoAlgs.IntegAlg = (IKE_INTEG_ALGS)transform.TransformID;
                            break;
                        case IKE_TRANSFORM_TYPES.PRF:
                            sa.prf = (IKE_PRFS)transform.TransformID;
                            break;
                        case IKE_TRANSFORM_TYPES.DH:
                            sa.dhGroup = (IKE_DH_GROUPS)transform.TransformID;
                            break;
                    }
                }
            }

            return sa;
        }

        private byte[] PRF(byte[] K, byte[] S, IKE_PRFS PRF)
        {
            byte[] buffer = null;
            switch (PRF)
            {
                case IKE_PRFS.PRF_HMAC_SHA1:
                    {
                        HMACSHA1 prf = new HMACSHA1(K);
                        buffer = prf.ComputeHash(S);
                    }
                    break;
                case IKE_PRFS.PRF_HMAC_MD5:
                    {
                        HMACMD5 prf = new HMACMD5(K);
                        buffer = prf.ComputeHash(S);
                    }
                    break;
            }
            return buffer;
        }
        private byte[] PRFPlus(byte[] SKEYSEED, short OutputLength)
        {
            byte[] buffer = new byte[OutputLength];
            int bufferIndex = 0;
            //Ni | Nr | SPIi | SPIr
            int seedLength = this.ikeINITi.NoncePayload.Data.Length + this.ikeINITr.NoncePayload.Data.Length + (8 * 2);
            byte[] S = new byte[seedLength + 1];
            this.ikeINITi.NoncePayload.Data.CopyTo(S, 0);
            this.ikeINITr.NoncePayload.Data.CopyTo(S, (this.ikeINITi.NoncePayload.Data.Length));
            Utils.LongToBytes(this.ikeINITr.InitiatorSPI).CopyTo(S, this.ikeINITi.NoncePayload.Data.Length + this.ikeINITr.NoncePayload.Data.Length);
            Utils.LongToBytes(this.ikeINITr.ResponderSPI).CopyTo(S, this.ikeINITi.NoncePayload.Data.Length + 8);

            byte i = 0x01;
            S[S.Length - 1] = i;
            
            byte[] T = PRF(SKEYSEED, S, this.prf);
            T.CopyTo(buffer, bufferIndex);
            bufferIndex += T.Length;
            OutputLength -= this.prfOutputLength;

            byte[] S1;
            
            while (OutputLength > this.prfOutputLength)
            {
                S1 = new byte[T.Length + S.Length];
                T.CopyTo(S1, 0);
                S[S.Length - 1] = ++i;
                S.CopyTo(S1, T.Length);
                T = PRF(SKEYSEED, S1, this.prf);

                T.CopyTo(buffer, bufferIndex);
                bufferIndex += T.Length;
                OutputLength -= this.prfOutputLength;
            }
            if (OutputLength > 0)
            {
                S1 = new byte[T.Length + S.Length];
                T.CopyTo(S1, 0);
                S[S.Length - 1] = ++i;
                S.CopyTo(S1, T.Length);
                T = PRF(SKEYSEED, S1, this.prf);

                Utils.MemCpy(T, 0, ref buffer, bufferIndex, OutputLength);
            }

            return buffer;
        }
        private byte[] PRFPlusChildSA(byte[] SKEYSEED, short OutputLength)
        {
            byte[] buffer = new byte[OutputLength];
            int bufferIndex = 0;
            //Ni | Nr 
            int seedLength = this.ikeINITi.NoncePayload.Data.Length + this.ikeINITr.NoncePayload.Data.Length;
            byte[] S = new byte[seedLength + 1];
            this.ikeINITi.NoncePayload.Data.CopyTo(S, 0);
            this.ikeINITr.NoncePayload.Data.CopyTo(S, (this.ikeINITi.NoncePayload.Data.Length));

            byte i = 0x01;
            S[S.Length - 1] = i;

            byte[] T = PRF(SKEYSEED, S, this.prf);
            T.CopyTo(buffer, bufferIndex);
            bufferIndex += T.Length;
            OutputLength -= this.prfOutputLength;

            byte[] S1;

            while (OutputLength > this.prfOutputLength)
            {
                S1 = new byte[T.Length + S.Length];
                T.CopyTo(S1, 0);
                S[S.Length - 1] = ++i;
                S.CopyTo(S1, T.Length);
                T = PRF(SKEYSEED, S1, this.prf);

                T.CopyTo(buffer, bufferIndex);
                bufferIndex += T.Length;
                OutputLength -= this.prfOutputLength;
            }
            if (OutputLength > 0)
            {
                S1 = new byte[T.Length + S.Length];
                T.CopyTo(S1, 0);
                S[S.Length - 1] = ++i;
                S.CopyTo(S1, T.Length);
                T = PRF(SKEYSEED, S1, this.prf);

                Utils.MemCpy(T, 0, ref buffer, bufferIndex, OutputLength);
            }

            return buffer;
        }

        public bool SetKeys()
        {

            byte[] SKEYSEED;
            //Calculating SKEYSEED = prf(Ni | Nr, g^ir)
            byte[] NiNr = new byte[(this.ikeINITi.NoncePayload.Data.Length) + (this.ikeINITr.NoncePayload.Data.Length)];
            this.ikeINITi.NoncePayload.Data.CopyTo(NiNr, 0);
            this.ikeINITr.NoncePayload.Data.CopyTo(NiNr, (this.ikeINITi.NoncePayload.Data.Length));

            string groupGenerator = "";
            string modP = Utils.ModP((DH_GROUPS)this.dhGroup, out groupGenerator);

            BigInteger p;
            BigInteger publicKey;
            if (isInitiatorSA)
            {
                p = new BigInteger(this.ikeINITi.KeyExchangePayload.PrivateKey);
                publicKey = new BigInteger(this.ikeINITr.KeyExchangePayload.KeyExchangeData);
            }
            else
            {
                p = new BigInteger(this.ikeINITr.KeyExchangePayload.PrivateKey);
                publicKey = new BigInteger(this.ikeINITi.KeyExchangePayload.KeyExchangeData);
            }
            BigInteger mod = new BigInteger(modP, 16);
            BigInteger gir = publicKey.modPow(p, mod);
            byte[] S = gir.getBytes(); 
            SKEYSEED = PRF(NiNr, S, this.prf);

            //Calculating the length of keys
            short requiredKeyLength = 0;

            //SK_ei, SK_er
            switch (this.cryptoAlgs.EncrAlg)
            //TODO: Include key lengths for other methods
            {
                case IKE_ENCR_ALGS.ENCR_AES_CBC:
                    requiredKeyLength += (short) (this.cryptoAlgs.EncrKeyLength *  2);
                    break;
                case IKE_ENCR_ALGS.ENCR_DES:
                    requiredKeyLength += (short)(this.cryptoAlgs.EncrKeyLength * 2);
                    break;
                case IKE_ENCR_ALGS.ENCR_DES3:
                    requiredKeyLength += (short)(this.cryptoAlgs.EncrKeyLength * 2);
                    break;
                default:
                    requiredKeyLength = 0;
                    break;
            }

            //SK_d, SK_ai, SK_ar, SK_pi, SK_pr
            switch (this.cryptoAlgs.IntegAlg)
            //TODO: Include key lengths for other integrity check algorithms
            //RFC 4306 : For integrity check algorithms based on hashed keys, the key length is the block length of the underlaying hash algorithm
            {
                case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                    {
                        this.cryptoAlgs.IntegAlgKeyLength = (short)HASH_ALGS_OUTPUT_LENGTH.SHA1;
                        requiredKeyLength += (short)HASH_ALGS_OUTPUT_LENGTH.SHA1 * 5;
                    }
                    break;
                case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                    {
                        this.cryptoAlgs.IntegAlgKeyLength = (short)HASH_ALGS_OUTPUT_LENGTH.MD5;
                        requiredKeyLength += (short)HASH_ALGS_OUTPUT_LENGTH.MD5 * 5;
                    }
                    break;
                case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                    {
                        this.cryptoAlgs.IntegAlgKeyLength = (short)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC;
                        requiredKeyLength += (short)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC * 5;
                    }
                    break;
                default:
                    break;
            }
            switch (this.prf)
            {
                case IKE_PRFS.PRF_HMAC_SHA1:
                    {
                        this.prfOutputLength = (short)HASH_ALGS_OUTPUT_LENGTH.HMAC_SHA1;
                        requiredKeyLength += (short)HASH_ALGS_OUTPUT_LENGTH.SHA1 * 5;
                    }
                    break;
                case IKE_PRFS.PRF_HMAC_MD5:
                    {
                        this.prfOutputLength = (short)HASH_ALGS_OUTPUT_LENGTH.HMAC_MD5;
                        requiredKeyLength += (short)HASH_ALGS_OUTPUT_LENGTH.MD5 * 5;
                    }
                    break;
                case IKE_PRFS.PRF_AES128_XCBC:
                    {
                        this.prfOutputLength = (short)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC;
                        requiredKeyLength += (short)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC * 5;
                    }
                    break;
                default:
                    break;
            }

            byte[] rawKeys = PRFPlus(SKEYSEED, requiredKeyLength);
            int index = 0;

            this.keys.SK_d = new byte[this.cryptoAlgs.IntegAlgKeyLength];
            Utils.MemCpy(rawKeys, index, ref this.keys.SK_d, 0, this.cryptoAlgs.IntegAlgKeyLength);
            index += this.cryptoAlgs.IntegAlgKeyLength;

            this.keys.SK_ai = new byte[this.cryptoAlgs.IntegAlgKeyLength];
            Utils.MemCpy(rawKeys, index, ref this.keys.SK_ai, 0, this.cryptoAlgs.IntegAlgKeyLength);
            index += this.cryptoAlgs.IntegAlgKeyLength;

            this.keys.SK_ar = new byte[this.cryptoAlgs.IntegAlgKeyLength];
            Utils.MemCpy(rawKeys, index, ref this.keys.SK_ar, 0, this.cryptoAlgs.IntegAlgKeyLength);
            index += this.cryptoAlgs.IntegAlgKeyLength;

            this.keys.SK_ei = new byte[this.cryptoAlgs.EncrKeyLength];
            Utils.MemCpy(rawKeys, index, ref this.keys.SK_ei, 0, this.cryptoAlgs.EncrKeyLength);
            index += this.cryptoAlgs.EncrKeyLength;

            this.keys.SK_er = new byte[this.cryptoAlgs.EncrKeyLength];
            Utils.MemCpy(rawKeys, index, ref this.keys.SK_er, 0, this.cryptoAlgs.EncrKeyLength);
            index += this.cryptoAlgs.EncrKeyLength;

            this.keys.SK_pi = new byte[this.cryptoAlgs.IntegAlgKeyLength];
            Utils.MemCpy(rawKeys, index, ref this.keys.SK_pi, 0, this.cryptoAlgs.IntegAlgKeyLength);
            index += this.cryptoAlgs.IntegAlgKeyLength;

            this.keys.SK_pr = new byte[this.cryptoAlgs.IntegAlgKeyLength];
            Utils.MemCpy(rawKeys, index, ref this.keys.SK_pr, 0, this.cryptoAlgs.IntegAlgKeyLength);
            index += this.cryptoAlgs.IntegAlgKeyLength;


            return true;
        }
        private bool addPadding()
        {
            return true;
        }
        public bool IsMACValid(byte[] RawIKEMessage)
        {
            bool result = true;
            byte[] mac = null;
            byte[] SK_a;
            byte[] messageBytes;
            int macLength = 0;

            if (this.isInitiatorSA) //Then the message we are authenticating is from the responder
            {
                SK_a = this.keys.SK_ar;
            }
            else
            {
                SK_a = this.keys.SK_ai;
            }
            double executionTime = 0;
            switch (this.cryptoAlgs.IntegAlg)
            {
                case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                    {
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.HMAC_SHA1_96;
                        messageBytes = new byte[RawIKEMessage.Length - macLength];
                        Utils.MemCpy(RawIKEMessage, 0, ref messageBytes, 0, messageBytes.Length);
                        mac = HMAC_SHA1.GetMAC(messageBytes, 0, messageBytes.Length, SK_a, ref executionTime);
                    }
                    break;
                case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                    {
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.HMAC_MD5_96;
                        messageBytes = new byte[RawIKEMessage.Length - macLength];
                        Utils.MemCpy(RawIKEMessage, 0, ref messageBytes, 0, messageBytes.Length);
                        mac = HMAC_MD5.GetMAC(messageBytes, 0, messageBytes.Length, SK_a, ref executionTime);
                    }
                    break;
                case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                    {
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC_96;
                        messageBytes = new byte[RawIKEMessage.Length - macLength];
                        Utils.MemCpy(RawIKEMessage, 0, ref messageBytes, 0, messageBytes.Length);
                        mac = AES_XCBC_MAC.GetMAC(messageBytes, 0, messageBytes.Length, SK_a, ref executionTime);
                    }
                    break;
            }
            for (int i = 0; i < macLength ; i++)
                if (RawIKEMessage[RawIKEMessage.Length - macLength + i] != mac[i])
                {
                    result = false;
                    break;
                }

                return result;
        }

        private byte[] integrityDataChecksum(IKE ikeMessage)
        {
            byte[] mac = null;
            byte[] SK_a;
            byte[] messageBytes = ikeMessage.ToBytes();
            int macLength = 0;

            if (ikeMessage.Flags == 0x08) //Initiators is sending the message
            {
                SK_a = this.keys.SK_ai;
            }
            else 
            {
                SK_a = this.keys.SK_ar;
            }
            double executionTime = 0;
            switch (this.cryptoAlgs.IntegAlg)
            {
                case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                    {
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.HMAC_SHA1_96;
                        mac = HMAC_SHA1.GetMAC(messageBytes, 0, messageBytes.Length, SK_a, ref executionTime);
                    }
                    break;
                case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                    {
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.HMAC_MD5_96;
                        mac = HMAC_MD5.GetMAC(messageBytes, 0, messageBytes.Length, SK_a, ref executionTime);
                    }
                    break;
                case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                    {
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC_96;
                        mac = AES_XCBC_MAC.GetMAC(messageBytes, 0, messageBytes.Length, SK_a, ref executionTime);
                    }
                    break;
            }

            byte[] macToReturn = new byte[macLength];
            Utils.MemCpy(mac, 0, ref macToReturn, 0, macLength);

            return macToReturn;
        }
        public void ProtectEncryptedPayload(IKE_Encrypted_Payload EncryptedPayload)
        {
            byte[] innerPayloadsBytes = EncryptedPayload.GetInnerPayloadsBytes;

            byte[] SK_e;

            if (isInitiatorSA)
            {
                SK_e = this.keys.SK_ei;
            }
            else
            {
                SK_e = this.keys.SK_er;
            }

            switch (this.cryptoAlgs.EncrAlg)
            {
                case IKE_ENCR_ALGS.ENCR_AES_CBC:
                    {
                        AES alg = new AES(innerPayloadsBytes, SK_e, this.cryptoAlgs.EncrBlockSize, CipherMode.CBC);
                        alg.Encrypt();
                        EncryptedPayload.SetEncryptedContent(alg.Encrypted, alg.IV);

                        break;
                    }
                case IKE_ENCR_ALGS.ENCR_DES:
                    {
                        IPsecLite.Cryptography.DES alg = new IPsecLite.Cryptography.DES(innerPayloadsBytes, SK_e, this.cryptoAlgs.EncrBlockSize, CipherMode.CBC);
                        alg.Encrypt();
                        EncryptedPayload.SetEncryptedContent(alg.Encrypted, alg.IV);
                        break;
                    }
                case IKE_ENCR_ALGS.ENCR_DES3:
                    {
                        IPsecLite.Cryptography.DES3 alg = new IPsecLite.Cryptography.DES3(innerPayloadsBytes, SK_e, this.cryptoAlgs.EncrBlockSize, CipherMode.CBC);
                        alg.Encrypt();
                        EncryptedPayload.SetEncryptedContent(alg.Encrypted, alg.IV);
                        break;
                    }
            }

        }
        public void SendProtected(IKE ikeMessage)
        {
            byte[] mac;
            mac = integrityDataChecksum(ikeMessage);
            if (ikeMessage.Flags == 0x08)
            {
                if (mac == null)
                    Program.IKEv2Exchange.Send(ikeMessage, this.initiatorIP, this.responderIP);
                else
                    Program.IKEv2Exchange.Send(ikeMessage, this.initiatorIP, this.responderIP, mac);
            }
            else
            {
                if (mac == null)
                    Program.IKEv2Exchange.Send(ikeMessage, this.responderIP, this.initiatorIP);
                else
                    Program.IKEv2Exchange.Send(ikeMessage, this.responderIP, this.initiatorIP, mac);
            }
            switch (ikeMessage.ExchangeType)
            {
                case IKE_EXCHANGE_TYPES.AUTH:
                    this.status = ikeMessage.Flags == 0x08 ? SA_STATUS.AUTHi_SENT : SA_STATUS.AUTHr_SENT;
                    break;
                case IKE_EXCHANGE_TYPES.INFO:
                    switch (ikeMessage.EncryptedPayload.NextPayload)
                    {
                        case IKE_PAYLOADS.DEL:
                            childSAs.Remove(this.childSAKeys[0]);
                            childSAs.Remove(this.childSAKeys[1]);

                            long ikeSASPI = this.isInitiatorSA ? this.initiatorSPI : this.responderSPI;

                            ChildSADeletedEventArgs e1 = new ChildSADeletedEventArgs(ikeSASPI, this.childSAKeys[0]);
                            ChildSADeletedEventArgs e2 = new ChildSADeletedEventArgs(ikeSASPI, this.childSAKeys[1]);
                            ChildSADeleted(this, e1, e2);

                            SADeleted(this, this.SAKey);
                            break;
                    }
                    break;
            }
        }

        private bool isPaddingOk(byte[] DecryptedBytes)
        {
            bool isOk = true;

            byte paddingLength = DecryptedBytes[DecryptedBytes.Length - 1];
            for (int i = 1; i <= paddingLength; i++)
                if (DecryptedBytes[DecryptedBytes.Length - paddingLength - 1 + (i - 1)] != i)
                {
                    isOk = false;
                    break;
                }

            return isOk;
        }
        
        public bool ProcessEncryptedPayload(ref IKE_Encrypted_Payload EncryptedPayload)
        {
            bool processResult = true;

            short macLength = 0;
            byte[] contentToDecrypt;
            byte[] decrypted;
            byte[] iv;
            byte[] SK_e;

            if (isInitiatorSA)
            {
                SK_e = this.keys.SK_er;
            }
            else
            {
                SK_e = this.keys.SK_ei;
            }
         
            switch (this.cryptoAlgs.EncrAlg)
            {
                case IKE_ENCR_ALGS.ENCR_AES_CBC:
                    {
                        iv = new byte[this.cryptoAlgs.EncrBlockSize];
                        contentToDecrypt = new byte[EncryptedPayload.Length - 4 - iv.Length - macLength];
                        //Copy IV
                        Utils.MemCpy(EncryptedPayload.EncryptedPayloads, 0, ref iv, 0, this.cryptoAlgs.EncrBlockSize);
                        //Copy content to decrypt
                        Utils.MemCpy(EncryptedPayload.EncryptedPayloads, iv.Length, ref contentToDecrypt, 0, contentToDecrypt.Length);
                        AES alg = new AES(contentToDecrypt, SK_e, this.cryptoAlgs.EncrBlockSize, CipherMode.CBC, iv);
                        alg.Decrypt();
                        decrypted = alg.Decrypted;
                        processResult = isPaddingOk(decrypted);
                        if (processResult)
                            EncryptedPayload.TryParse(decrypted);
                        break;
                    }
                case IKE_ENCR_ALGS.ENCR_DES:
                    {
                        iv = new byte[this.cryptoAlgs.EncrBlockSize];
                        contentToDecrypt = new byte[EncryptedPayload.Length - 4 - iv.Length - macLength];
                        //Copy IV
                        Utils.MemCpy(EncryptedPayload.EncryptedPayloads, 0, ref iv, 0, this.cryptoAlgs.EncrBlockSize);
                        //Copy content to decrypt
                        Utils.MemCpy(EncryptedPayload.EncryptedPayloads, iv.Length, ref contentToDecrypt, 0, contentToDecrypt.Length);
                        IPsecLite.Cryptography.DES alg = new IPsecLite.Cryptography.DES(contentToDecrypt, SK_e, this.cryptoAlgs.EncrBlockSize, CipherMode.CBC, iv);
                        alg.Decrypt();
                        decrypted = alg.Decrypted;
                        processResult = isPaddingOk(decrypted);
                        if (processResult)
                            EncryptedPayload.TryParse(decrypted);
                        break;
                    }
                case IKE_ENCR_ALGS.ENCR_DES3:
                    {
                        iv = new byte[this.cryptoAlgs.EncrBlockSize];
                        contentToDecrypt = new byte[EncryptedPayload.Length - 4 - iv.Length - macLength];
                        //Copy IV
                        Utils.MemCpy(EncryptedPayload.EncryptedPayloads, 0, ref iv, 0, this.cryptoAlgs.EncrBlockSize);
                        //Copy content to decrypt
                        Utils.MemCpy(EncryptedPayload.EncryptedPayloads, iv.Length, ref contentToDecrypt, 0, contentToDecrypt.Length);
                        DES3 alg = new DES3(contentToDecrypt, SK_e, this.cryptoAlgs.EncrBlockSize, CipherMode.CBC, iv);
                        alg.Decrypt();
                        decrypted = alg.Decrypted;
                        processResult = isPaddingOk(decrypted);
                        if (processResult)
                            EncryptedPayload.TryParse(decrypted);
                        break;
                    }
            }

            return processResult;
        }
        public byte[] AuthData(bool IsForInitiatorMessage, IKE_Identification_Payload IDPayload, string PSK)
        {
            byte[] Key_Pad_for_IKEv2 = { 75, 101, 121, 32, 80, 97, 100, 32, 102, 111, 114, 32, 73, 75, 69, 118, 50};
            byte[] authData = null;
            byte[] msgToSign = null;
            byte[] nonce = null;
            byte[] init = null;
            byte[] idMac = null;
            byte[] padMac = null;
            byte[] SK_p = null;
            byte[] id = null;
            byte[] pskBytes = null;
            int macLength = 0;

            pskBytes = new byte[PSK.Length - 1];
            for (int i = 0; i < pskBytes.Length - 1; i++)
                pskBytes[i] = (byte) PSK[i];

            if (IsForInitiatorMessage)
            {
                nonce = this.ikeINITr.NoncePayload.ToBytes();
                init = this.ikeINITi.ToBytes();
                SK_p = this.keys.SK_pi;
            }
            else
            {
                nonce = this.ikeINITi.NoncePayload.ToBytes();
                init = this.ikeINITr.ToBytes();
                SK_p = this.keys.SK_pr;
            }

            id = new byte[IDPayload.Length - 4];
            Utils.MemCpy(IDPayload.ToBytes(), 4, ref id, 0, id.Length);

            double executionTime = 0;
            switch (this.authMethod.AuthAlg)
            {
                case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                    {
                        //I know, for some reason StrongS/wan uses 20 byte
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.HMAC_SHA1;
                        idMac = HMAC_SHA1.GetMAC(id, 0, id.Length, SK_p, ref executionTime);
                        padMac = HMAC_SHA1.GetMAC(Key_Pad_for_IKEv2, 0, Key_Pad_for_IKEv2.Length, pskBytes, ref executionTime);
                    }
                    break;
                case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                    {
                        //I know, for some reason StrongS/wan uses 20 byte
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.HMAC_MD5;
                        idMac = HMAC_MD5.GetMAC(id, 0, id.Length, SK_p, ref executionTime);
                        padMac = HMAC_MD5.GetMAC(Key_Pad_for_IKEv2, 0, Key_Pad_for_IKEv2.Length, pskBytes, ref executionTime);
                    }
                    break;
                case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                    {
                        //I know, for some reason StrongS/wan uses 20 byte
                        macLength = (int)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC;
                        idMac = AES_XCBC_MAC.GetMAC(id, 0, id.Length, SK_p, ref executionTime);
                        padMac = AES_XCBC_MAC.GetMAC(Key_Pad_for_IKEv2, 0, Key_Pad_for_IKEv2.Length, pskBytes, ref executionTime);
                    }
                    break;
            }
            msgToSign = new byte[init.Length + (nonce.Length - 4) + macLength];
            init.CopyTo(msgToSign, 0);
            Utils.MemCpy(nonce, 4, ref msgToSign, init.Length, nonce.Length - 4);
            idMac.CopyTo(msgToSign, init.Length + nonce.Length - 4);

            switch (this.authMethod.AuthAlg)
            {
                case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                    authData = HMAC_SHA1.GetMAC(msgToSign, 0, msgToSign.Length, padMac, ref executionTime);
                    break;
                case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                    authData = HMAC_MD5.GetMAC(msgToSign, 0, msgToSign.Length, padMac, ref executionTime);
                    break;
                case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                    authData = AES_XCBC_MAC.GetMAC(msgToSign, 0, msgToSign.Length, padMac, ref executionTime);
                    break;
            }

            return authData;
        }
        public static SA GetSAByIP(string IPAddress)
        {
            SA sa = null;
            foreach (KeyValuePair<long, SA> s in saDB)
            {
                if ((Utils.ToShortStringIP(s.Value.ResponderIP) == IPAddress) || (Utils.ToShortStringIP(s.Value.InitiatorIP) == IPAddress))
                    sa = s.Value;
            }
            return sa;
        }
        public static SA GetSA(long SAKey)
        {
            SA sa;
            saDB.TryGetValue(SAKey, out sa);

            return sa;
        }
        public static CHILD_SA_TYPE GetChildSA(long SAKey, bool IsOutgoingPacket)
        {
            SA sa = null;
            string childSAKey = "";
            CHILD_SA_TYPE childSA = null;
           if (saDB.TryGetValue(SAKey, out sa))
           {
                if (IsOutgoingPacket)
                {
                    if (sa.isInitiatorSA)
                        childSAKey = sa.childSAKeys[0];
                    else
                        childSAKey = sa.childSAKeys[1];
                }
                else
                {
                    if (sa.isInitiatorSA)
                        childSAKey = sa.childSAKeys[1];
                    else
                        childSAKey = sa.childSAKeys[0];
                }
                childSAs.TryGetValue(childSAKey, out childSA);
           }
            return childSA;
        }

        public static CHILD_SA_TYPE GetChildSA(string ChildSAKey)
        {
            CHILD_SA_TYPE childSA;
            SA.childSAs.TryGetValue(ChildSAKey, out childSA);
            return childSA;
        }
        public bool ProcessAUTH(IKE ikeMessage)
        {
            IKE_Identification_Payload idPayload = null;
            IKE_Auth_Payload authPayload = null;
            if (ikeMessage.Flags == 0x08)
                foreach (PAYLOAD_NODE_TYPE p in ikeMessage.EncryptedPayload.InnerPayloads)
                    switch (p.PayloadType)
                    {
                        case IKE_PAYLOADS.IDi:
                            idPayload = (IKE_Identification_Payload)p.Payload;
                            break;
                        case IKE_PAYLOADS.AUTH:
                            authPayload = (IKE_Auth_Payload)p.Payload;
                            break;
                        case IKE_PAYLOADS.SA:
                            IKE_SA_Payload saPayload = (IKE_SA_Payload)p.Payload;
                            this.initiatorChildSAProposal = saPayload.Proposals;
                            break;
                        case IKE_PAYLOADS.NOTI:
                            IKE_Notify_Payload notifyPayload = (IKE_Notify_Payload)p.Payload;
                            if (notifyPayload.NotifyMessageType == IKE_NOTIFY_MSG_TYPES.USE_TRANSPORT_MODE)
                                this.mode = IKE_MODE.TRANSPORT;
                            break;
                    }
            else
                foreach (PAYLOAD_NODE_TYPE p in ikeMessage.EncryptedPayload.InnerPayloads)
                    switch (p.PayloadType)
                    {
                        case IKE_PAYLOADS.IDr:
                            idPayload = (IKE_Identification_Payload)p.Payload;
                            break;
                        case IKE_PAYLOADS.AUTH:
                            authPayload = (IKE_Auth_Payload)p.Payload;
                            break;
                        case IKE_PAYLOADS.SA:
                            //TODO: We should really compare this with what we had sent
                            IKE_SA_Payload saPayload = (IKE_SA_Payload) p.Payload;
                            this.responderChildSAProposal = saPayload.Proposals;
                            break;
                        case IKE_PAYLOADS.NOTI:
                            IKE_Notify_Payload notifyPayload = (IKE_Notify_Payload)p.Payload;
                            if (notifyPayload.NotifyMessageType == IKE_NOTIFY_MSG_TYPES.USE_TRANSPORT_MODE)
                                this.mode = IKE_MODE.TRANSPORT;
                            break;
                    }
            byte[] authData = AuthData(ikeMessage.Flags == 0x08, idPayload, "TODO:THIS NEEDS TO BE REPLACED.");

            bool result = true;

            if (authData.Length != authPayload.AuthData.Length)
                result = false;
            else
                for(int i = 0; i < authPayload.AuthData.Length; i++)
                    if (authData[i] != authPayload.AuthData[i])
                    {
                        result = false;
                        break;
                    }

            if (result)
                this.status = SA_STATUS.ESTABLISHED;

            return result;
        }
        public void CreateChildSA()
        {
            //Calculating keys
            short keyLength = 0;

            //TODO: We need to make the mode dynamic
            CHILD_SA_TYPE childSA1 = new CHILD_SA_TYPE(Utils.BytesToInt(this.initiatorChildSAProposal[0].SPI), this.mode, Utils.ToShortStringIP(this.initiatorIP), this.responderIP, this.initiatorChildSAProposal[0].ProtocolID);
            CHILD_SA_TYPE childSA2 = new CHILD_SA_TYPE(Utils.BytesToInt(this.responderChildSAProposal[0].SPI), this.mode,  Utils.ToShortStringIP(this.responderIP), this.initiatorIP, this.responderChildSAProposal[0].ProtocolID);

            foreach (IKE_Transform t in this.initiatorChildSAProposal[0].Transforms)
            {
                switch (t.TransformType)
                {
                    case IKE_TRANSFORM_TYPES.ENCR:
                        childSA1.cryptoAlgs.EncrAlg = (IKE_ENCR_ALGS) t.TransformID;
                        childSA2.cryptoAlgs.EncrAlg = (IKE_ENCR_ALGS) t.TransformID;
                        if (t.Attributes != null)
                        {
                            foreach (IKE_Transform_Attribute attribute in t.Attributes)
                            {
                                switch ((IKE_ATTRIBUTE_TYPES)attribute.AttributeType)
                                {
                                    case IKE_ATTRIBUTE_TYPES.KEY_LENGTH:
                                        childSA1.cryptoAlgs.EncrKeyLength = (short)(Utils.BytesToShort(attribute.Value[0], attribute.Value[1]) / 8);
                                        childSA2.cryptoAlgs.EncrKeyLength = (short)(Utils.BytesToShort(attribute.Value[0], attribute.Value[1]) / 8);
                                        childSA1.cryptoAlgs.EncrBlockSize = childSA1.cryptoAlgs.EncrKeyLength;
                                        childSA2.cryptoAlgs.EncrBlockSize = childSA2.cryptoAlgs.EncrKeyLength;
                                        break;
                                    case IKE_ATTRIBUTE_TYPES.BLOCK_SIZE:
                                        childSA1.cryptoAlgs.EncrBlockSize = (short)(Utils.BytesToShort(attribute.Value[0], attribute.Value[1]) / 8);
                                        childSA2.cryptoAlgs.EncrBlockSize = (short)(Utils.BytesToShort(attribute.Value[0], attribute.Value[1]) / 8);
                                        break;
                                }
                            }
                        }
                        break;
                    case IKE_TRANSFORM_TYPES.INTEG:
                        childSA1.cryptoAlgs.IntegAlg = (IKE_INTEG_ALGS) t.TransformID;
                        childSA2.cryptoAlgs.IntegAlg = (IKE_INTEG_ALGS) t.TransformID;
                        switch ((IKE_INTEG_ALGS) t.TransformID)
                        {
                            case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                                childSA1.cryptoAlgs.IntegAlgKeyLength = (short) HASH_ALGS_OUTPUT_LENGTH.SHA1;
                                childSA2.cryptoAlgs.IntegAlgKeyLength = (short) HASH_ALGS_OUTPUT_LENGTH.SHA1;
                                break;
                            case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                                childSA1.cryptoAlgs.IntegAlgKeyLength = (short)HASH_ALGS_OUTPUT_LENGTH.MD5;
                                childSA2.cryptoAlgs.IntegAlgKeyLength = (short)HASH_ALGS_OUTPUT_LENGTH.MD5;
                                break;
                            case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                                childSA1.cryptoAlgs.IntegAlgKeyLength = (short)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC;
                                childSA2.cryptoAlgs.IntegAlgKeyLength = (short)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC;
                                break;
                        }
                        break;
                }
            }

            keyLength = (short)(childSA1.cryptoAlgs.EncrKeyLength + childSA1.cryptoAlgs.IntegAlgKeyLength + childSA2.cryptoAlgs.EncrKeyLength + childSA2.cryptoAlgs.IntegAlgKeyLength);
            byte[] EAEAKeys = PRFPlusChildSA(this.keys.SK_d, keyLength);
            int index = 0;
            childSA1.SK_e = new byte[childSA1.cryptoAlgs.EncrKeyLength];
            childSA1.SK_a = new byte[childSA1.cryptoAlgs.IntegAlgKeyLength];

            childSA2.SK_e = new byte[childSA2.cryptoAlgs.EncrKeyLength];
            childSA2.SK_a = new byte[childSA2.cryptoAlgs.IntegAlgKeyLength];

            Utils.MemCpy(EAEAKeys, index, ref childSA1.SK_e, 0, childSA1.cryptoAlgs.EncrKeyLength);
            index += childSA1.cryptoAlgs.EncrKeyLength;
            Utils.MemCpy(EAEAKeys, index, ref childSA1.SK_a, 0, childSA1.cryptoAlgs.IntegAlgKeyLength);
            index += childSA1.cryptoAlgs.IntegAlgKeyLength;
            Utils.MemCpy(EAEAKeys, index, ref childSA2.SK_e, 0, childSA2.cryptoAlgs.EncrKeyLength);
            index += childSA2.cryptoAlgs.EncrKeyLength;
            Utils.MemCpy(EAEAKeys, index, ref childSA2.SK_a, 0, childSA2.cryptoAlgs.IntegAlgKeyLength);

            childSAs.Add(childSA1.ChildSAKey, childSA1);
            childSAs.Add(childSA2.ChildSAKey, childSA2);

            this.childSAKeys[0] = childSA1.ChildSAKey;
            this.childSAKeys[1] = childSA2.ChildSAKey;

            long ikeSASPI = this.isInitiatorSA ? this.initiatorSPI : this.responderSPI;

            if (ChildSACreated != null)
            {
                ChildSACreatedEventArgs e1 = new ChildSACreatedEventArgs(ikeSASPI, childSA1.ChildSAKey, this.initiatorIP, this.responderIP);
                ChildSACreatedEventArgs e2 = new ChildSACreatedEventArgs(ikeSASPI, childSA2.ChildSAKey, this.responderIP, this.initiatorIP);
                ChildSACreated(this, e1, e2);
            }
        }
        public bool RemoveSA()
        {
            childSAs.Remove(this.childSAKeys[0]);
            childSAs.Remove(this.childSAKeys[1]);

            long ikeSASPI = this.isInitiatorSA ? this.initiatorSPI : this.responderSPI;

            SPD.RemoveIKESA(ikeSASPI);


            if (ChildSADeleted != null)
            {
                ChildSADeletedEventArgs e1 = new ChildSADeletedEventArgs(ikeSASPI, this.childSAKeys[0]);
                ChildSADeletedEventArgs e2 = new ChildSADeletedEventArgs(ikeSASPI, this.childSAKeys[1]);
                ChildSADeleted(this, e1, e2);
            }
            bool ok = SADB.Remove(ikeSASPI);
            if (ok)
                if (SADeleted != null)
                    SADeleted(this, this.SAKey);
            return ok;
        }
        public bool ProcessINFO(IKE ikeMessage)
        {
            IKE_Delete_Payload delPayload = null;

            foreach (PAYLOAD_NODE_TYPE p in ikeMessage.EncryptedPayload.InnerPayloads)
                switch (p.PayloadType)
                {
                    case IKE_PAYLOADS.DEL:
                        delPayload = (IKE_Delete_Payload)p.Payload;
                        childSAs.Remove(this.childSAKeys[0]);
                        childSAs.Remove(this.childSAKeys[1]);

                        long ikeSASPI = this.isInitiatorSA ? this.initiatorSPI : this.responderSPI;

                        SPD.RemoveIKESA(ikeSASPI);

                        bool ok = SADB.Remove(ikeSASPI);

                        if (ChildSADeleted != null)
                        {
                            ChildSADeletedEventArgs e1 = new ChildSADeletedEventArgs(ikeSASPI, this.childSAKeys[0]);
                            ChildSADeletedEventArgs e2 = new ChildSADeletedEventArgs(ikeSASPI, this.childSAKeys[1]);
                            ChildSADeleted(this, e1, e2);
                        }

                        if (SADeleted != null)
                            SADeleted(this, this.SAKey);                        

                        break;
                }
            return true;
        }
        public static void Delete(long SAKey)
        {
            SA.saDB.Remove(SAKey);
        }
    
    }
}
