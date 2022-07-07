using System;
using System.Collections.Generic;
using System.Text;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.EventArguments;
namespace adabtek.IPsecLite.NetworkProtocols
{
    public class ICMP
    {
        public delegate void ICMPPacketArrivedHandler(object sender, ICMPPacketArrivedEventArgs e);
        public delegate void ICMPPacketSentHandler(object sender, ICMPPacketSentEventArgs e);
       // public event ICMPPacketArrivedHandler ICMPPacketArrived;
        public event ICMPPacketSentHandler ICMPPacketSent;

        public ICMP()
        {
            Program.IP.ICMPPacketArrived += new IP.ICMPRawPacketArrivedHandler(IP_ICMPPacketArrived);
        }

        void IP_ICMPPacketArrived(object sender, IPsecLite.EventArguments.NetworkDataEventArgs e)
        {
            //ICMPPacket icmpPacket = TryParse(e.NetworkData);
            //if (icmpPacket != null)
            //{
            //    if (ICMPPacketArrived != null)
            //        ICMPPacketArrived(this, new ICMPPacketArrivedEventArgs(icmpPacket, e.SourceIP, e.DestinationIP));

            //    if (Utils.AreSameIPs(Utils.IPToBytes(Utils.GetHostIPAddress()), e.DestinationIP))
            //        if (icmpPacket.Type == ICMP_TYPE.REQUEST)
            //        {
            //            ICMPPacket icmpResponse = new ICMPPacket(ICMP_TYPE.RESPONSE, icmpPacket.Data);
            //            Program.ICMP.Send(e.DestinationIP, e.SourceIP, icmpResponse);
            //        }
            //}
        }

        public void Send(byte[] SourceIP, byte[] Destination, ICMPPacket ICMPPacket)
        {
            IPDatagram ipDatagram = new IPDatagram(SourceIP, Destination, ICMPPacket);

            if (ICMPPacketSent != null)
               ICMPPacketSent(this, new ICMPPacketSentEventArgs(ICMPPacket, ipDatagram.SourceIP, ipDatagram.DestinationIP)); 

            Program.IP.Send(ipDatagram);
        }
        public static ICMPPacket TryParse(byte[] RawICMP)
        {
            ICMPPacket icmpPacket;
            try
            {
                 icmpPacket = new ICMPPacket((ICMP_TYPE)RawICMP[0], RawICMP[1]);
                icmpPacket.Checksum = Utils.BytesToShort(RawICMP[2], RawICMP[3]);
                icmpPacket.Identifier = Utils.BytesToShort(RawICMP[4], RawICMP[5]);
                icmpPacket.SequenceNumber = Utils.BytesToShort(RawICMP[6], RawICMP[7]);
                string data = "";
                for (int i = 8; i < RawICMP.Length; i++)
                    data += (char)RawICMP[i];
                icmpPacket.Data = data;
                return icmpPacket;
            }
            catch (Exception e)
            {
                icmpPacket = null;
                throw new Exception("Invalid raw ICMP data. " + e.Message);
            }

        }
    }

}
