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
    public class AH
    {
        struct ProcessingCost
        {
            long integrityCheckCycles;
            double integrityCheckTime;

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

        PROTOCOLS nextProtocol;
        byte payloadLength;
        short reserved;
        int spi;
        int sequenceNumber;
        byte[] mac;
        IPDatagram input;

        bool isOutgoing;
        CHILD_SA_TYPE childSA;

        IKE_INTEG_ALGS integrityCheckAlg;
        byte[] integrityCheckAlgKey;
        short macLength;
        byte paddingLength;
        ProcessingCost processingCost;

        public IPDatagram GetProcessedData
        {
            get 
            {
                return this.input;
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
        public byte[] MAC
        {
            get 
            {
                return this.mac; 
            }
        }
        public short MACLength
        {
            get { return this.macLength; }
        }
        public short RESERVED
        {
            get { return this.reserved; }
        }
        public PROTOCOLS NextProtocol
        {
            get { return this.nextProtocol; }
        }
        public byte PaddingLength
        {
            get { return this.paddingLength; }
        }
        public long IntegrityCheckCycles
        {
            get { return this.processingCost.IntegrityCheckCycles; }
        }
        public double IntegrityCheckTime
        {
            get { return this.processingCost.IntegrityCheckTime; }
        }

        public AH(IPDatagram Input, CHILD_SA_TYPE ChildSA, bool IsOutgoing)
        {
            this.input = Input;
            this.isOutgoing = IsOutgoing;
            this.childSA = ChildSA;
            this.reserved = 0;
            if (this.isOutgoing)
            {
                this.spi = ChildSA.SPI;
                this.integrityCheckAlg = ChildSA.cryptoAlgs.IntegAlg;
                this.integrityCheckAlgKey = ChildSA.SK_a;
                this.sequenceNumber = ChildSA.GetNextSequenceNumber();
                this.nextProtocol = Input.Protocol;

                //Next Header, Payload Length, Reserved, Size of SPI, Sequence Number

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
            {
                byte[] t = new byte[4];
                this.nextProtocol = (PROTOCOLS)Input.Payload[0];
                this.payloadLength = Input.Payload[1];
                Utils.MemCpy(Input.Payload, 4, ref t, 0, 4);
                this.spi = Utils.BytesToInt(t);
                Utils.MemCpy(Input.Payload, 8, ref t, 0, 4);
                this.sequenceNumber = Utils.BytesToInt(t);
                this.integrityCheckAlg = ChildSA.cryptoAlgs.IntegAlg;
                this.integrityCheckAlgKey = ChildSA.SK_a;

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
                this.mac = new byte[this.macLength];
                Utils.MemCpy(Input.Payload, 12, ref this.mac, 0, this.macLength);

                byte[] newPayload = new byte[this.input.Payload.Length - (this.macLength + 12)];
                Utils.MemCpy(this.input.Payload, (this.macLength + 12), ref newPayload, 0, newPayload.Length);
                this.input.Payload = null;
                this.input.Payload = newPayload;
                this.input.TotalLength -= (short)(this.macLength + 12);
            }
        }

        private PROTECTION_RESULTS getMAC()
        {
            PROTECTION_RESULTS result;

            byte[] dataBytes = this.input.ToBytes();
            //Checksum set to zero
            dataBytes[10] = 0;
            dataBytes[11] = 0;
            try
            {
                HiPerfTimer timer = new HiPerfTimer();
                double executionTime = 0;

                switch (this.integrityCheckAlg)
                {

                    case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                        timer.Start();
                        this.mac = HMAC_SHA1.GetMAC(dataBytes, 0, dataBytes.Length, this.integrityCheckAlgKey, ref executionTime);
                        timer.Stop();
                        break;
                    case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                        timer.Start();
                        this.mac = HMAC_MD5.GetMAC(dataBytes, 0, dataBytes.Length, this.integrityCheckAlgKey, ref executionTime);
                        timer.Stop();
                        break;
                    case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                        timer.Start();
                        this.mac = AES_XCBC_MAC.GetMAC(dataBytes, 0, dataBytes.Length, this.integrityCheckAlgKey, ref executionTime);
                        timer.Stop();
                        break;
                }
                byte[] tempMac = new byte[this.macLength];
                Utils.MemCpy(this.mac, 0, ref tempMac, 0, this.macLength);
                this.mac = tempMac;
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

                byte[] newPayload = new byte[this.input.Payload.Length + 12];
                int index = 0;
                newPayload[index++] = (byte) this.nextProtocol;
                newPayload[index++] = this.payloadLength;
                newPayload[index++] = 0;
                newPayload[index++] = 0;
                Utils.IntToBytes(this.spi).CopyTo(newPayload, index);
                index+= 4;
                Utils.IntToBytes(this.sequenceNumber).CopyTo(newPayload, index);
                index+= 4;
                this.input.Payload.CopyTo(newPayload, index);

                this.input.TotalLength += 12;

                byte[] dataBytes = new byte[this.input.TotalLength];
                Utils.MemCpy(this.input.ToBytes(), 0, ref dataBytes, 0, this.input.HLen * 4);
                newPayload.CopyTo(dataBytes, this.input.HLen * 4);

                this.input.TotalLength += 12;

                double executionTime = 0;
                HiPerfTimer timer = new HiPerfTimer();
                timer.Start();

                byte[] newMac = null;

                dataBytes[10] = 0;
                dataBytes[11] = 0;

                switch (this.integrityCheckAlg)
                {
                    case IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96:
                        newMac = HMAC_SHA1.GetMAC(dataBytes, 0, dataBytes.Length, this.integrityCheckAlgKey, ref executionTime);
                        break;
                    case IKE_INTEG_ALGS.AUTH_HMAC_MD5_96:
                        newMac = HMAC_MD5.GetMAC(dataBytes, 0, dataBytes.Length, this.integrityCheckAlgKey, ref executionTime);
                        break;
                    case IKE_INTEG_ALGS.AUTH_AES_XCBC_96:
                        newMac = AES_XCBC_MAC.GetMAC(dataBytes, 0, dataBytes.Length, this.integrityCheckAlgKey, ref executionTime);
                        break;
                }
                for (int i = 0; i < this.macLength; i++)
                    if (this.mac[i] != newMac[i])
                    {
                        result = PROTECTION_RESULTS.INVALID_MAC;
                        break;
                    }

                if (this.childSA.Mode == IKE_MODE.TUNNEL)
                {
                    IPDatagram innerIPDatagaram = IP.TryParse(this.input.Payload);
                    this.input = innerIPDatagaram;
                }
                else
                    this.input.Protocol = this.nextProtocol;


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
        public PROTECTION_RESULTS Protect()
        {
            PROTECTION_RESULTS result = PROTECTION_RESULTS.NONE;

            if (this.input != null)
            {

                int index = 0;
                if (this.childSA.Mode == IKE_MODE.TRANSPORT)
                {

                    byte[] newPayload = new byte[this.input.Payload.Length + 12];
                    newPayload[index++] = (byte)this.nextProtocol;
                    newPayload[index++] = (byte)(this.input.Payload.Length / 32);
                    newPayload[index++] = 0;
                    newPayload[index++] = 0;
                    Utils.IntToBytes(this.spi).CopyTo(newPayload, index);
                    index += 4;
                    Utils.IntToBytes(this.sequenceNumber).CopyTo(newPayload, index);
                    index += 4;
                    this.input.Payload.CopyTo(newPayload, index);
                    this.input.Payload = null;
                    this.input.Payload = newPayload;
                    this.input.TotalLength += 12;

                    this.input.Protocol = PROTOCOLS.AH;
                }
                else
                {
                    byte[] sourceIP = Utils.IPToBytes(Utils.GetHostIPAddress());
                    IPDatagram newIPDatagram = new IPDatagram(sourceIP, this.childSA.PeerIP, PROTOCOLS.AH, this.input.ToBytes());
                    this.input = null;
                    this.input = newIPDatagram;

                    byte[] newPayload = new byte[this.input.Payload.Length + 12];
                    newPayload[index++] = (byte)this.nextProtocol;
                    newPayload[index++] = (byte)(this.input.Payload.Length / 32);
                    newPayload[index++] = 0;
                    newPayload[index++] = 0;
                    Utils.IntToBytes(this.spi).CopyTo(newPayload, index);
                    index += 4;
                    Utils.IntToBytes(this.sequenceNumber).CopyTo(newPayload, index);
                    index += 4;
                    this.input.Payload.CopyTo(newPayload, index);
                    this.input.Payload = null;
                    this.input.Payload = newPayload;
                    this.input.TotalLength += 12;

                }

                result = this.getMAC();

                if (result == PROTECTION_RESULTS.OK)
                {
                    byte[] newPayload = new byte[this.input.Payload.Length + this.macLength];
                    index = 0;
                    Utils.MemCpy(this.input.Payload, 0, ref newPayload, index, 12);
                    index += 12;
                    this.mac.CopyTo(newPayload, index);
                    index += this.macLength;
                    Utils.MemCpy(this.input.Payload, 12, ref newPayload, index, this.input.Payload.Length - 12);

                    this.input.Payload = null;
                    this.input.Payload = newPayload;
                    this.input.TotalLength += this.macLength;

                }
                return result;
            }
            else
                throw new ArgumentException("No data to protect.");
        }
        public PROTECTION_RESULTS Unprotect()
        {
            if (this.input != null)
                return this.checkMAC();
            else
                throw new ArgumentException("No data to unprotect.");

        }
    }
}