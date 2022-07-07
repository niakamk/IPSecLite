using System;
using System.Collections;
using System.Collections.Generic;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.SADB;
using adabtek.IPsecLite.Types;
namespace adabtek.IPsecLite.IKEv2
{
    public enum IKE_EXCHANGE_TYPES: byte
    {
        INIT = 34,
        AUTH = 35,
        INFO = 37
    }
    public enum IKE_PAYLOADS: byte
    {
         NONE = 0,
         MORE_PROPOSAL = 2,
         MORE_ATTRIBUTES = 3,
         SA = 33,
         KE = 34,
         IDi = 35,
         IDr = 36,
         AUTH = 39,
         N = 40,
         NOTI = 41,
         DEL = 42,
         ENCR = 46
    }
    public enum IKE_PROTOCOLS: byte
    {
        RESERVED = 0,
        IKE = 1,
        AH = 2,
        ESP = 3,
        ESP_NO_ICV = 4
    }
    public enum IKE_MODE : byte
    {
        TRANSPORT = 0,
        TUNNEL = 1
    }
    public enum IKE_TRANSFORM_TYPES : byte
    {
         ENCR = 1,
         PRF = 2,
         INTEG = 3,
         DH = 4,
         ESN = 5
    }
    public enum IKE_ENCR_ALGS : byte
    {
        ENCR_DES_IV64 = 1,
        ENCR_DES = 2,
        ENCR_DES3 = 3,
        ENCR_RC5 = 4,
        ENCR_IDEA = 5,
        ENCR_CAST = 6,
        ENCR_BLOWFISH = 7,
        ENCR_IDEA3 = 8,
        ENCR_DES_IV32 = 9,
        ENCR_NULL = 11,
        ENCR_AES_CBC = 12,
        ENCR_AES_CTR = 13
    }
    public enum IKE_PRFS : byte
    {
         PRF_HMAC_MD5 = 1,
         PRF_HMAC_SHA1 = 2,
         PRF_HMAC_TIGER = 3,
         PRF_AES128_XCBC = 4
    }
    public enum IKE_INTEG_ALGS : byte
    {
        AUTH_NONE = 0,
        AUTH_HMAC_MD5_96 = 1,
        AUTH_HMAC_SHA1_96 = 2,
        AUTH_DES_MAC = 3,
        AUTH_KPDK_MD5 = 4,
        AUTH_AES_XCBC_96 = 5
    }

    public enum IKE_ATTRIBUTE_TYPES : short
    {
        KEY_LENGTH = 14,
        BLOCK_SIZE = 15
    }
    public enum IKE_DH_GROUPS : short 
    {
         GROUP1_768BIT_MODP = 1, /* RFC 4306 */
         GROUP2_1024BIT_MODP = 2, /* RFC 4306 */
         GROUP5_1536BIT_MODP = 5, /* RFC 3526 */
         GROUP5_2048BIT_MODP = 14, /* RFC 3526 */
         GROUP5_3072BIT_MODP = 15, /* RFC 3526 */
         GROUP5_4096BIT_MODP = 16, /* RFC 3526 */
         GROUP5_6144BIT_MODP = 17, /* RFC 3526 */
         GROUP5_8192BIT_MODP = 18 /* RFC 3526 */
    }
    public enum IKE_ID_TYPES : byte 
    {
         IPV4_ADDR = 1,
         FQDN = 2,
         RFC822_ADDR = 3,
         IPV6_ADDR = 5,
         DER_ASN1_DN = 9,
         DER_ASN1_GN= 10,
         KEY_ID = 11
    }
     public enum IKE_AUTH_METHODS : byte 
     {
         RSA_SIGN = 1,
         SHARED_KEY_MIC = 2,
         DSS_SIGN = 3
    }
    public enum IKE_NOTIFY_MSG_TYPES : short
    {
         UNSUPPORTED_CRITICAL_PAYLOAD = 1,
         INVALID_IKE_SPI = 4,
         INVALID_MAJOR_VERSION = 5,
         INVALID_SYNTAX = 7,
         INVALID_MESSAGE_ID = 9,
         INVALID_SPI = 11,
         NO_PROPOSAL_CHOSEN = 14,
         INVALID_KE_PAYLOAD = 17,
         AUTHENTICATION_FAILED = 24,
         SINGLE_PAIR_REQUIRED = 34,
         NO_ADDITIONAL_SAS = 35,
         INTERNAL_ADDRESS_FAILURE = 36,
         FAILED_CP_REQUIRED = 37,
         TS_UNACCEPTABLE = 38,
         INVALID_SELECTORS = 39,

         USE_TRANSPORT_MODE = 16391
    }

    public class IKE_Payload_Header
    {
        IKE_PAYLOADS nextPayload;
        byte RESERVED;
        short length;

        public IKE_PAYLOADS NextPayload { get { return this.nextPayload; } }
        public short Length { get { return this.length; } set { this.length = value; } }

        public IKE_Payload_Header(IKE_PAYLOADS NextPayload)
        {
            this.nextPayload = NextPayload;
            this.RESERVED = 0;
        }

        public static IKE_Payload_Header TryParse(byte[] RawPaylodHeader, int Index)
        {
            IKE_Payload_Header header = new IKE_Payload_Header((IKE_PAYLOADS)RawPaylodHeader[Index]);
            header.length = Utils.BytesToShort(RawPaylodHeader[Index + 2], RawPaylodHeader[Index + 3]);
            return header;
        }

        public byte[] ToBytes()
        {
            byte[] header = new byte[4];
            int index = 0;
            header[index++] = (byte)this.nextPayload;
            header[index++] = this.RESERVED;
            header[index++] = (byte)((this.length & 0xff00) >> 8);
            header[index++] = (byte)(this.length & 0x00ff);
            return header;
        }
    }
    public class IKE_Transform_Attribute
    {
        ushort af_and_AttributeType;
        short lengthOrValue;
        byte[] value;

        public byte[] Value
        {
            get
            {
                if ((this.af_and_AttributeType >> 15) == 1)
                    return Utils.ShortToBytes(lengthOrValue);
                else
                    return value;
            }
        }
        public short Length
        {
            get
            {
                if ((this.af_and_AttributeType >> 15) == 1)
                    return 4;
                else
                    return (short)(4 + this.lengthOrValue);
            }
        }
        public short AttributeType {
            get
            {
                return (short)(this.af_and_AttributeType & 0x7fff);
            }
        }
        public IKE_Transform_Attribute(IKE_ATTRIBUTE_TYPES AttributeType, short Value)
        {
            this.af_and_AttributeType = 0x8000;
            this.af_and_AttributeType += (byte)AttributeType;
            this.lengthOrValue = Value;
        }
        public IKE_Transform_Attribute(IKE_ATTRIBUTE_TYPES AttributeType, byte[] Value)
        {
            this.af_and_AttributeType += (ushort)AttributeType;
            this.lengthOrValue = (short)Value.Length;
            this.value = Value;
        }
        public byte[] ToBytes()
        {
            byte[] attribute = new byte[this.Length];
            attribute[0] = (byte)(this.af_and_AttributeType >> 8);
            attribute[1] = (byte)(this.af_and_AttributeType & 0x00ff);
            attribute[2] = (byte)(this.lengthOrValue >> 8);
            attribute[3] = (byte)(this.lengthOrValue & 0x00ff);
            if ((this.af_and_AttributeType >> 15) == 0)
            {
                int index = 4;
                for (int i = 0; i < this.Length - 4; i++)
                    attribute[++index] = this.value[i];
            }
            return attribute;
        }
        public static IKE_Transform_Attribute TryParse(byte[] RawTransformAttribute, short Index)
        {
            IKE_Transform_Attribute attribute;
            try
            {
                short lengthOrValue = Utils.BytesToShort(RawTransformAttribute[2 + Index], RawTransformAttribute[3 + Index]);
                if ((RawTransformAttribute[Index] >> 7) == 1)
                    attribute = new IKE_Transform_Attribute((IKE_ATTRIBUTE_TYPES)RawTransformAttribute[1 + Index], lengthOrValue);
                else
                {
                    byte[] value = new byte[lengthOrValue];
                    for (int i = 0; i < lengthOrValue; i++)
                        value[i] = RawTransformAttribute[4 + i + Index];

                    attribute = new IKE_Transform_Attribute((IKE_ATTRIBUTE_TYPES)RawTransformAttribute[1 + Index], value);
                }              
            }
            catch 
            {
                attribute =  null;
                throw;
            }
            return attribute;
        }
    }

    public class IKE_Transform
    {
        IKE_Payload_Header header;
        IKE_TRANSFORM_TYPES transformType;
        byte RESERVED;
        short transformID; 
        IKE_Transform_Attribute[] attributes;
        IKE_Transform(IKE_PAYLOADS NextPayload, byte RESERVED, short Length)
        {
            this.header = new IKE_Payload_Header(NextPayload);
            //TODO: Maybe we need to set the header too!
            header.Length = Length;
        }
        public IKE_Transform(bool IsLastTransform, IKE_TRANSFORM_TYPES TransformType, byte TransformID)
        {
            this.header = new IKE_Payload_Header((IKE_PAYLOADS) (IsLastTransform ? 0 : 3));
            this.header.Length = 8;
            this.transformType = TransformType;
            this.transformID = TransformID;
        }
        public short Length { get { return this.header.Length;} }
        public IKE_TRANSFORM_TYPES TransformType { get { return this.transformType; } }
        public IKE_Transform_Attribute[] Attributes { get { return this.attributes; } }
        public short TransformID { get { return transformID; } }
        public void AddAttribute(IKE_Transform_Attribute NewAttribute)
        {
            if (attributes == null)
            {
                attributes = new IKE_Transform_Attribute[1];
                attributes[0] = NewAttribute;
                this.header.Length += NewAttribute.Length;
            }
            else
            {
                IKE_Transform_Attribute[] attrs = new IKE_Transform_Attribute[attributes.Length + 1];
                attributes.CopyTo(attrs, 0);
                attributes = null;
                attributes = attrs;
                attributes[attributes.Length - 1] = NewAttribute;
                this.header.Length += NewAttribute.Length;
            }
        }
        static void AddAttribute(IKE_Transform transform, byte[] RawAttributes, short Index, byte NoOfAttributes)
        {
            transform.attributes = new IKE_Transform_Attribute[NoOfAttributes];
            byte i = 0;
            while (Index < RawAttributes.Length)
            {
                transform.attributes[i] = IKE_Transform_Attribute.TryParse(RawAttributes, Index);
                Index += transform.attributes[i].Length;
                i++;
            }

        }
        public byte[] ToBytes()
        {
            byte[] transform = new byte[this.Length];
            this.header.ToBytes().CopyTo(transform, 0);
            int index = 4;
            transform[index++] = (byte)this.transformType;
            transform[index++] = this.RESERVED;
            transform[index++] = (byte)(this.transformID >> 8);
            transform[index++] = (byte) (this.transformID & 0x00ff);

            byte[] byteAttr;
            if (this.attributes != null)
            {
                foreach (IKE_Transform_Attribute attr in this.attributes)
                {
                    byteAttr = attr.ToBytes();
                    Utils.MemCpy(byteAttr, 0, ref transform, index, byteAttr.Length);
                    index += byteAttr.Length;
                    byteAttr = null;
                }
            }

            return transform;

        }
        public static IKE_Transform TryParse(byte[] RawTransform)
        {
            IKE_Transform transform;
            try
            {
                short Length = Utils.BytesToShort(RawTransform[2], RawTransform[3]);
                if (Length != RawTransform.Length)
                    throw new ArgumentOutOfRangeException("Invalid Transform Length.");

                transform = new IKE_Transform((IKE_PAYLOADS)RawTransform[0],RawTransform[1], Length);
                transform.transformType = (IKE_TRANSFORM_TYPES) RawTransform[4];
                transform.RESERVED = RawTransform[5];
                transform.transformID = Utils.BytesToShort(RawTransform[6], RawTransform[7]);

                int index = 8;
                byte attributeCount = 0;
                //byte[] attribute;
                while (index < RawTransform.Length)
                {
                    attributeCount++;
                    if ((RawTransform[index] >> 7) == 1)
                        //Attribute includes value in bytes 2 and 3 so total size is 4
                        index += 4;
                    else
                        index += 4 + Utils.BytesToShort(RawTransform[index + 2], RawTransform[index + 3]);
                }
                if (index != RawTransform.Length)
                    throw new ArgumentOutOfRangeException("Invalid Transform Attribtues.");

                AddAttribute(transform, RawTransform, 8, attributeCount);
                
            }
            catch 
            {
                transform = null;
                throw;
            }
            return transform;
        }

    }
    //***************************************************************************
    //Proposal Substructure
    //***************************************************************************
    public class IKE_Proposal
    {
        IKE_Payload_Header header;
        byte proposalNumber;
        IKE_PROTOCOLS protocolID;
        byte spiSize;
        byte proposalTransforms;
        byte[] spi;
        IKE_Transform[] transforms;

        public IKE_Proposal(byte ProposalNumber, IKE_PROTOCOLS Protocol, bool IsLastProposal)
        {
            this.header = new IKE_Payload_Header((IKE_PAYLOADS)(IsLastProposal ? 0 : 3));
            this.header.Length = 8;
            this.proposalNumber = ProposalNumber;
            this.protocolID = Protocol;
            switch (Protocol)
            {
                case IKE_PROTOCOLS.AH:
                    this.spiSize = 4;
                    break;
                case IKE_PROTOCOLS.ESP:
                    this.spiSize = 4;
                    break;
                case IKE_PROTOCOLS.ESP_NO_ICV:
                    this.spiSize = 4;
                    break;
                case IKE_PROTOCOLS.IKE:
                    this.spiSize = 8;
                    break;
            }
            //# of Transforms will be updated when transforms are added
            this.header.Length += this.spiSize;

            spi = new byte[this.spiSize];
            Random rnd = new Random();
            rnd.NextBytes(this.spi);
        }
        public IKE_Proposal(IKE_PAYLOADS MoreProposal)
        {
            this.header = new IKE_Payload_Header(MoreProposal);
        }

        public short Length { get { return this.header.Length; } }
        public IKE_Transform[] Transforms { get { return this.transforms; } }
        public byte Number { get { return this.proposalNumber; } }
        public byte[] SPI { get { return this.spi; } }
        public string SPIHex { get { return Utils.ToHexString(this.spi); } }
        public IKE_PROTOCOLS ProtocolID 
        {
            //TODO: Remove the set and use update...
            set { this.protocolID = value; }
            get { return this.protocolID; } 
        }

        public void AddTransform(IKE_Transform NewTransform, bool IsRaw)
        {
            this.proposalTransforms += 1;
            if (this.transforms == null)
            {
                this.transforms = new IKE_Transform[this.proposalTransforms];
                this.transforms[0] = NewTransform;
            }
            else
            {
                IKE_Transform[] trans = new IKE_Transform[this.proposalTransforms];
                transforms.CopyTo(trans, 0);
                this.transforms = null;
                this.transforms = trans;
                this.transforms[this.transforms.Length - 1] = NewTransform;
            }
            if (!IsRaw)
                this.header.Length += (short)(NewTransform.Length);
        }
        public byte[] ToBytes()
        {
            byte[] proposal = new byte[this.header.Length];
            this.header.ToBytes().CopyTo(proposal, 0);
            int index = 4;
            proposal[index++] = this.proposalNumber;
            proposal[index++] = (byte) this.protocolID;
            proposal[index++] = this.spiSize;
            proposal[index++] = this.proposalTransforms;
            Utils.MemCpy(this.spi, 0, ref proposal, index, this.spiSize);
            index += this.spiSize;
            byte[] byteTranform;
            foreach (IKE_Transform transform in this.transforms)
            {
                byteTranform = transform.ToBytes();
                Utils.MemCpy(byteTranform, 0, ref proposal, index, byteTranform.Length);
                index += byteTranform.Length;
                byteTranform = null;
            }

            return proposal;
        }
        public static IKE_Proposal TryParse(byte[] RawProposal)
        {
            IKE_Proposal proposal;
            try
            {
                if ((RawProposal[0] != 0) && (RawProposal[0] != 2))
                    throw new ArgumentOutOfRangeException("Invalid Next Proposal Indicator.");
                else
                {   proposal = new IKE_Proposal((IKE_PAYLOADS)RawProposal[0]);
                }

                short Length = Utilities.Utils.BytesToShort(RawProposal[2], RawProposal[3]);
                if (RawProposal.Length != Length)
                    throw new ArgumentOutOfRangeException("Invalid Proposal Length;");
                else
                    proposal.header.Length = Length;

                proposal.proposalNumber = RawProposal[4];
                proposal.protocolID = (IKE_PROTOCOLS)RawProposal[5];
                proposal.spiSize = RawProposal[6];
                //No of transforms is set by AddTransform
                proposal.spi = new byte[proposal.spiSize];
                Utils.MemCpy(RawProposal, 8, ref proposal.spi, 0, proposal.spiSize);

                IKE_Transform transform;
                byte[] RawTransform;
                short transformLength;
                int index = 8 + proposal.spiSize;
                int noOfTransforms = RawProposal[7];
                for (int i = 0; i < noOfTransforms; i++)
                {
                    transformLength = Utils.BytesToShort(RawProposal[index + 2], RawProposal[index + 3]);
                    RawTransform = new byte[transformLength];
                    Utils.MemCpy(RawProposal, index, ref RawTransform, 0, transformLength);
                    transform = IKE_Transform.TryParse(RawTransform);
                    RawTransform = null;

                    proposal.AddTransform(transform, true);

                    index += transformLength;
                }
              
            }
            catch 
            {
                proposal = null;
                throw;
            }
            return proposal;
        }
    }
    //***************************************************************************
    //SA Payload
    //***************************************************************************
    public class IKE_SA_Payload
    {
        IKE_Payload_Header header;
        IKE_Proposal[] proposals;

        public IKE_PAYLOADS NextPayload { get { return this.header.NextPayload; } }
        public short Length { get { return this.header.Length; } }

        public IKE_Proposal[] Proposals { get {return this.proposals;} }

        public IKE_SA_Payload(IKE_PAYLOADS NextPayload)
        {
            this.header = new IKE_Payload_Header(NextPayload);
            this.header.Length = 4;
        }
        public void AddProposal(IKE_Proposal NewProposal)
        {
            this.header.Length += NewProposal.Length;
            if (this.Proposals == null)
            {
                this.proposals = new IKE_Proposal[1];
                this.proposals[0] = NewProposal;
            }
            else
            {
                IKE_Proposal[] proposals = new IKE_Proposal[this.Proposals.Length + 1];
                this.proposals.CopyTo(proposals, 0);
                this.proposals = null;
                this.proposals = proposals;
                this.proposals[this.proposals.Length - 1] = NewProposal;
            }
        }
        public void IncludeProposal(IKE_Proposal NewProposal)
        {
            if (this.Proposals == null)
            {
                this.proposals = new IKE_Proposal[1];
                this.proposals[0] = NewProposal;
            }
            else
            {
                IKE_Proposal[] proposals = new IKE_Proposal[this.Proposals.Length + 1];
                this.proposals.CopyTo(proposals, 0);
                this.proposals = null;
                this.proposals = proposals;
                this.proposals[this.proposals.Length - 1] = NewProposal;
            }
        }

        public byte[] ToBytes()
        {
            byte[] SA = new byte[this.header.Length];
            this.header.ToBytes().CopyTo(SA, 0);
            int index = 4; 
            byte[] byteProposal;
            foreach (IKE_Proposal proposal in this.Proposals)
            {
                byteProposal = proposal.ToBytes();
                Utils.MemCpy(byteProposal, 0, ref SA, index, byteProposal.Length);
                index += byteProposal.Length;
                byteProposal = null;
            }
            return SA;
        }
        public static IKE_SA_Payload TryParse(byte[] RawSA, int Index)
        {
            IKE_SA_Payload saPayload;
            try
            {
                saPayload = new IKE_SA_Payload((IKE_PAYLOADS) RawSA[Index]);
                saPayload.header.Length = Utils.BytesToShort(RawSA[Index + 2], RawSA[Index + 3]);

                IKE_Proposal proposal;
                byte[] RawProposal;
                short proposalLength;
                Index += 4;
                while (RawSA[Index] != 0)   //There are more proposals
                {
                    proposalLength = Utils.BytesToShort(RawSA[Index + 2], RawSA[Index + 3]);
                    RawProposal = new byte[proposalLength];
                    Utils.MemCpy(RawSA, Index, ref RawProposal, 0, proposalLength);
                    proposal = IKE_Proposal.TryParse(RawProposal);
                    RawProposal = null;

                    saPayload.IncludeProposal(proposal);

                    Index += proposalLength;
                }
                //Get the last proposal
                proposalLength = Utils.BytesToShort(RawSA[Index + 2], RawSA[Index + 3]);
                RawProposal = new byte[proposalLength];
                Utils.MemCpy(RawSA, Index, ref RawProposal, 0, proposalLength);
                proposal = IKE_Proposal.TryParse(RawProposal);
                RawProposal = null;

                saPayload.IncludeProposal(proposal);
            }
            catch 
            {
                saPayload = null;
                throw;
            }
            return saPayload;
        }

    }
    //***************************************************************************
    //Key Exchange Payload
    //***************************************************************************
    public class IKE_KE_Payload
    {
        IKE_Payload_Header header;
        IKE_DH_GROUPS dhGroup;
        short RESERVED;
        byte[] keyExchangeData;
        byte[] privateKey;

        public IKE_PAYLOADS NextPayload { get { return this.header.NextPayload; } }
        public short Length { get { return this.header.Length; } }
        public byte[] PrivateKey { get { return this.privateKey; } }
        public IKE_DH_GROUPS DHGroup { get { return this.dhGroup; } }
        public byte[] KeyExchangeData { get { return this.keyExchangeData; } }
        public IKE_KE_Payload(IKE_PAYLOADS NextPayload, IKE_DH_GROUPS DHGroup)
        {
            string modP = "";
            string groupGenerator = "";

            this.header = new IKE_Payload_Header(NextPayload);
            this.dhGroup = DHGroup;
            this.RESERVED = 0;
            this.header.Length = 8;
            modP = Utils.ModP((Utilities.DH_GROUPS)this.dhGroup, out groupGenerator);
            //TODO: Check the length of the following string
            this.header.Length += (short) (modP.Length / 2 );

            Random rand = new Random();
            BigInteger p = BigInteger.genPseudoPrime(this.header.Length - 8, 5, rand); //8 bytes is the header and the rest is the data
            BigInteger mod = new BigInteger(modP, 16);
            BigInteger g = new BigInteger(groupGenerator, 10);
            BigInteger publicKey = g.modPow(p, mod);
            this.privateKey = p.getBytes();
            this.keyExchangeData = publicKey.getBytes();

        }
        public IKE_KE_Payload(IKE_PAYLOADS NextPayload, IKE_DH_GROUPS DHGroup, short Length)
        {
            this.header = new IKE_Payload_Header(NextPayload);
            this.dhGroup = DHGroup;
            this.header.Length = 8;
            switch (this.dhGroup)
            {
                case IKE_DH_GROUPS.GROUP1_768BIT_MODP:
                    this.header.Length += 96;
                    break;
                case IKE_DH_GROUPS.GROUP2_1024BIT_MODP:
                    this.header.Length += 128;
                    break;
                case IKE_DH_GROUPS.GROUP5_1536BIT_MODP:
                    this.header.Length += 192;
                    break;
                case IKE_DH_GROUPS.GROUP5_2048BIT_MODP:
                    this.header.Length += 256;
                    break;
                case IKE_DH_GROUPS.GROUP5_3072BIT_MODP:
                    this.header.Length += 384;
                    break;
                case IKE_DH_GROUPS.GROUP5_4096BIT_MODP:
                    this.header.Length += 512;
                    break;
                case IKE_DH_GROUPS.GROUP5_6144BIT_MODP:
                    this.header.Length += 768;
                    break;
                case IKE_DH_GROUPS.GROUP5_8192BIT_MODP:
                    this.header.Length += 1024;
                    break;
            }
            if (Length != this.Length)
                throw new ArgumentException("Invalid Key Exchange Payload Length.");
        }

        public byte[] ToBytes()
        {
            byte[] kePayload = new byte[this.header.Length];
            this.header.ToBytes().CopyTo(kePayload, 0);
            int index = 4;
            kePayload[index++] = (byte)((short)this.dhGroup >> 8);
            kePayload[index++] = (byte)((short)this.dhGroup & 0x00ff);
            kePayload[index++] = (byte)(this.RESERVED >> 8);
            kePayload[index++] = (byte)(this.RESERVED & 0x00ff);
            Utils.MemCpy(this.KeyExchangeData, 0, ref kePayload, index, this.KeyExchangeData.Length);
            return kePayload;
        }
        public static IKE_KE_Payload TryParse(byte[] RawKE, int Index)
        {
            IKE_KE_Payload kePayload;
            try
            {
                short Length = Utils.BytesToShort(RawKE[Index + 2], RawKE[Index + 3]);

                kePayload = new IKE_KE_Payload((IKE_PAYLOADS)RawKE[Index], (IKE_DH_GROUPS)Utils.BytesToShort(RawKE[Index + 4], RawKE[Index + 5]), Length);
                kePayload.RESERVED = Utils.BytesToShort(RawKE[Index + 6], RawKE[Index + 7]);
                if (kePayload.RESERVED != 0)
                    throw new ArgumentOutOfRangeException("RESERVED","Invalid RESERVED for Key Exchange Payload.");
                kePayload.keyExchangeData = new byte[kePayload.header.Length - 8];
                Utils.MemCpy(RawKE, Index + 8, ref kePayload.keyExchangeData, 0, kePayload.header.Length - 8);
            }
            catch 
            {
                kePayload = null;
                throw;
            }
            return kePayload;
        }
    }
    //***************************************************************************
    //Nonce Payload
    //***************************************************************************
    public class IKE_Nonce_Payload
    {
        IKE_Payload_Header header;
        byte[] nonceData;

        public IKE_PAYLOADS NextPayload { get { return this.header.NextPayload; } }

        public IKE_Nonce_Payload(IKE_PAYLOADS NextPayload, short DataLength) 
        {
            this.header = new IKE_Payload_Header(NextPayload);
            this.header.Length =(short)(DataLength + 4);

            nonceData = new byte[DataLength];
            Random rand = new Random();
            rand.NextBytes(nonceData);
        }
        public short Length { get { return this.header.Length; } }
        public byte[] Data { get { return this.nonceData; } }
        public byte[] ToBytes()
        {
            byte[] nonce = new byte[this.header.Length];
            this.header.ToBytes().CopyTo(nonce, 0);
            int index = 4;
            this.nonceData.CopyTo(nonce, index);

            return nonce;
        }
        public static IKE_Nonce_Payload TryParse(byte[] RawNonce, int Index)
        {
            short Length = Utils.BytesToShort(RawNonce[Index + 2], RawNonce[Index + 3]);
            IKE_Nonce_Payload nonce;
            try
            {
                nonce = new IKE_Nonce_Payload((IKE_PAYLOADS)RawNonce[Index], (short)(Length - 4));
                nonce.nonceData = new byte[nonce.header.Length - 4];
                Utils.MemCpy(RawNonce, Index + 4, ref nonce.nonceData, 0, nonce.header.Length - 4);
            }
            catch 
            {
                nonce = null;
                throw;
            }
            return nonce;
        }
    }

    //***************************************************************************
    //Notify Payload
    //***************************************************************************
    public class IKE_Notify_Payload
    {
        IKE_Payload_Header header;
        IKE_PROTOCOLS protocolID;
        byte spiSize;
        IKE_NOTIFY_MSG_TYPES notifyMessageType;
        byte[] spi;
        public short Length
        {
            get
            {
                return this.header.Length;
            }
        }
        public IKE_PAYLOADS NextPayload { get { return this.header.NextPayload; } }
        public IKE_NOTIFY_MSG_TYPES NotifyMessageType
        {
            get { return this.notifyMessageType; }
        }

        public IKE_Notify_Payload()
        {
        }
        public IKE_Notify_Payload(IKE_PAYLOADS NextPayload, IKE_PROTOCOLS ProtocolID, IKE_NOTIFY_MSG_TYPES MessageType)
        {
            this.header = new IKE_Payload_Header(NextPayload);
            this.header.Length = 8;
            this.protocolID = ProtocolID;
            switch (ProtocolID)
            {
                case IKE_PROTOCOLS.ESP:
                    this.spiSize = 4;
                    break;
                case IKE_PROTOCOLS.AH:
                    this.spiSize = 4;
                    break;
                case IKE_PROTOCOLS.IKE:
                    this.spiSize = 8;
                    break;
                default:
                    this.spiSize = 0;
                    break;
            }

            this.notifyMessageType = MessageType;
        }
        public void AddSPI(byte[] SPI)
        {
            if (SPI.Length != this.spiSize)
                throw new ArgumentException("Invalid SPI.");

            this.spi = SPI;
        }
        public IKE_PROTOCOLS ProtocolID { get { return this.protocolID; } }
        public byte SPISize { get { return this.spiSize; } }
        public byte[] ToBytes()
        {
            byte[] notifyPayload = new byte[this.header.Length];
            int index = 0;
            this.header.ToBytes().CopyTo(notifyPayload, 0);
            index += 4;
            notifyPayload[index++] = (byte)this.protocolID;
            notifyPayload[index++] = this.spiSize;
            notifyPayload[index++] = (byte)((short)this.notifyMessageType >> 8);
            notifyPayload[index++] = (byte)((short)this.notifyMessageType & 0x00ff);
            if (spi != null)
                spi.CopyTo(notifyPayload, index);
            return notifyPayload;
        }
        public static IKE_Notify_Payload TryParse(byte[] RawNotifyPayload, int Index)
        {
            IKE_Notify_Payload notifyPayload = new IKE_Notify_Payload();
            notifyPayload.header = IKE_Payload_Header.TryParse(RawNotifyPayload, 0);
            Index = 4;
            notifyPayload.protocolID = (IKE_PROTOCOLS)RawNotifyPayload[Index++];
            notifyPayload.spiSize = RawNotifyPayload[Index++];
            notifyPayload.notifyMessageType = (IKE_NOTIFY_MSG_TYPES) Utils.BytesToShort(RawNotifyPayload[Index], RawNotifyPayload[Index + 1]);
            Index += 2;
            byte[] spi = new byte[notifyPayload.spiSize];
            Utils.MemCpy(RawNotifyPayload, Index, ref spi, 0, notifyPayload.spiSize);
            notifyPayload.AddSPI(spi);
            return notifyPayload;
        }

    }

    //***************************************************************************
    //Identification Payload
    //***************************************************************************
    public class IKE_Identification_Payload
    {
        IKE_Payload_Header header;
        IKE_ID_TYPES idType;
        byte[] RESERVED = new byte[3];
        byte[] idData;

        public IKE_ID_TYPES IdType { get { return this.idType; } }
        public IKE_PAYLOADS NextPayload { get { return this.header.NextPayload;} }
        public short Length { get { return this.header.Length; } }

        public IKE_Identification_Payload(byte[] IP, IKE_PAYLOADS NextPayload)
        {
            this.header = new IKE_Payload_Header(NextPayload);
            this.header.Length = (short)(8 + IP.Length);

            this.idType = IKE_ID_TYPES.IPV4_ADDR;
            this.idData = new byte[IP.Length];
            IP.CopyTo(idData, 0);
        }

        public byte[] ToBytes()
        {
            byte[] idPayload = new byte[this.header.Length];

            this.header.ToBytes().CopyTo(idPayload, 0);
            idPayload[4] =(byte)this.idType;
            Utils.MemSet(ref idPayload, 5, 0, 3);
            int index = 8;
            Utils.MemCpy(this.idData, 0, ref idPayload, index, this.idData.Length);

            return idPayload;
        }
        IKE_Identification_Payload()
        {
        }
        public static IKE_Identification_Payload TryParse(byte[] RawIdentificationPayload, int Index)
        {
            IKE_Identification_Payload idPayload = new IKE_Identification_Payload();
            idPayload.header = IKE_Payload_Header.TryParse(RawIdentificationPayload, Index);
            idPayload.idType = (IKE_ID_TYPES) RawIdentificationPayload[Index + 4];
            idPayload.idData = new byte[idPayload.header.Length - 8];
            Utils.MemCpy(RawIdentificationPayload, Index + 8, ref idPayload.idData, 0, idPayload.header.Length - 8);

            return idPayload;
        }

    }
    //***************************************************************************
    //AUTH Payload
    //***************************************************************************
    public class IKE_Auth_Payload
    {
        IKE_Payload_Header header;
        IKE_AUTH_METHODS authMethod;
        byte[] RESERVED = new byte[3];
        byte[] authData;

        public IKE_AUTH_METHODS AuthMethod { get { return this.authMethod; } }
        public IKE_PAYLOADS NextPayload { get { return this.header.NextPayload; } }
        public short Length { get { return this.header.Length; } }
        public byte[] AuthData { get { return this.authData; } }

        public IKE_Auth_Payload(IKE_PAYLOADS NextPayload, byte[] AuthData)
        {
            this.header = new IKE_Payload_Header(NextPayload);
            this.header.Length = (short)(8 + AuthData.Length);

            this.authMethod = IKE_AUTH_METHODS.SHARED_KEY_MIC;
            this.authData = new byte[AuthData.Length];
            AuthData.CopyTo(this.authData, 0);
        }

        IKE_Auth_Payload()
        {
        }
        public static IKE_Auth_Payload TryParse(byte[] RawAuthPayload, int Index)
        {
            IKE_Auth_Payload authPayload = new IKE_Auth_Payload();
            authPayload.header = IKE_Payload_Header.TryParse(RawAuthPayload, Index);
            authPayload.authMethod = (IKE_AUTH_METHODS)RawAuthPayload[Index + 4];
            authPayload.authData = new byte[authPayload.header.Length - 8];
            Utils.MemCpy(RawAuthPayload, Index + 8, ref authPayload.authData, 0, authPayload.header.Length - 8);

            return authPayload;
        }
        public byte[] ToBytes()
        {
            byte[] authPayload = new byte[this.header.Length];

            this.header.ToBytes().CopyTo(authPayload, 0);
            authPayload[4] = (byte)this.authMethod;
            Utils.MemSet(ref authPayload, 5, 0, 3);
            int index = 8;
            Utils.MemCpy(this.authData, 0, ref authPayload, index, this.authData.Length);

            return authPayload;
        }
    }
    //***************************************************************************
    //Delete Payload
    //***************************************************************************
    public class IKE_Delete_Payload
    {
        IKE_Payload_Header header;
        IKE_PROTOCOLS protocolID;
        byte spiSize;
        short noOfSPIs;
        Dictionary<int, byte[]> spis;
        public short Length
        {
            get
            {
                return this.header.Length;
            }
        }
        public IKE_PAYLOADS NextPayload { get { return this.header.NextPayload; } }
        public IKE_Delete_Payload()
        {
            this.spis = new Dictionary<int, byte[]>();
        }
        public IKE_Delete_Payload(IKE_PAYLOADS NextPayload, IKE_PROTOCOLS ProtocolID)
        {
            this.header = new IKE_Payload_Header(NextPayload);
            this.header.Length = 8;
            this.protocolID = ProtocolID;
            switch (ProtocolID)
            {
                case IKE_PROTOCOLS.ESP:
                    this.spiSize = 4;
                    break;
                case IKE_PROTOCOLS.AH:
                    this.spiSize = 4;
                    break;
                case IKE_PROTOCOLS.IKE:
                    this.spiSize = 8;
                    break;
            }
            
            this.spis = new Dictionary<int, byte[]>();
            this.noOfSPIs = 0;
        }
        public void AddSPI(byte[] SPI)
        {
            if (SPI.Length != this.spiSize)
                throw new ArgumentException("Invalid SPI.");

            spis.Add(++this.noOfSPIs, SPI);
        }
        public IKE_PROTOCOLS ProtocolID { get { return this.protocolID; } }
        public short NoOfSPIs { get { return this.noOfSPIs; } }
        public byte SPISize { get { return this.spiSize; } }
        public byte[] ToBytes()
        {
            byte[] deletePayload = new byte[this.header.Length];
            int index = 0;
            this.header.ToBytes().CopyTo(deletePayload, 0);
            index += 4;
            deletePayload[index++] = (byte)this.protocolID;
            deletePayload[index++] = this.spiSize;
            deletePayload[index++] = (byte)(this.noOfSPIs >> 8);
            deletePayload[index++] = (byte)(this.noOfSPIs & 0x00ff);
            for (int i = 1; i < this.noOfSPIs; i++)
            {
                byte[] spi = spis[i];
                spi.CopyTo(deletePayload, index);
            }
            return deletePayload;
        }
        public static IKE_Delete_Payload TryParse(byte[] RawDeletePayload, int Index)
        {
            IKE_Delete_Payload deletePayload = new IKE_Delete_Payload();
            deletePayload.header = IKE_Payload_Header.TryParse(RawDeletePayload, 0);
            Index = 4;
            deletePayload.protocolID = (IKE_PROTOCOLS)RawDeletePayload[Index++];
            deletePayload.spiSize = RawDeletePayload[Index++];
            deletePayload.noOfSPIs = Utils.BytesToShort(RawDeletePayload[Index], RawDeletePayload[Index + 1]);
            Index += 2;
            for (int i = 1; i <= deletePayload.noOfSPIs; i++)
            {
                byte[] spi = new byte[deletePayload.spiSize];
                Utils.MemCpy(RawDeletePayload, Index, ref spi, 0, deletePayload.spiSize);
                deletePayload.spis.Add(i, spi);
                Index += deletePayload.spiSize;
            }
            return deletePayload;
        }

    }
    //***************************************************************************
    //Encrypted Payload
    //***************************************************************************
    public class IKE_Encrypted_Payload
    {
        IKE_Payload_Header header;
        byte[] iv;
        byte[] encryptedPayloads;
        LinkedList<PAYLOAD_NODE_TYPE> innerPayloads = new LinkedList<PAYLOAD_NODE_TYPE>();

        public short Length 
        { 
            get 
            { 
                return this.header.Length; 
            } 
        }
        public byte[] EncryptedPayloads
        {
            get
            {
                return this.encryptedPayloads;
            }
        }
        public short EncryptedPayloadsLength 
        { 
            get 
            {
                if (this.encryptedPayloads != null)
                    return (short)this.encryptedPayloads.Length;
                else
                    return 0;
            } 
        }
        public byte[] IV
        {
            set { this.iv = value; }
            get { return this.iv; }
        }
        public short IVLength
        {
            get
            {
                if (this.iv != null)
                    return (short)this.iv.Length;
                else
                    return 0;
            }
        }
        public IKE_PAYLOADS NextPayload 
        { 
            get 
            { 
                return this.header.NextPayload; 
            } 
        }
        public LinkedList<PAYLOAD_NODE_TYPE> InnerPayloads
        {
            get
            {
                return this.innerPayloads;
            }
        }
        public byte[] GetInnerPayloadsBytes
        {
            get
            {
                byte[] innerPayloadsBytes = new byte[this.header.Length - 4];
                int index = 0;
                foreach (PAYLOAD_NODE_TYPE p in this.innerPayloads)
                {
                    switch (p.PayloadType)
                    {
                        case IKE_PAYLOADS.SA:
                            {
                                IKE_SA_Payload payload = (IKE_SA_Payload)p.Payload;
                                Utils.MemCpy(payload.ToBytes(), 0, ref innerPayloadsBytes, index, payload.Length);
                                index += payload.Length;
                            }
                            break;
                        case IKE_PAYLOADS.KE:
                            {
                                IKE_KE_Payload payload = (IKE_KE_Payload)p.Payload;
                                Utils.MemCpy(payload.ToBytes(), 0, ref innerPayloadsBytes, index, payload.Length);
                                index += payload.Length;
                            }
                            break;
                        case IKE_PAYLOADS.N:
                            {
                                IKE_Nonce_Payload payload = (IKE_Nonce_Payload)p.Payload;
                                Utils.MemCpy(payload.ToBytes(), 0, ref innerPayloadsBytes, index, payload.Length);
                                index += payload.Length;
                            }
                            break;
                        case IKE_PAYLOADS.NOTI:
                            {
                                IKE_Notify_Payload payload = (IKE_Notify_Payload)p.Payload;
                                Utils.MemCpy(payload.ToBytes(), 0, ref innerPayloadsBytes, index, payload.Length);
                                index += payload.Length;
                            }
                            break;
                        case IKE_PAYLOADS.IDi:
                            {
                                IKE_Identification_Payload payload = (IKE_Identification_Payload)p.Payload;
                                Utils.MemCpy(payload.ToBytes(), 0, ref innerPayloadsBytes, index, payload.Length);
                                index += payload.Length;
                            }
                            break;
                        case IKE_PAYLOADS.IDr:
                            {
                                IKE_Identification_Payload payload = (IKE_Identification_Payload)p.Payload;
                                Utils.MemCpy(payload.ToBytes(), 0, ref innerPayloadsBytes, index, payload.Length);
                                index += payload.Length;
                            }
                            break;
                        case IKE_PAYLOADS.AUTH:
                            {
                                IKE_Auth_Payload payload = (IKE_Auth_Payload)p.Payload;
                                Utils.MemCpy(payload.ToBytes(), 0, ref innerPayloadsBytes, index, payload.Length);
                                index += payload.Length;
                            }
                            break;
                        case IKE_PAYLOADS.DEL:
                            {
                                IKE_Delete_Payload payload = (IKE_Delete_Payload)p.Payload;
                                Utils.MemCpy(payload.ToBytes(), 0, ref innerPayloadsBytes, index, payload.Length);
                                index += payload.Length;
                            }
                            break;
                        //TODO: NEW PAYLOADS
                    }
                }
                return innerPayloadsBytes;
            }
        }

        public IKE_Encrypted_Payload() 
        {
        }
        public IKE_Encrypted_Payload(IKE_PAYLOADS NextPayload)
        {
            this.header = new IKE_Payload_Header(NextPayload);
        }

        public void AddInnerPayload(object Payload, IKE_PAYLOADS PayloadType, short PayloadLength)
        {

            PAYLOAD_NODE_TYPE p;
            p.PayloadType = PayloadType;
            p.Payload = Payload;

            if (innerPayloads.Count == 0)
            {
                innerPayloads.AddFirst(p);
                this.header = new IKE_Payload_Header(p.PayloadType);
                this.header.Length = 4;
            }
            else
                innerPayloads.AddLast(p);

            this.header.Length += PayloadLength;

        }
        public void SetEncryptedContent(byte[] EncryptedContent, byte[] IV)
        {
            this.encryptedPayloads = EncryptedContent;
            this.iv = IV;
            this.header.Length = (short) (4 + this.encryptedPayloads.Length + this.iv.Length);
            //Clear all inner payloads as they are encrypted now
            this.innerPayloads.Clear();
        }
        public byte[] ToBytes()
        {
            byte[] encryptedPayload = new byte[this.header.Length];
            this.header.ToBytes().CopyTo(encryptedPayload, 0);
            int index = 4;
            this.iv.CopyTo(encryptedPayload, index);
            index += this.iv.Length;
            this.encryptedPayloads.CopyTo(encryptedPayload, index);
            return encryptedPayload;
        }
        public static IKE_Encrypted_Payload TryParse(byte[] RawEncrypted, int index)
        {
            IKE_Encrypted_Payload encryptedPayload = new IKE_Encrypted_Payload((IKE_PAYLOADS)RawEncrypted[index]);
            short length = Utils.BytesToShort(RawEncrypted[index + 2], RawEncrypted[index + 3]);
            encryptedPayload.header.Length = length;
            //This includes the iv we will separated it when decrypting it
            encryptedPayload.encryptedPayloads = new byte[length];
            Utils.MemCpy(RawEncrypted, index + 4, ref encryptedPayload.encryptedPayloads, 0, length - 4);
            return encryptedPayload;
        }
        public bool TryParse(byte[] RawDecryptedPayloads)
        {
            this.header.Length = 4;

            int index = 0;
            IKE_PAYLOADS payloadType = this.header.NextPayload;
            while (payloadType != IKE_PAYLOADS.NONE)
            {
                switch (payloadType)
                {
                    case IKE_PAYLOADS.NOTI:
                        {
                            IKE_Notify_Payload payload = IKE_Notify_Payload.TryParse(RawDecryptedPayloads, index);
                            this.AddInnerPayload(payload, IKE_PAYLOADS.NOTI, payload.Length);
                            index += payload.Length;
                            payloadType = payload.NextPayload;
                        }
                        break;
                    case IKE_PAYLOADS.N:
                        {
                            IKE_Nonce_Payload payload = IKE_Nonce_Payload.TryParse(RawDecryptedPayloads, index);
                            this.AddInnerPayload(payload, IKE_PAYLOADS.N, payload.Length);
                            index += payload.Length;
                            payloadType = payload.NextPayload;
                        }
                        break;
                    case IKE_PAYLOADS.IDi:
                        {
                            IKE_Identification_Payload payload = IKE_Identification_Payload.TryParse(RawDecryptedPayloads, index);
                            this.AddInnerPayload(payload, IKE_PAYLOADS.IDi, payload.Length);
                            index += payload.Length;
                            payloadType = payload.NextPayload;
                        }
                        break;
                    case IKE_PAYLOADS.IDr:
                        {
                            IKE_Identification_Payload payload = IKE_Identification_Payload.TryParse(RawDecryptedPayloads, index);
                            this.AddInnerPayload(payload, IKE_PAYLOADS.IDr, payload.Length);
                            index += payload.Length;
                            payloadType = payload.NextPayload;
                        }
                        break;
                    case IKE_PAYLOADS.AUTH:
                        {
                            IKE_Auth_Payload payload = IKE_Auth_Payload.TryParse(RawDecryptedPayloads, index);
                            this.AddInnerPayload(payload, IKE_PAYLOADS.AUTH, payload.Length);
                            index += payload.Length;
                            payloadType = payload.NextPayload;
                        }
                        break;
                    case IKE_PAYLOADS.SA:
                        {
                            IKE_SA_Payload payload = IKE_SA_Payload.TryParse(RawDecryptedPayloads, index);
                            this.AddInnerPayload(payload, IKE_PAYLOADS.SA, payload.Length);
                            index += payload.Length;
                            payloadType = payload.NextPayload;
                        }
                        break;
                    case IKE_PAYLOADS.DEL:
                        {
                            IKE_Delete_Payload payload = IKE_Delete_Payload.TryParse(RawDecryptedPayloads, index);
                            this.AddInnerPayload(payload, IKE_PAYLOADS.DEL, payload.Length);
                            index += payload.Length;
                            payloadType = payload.NextPayload;
                        }
                        break;

                    //TODO: MORE PAYLOADS
                }
            }
            this.encryptedPayloads = null;
            this.iv = null;

            return true;
        }
    }
    //***************************************************************************
    //IKE Message
    //***************************************************************************
    public struct PAYLOAD_NODE_TYPE
    {
        public IKE_PAYLOADS PayloadType;
        public object Payload;
    }
    public class IKE
    {

        LinkedList<PAYLOAD_NODE_TYPE> payloads = new LinkedList<PAYLOAD_NODE_TYPE>();

        long initiatorSPI;
        public long InitiatorSPI
        {
            get {return this.initiatorSPI;}
        }
        public string InitiatorSPIToHex
        {
            get { return Utils.ToHexString(this.initiatorSPI); }
        }

        long responderSPI;
        public long ResponderSPI
        {
            get { return this.responderSPI; }
        }
        public string ResponderSPIToHex
        {
            get { return Utils.ToHexString(this.responderSPI); }
        }

        IKE_PAYLOADS nextPayload;
        public IKE_PAYLOADS NextPayload
        {
            get { return this.nextPayload; }
        }

        IKE_EXCHANGE_TYPES exchangeType;
        public IKE_EXCHANGE_TYPES ExchangeType
        {
            get 
            {
                return this.exchangeType;
            }
        }

        byte version;
        public byte Version
        {
            get { return this.version; }
        }

        byte flags;
        public byte Flags
        {
            get { return this.flags; }
        }

        int messageID;
        public int MessageID
        {
            get { return this.messageID; }
        }

        int length;
        public int Length
        {
            get { return this.length; }
        }

        public LinkedList<PAYLOAD_NODE_TYPE> Payloads
        {
            get
            {
                return this.payloads;
            }
        }

        private static int MessageIDGenerator = 0;

        public IKE_SA_Payload SAPayload 
        { 
            get 
            {
                IKE_SA_Payload saPayload = null;
                if (payloads.Count > 0)
                    foreach (PAYLOAD_NODE_TYPE p in payloads)
                        if (p.PayloadType == IKE_PAYLOADS.SA)
                            saPayload = (IKE_SA_Payload) p.Payload;
                    
                return saPayload;
            }
        }
        public IKE_KE_Payload KeyExchangePayload
        { 
            get 
            {
                IKE_KE_Payload kePayload = null;
                if (payloads.Count > 0)
                    foreach (PAYLOAD_NODE_TYPE p in payloads)
                        if (p.PayloadType == IKE_PAYLOADS.KE)
                            kePayload = (IKE_KE_Payload)p.Payload;

                return kePayload;
            } 
        }
        public IKE_Nonce_Payload NoncePayload 
        { 
            get 
            {
                IKE_Nonce_Payload noncePayload = null;
                if (payloads.Count > 0)
                    foreach (PAYLOAD_NODE_TYPE p in payloads)
                        if (p.PayloadType == IKE_PAYLOADS.N)
                            noncePayload = (IKE_Nonce_Payload)p.Payload;

                return noncePayload;
            } 
        }
        public IKE_Encrypted_Payload EncryptedPayload 
        { 
            get
            {
                IKE_Encrypted_Payload encryptedPayload = null;
                if (payloads.Count > 0)
                    foreach (PAYLOAD_NODE_TYPE p in payloads)
                        if (p.PayloadType == IKE_PAYLOADS.ENCR)
                            encryptedPayload = (IKE_Encrypted_Payload)p.Payload;

                return encryptedPayload;
            } 
        }
        static private long genSPI()
        {
            //TODO: This needs to become Hash of (DestIP, Port, Protocol, Timestamp)
            Random rnd = new Random();
            long spi = 0;
            for (int i = 0; i < 8; i++)
            {
                spi <<= 8;
                spi += (byte)(rnd.Next() & 0x000000ff);
            }
            return spi;
        }
        public void INITi()
        {
            this.initiatorSPI = genSPI();
            this.responderSPI = 0;
            this.nextPayload = 0;
            this.version = 0x20;
            this.exchangeType = IKE_EXCHANGE_TYPES.INIT;
            this.flags = 0x08;
            this.messageID = ++MessageIDGenerator;
            this.length = 2 * sizeof(long) + 4 * sizeof(byte) + 2 * sizeof(int);
        }
        public void INITr(IKE INITi)
        {
            this.initiatorSPI = INITi.initiatorSPI;
            this.responderSPI = genSPI();
            this.nextPayload = 0;
            this.version = 0x20;
            this.exchangeType = IKE_EXCHANGE_TYPES.INIT;
            this.flags = 0x20;
            this.messageID = INITi.messageID;
            this.length = 2 * sizeof(long) + 4 * sizeof(byte) + 2 * sizeof(int);
        }
        public void AUTHi(long InitiatorSPI, long ResponderSPI)
        {

            this.initiatorSPI = InitiatorSPI;
            this.responderSPI = ResponderSPI;
            this.nextPayload = 0;
            this.version = 0x20;
            this.exchangeType = IKE_EXCHANGE_TYPES.AUTH;
            this.flags = 0x08;
            this.messageID = ++MessageIDGenerator;
            this.length = 2 * sizeof(long) + 4 * sizeof(byte) + 2 * sizeof(int);
        }
        public void AUTHr(IKE AUTHi)
        {

            this.initiatorSPI = AUTHi.InitiatorSPI;
            this.responderSPI = AUTHi.ResponderSPI;
            this.nextPayload = 0;
            this.version = 0x20;
            this.exchangeType = IKE_EXCHANGE_TYPES.AUTH;
            this.flags = 0x20;
            this.messageID = AUTHi.MessageID;
            this.length = 2 * sizeof(long) + 4 * sizeof(byte) + 2 * sizeof(int);
        }
        public void NOTIFY(long InitiatorSPI, long ResponderSPI, bool IsInitiator)
        {

            this.initiatorSPI = InitiatorSPI;
            this.responderSPI = ResponderSPI;
            this.nextPayload = 0;
            this.version = 0x20;
            this.exchangeType = IKE_EXCHANGE_TYPES.INFO;
            this.flags = (byte) (IsInitiator ? 0x08 : 0x20);
            this.messageID = ++MessageIDGenerator;
            this.length = 2 * sizeof(long) + 4 * sizeof(byte) + 2 * sizeof(int);
        }

        public void AddPayload(object Payload, IKE_PAYLOADS PayloadType, short PayloadLength)
        {

            PAYLOAD_NODE_TYPE p;
            p.PayloadType = PayloadType;
            p.Payload = Payload;

            if (payloads.Count == 0)
            {
                payloads.AddFirst(p);
                this.nextPayload = p.PayloadType;
            }
            else
                payloads.AddLast(p);

            this.length += PayloadLength;
               
        }
        public byte[] ToBytes()
        {
            byte[] ikeMessage = new byte[Length];

            int index = 0;

            Utils.LongToBytes(this.initiatorSPI).CopyTo(ikeMessage, index);
            index += 8;
            Utils.LongToBytes(this.responderSPI).CopyTo(ikeMessage, index);
            index += 8;
            ikeMessage[index] = (byte) this.nextPayload;
            ikeMessage[++index] = this.version;
            ikeMessage[++index] = (byte) this.exchangeType;
            ikeMessage[++index] = this.flags;
            index++;
            Utils.IntToBytes(this.messageID).CopyTo(ikeMessage, index);
            index += 4;
            Utils.IntToBytes(this.length).CopyTo(ikeMessage, index);
            index += 4;

            foreach (PAYLOAD_NODE_TYPE p in payloads)
            {
                switch (p.PayloadType)
                {
                    case IKE_PAYLOADS.SA:
                        IKE_SA_Payload saPayload = (IKE_SA_Payload)p.Payload;
                        Utils.MemCpy(saPayload.ToBytes(), 0, ref ikeMessage, index, saPayload.Length);
                        index += saPayload.Length;
                        break;
                    case IKE_PAYLOADS.KE:
                        IKE_KE_Payload kePayload = (IKE_KE_Payload)p.Payload;
                        Utils.MemCpy(kePayload.ToBytes(), 0, ref ikeMessage, index, kePayload.Length);
                        index += kePayload.Length;
                        break;
                    case IKE_PAYLOADS.N:
                        IKE_Nonce_Payload noncePayload = (IKE_Nonce_Payload)p.Payload;
                        Utils.MemCpy(noncePayload.ToBytes(), 0, ref ikeMessage, index, noncePayload.Length);
                        index += noncePayload.Length;
                        break;
                    case IKE_PAYLOADS.NOTI:
                        IKE_Notify_Payload notifyPayload = (IKE_Notify_Payload)p.Payload;
                        Utils.MemCpy(notifyPayload.ToBytes(), 0, ref ikeMessage, index, notifyPayload.Length);
                        index += notifyPayload.Length;
                        break;
                    case IKE_PAYLOADS.ENCR:
                        IKE_Encrypted_Payload encrPayload = (IKE_Encrypted_Payload)p.Payload;
                        Utils.MemCpy(encrPayload.ToBytes(), 0, ref ikeMessage, index, encrPayload.Length);
                        index += encrPayload.Length;
                        break;
                }
            }

            return ikeMessage;
        }
        public static IKE TryParse(byte[] RawIKEMessage)
        {
            IKE ikeMessage = new IKE();
            byte[] b = new byte[8];
            try{
                int index = 0;
                int ikeMsgLength = 28; //We compare what we received and what we calculate
                Utils.MemCpy(RawIKEMessage, 0, ref b, 0, 8);
                ikeMessage.initiatorSPI = Utils.BytesToLong(b);
                index += 8;
                Utils.MemCpy(RawIKEMessage, index, ref b, 0, 8);
                ikeMessage.responderSPI = Utils.BytesToLong(b);
                index += 8;
                ikeMessage.nextPayload = (IKE_PAYLOADS) RawIKEMessage[index];
                ikeMessage.version = RawIKEMessage[++index];
                ikeMessage.exchangeType = (IKE_EXCHANGE_TYPES) RawIKEMessage[++index];
                ikeMessage.flags = RawIKEMessage[++index];
                index++;
                for (int i = 0; i < 4; i++)
                    b[i] = RawIKEMessage[index + i];
                index += 4;
                ikeMessage.messageID = Utils.BytesToInt(b);
                for (int i = 0; i < 4; i++)
                    b[i] = RawIKEMessage[index + i];
                index += 4;
                ikeMessage.length = ikeMsgLength;
                ikeMsgLength = Utils.BytesToInt(b);

                IKE_PAYLOADS nextPayload = ikeMessage.nextPayload;
                while (nextPayload != IKE_PAYLOADS.NONE)
                {
                    switch (nextPayload)
                    {
                        case IKE_PAYLOADS.SA: //Parse SA payload
                            IKE_SA_Payload saPayload = IKE_SA_Payload.TryParse(RawIKEMessage, index);
                            index += saPayload.Length;
                            nextPayload = saPayload.NextPayload;
                            ikeMessage.AddPayload(saPayload, IKE_PAYLOADS.SA, saPayload.Length);
                            break;
                        case IKE_PAYLOADS.KE: //Parse KE paylaod
                            IKE_KE_Payload kePayload = IKE_KE_Payload.TryParse(RawIKEMessage, index);
                            index += kePayload.Length;
                            nextPayload = kePayload.NextPayload;
                            ikeMessage.AddPayload(kePayload, IKE_PAYLOADS.KE, kePayload.Length);
                            break;
                        case IKE_PAYLOADS.N: //Parse Nonce payload
                            IKE_Nonce_Payload noncePayload = IKE_Nonce_Payload.TryParse(RawIKEMessage, index);
                            index += noncePayload.Length;
                            nextPayload = noncePayload.NextPayload;
                            ikeMessage.AddPayload(noncePayload, IKE_PAYLOADS.N, noncePayload.Length);
                            break;
                        case IKE_PAYLOADS.ENCR: //Parse Encrypted payload
                            SA sa = SA.GetSA(ikeMessage.flags == 0x08 ? ikeMessage.ResponderSPI : ikeMessage.InitiatorSPI);
                            if (sa != null)
                            {
                                if (sa.IsMACValid(RawIKEMessage))
                                {
                                    IKE_Encrypted_Payload encryptedPayload = IKE_Encrypted_Payload.TryParse(RawIKEMessage, index);
                                    if (sa.ProcessEncryptedPayload(ref encryptedPayload))
                                    {
                                        ikeMessage.AddPayload(encryptedPayload, IKE_PAYLOADS.ENCR, encryptedPayload.Length);
                                        index += encryptedPayload.Length;
                                    }
                                }
                                else
                                    throw new ArgumentException("Invalid MAC! Message was dropped.");
                            }
                            nextPayload = IKE_PAYLOADS.NONE;
                            break;
                        default:
                            throw new ArgumentException("Unknown payload.");
                    }
                }
            }
            catch 
            {
                throw;
            }
            return ikeMessage;
        }

       
    }

}