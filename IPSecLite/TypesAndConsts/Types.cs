using System;
using System.Net;
using System.Net.Sockets;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.Utilities;
namespace adabtek.IPsecLite.Types
{
    public class ICMPPacket
    {

        static short icmpIdentifier = 0;
        static short icmpSequenceNumber = 0;

        ICMP_TYPE type;
        byte code;
        short checksum;
        short identifier;
        short sequenceNumber;
        char[] data;

        public ICMPPacket(ICMP_TYPE Type, string Data)
        {
            this.type = Type;
            this.code = 0;
            this.checksum = 0;
            this.identifier = ++icmpIdentifier;
            this.sequenceNumber = ++icmpSequenceNumber;
            data = new char[Data.Length];
            for (int i = 0; i < Data.Length; i++)
                data[i] = Data[i];
            this.checksum = Utils.Checksum(this.ToBytes());
        }
        public ICMPPacket(ICMP_TYPE Type, byte Code)
        {
            this.type = Type;
            this.code = Code;
        }

        public ICMP_TYPE Type 
        {
            set { type = value; }
            get { return type; } 
        }
        public byte Code
        {
            set { code = value; }
            get { return code; }
        }
        public short Checksum
        {
            set { checksum = value; }
            get { return checksum; }
        }
        public short Identifier
        {
            set { identifier = value; }
            get { return identifier; }
        }
        public short SequenceNumber
        {
            set { sequenceNumber = value; }
            get { return sequenceNumber; }
        }
        public string Data
        {
            set
            {
                data = new char[value.Length];
                for (int i = 0; i < value.Length; i++)
                    data[i] = value[i];
            }
            get
            {
                string icmpData = "";
                for (int i = 0; i < this.data.Length; i++)
                    icmpData += data[i];
                return icmpData;
            }
        }
        public byte[] ToBytes()
        {
            byte[] icmp = new byte[data.Length + 8];

            int index = 0;
            icmp[index++] = (byte)this.type;
            icmp[index++] = this.code;
            icmp[index++] = (byte)(this.checksum >> 8);
            icmp[index++] = (byte)(this.checksum & 0x00ff);
            icmp[index++] = (byte)(this.identifier >> 8);
            icmp[index++] = (byte)(this.identifier & 0x00ff);
            icmp[index++] = (byte)(this.sequenceNumber >> 8);
            icmp[index++] = (byte)(this.sequenceNumber & 0x00ff);
            for (int i = 0; i < this.data.Length; i++)
                icmp[index++] = (byte)this.data[i];

            return icmp;
        }

    }
    public class UserDatagramProtocolPacket
    {
        short sourcePort;
        short destinationPort;
        short messageLength;
        short checksum;
        byte[] payload; 

        public short SourcePort 
        {
            set { sourcePort = value; }
            get { return sourcePort; } 
        }
        public short DestinationPort 
        {
            set { destinationPort = value; }
            get { return destinationPort; } 
        }
        public short MessageLength 
        {
            set { messageLength = value; }
            get { return messageLength; } 
        }
        public short Checksum 
        {
            set { checksum = value; }
            get { return checksum; } 
        }
        public byte[] Payload 
        {
            set { payload = value; }
            get { return payload; } 
        }

        public UserDatagramProtocolPacket()
        {
        }

        public UserDatagramProtocolPacket(short SourcePort, short DestinationPort, byte[] Payload)
        {
            this.sourcePort = SourcePort;
            this.destinationPort = DestinationPort;
            this.messageLength = 8;
            if (Payload != null)
            {
                this.payload = Payload;
                this.messageLength += (short)Payload.Length;
            }
            this.checksum = 0;
            this.checksum = Utils.Checksum(this.ToBytes());
        }

        public byte[] ToBytes()
        {
            byte[] packet;
            packet = new byte[messageLength];

            int index = 0;
            int i;

            packet[index] = (byte)((sourcePort >> 8) & 0xff);
            packet[++index] = (byte)(sourcePort & 0xff);
            packet[++index] = (byte)((destinationPort >> 8) & 0xff);
            packet[++index] = (byte)(destinationPort & 0xff);
            packet[++index] = (byte)((messageLength >> 8) & 0xff);
            packet[++index] = (byte)(messageLength & 0xff);
            packet[++index] = (byte)((checksum >> 8) & 0xff);
            packet[++index] = (byte)(checksum & 0x0f);
            if (payload != null)
                for (i = 0; i < payload.Length; i++)
                    packet[++index] = payload[i];

            return packet;
        }

    }
    public class IPDatagram
    {
        static short ipID = 0;

        byte version4_hLen4;
        byte serviceType;
        short totalLength;
        short identification;
        short flags4_FragmentOffset12;
        byte timeToLive;
        PROTOCOLS protocol;
        short headerChecksum;
        byte[] sourceIP;
        byte[] destinationIP;
        byte[] payload;

        public byte Version 
        {
            set 
            {
                if ((value & 0xf0) > 0)
                    throw new OverflowException("Invalid IP version.");

                version4_hLen4 = (byte)((value << 4) + (version4_hLen4 & 0x0f)); 
            }
            get { return (byte)(version4_hLen4 >> 4); } 
        }
        public byte HLen 
        {
            set 
            {
                if ((value & 0xf0) > 0)
                    throw new OverflowException("Invalid IP HLen.");

                version4_hLen4 = (byte)((version4_hLen4 & 0xf0) + value); 
            }
            get { return (byte)(version4_hLen4 & 0x0f); } 
        }
        public byte ServiceType 
        {
            set { serviceType = value;}
            get { return serviceType; } 
        }
        public short TotalLength 
        {
            set { totalLength = value; }
            get { return totalLength; } 
        }
        public short Identification 
        {
            set { identification = value; }
            get { return identification; } 
        }
        public byte Flags 
        {
            set 
            {
                if ((value & 0xf0) > 0)
                    throw new OverflowException("Invalid IP flags.");

                flags4_FragmentOffset12 = (short) (flags4_FragmentOffset12 & 0x0fff + value); 
            }
            get { return (byte)(flags4_FragmentOffset12 >> 12); } 
        }
        public short FragmentOffest 
        {
            set 
            {
                if ((value & 0xf000) > 0)
                    throw new OverflowException("Invalid IP flags.");

                flags4_FragmentOffset12 = (short)(flags4_FragmentOffset12 & 0xf000 + value);
            }
            get { return (short)(flags4_FragmentOffset12 & 0x0fff); } 
        }
        public byte TimeToLive 
        {
            set { timeToLive = value; }
            get { return timeToLive; } 
        }
        public PROTOCOLS Protocol 
        {
            set { protocol = value; }
            get { return protocol; } 
        }
        public short HeaderChecksum 
        {
            set { headerChecksum = value; }
            get { return headerChecksum; } 
        }
        public byte[] SourceIP 
        {
            set { sourceIP = value; }
            get { return this.sourceIP; } 
        }
        public byte[] DestinationIP 
        {
            set { destinationIP = value; }
            get { return this.destinationIP; } 
        }
        public byte[] Payload 
        {
            set { payload = value; }
            get { return payload; } 
        }
        public void UpdatePayload(int ByteIndex, byte NewValue)
        {
            this.payload[ByteIndex] = NewValue;
        }

        private void SetIPHeader()
        {
            this.Version = 4;
            this.HLen = 5;
            this.ServiceType = 0x0;
            this.Identification = ++ipID;
            this.Flags = 0;
            this.FragmentOffest = 0;
            this.TimeToLive = 128;
            this.HeaderChecksum = 0;
            this.TotalLength = 20;

        }
        public IPDatagram()
        {
        }
        short checksum()
        {
            int index = 0;
            byte[] headerBytes = new byte[20];
            headerBytes[index] = version4_hLen4;
            headerBytes[++index] = serviceType;
            headerBytes[++index] = (byte)((totalLength >> 8) & 0xff);
            headerBytes[++index] = (byte)(totalLength & 0xff);
            headerBytes[++index] = (byte)((identification >> 8) & 0xff);
            headerBytes[++index] = (byte)(identification & 0xff);
            headerBytes[++index] = (byte)((flags4_FragmentOffset12 >> 8) & 0xff);
            headerBytes[++index] = (byte)(flags4_FragmentOffset12 & 0x00ff);
            headerBytes[++index] = timeToLive;
            headerBytes[++index] = (byte)protocol;
            headerBytes[++index] = (byte)((headerChecksum >> 8) & 0xff);
            headerBytes[++index] = (byte)(headerChecksum & 0x0f);
            headerBytes[++index] = sourceIP[0];
            headerBytes[++index] = sourceIP[1];
            headerBytes[++index] = sourceIP[2];
            headerBytes[++index] = sourceIP[3];
            headerBytes[++index] = destinationIP[0];
            headerBytes[++index] = destinationIP[1];
            headerBytes[++index] = destinationIP[2];
            headerBytes[++index] = destinationIP[3];
            return Utils.Checksum(headerBytes);
        }
        public IPDatagram(byte[] SourceIP, byte[] DestinationIP, PROTOCOLS Protocol, byte[] Payload)
        {
            this.SetIPHeader();

            this.sourceIP = SourceIP;
            this.destinationIP = DestinationIP;
            this.protocol = Protocol;
            this.payload = Payload;
            this.totalLength += (short)Payload.Length;
            this.headerChecksum = checksum();
        }
        public IPDatagram(byte[] SourceIP, byte[] DestinationIP, ICMPPacket ICMPPacket)
        {
            SetIPHeader();

            this.sourceIP = SourceIP;
            this.destinationIP = DestinationIP;
            this.protocol = PROTOCOLS.ICMP;
            this.payload = ICMPPacket.ToBytes();
            this.totalLength += (short)this.payload.Length;
            this.headerChecksum = checksum();
        }

        public string SourceIPToString
        {
            get { return sourceIP[0].ToString().PadLeft(3, '0') + "." + sourceIP[1].ToString().PadLeft(3, '0') + "." + sourceIP[2].ToString().PadLeft(3, '0') + "." + sourceIP[3].ToString().PadLeft(3, '0'); }
        }
        public string DestinationIPToString
        {
            get { return destinationIP[0].ToString().PadLeft(3, '0') + "." + destinationIP[1].ToString().PadLeft(3, '0') + "." + destinationIP[2].ToString().PadLeft(3, '0') + "." + destinationIP[3].ToString().PadLeft(3, '0'); }
        }

        public byte[] ToBytes()
        {
            byte[] Datagram = new byte[TotalLength];

            int index = 0;
            int i;

            Datagram[index] = version4_hLen4;
            Datagram[++index] = serviceType;
            Datagram[++index] = (byte)((totalLength >> 8) & 0xff);
            Datagram[++index] = (byte)(totalLength & 0xff);
            Datagram[++index] = (byte)((identification >> 8) & 0xff);
            Datagram[++index] = (byte)(identification & 0xff);
            Datagram[++index] = (byte)((flags4_FragmentOffset12 >> 8) & 0xff);
            Datagram[++index] = (byte)(flags4_FragmentOffset12 & 0x00ff);
            Datagram[++index] = timeToLive;
            Datagram[++index] = (byte)protocol;
            Datagram[++index] = (byte)((headerChecksum >> 8) & 0xff);
            Datagram[++index] = (byte)(headerChecksum & 0x0f);
            Datagram[++index] = sourceIP[0];
            Datagram[++index] = sourceIP[1];
            Datagram[++index] = sourceIP[2];
            Datagram[++index] = sourceIP[3];
            Datagram[++index] = destinationIP[0];
            Datagram[++index] = destinationIP[1];
            Datagram[++index] = destinationIP[2];
            Datagram[++index] = destinationIP[3];
            if (payload != null)
                for (i = 0; i < payload.Length; i++)
                    Datagram[++index] = payload[i];

            return Datagram;
        }
        




    }
}