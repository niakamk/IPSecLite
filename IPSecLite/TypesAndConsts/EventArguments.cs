using System;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.IKEv2;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite.Utilities;
namespace adabtek.IPsecLite.EventArguments
{
    #region PROTECTED
    public class ProtectedPacketTrafficEventArgs : EventArgs
    {
        string saKey;
        string sourceIP;
        string destinationIP;
        int sequenceNumber;
        byte payloadLength;
        DateTime arrivalTime;
        DateTime processCompleteTime = DateTime.Now;
        PROTECTION_RESULTS protectionResult;
        byte[] iv;
        byte[] encrypted;
        byte[] mac;
        int macLength;
        byte paddingLength;
        PROTOCOLS nextProtocol;
        long processingCycles;
        double processingTime;
        long encryptionCycles;
        double encryptionTime;
        long integrityCheckCycles;
        double integrityCheckTime;

        public string SAKey
        {
            set { this.saKey = value; }
            get { return this.saKey; }
        }
        public string SourceIP
        {
            set { this.sourceIP = value; }
            get { return this.sourceIP; }
        }
        public string DestinationIP
        {
            set { this.destinationIP = value; }
            get { return this.destinationIP; }

        }
        public int SPI
        {
            get
            {
                if (SAKey.IndexOf(":") > 0)
                    return int.Parse(SAKey.Substring(SAKey.IndexOf(":") + 1));
                else
                    return 0;
            }
        }
        public byte PayloadLength
        {
            set { this.payloadLength = value; }
            get { return this.payloadLength; }
        }

        public PROTECTION_RESULTS ProtectionResult
        {
            set { this.protectionResult = value; }
            get { return this.protectionResult; }
        }
        public int SequenceNumber 
        { 
            set { this.sequenceNumber = value; }
            get { return this.sequenceNumber; } 
        }
        public byte[] IV
        {
            set { this.iv = value; }
            get { return this.iv; }
        }
        public int IVLength
        {
            get 
            {
                if (this.iv != null)
                    return this.iv.Length;
                else
                    return 0;
            }
        }
        public byte[] Encrypted
        {
            set { this.encrypted = value; }
            get { return this.encrypted; }
        }
        public int EncryptedLength
        {
            get
            {
                if (this.encrypted != null)
                    return this.encrypted.Length;
                else
                    return 0;
            }

        }
        public byte[] MAC
        {
            set { this.mac = value; }
            get { return this.mac; }
        }
        public int MACLength
        {
            set { this.macLength = value; }
            get
            {
                if (this.MAC != null)
                    return this.MAC.Length;
                else
                    return 0;
            }
        }
        public DateTime ArrivalTime
        {
            set { this.arrivalTime = value; }
            get { return this.arrivalTime; }
        }
        public DateTime ProcessCompleteTime
        {
            get { return this.processCompleteTime; }
        }
        public byte PaddingLength
        {
            set { this.paddingLength = value; }
            get { return this.paddingLength; }
        }
        public PROTOCOLS NextProtocol
        {
            set { this.nextProtocol = value; }
            get { return this.nextProtocol; }
        }
        public long ProcessingCycles
        {
            set { this.processingCycles = value; }
            get { return this.processingCycles; }
        }
        public double ProcessingTime
        {
            set { this.processingTime = value; }
            get { return this.processingTime; }
        }
        public long EncryptionCycles
        {
            set { this.encryptionCycles = value; }
            get { return this.encryptionCycles; }
        }
        public double EncryptionTime
        {
            set { this.encryptionTime = value; }
            get { return this.encryptionTime; }
        }
        public long IntegrityCheckCycles
        {
            set { this.integrityCheckCycles = value; }
            get { return this.integrityCheckCycles; }
        }
        public double IntegrityCheckTime
        {
            set { this.integrityCheckTime = value; }
            get { return this.integrityCheckTime; }
        }
    }
    public class ProtectedPacketArrivedEventArgs : ProtectedPacketTrafficEventArgs
    {
        public ProtectedPacketArrivedEventArgs(string SAKey, byte[] SourceIP, byte[] DestinationIP, DateTime ArrivalTime, PROTECTION_RESULTS ProtectionResult, long ProcessingCycles, double ProcessingTime, long DecryptionCycles, double DecryptionTime, long IntegrityCheckCycles, double IntegrityCheckTime, int SequenceNumber, byte[] IV, byte[] Encrypted, byte PaddingLength, PROTOCOLS NextProtocol, byte[] MAC, short MacLength)
        {
            base.SAKey = SAKey;
            base.SourceIP = Utils.ToShortStringIP(SourceIP);
            base.DestinationIP = Utils.ToShortStringIP(DestinationIP);
            base.ProtectionResult = ProtectionResult;
            base.ArrivalTime = ArrivalTime;
            base.ProcessingCycles = ProcessingCycles;
            base.ProcessingTime = ProcessingTime;
            base.EncryptionCycles = DecryptionCycles;
            base.EncryptionTime = DecryptionTime;
            base.IntegrityCheckCycles = IntegrityCheckCycles;
            base.IntegrityCheckTime = IntegrityCheckTime;
            base.SequenceNumber = SequenceNumber;
            base.IV = IV;
            base.Encrypted = Encrypted;
            base.MAC = MAC;
            base.MACLength = MacLength;
            base.PaddingLength = PaddingLength;
            base.NextProtocol = NextProtocol;
        }
        public ProtectedPacketArrivedEventArgs(string SAKey, byte[] SourceIP, byte[] DestinationIP, DateTime ArrivalTime, PROTECTION_RESULTS ProtectionResult, long ProcessingCycles, double ProcessingTime, long DecryptionCycles, double DecryptionTime, long IntegrityCheckCycles, double IntegrityCheckTime, int SequenceNumber)
        {
            base.SAKey = SAKey;
            base.SourceIP = Utils.ToShortStringIP(SourceIP);
            base.DestinationIP = Utils.ToShortStringIP(DestinationIP);
            base.ProtectionResult = ProtectionResult;
            base.ArrivalTime = ArrivalTime;
            base.ProcessingCycles = ProcessingCycles;
            base.ProcessingTime = ProcessingTime;
            base.EncryptionCycles = DecryptionCycles;
            base.EncryptionTime = DecryptionTime;
            base.IntegrityCheckCycles = IntegrityCheckCycles;
            base.IntegrityCheckTime = IntegrityCheckTime;
            base.SequenceNumber = SequenceNumber;
        }
    }
    public class ProtectedPacketSentEventArgs : ProtectedPacketTrafficEventArgs
    {
        public ProtectedPacketSentEventArgs(string SAKey, byte[] SourceIP, byte[] DestinationIP, DateTime ArrivalTime, PROTECTION_RESULTS ProtectionResult, long ProcessingCycles, double ProcessingTime, long DecryptionCycles, double DecryptionTime, long IntegrityCheckCycles, double IntegrityCheckTime, int SequenceNumber, byte[] IV, byte[] Encrypted, byte PaddingLength, PROTOCOLS NextProtocol, byte[] MAC, short MacLength)
        {
            base.SAKey = SAKey;
            base.SourceIP = Utils.ToShortStringIP(SourceIP);
            base.DestinationIP = Utils.ToShortStringIP(DestinationIP);
            base.ProtectionResult = ProtectionResult;
            base.ArrivalTime = ArrivalTime;
            base.ProcessingCycles = ProcessingCycles;
            base.ProcessingTime = ProcessingTime;
            base.EncryptionCycles = DecryptionCycles;
            base.EncryptionTime = DecryptionCycles;
            base.IntegrityCheckCycles = IntegrityCheckCycles;
            base.IntegrityCheckTime = IntegrityCheckTime;
            base.SequenceNumber = SequenceNumber;
            base.IV = IV;
            base.Encrypted = Encrypted;
            base.MAC = MAC;
            base.MACLength = MacLength;
            base.PaddingLength = PaddingLength;
            base.NextProtocol = NextProtocol;
        }
    }

    #endregion
    #region SAs
    public class ChildSACreatedEventArgs : EventArgs
    {
        string childSAKey;
        long ikeSASPI;
        string sourceIP;
        string destinationIP;

        public long IKESASPI
        {
            get { return this.ikeSASPI; }
        }
        public string SourceIP
        {
            get { return this.sourceIP; }
        }
        public string DestinationIP
        {
            get { return this.destinationIP; }
        }
        public string ChildSAKey
        {
            get { return this.childSAKey; }
        }

        public ChildSACreatedEventArgs(long IKESASPI, string ChildSAKey, byte[] SourceIP, byte[] DestinationIP)
        {
            this.ikeSASPI = IKESASPI;
            this.childSAKey = ChildSAKey;
            this.sourceIP = Utils.ToShortStringIP(SourceIP);
            this.destinationIP = Utils.ToShortStringIP(DestinationIP);
        }
    }
    public class ChildSADeletedEventArgs : EventArgs
    {
        long ikeSASPI;
        string childSAKey;

        public long IKESASPI
        {
            get { return this.ikeSASPI; }
        }
        public string ChildSAKey
        {
            get { return this.childSAKey; }
        }

        public ChildSADeletedEventArgs(long IKESASPI, string ChildSAKey)
        {
            this.ikeSASPI = IKESASPI;
            this.childSAKey = ChildSAKey;
        }
    }
    #endregion
    #region IKEMessages
    public class IKEMessageTrafficEventArgs : EventArgs
    {
        IKE ikeMessage;
        byte[] sourceIP;
        byte[] destinationIP;
        DateTime trafficTime = DateTime.Now;

        public IKE IKEMessage
        {
            set { this.ikeMessage = value; }
            get { return this.ikeMessage; }
        }
        public byte[] SourceIP
        {
            set { this.sourceIP = value; }
            get { return this.sourceIP; }
        }
        public byte[] DestinationIP
        {
            set { this.destinationIP = value; }
            get { return this.destinationIP; }
        }
        public DateTime Time
        {
            get { return this.trafficTime;}
        }
    }
    public class IKEMessageArrivedEventArgs : IKEMessageTrafficEventArgs
    {
        public IKEMessageArrivedEventArgs(IKE IKEMessage, byte[] SourceIP, byte[] DestinationIP)
        {
            base.IKEMessage = IKEMessage;
            base.SourceIP = SourceIP;
            base.DestinationIP = DestinationIP;
        }
    }
    public class IKEMessageSentEventArgs : IKEMessageTrafficEventArgs
    {
        public IKEMessageSentEventArgs(IKE IKEMessage, byte[] SourceIP, byte[] DestinationIP)
        {
            base.IKEMessage = IKEMessage;
            base.SourceIP = SourceIP;
            base.DestinationIP = DestinationIP;
        }
    }
    public class IKEMessageProcessingEventArgs : EventArgs
    {
        DateTime time;
        string ikeMessageType;

        public IKEMessageProcessingEventArgs()
        {
            this.time = DateTime.Now;
        }
        public DateTime Time
        {
            get { return this.time; }
        }
        public string IKEMessageType
        {
            set { this.ikeMessageType = value; }
            get { return ikeMessageType; }
        }
    }
    public class IKEMessageProcessingStartedEventArgs : IKEMessageProcessingEventArgs
    {
        public IKEMessageProcessingStartedEventArgs(string IKEMessageType)
        {
            base.IKEMessageType = IKEMessageType;
        }
    }
    public class IKEMessageProcessingFinishedEventArgs : IKEMessageProcessingEventArgs
    {
        public IKEMessageProcessingFinishedEventArgs(string IKEMessageType)
        {
            base.IKEMessageType = IKEMessageType;
        }
    }
    public class IKESAEstablishedEventArgs : EventArgs
    {
        bool isInitiatorSA;
        long initiatorSPI;
        long responderSPI;
        byte[] initiatorIP;
        byte[] responderIP;
        DateTime time;

        public IKESAEstablishedEventArgs(bool IsInitiatorSA, long InitiatorSPI, long ResponderSPI, byte[] InitiatorIP, byte[] ResponderIP)
        {
            time = DateTime.Now;
            this.isInitiatorSA = IsInitiatorSA;
            this.initiatorSPI = InitiatorSPI;
            this.responderSPI = ResponderSPI;
            this.initiatorIP = InitiatorIP;
            this.responderIP = ResponderIP;
        }
        public bool IsInitiatorSA
        {
            get { return this.isInitiatorSA; }
        }
        public long InitiatorSPI
        {
            get { return this.initiatorSPI; }
        }
        public long ResponderSPI
        {
            get { return this.responderSPI; }
        }
        public byte[] InitiatorIP
        {
            get { return this.initiatorIP; }
        }
        public byte[] ResponderIP
        {
            get { return this.responderIP; }
        }
        public DateTime Time
        {
            get { return this.time; }
        }
    }
    #endregion 
    #region UDP
    public class UDPPacketTrafficEventArgs : EventArgs
    {
        UserDatagramProtocolPacket udpPacket;
        byte[] sourceIP;
        byte[] destinationIP;
        DateTime trafficTime = DateTime.Now;

        public UserDatagramProtocolPacket UDPPacket
        {
            set { this.udpPacket = value; }
            get { return this.udpPacket; }
        }
        public byte[] SourceIP
        {
            set { this.sourceIP = value; }
            get { return this.sourceIP; }
        }
        public byte[] DestinationIP
        {
            set { this.destinationIP = value; }
            get { return this.destinationIP; }
        }
        public DateTime Time
        {
            get { return this.trafficTime; }
        }
     }
    public class UDPPacketArrivedEventArgs : UDPPacketTrafficEventArgs
    {
        public UDPPacketArrivedEventArgs(UserDatagramProtocolPacket UDPPacket, byte[] SourceIP, byte[] DestinationIP)
        {
            base.SourceIP = SourceIP;
            base.DestinationIP = DestinationIP;
            base.UDPPacket = UDPPacket;
        }
    }
    public class UDPPacketSentEventArgs : UDPPacketTrafficEventArgs
    {
        public UDPPacketSentEventArgs(UserDatagramProtocolPacket UDPPacket, byte[] SourceIP, byte[] DestinationIP)
        {
            base.SourceIP = SourceIP;
            base.DestinationIP = DestinationIP;
            base.UDPPacket = UDPPacket;
        }
    }
    #endregion
    #region
    public class IPDatagramTrafficEventArgs : EventArgs
    {
        IPDatagram ipDatagram;
        DateTime trafficTime = DateTime.Now;
        byte[] sentTo;

        public byte[] SentTo
        {
            set { this.sentTo = value; }
            get { return this.sentTo; }
        }
        public IPDatagram IPDatagram
        {
            set { this.ipDatagram = value; }
            get { return this.ipDatagram; }
        }
        public DateTime Time
        {
            get { return this.trafficTime; }
        }
    }
    public class IPDatagramArrivedEventArgs : IPDatagramTrafficEventArgs
    {
        public IPDatagramArrivedEventArgs(IPDatagram IPDatagram, byte[] SentTo)
        {
            base.SentTo = SentTo;
            base.IPDatagram = IPDatagram;
        }
    }
    public class IPDatagramSentEventArgs : IPDatagramTrafficEventArgs
    {
        public IPDatagramSentEventArgs(IPDatagram IPDatagram, byte[] SentTo)
        {
            base.SentTo = SentTo;
            base.IPDatagram = IPDatagram;
        }
    }
    #endregion
    public class InvalidNetworkDataEventArgs : EventArgs
    {
        string message;
        PROTOCOLS protocol;
        byte[] networkData;
        public InvalidNetworkDataEventArgs(string Message, PROTOCOLS Protocol, byte[] NetworkData)
        {
            this.message = Message;
            this.protocol = Protocol;
            this.networkData = NetworkData;
        }
        public string Message
        {
            get { return this.message; }
        }
        public PROTOCOLS Protocol
        {
            get { return this.protocol; }
        }
        public byte[] NetworkData
        {
            get { return this.networkData; }
        }
    }

    public class RawNetworkDataEventArgs : EventArgs
    {
        byte[] networkData;
        DateTime trafficTime = DateTime.Now;
        public RawNetworkDataEventArgs(byte[] NetworkData)
        {
            this.networkData = NetworkData;
        }
        public byte[] NetworkData
        {
            get { return networkData; }
        }
        public DateTime Time
        {
            get { return this.trafficTime; }
        }
    }
    public class NetworkDataEventArgs : EventArgs
    {

        byte[] networkData;
        byte[] sourceIP;
        byte[] destinationIP;
        DateTime trafficTime = DateTime.Now;
        public NetworkDataEventArgs(byte[] NetworkData, byte[] SourceIP, byte[] DestinationIP)
        {
            this.networkData = NetworkData;
            this.sourceIP = SourceIP;
            this.destinationIP = DestinationIP;
        }
        public byte[] NetworkData
        {
            get { return networkData; }
        }
        public byte[] SourceIP
        {
            get { return this.sourceIP; }
        }
        public byte[] DestinationIP
        {
            get { return this.destinationIP; }
        }
        public DateTime Time
        {
            get { return this.trafficTime; }
        }
    }

    public class ICMPPacketTrafficEventArgs : EventArgs
    {
        ICMPPacket icmpPacket;
        byte[] sourceIP;
        byte[] destinationIP;
        DateTime trafficTime = DateTime.Now;

        public ICMPPacket ICMPPacket
        {
            set { this.icmpPacket = value; }
            get { return this.icmpPacket; }
        }
        public DateTime Time
        {
            get { return this.trafficTime; }
        }
        public byte[] SourceIP
        {
            set { this.sourceIP = value; }
            get { return this.sourceIP; }
        }
        public byte[] DestinationIP
        {
            set { this.destinationIP = value; }
            get { return this.destinationIP; }
        }
    }
    public class ICMPPacketArrivedEventArgs : ICMPPacketTrafficEventArgs
    {
        public ICMPPacketArrivedEventArgs(ICMPPacket ICMPPacket, byte[] SourceIP, byte[] DestinationIP)
        {
            base.ICMPPacket = ICMPPacket;
            base.SourceIP = SourceIP;
            base.DestinationIP = DestinationIP;
        }
    }
    public class ICMPPacketSentEventArgs : ICMPPacketTrafficEventArgs
    {
        public ICMPPacketSentEventArgs(ICMPPacket ICMPPacket, byte[] SourceIP, byte[] DestinationIP)
        {
            base.ICMPPacket = ICMPPacket;
            base.SourceIP = SourceIP;
            base.DestinationIP = DestinationIP;
        }
    }

  

}
