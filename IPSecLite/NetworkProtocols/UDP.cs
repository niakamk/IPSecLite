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
    public class UDP
    {
        public delegate void UDPPacketSentHandler(object sender, UDPPacketSentEventArgs e);
        public delegate void UDPPacketArrivedHandler(object sender, UDPPacketArrivedEventArgs e);

        public delegate void IKEMessageArrivedHandler(object sender, NetworkDataEventArgs e);
        public delegate void IKEMessageSentHandler(object sender, NetworkDataEventArgs e);

        public event UDPPacketArrivedHandler UDPPacketArrived;
        public event UDPPacketSentHandler UDPPacketSent;

        public event IKEMessageArrivedHandler IKEMessageArrived;

        public UDP()
        {
            Program.IP.UDPPacketArrived += new IP.UPDPacketArrivedHandler(IP_UDPPacketArrived);
        }

        void IP_UDPPacketArrived(object sender, NetworkDataEventArgs e)
        {

            UserDatagramProtocolPacket udp = UDP.TryParse(e.NetworkData);
            if (udp != null)
                if (UDPPacketArrived != null)
                    UDPPacketArrived(this, new UDPPacketArrivedEventArgs(udp, e.SourceIP, e.DestinationIP));

            if (Utils.GetHostIPAddress() == Utils.ToShortStringIP(e.DestinationIP))
            {
                switch (udp.DestinationPort)
                {
                    case APP_CONFIG.ISKAMP: //an IKE message has arrived
                        if (IKEMessageArrived != null)
                            IKEMessageArrived(this, new NetworkDataEventArgs(udp.Payload, e.SourceIP, e.DestinationIP));
                        break;
                }
            }
        }


        public void Send(byte[] SourceIP, byte[] DestinationIP, UserDatagramProtocolPacket UDPPacket)
        {
            IPDatagram ipDatagram = new IPDatagram(SourceIP, DestinationIP, PROTOCOLS.UDP, UDPPacket.ToBytes());
            if (UDPPacketSent != null)
                UDPPacketSent(this, new UDPPacketSentEventArgs(UDPPacket, ipDatagram.SourceIP, ipDatagram.DestinationIP));
            Program.IP.Send(ipDatagram);
        }

        public static UserDatagramProtocolPacket TryParse(byte[] RawUDPPacket)
        {

            UserDatagramProtocolPacket udpPacket = new UserDatagramProtocolPacket();
            try
            {
                udpPacket.SourcePort = Utils.BytesToShort(RawUDPPacket[0], RawUDPPacket[1]);
                udpPacket.DestinationPort = Utils.BytesToShort(RawUDPPacket[2], RawUDPPacket[3]);
                udpPacket.MessageLength = Utils.BytesToShort(RawUDPPacket[4], RawUDPPacket[5]);
                udpPacket.Checksum = Utils.BytesToShort(RawUDPPacket[6], RawUDPPacket[7]);
                if (RawUDPPacket.Length > 8)
                {
                    udpPacket.Payload = new byte[RawUDPPacket.Length - 8];
                    for (int i = 0; i < RawUDPPacket.Length - 8; i++)
                        udpPacket.Payload[i] = RawUDPPacket[8 + i];
                }
                return udpPacket;
            }
            catch (Exception e)
            {
                udpPacket = null;
                throw new Exception("Invalid raw UDP data. " + e.Message);
            }
        }
    } 
}
