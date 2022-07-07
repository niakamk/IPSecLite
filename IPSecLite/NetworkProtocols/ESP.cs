using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;
using adabtek.IPsecLite.IKEv2;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.SADB;
using adabtek.IPsecLite.SPDB;
using adabtek.IPsecLite.Cryptography;
using adabtek.IPsecLite.Timing;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.EventArguments;
namespace adabtek.IPsecLite.NetworkProtocols
{
    public class ESP
    {
        struct ProcessingCost
        {
            long encryptionCycles;
            double encryptionTime;
            long integrityCheckCycles;
            double integrityCheckTime;

            public long EncryptionCycles
            {
                set { encryptionCycles = value; }
                get { return encryptionCycles; }
            }
            public double EncryptionTime
            {
                set { encryptionTime = value; }
                get { return encryptionTime; }
            }
            public long IntegrityCheckCycles
            {
                set { integrityCheckCycles = value; }
                get { return integrityCheckCycles; }
            }
            public double IntegrityCheckTime
            {
                set { integrityCheckTime = value; }
                get { return integrityCheckTime; }
            }
        };

        IPDatagram input;
        bool isOutgoing;

        int spi;
        int sequenceNumber;
        byte[] iv;
        byte[] data;
        byte paddingLength;
        PROTOCOLS nextProtocol;
        byte[] mac;

        CHILD_SA_TYPE childSA;
        IKE_ENCR_ALGS encryptionAlg;
        short encryptionAlgBlockSize;
        byte[] encryptionKey;
        bool noICV;
        IKE_INTEG_ALGS integrityCheckAlg;
        byte[] integrityCheckAlgKey;
        short macLength;
        ProcessingCost processingCost;
        byte[] processedData;
        short processedDataLength;


        public IPDatagram GetProcessedData
        {
            get 
            {
                int prevPayloadLength = this.input.Payload.Length;
                if (this.isOutgoing)
                {
                    if (this.childSA.Mode == IKE_MODE.TRANSPORT)
                    {
                        this.input.Payload = null;
                        this.input.Payload = this.processedData;
                        this.input.TotalLength += (short)(this.input.Payload.Length - prevPayloadLength);
                        this.input.Protocol = PROTOCOLS.ESP;
                        return this.input;
                    }
                    else
                    {
                        byte[] sourceIP = Utils.IPToBytes(Utils.GetHostIPAddress());
                        IPDatagram newIPDatagram = new IPDatagram(sourceIP, this.childSA.PeerIP, PROTOCOLS.ESP, this.processedData);
                        return newIPDatagram;
                    }
                }
                else
                {
                    byte[] thisIPDatagramPayload = new byte[this.processedData.Length - this.paddingLength - 2];
                    Utils.MemCpy(this.processedData, 0, ref thisIPDatagramPayload, 0, thisIPDatagramPayload.Length);
                    if (this.childSA.Mode == IKE_MODE.TRANSPORT)
                    {
                        this.input.Payload = thisIPDatagramPayload;
                        this.input.TotalLength -= (short)((prevPayloadLength - this.input.Payload.Length));
                        this.input.Protocol = this.nextProtocol;

                        return this.input;
                    }
                    else
                    {
                        IPDatagram innerIPDatagram = IP.TryParse(thisIPDatagramPayload);
                        return innerIPDatagram;
                    }
                }
            }
        }
        public int SPI
        {
            get { return this.spi; }
        }
        public int SequenceNumber
        {
            get { return this.sequenceNumber; }
        }
        public bool NoICV
        {
            get { return this.noICV; }
        }
        public byte[] IV
        {
            get { return this.iv; }
        }
        public byte[] MAC
        {
            get { return this.mac; }
        }
        public short MACLength
        {
            get { return this.macLength; }
        }
        public PROTOCOLS NextProtocol
        {
            get { return this.nextProtocol; }
        }
        public byte PaddingLength
        {
            get { return this.paddingLength; }
        }
        public long EncryptionCycles
        {
            get { return this.processingCost.EncryptionCycles; }
        }
        public double EncryptionTime
        {
            get { return this.processingCost.EncryptionTime; }
        }
        public long IntegrityCheckCycles
        {
            get { return this.processingCost.IntegrityCheckCycles; }
        }
        public double IntegrityCheckTime
        {
            get { return this.processingCost.IntegrityCheckTime; }
        }

        public ESP(IPDatagram Input, CHILD_SA_TYPE ChildSA, bool IsOutgoing)
        {
            this.input = Input;
            this.childSA = ChildSA;
            this.isOutgoing = IsOutgoing;
            if (this.isOutgoing)
            {
                if (this.childSA.Mode == IKE_MODE.TRANSPORT)
                    this.data = Input.Payload;
                else
                    this.data = Input.ToBytes();

                this.spi = ChildSA.SPI;
                this.noICV = (ChildSA.Protocol == IKE_PROTOCOLS.ESP_NO_ICV);
                this.encryptionAlg = ChildSA.cryptoAlgs.EncrAlg;
                this.encryptionAlgBlockSize = ChildSA.cryptoAlgs.EncrBlockSize;
                this.encryptionKey = ChildSA.SK_e;
                this.integrityCheckAlg = ChildSA.cryptoAlgs.IntegAlg;
                this.integrityCheckAlgKey = ChildSA.SK_a;
                this.sequenceNumber = ChildSA.GetNextSequenceNumber();
                this.nextProtocol = Input.Protocol;

                //Size of SPI + Sequence Number
                this.processedDataLength = 8;
                switch (this.encryptionAlg)
                {
                    case IKE_ENCR_ALGS.ENCR_AES_CBC:
                        {
                            //Length of IV
                            this.processedDataLength += this.encryptionAlgBlockSize;
                            //Length of data
                            this.processedDataLength += (short)this.data.Length;
                            //Length of padding + padding length + next protocol
                            this.paddingLength = (byte)(this.encryptionAlgBlockSize - ((this.data.Length + 2) % this.encryptionAlgBlockSize));
                            this.processedDataLength += this.paddingLength;
                            this.processedDataLength += 2;
                        }
                        break;
                    case IKE_ENCR_ALGS.ENCR_DES:
                        {
                            //Length of IV
                            this.processedDataLength += this.encryptionAlgBlockSize;
                            //Length of data
                            this.processedDataLength += (short)this.data.Length;
                            //Length of padding + padding length + next protocol
                            this.paddingLength = (byte)(this.encryptionAlgBlockSize - ((this.data.Length + 2) % this.encryptionAlgBlockSize));
                            this.processedDataLength += this.paddingLength;
                            this.processedDataLength += 2;
                        }
                        break;
                    case IKE_ENCR_ALGS.ENCR_DES3:
                        {
                            //Length of IV
                            this.processedDataLength += this.encryptionAlgBlockSize;
                            //Length of data
                            this.processedDataLength += (short)this.data.Length;
                            //Length of padding + padding length + next protocol
                            this.paddingLength = (byte)(this.encryptionAlgBlockSize - ((this.data.Length + 2) % this.encryptionAlgBlockSize));
                            this.processedDataLength += this.paddingLength;
                            this.processedDataLength += 2;
                        }
                        break;
                }
                if (!noICV)
                {
                    switch (this.integrityCheckAlg)
                    {
                        case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                            this.macLength = (short)HASH_ALGS_OUTPUT_LENGTH.HMAC_SHA1_96;
                            break;
                        case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                            this.macLength = (short)HASH_ALGS_OUTPUT_LENGTH.HMAC_MD5_96;
                            break;
                        case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                            this.macLength = (short)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC_96;
                            break;
                    }
                    this.processedDataLength += this.macLength;
                }
                else
                    this.macLength = 0;
                this.processedData = new byte[this.processedDataLength];
            }
            else
            {
                byte[] t = new byte[4];
                Utils.MemCpy(Input.Payload, 0, ref t, 0, 4);
                this.spi = Utils.BytesToInt(t);
                Utils.MemCpy(Input.Payload, 4, ref t, 0, 4);
                this.noICV = (ChildSA.Protocol == IKE_PROTOCOLS.ESP_NO_ICV);
                this.sequenceNumber = Utils.BytesToInt(t);
                this.data = Input.Payload;
                this.encryptionAlg = ChildSA.cryptoAlgs.EncrAlg;
                this.encryptionAlgBlockSize = ChildSA.cryptoAlgs.EncrBlockSize;
                this.encryptionKey = ChildSA.SK_e;
                this.integrityCheckAlg = ChildSA.cryptoAlgs.IntegAlg;
                this.integrityCheckAlgKey = ChildSA.SK_a;

                if (!this.noICV)
                {
                    switch (this.integrityCheckAlg)
                    {
                        case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                            this.macLength = (short)HASH_ALGS_OUTPUT_LENGTH.HMAC_SHA1_96;
                            break;
                        case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                            this.macLength = (short)HASH_ALGS_OUTPUT_LENGTH.HMAC_MD5_96;
                            break;
                        case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                            this.macLength = (short)HASH_ALGS_OUTPUT_LENGTH.AES_XCBC_MAC_96;
                            break;
                    }
                }
                else
                    this.macLength = 0;
                this.processedData = new byte[this.data.Length - this.macLength - this.encryptionAlgBlockSize - 8];
            }
        }
        private PROTECTION_RESULTS encrypt()
        {
            PROTECTION_RESULTS result;
            try
            {
                HiPerfTimer timer = new HiPerfTimer();
                switch (this.encryptionAlg)
                {
                    case IKE_ENCR_ALGS.ENCR_AES_CBC:
                        {
                            iv = new byte[this.encryptionAlgBlockSize];
                            timer.Start();
                            AES alg = new AES(this.data, this.encryptionKey, this.encryptionAlgBlockSize, CipherMode.CBC, this.nextProtocol);
                            alg.Encrypt();
                            timer.Stop();
                            alg.IV.CopyTo(this.processedData, 8);
                            alg.IV.CopyTo(this.iv, 0);
                            alg.Encrypted.CopyTo(this.processedData, this.encryptionAlgBlockSize + 8);
                            break;
                        }
                    case IKE_ENCR_ALGS.ENCR_DES:
                        {
                            iv = new byte[this.encryptionAlgBlockSize];
                            timer.Start();
                            IPsecLite.Cryptography.DES alg = new IPsecLite.Cryptography.DES(this.data, this.encryptionKey, this.encryptionAlgBlockSize, CipherMode.CBC, this.nextProtocol);
                            alg.Encrypt();
                            timer.Stop();
                            alg.IV.CopyTo(this.processedData, 8);
                            alg.IV.CopyTo(this.iv, 0);
                            alg.Encrypted.CopyTo(this.processedData, this.encryptionAlgBlockSize + 8);
                            break;
                        }
                    case IKE_ENCR_ALGS.ENCR_DES3:
                        {
                            iv = new byte[this.encryptionAlgBlockSize];
                            timer.Start();
                            DES3 alg = new DES3(this.data, this.encryptionKey, this.encryptionAlgBlockSize, CipherMode.CBC, this.nextProtocol);
                            alg.Encrypt();
                            timer.Stop();
                            alg.IV.CopyTo(this.processedData, 8);
                            alg.IV.CopyTo(this.iv, 0);
                            alg.Encrypted.CopyTo(this.processedData, this.encryptionAlgBlockSize + 8);
                            break;
                        }
                }
                this.processingCost.EncryptionCycles = timer.Cycles;
                this.processingCost.EncryptionTime = timer.Duration;
                result = PROTECTION_RESULTS.OK;
            }
            catch
            {
                result = PROTECTION_RESULTS.ENC_FAILED;
            }
            return result;
        }
        private PROTECTION_RESULTS addMAC()
        {
            PROTECTION_RESULTS result;
            try
            {
                HiPerfTimer timer = new HiPerfTimer();
                int macStartingByte = this.processedData.Length - this.macLength;
                double executionTime = 0;
                switch (this.integrityCheckAlg)
                {

                    case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                        timer.Start();
                        mac = HMAC_SHA1.GetMAC(this.processedData, 0, macStartingByte, this.integrityCheckAlgKey, ref executionTime);
                        timer.Stop();
                        Utils.MemCpy(mac, 0, ref this.processedData, macStartingByte, this.macLength);
                        break;
                    case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                        timer.Start();
                        mac = HMAC_MD5.GetMAC(this.processedData, 0, macStartingByte, this.integrityCheckAlgKey, ref executionTime);
                        timer.Stop();
                        Utils.MemCpy(mac, 0, ref this.processedData, macStartingByte, this.macLength);
                        break;
                    case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                        timer.Start();
                        mac = AES_XCBC_MAC.GetMAC(this.processedData, 0, macStartingByte, this.integrityCheckAlgKey, ref executionTime);
                        timer.Stop();
                        Utils.MemCpy(mac, 0, ref this.processedData, macStartingByte, this.macLength);
                        break;
                }
                this.processingCost.IntegrityCheckCycles = timer.Cycles;
                this.processingCost.IntegrityCheckTime = timer.Duration;
                result = PROTECTION_RESULTS.OK;
            }
            catch
            {
                result = PROTECTION_RESULTS.AUTH_FAILED;
            }
            return result;
        }
        private PROTECTION_RESULTS checkMAC()
        {
            PROTECTION_RESULTS result = PROTECTION_RESULTS.OK;
            try
            {
                mac = new byte[this.macLength];
                Utils.MemCpy(this.data, this.data.Length - this.macLength, ref mac, 0, this.macLength);
                byte[] newMac = null;
                int macStartingByte = this.data.Length - this.macLength;
                double executionTime = 0;
                HiPerfTimer timer = new HiPerfTimer();
                timer.Start();
                switch (this.integrityCheckAlg)
                {
                    case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                        newMac = HMAC_SHA1.GetMAC(this.data, 0, macStartingByte, this.integrityCheckAlgKey, ref executionTime);
                        break;
                    case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                        newMac = HMAC_MD5.GetMAC(this.data, 0, macStartingByte, this.integrityCheckAlgKey, ref executionTime);
                        break;
                    case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                        newMac = AES_XCBC_MAC.GetMAC(this.data, 0, macStartingByte, this.integrityCheckAlgKey, ref executionTime);
                        break;
                }
                for (int i = 0; i < this.macLength; i++)
                    if (this.data[this.data.Length - this.macLength + i] != newMac[i])
                    {
                        result = PROTECTION_RESULTS.INVALID_MAC;
                        break;
                    }
                timer.Stop();
                this.processingCost.IntegrityCheckCycles = timer.Cycles;
                this.processingCost.IntegrityCheckTime = timer.Duration;
            }
            catch
            {
                result = PROTECTION_RESULTS.AUTH_FAILED;
            }
            return result;
        }
        private PROTECTION_RESULTS decrypt()
        {
            PROTECTION_RESULTS result = PROTECTION_RESULTS.OK;
            try
            {
                HiPerfTimer timer = new HiPerfTimer();
                timer.Start();
                switch (this.encryptionAlg)
                {
                    case IKE_ENCR_ALGS.ENCR_AES_CBC:
                        {
                            iv = new byte[this.encryptionAlgBlockSize];
                            Utils.MemCpy(this.data, 8, ref iv, 0, this.encryptionAlgBlockSize);
                            byte[] dataWithoutHeader = new byte[this.data.Length - iv.Length - 8];
                            Utils.MemCpy(this.data, 8 + iv.Length, ref dataWithoutHeader, 0, dataWithoutHeader.Length);
                            AES alg = new AES(dataWithoutHeader, this.encryptionKey, this.encryptionAlgBlockSize, CipherMode.CBC, iv);
                            alg.Decrypt();
                            alg.Decrypted.CopyTo(this.processedData, 0);
                            break;
                        }
                    case IKE_ENCR_ALGS.ENCR_DES:
                        {
                            iv = new byte[this.encryptionAlgBlockSize];
                            Utils.MemCpy(this.data, 8, ref iv, 0, this.encryptionAlgBlockSize);
                            byte[] dataWithoutHeader = new byte[this.data.Length - iv.Length - 8];
                            Utils.MemCpy(this.data, 8 + iv.Length, ref dataWithoutHeader, 0, dataWithoutHeader.Length);
                            IPsecLite.Cryptography.DES alg = new IPsecLite.Cryptography.DES(dataWithoutHeader, this.encryptionKey, this.encryptionAlgBlockSize, CipherMode.CBC, iv);
                            alg.Decrypt();
                            alg.Decrypted.CopyTo(this.processedData, 0);
                            break;
                        }
                    case IKE_ENCR_ALGS.ENCR_DES3:
                        {
                            iv = new byte[this.encryptionAlgBlockSize];
                            Utils.MemCpy(this.data, 8, ref iv, 0, this.encryptionAlgBlockSize);
                            byte[] dataWithoutHeader = new byte[this.data.Length - iv.Length - 8];
                            Utils.MemCpy(this.data, 8 + iv.Length, ref dataWithoutHeader, 0, dataWithoutHeader.Length);
                            DES3 alg = new DES3(dataWithoutHeader, this.encryptionKey, this.encryptionAlgBlockSize, CipherMode.CBC, iv);
                            alg.Decrypt();
                            alg.Decrypted.CopyTo(this.processedData, 0);
                            break;
                        }
                }
                this.paddingLength = this.processedData[this.processedData.Length - 2];
                for (int i = 0; i < paddingLength; i++)
                    if (this.processedData[this.processedData.Length - paddingLength - 2 + i] != (i + 1))
                        result  = PROTECTION_RESULTS.INVALID_PAD;
                if (result == PROTECTION_RESULTS.OK)
                    this.nextProtocol = (PROTOCOLS)this.processedData[this.processedData.Length - 1];
                timer.Stop();
                this.processingCost.EncryptionCycles = timer.Cycles;
                this.processingCost.EncryptionTime = timer.Duration;
            }
            catch
            {
                result = PROTECTION_RESULTS.DECR_FAILED;
            }
            return result;
        }
        public PROTECTION_RESULTS Protect()
        {
            if (this.data != null)
            {
                if (this.data.Length != 0)
                {
                    PROTECTION_RESULTS result =  this.encrypt();
                    if (result == PROTECTION_RESULTS.OK)
                    {
                        Utils.IntToBytes(this.spi).CopyTo(this.processedData, 0);
                        Utils.IntToBytes(this.sequenceNumber).CopyTo(this.processedData, 4);
                        if (!this.noICV)
                            result = this.addMAC();
                    }
                    return result;
                }
                else
                    throw new Exception("No data to protect.");
            }
            else
                throw new Exception("Data is not set.");
        }
        public PROTECTION_RESULTS Unprotect()
        {
            if (this.data != null)
            {
                if (this.data.Length != 0)
                {
                    PROTECTION_RESULTS result = PROTECTION_RESULTS.OK;
                    if (!this.noICV)
                        result = this.checkMAC();
                    if (result == PROTECTION_RESULTS.OK)
                    {
                        if (!this.noICV)
                        {
                            byte[] dataWithMACRemoved = new byte[this.data.Length - this.macLength];
                            Utils.MemCpy(this.data, 0, ref dataWithMACRemoved, 0, dataWithMACRemoved.Length);
                            this.data = null;
                            this.data = dataWithMACRemoved;
                        }
                        result = this.decrypt();
                    }
                    return result;
                }
                else
                    throw new Exception("No data to unprotect.");
            }
            else
                throw new Exception("Data is not set.");
        }
    }
}