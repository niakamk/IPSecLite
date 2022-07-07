using System;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.SADB;
using adabtek.IPsecLite.SPDB;
using adabtek.IPsecLite.Timing;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.IKEv2;
namespace adabtek.IPsecLite.NetworkProtocols
{
    public class IP
    {
        public delegate void IPDatagramArrivedHandler(object sender, IPDatagramArrivedEventArgs e);
        public delegate void IPDatagramSentHandler(object sender, IPDatagramSentEventArgs e);
        public delegate void UPDPacketArrivedHandler(object sender, NetworkDataEventArgs e);
        public delegate void ICMPRawPacketArrivedHandler(object sender, NetworkDataEventArgs e);

        public event IPDatagramArrivedHandler IPDatagramArrived;
        public event IPDatagramSentHandler IPDatagramSent;
        public event UPDPacketArrivedHandler UDPPacketArrived;
        public event ICMPRawPacketArrivedHandler ICMPPacketArrived;

        public delegate void RawProtectedPacketHandler(object sender, NetworkDataEventArgs e);
        public delegate void ProtectedPacketArrivedHandler(object sender, ProtectedPacketArrivedEventArgs e);
        public delegate void ProtectedPacketSentHandler(object sender, ProtectedPacketSentEventArgs e);
        public event RawProtectedPacketHandler RawProtectedPacketArrived;
        public event ProtectedPacketArrivedHandler ProtectedPacketArrived;
        public event ProtectedPacketSentHandler ProtectedPacketSent;

        Random rnd = new Random();
        public IP()
        {
            Program.Ethernet.RawIPDatagramArrived += new Ethernet.RawIPDatagramHandler(Ethernet_IPDatagramArrived);

        }

        void Ethernet_IPDatagramArrived(object sender, RawNetworkDataEventArgs e)
        {
            bool mustRoute;
            byte[] newDestination;

            IPDatagram ipDatagram = IP.TryParse(e.NetworkData);
REPROCESS:
            if (ipDatagram == null)
                return;

            mustRoute = OnArriveMustRoute(ipDatagram.DestinationIP, out newDestination);

            if (IPDatagramArrived != null)
                IPDatagramArrived(null, new IPDatagramArrivedEventArgs(ipDatagram, (mustRoute ? newDestination : null)));

            if (mustRoute)
            {
                Program.IP.Send(ipDatagram, newDestination);
                return;
            }
            NetworkDataEventArgs networkData = new NetworkDataEventArgs(ipDatagram.Payload, ipDatagram.SourceIP, ipDatagram.DestinationIP);
            switch (ipDatagram.Protocol)
            {
                case PROTOCOLS.TCP:
                    break;
                case PROTOCOLS.UDP:
                    if (UDPPacketArrived != null)
                            UDPPacketArrived(this, networkData);
                    break;
                case PROTOCOLS.ESP:
                    {

                        DateTime arrivalTime = DateTime.Now;

                        if (RawProtectedPacketArrived != null)
                            RawProtectedPacketArrived(this, networkData);
                        HiPerfTimer timer = new HiPerfTimer();
                        timer.Start();

                        byte[] spiBytes = new byte[4];
                        byte[] snBytes = new byte[4];
                        Utils.MemCpy(ipDatagram.Payload, 0, ref spiBytes, 0, 4);
                        Utils.MemCpy(ipDatagram.Payload, 4, ref snBytes, 0, 4);

                        int sn = Utils.BytesToInt(snBytes);

                        string childSAKey = Utils.ToShortStringIP(ipDatagram.SourceIP) + ":" + Utils.BytesToInt(spiBytes).ToString();

                        CHILD_SA_TYPE childSA = SA.GetChildSA(childSAKey);
                        PROTECTION_RESULTS result = PROTECTION_RESULTS.NO_SA;

                        if (childSA != null)
                        {
                            result = childSA.IsSequenceNumberValid(sn);
                            if ( result == PROTECTION_RESULTS.OK)
                            {
                                int prevPayloadLength = ipDatagram.Payload.Length;
                                ESP esp = new ESP(ipDatagram, childSA, false);
                                result = esp.Unprotect();
                                if (result == PROTECTION_RESULTS.OK)
                                {

                                    ipDatagram = esp.GetProcessedData;

                                    timer.Stop();
                                    childSA.UpdateTrafficStatistics(ipDatagram.TotalLength, timer.Cycles, timer.Duration);
                                }
                                else
                                {
                                    childSA.RollbackReplayWindow();
                                    timer.Stop();
                                }
                                if (ProtectedPacketArrived != null)
                                    ProtectedPacketArrived(this, new ProtectedPacketArrivedEventArgs(childSA.ChildSAKey, ipDatagram.SourceIP, ipDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, esp.EncryptionCycles, esp.EncryptionTime, esp.IntegrityCheckCycles, esp.IntegrityCheckTime, esp.SequenceNumber, esp.IV, ipDatagram.Payload, esp.PaddingLength, ipDatagram.Protocol, esp.MAC, esp.MACLength));

                                if (result == PROTECTION_RESULTS.OK)
                                    goto REPROCESS;
                            }
                            else
                            {
                                timer.Stop();
                                if (ProtectedPacketArrived != null)
                                    ProtectedPacketArrived(this, new ProtectedPacketArrivedEventArgs(childSA.ChildSAKey, ipDatagram.SourceIP, ipDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration,0, 0, 0, 0, sn));
                            }
                        }else
                        {
                            timer.Stop();
                            if (ProtectedPacketArrived != null)
                                ProtectedPacketArrived(this, new ProtectedPacketArrivedEventArgs("N/A", ipDatagram.SourceIP, ipDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, 0, 0, 0, 0, sn));
                        }

                    }
                    break;
                case PROTOCOLS.AH:
                    {
                        DateTime arrivalTime = DateTime.Now;

                        if (RawProtectedPacketArrived != null)
                            RawProtectedPacketArrived(this, networkData);
                        HiPerfTimer timer = new HiPerfTimer();
                        timer.Start();

                        byte[] spiBytes = new byte[4];
                        byte[] snBytes = new byte[4];
                        int index = 4;
                        Utils.MemCpy(ipDatagram.Payload, index, ref spiBytes, 0, 4);
                        Utils.MemCpy(ipDatagram.Payload, index + 4, ref snBytes, 0, 4);

                        int sn = Utils.BytesToInt(snBytes);

                        string childSAKey = Utils.ToShortStringIP(ipDatagram.SourceIP) + ":" + Utils.BytesToInt(spiBytes).ToString();

                        CHILD_SA_TYPE childSA = SA.GetChildSA(childSAKey);
                        PROTECTION_RESULTS result = PROTECTION_RESULTS.NO_SA;

                        if (childSA != null)
                        {
                            result = childSA.IsSequenceNumberValid(sn);
                            if (result == PROTECTION_RESULTS.OK)
                            {
                                int prevPayloadLength = ipDatagram.Payload.Length;
                                AH ah = new AH(ipDatagram, childSA, false);
                                result = ah.Unprotect();
                                if (result == PROTECTION_RESULTS.OK)
                                {

                                    ipDatagram = null;
                                    ipDatagram = ah.GetProcessedData;
                                    byte paddingLength = ah.PaddingLength;

                                    timer.Stop();
                                    childSA.UpdateTrafficStatistics(ipDatagram.HLen * 4 + ipDatagram.Payload.Length - (paddingLength + 12 + ah.MACLength), timer.Cycles, timer.Duration);
                                }
                                else
                                {
                                    childSA.RollbackReplayWindow();
                                    timer.Stop();
                                }
                                if (ProtectedPacketArrived != null)
                                    ProtectedPacketArrived(this, new ProtectedPacketArrivedEventArgs(childSA.ChildSAKey, ipDatagram.SourceIP, ipDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, 0, 0, ah.IntegrityCheckCycles, ah.IntegrityCheckTime, ah.SequenceNumber, null, ah.GetProcessedData.ToBytes(), ah.PaddingLength, ah.NextProtocol, ah.MAC, ah.MACLength));

                                if (result == PROTECTION_RESULTS.OK)
                                    goto REPROCESS;
                            }
                            else
                            {
                                timer.Stop();
                                if (ProtectedPacketArrived != null)
                                    ProtectedPacketArrived(this, new ProtectedPacketArrivedEventArgs(childSA.ChildSAKey, ipDatagram.SourceIP, ipDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, 0, 0, 0, 0, sn));
                            }
                        }
                        else
                        {
                            timer.Stop();
                            if (ProtectedPacketArrived != null)
                                ProtectedPacketArrived(this, new ProtectedPacketArrivedEventArgs("N/A", ipDatagram.SourceIP, ipDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, 0, 0, 0, 0, sn));
                        }

                    }
                    break;
                case PROTOCOLS.ICMP:
                        if (ICMPPacketArrived != null)
                            ICMPPacketArrived(this, new NetworkDataEventArgs(ipDatagram.Payload, ipDatagram.SourceIP, ipDatagram.DestinationIP));

                    break;
                case PROTOCOLS.IP:
                    IPDatagram innerIPDatagram = IP.TryParse(ipDatagram.Payload);
                    Program.IP.Send(innerIPDatagram);
                    break;
            }
        }
        private bool OnArriveMustRoute(byte[] DestinationIP, out byte[] NewDestination)
        {
            byte[] ip = null;
            bool isMyHost = true;

            if (Utils.GetHostIPAddress() == Utils.ToShortStringIP(DestinationIP))
            {
                NewDestination = DestinationIP;
                return false;
            }
            else
            {
                //Check to see if destination is on our side
                foreach (byte[] host in APP_CONFIG.HOSTS)
                {
                    isMyHost = true;
                    for (int i = 0; i < 4; i++)
                        if (DestinationIP[i] != host[i])
                        {
                            isMyHost = false;
                            break;
                        }
                    if (isMyHost)
                        break;
                }
                if (isMyHost)
                    ip = DestinationIP;
                else
                    ip = APP_CONFIG.GATEWAY_IP;
            }

            NewDestination = ip;
            return true;
        }
        private bool OnSendMustRoute(byte[] DestinationIP, out byte[] NewDestination)
        {
            byte[] ip = null;
            bool isMyHost = true;

            if (APP_CONFIG.IS_GATEWAY)
            {
                //Check to see if destination is on our side
                foreach (byte[] host in APP_CONFIG.HOSTS)
                {
                    isMyHost = true;
                    for (int i = 0; i < 4; i++)
                        if (DestinationIP[i] != host[i])
                        {
                            isMyHost = false;
                            break;
                        }
                    if (isMyHost)
                        break;
                }
                if (isMyHost)
                    ip = DestinationIP;
                else
                    ip = APP_CONFIG.GATEWAY_IP;
            }
            else
                ip = APP_CONFIG.GATEWAY_IP;

            NewDestination = ip;

            return !isMyHost;
        }
        public void Router(IPDatagram IPDatagram)
        {
            byte[] newDestination;
            bool mustRoute = OnSendMustRoute(IPDatagram.DestinationIP, out newDestination);
            Program.Ethernet.Send(IPDatagram, newDestination);
            if (IPDatagramSent != null)
                IPDatagramSent(this, new IPDatagramSentEventArgs(IPDatagram, newDestination));

        }
        public void Send(IPDatagram IPDatagram, byte[] DestinationGatewayIP)
        {
            PROTECTION_RESULTS result = PROTECTION_RESULTS.NONE;
            bool shouldProtect = true;

            long saKey = SPD.GetSA(Utils.ToShortStringIP(DestinationGatewayIP));
            if (saKey == 0)
            {
                SA sa = SA.GetSAByIP(Utils.ToShortStringIP(DestinationGatewayIP));
                if (sa != null)
                    saKey = sa.SAKey;
                else
                    shouldProtect = false;
            }
            if (shouldProtect)
            {
                DateTime arrivalTime = DateTime.Now;
                HiPerfTimer timer = new HiPerfTimer();
                timer.Start();

                ESP esp = null;
                AH ah = null;

                CHILD_SA_TYPE childSA = SA.GetChildSA(saKey, true);

                if (childSA != null)
                {
                    int prevPayloadLength = IPDatagram.Payload.Length;
                    if (childSA.Protocol == IKE_PROTOCOLS.AH)
                    {
                        ah = new AH(IPDatagram, childSA, true);
                        result = ah.Protect();
                        if (result == PROTECTION_RESULTS.OK)
                            IPDatagram = ah.GetProcessedData;
                    }
                    else
                    {
                        esp = new ESP(IPDatagram, childSA, true);
                        result = esp.Protect();
                        if (result == PROTECTION_RESULTS.OK)
                            IPDatagram = esp.GetProcessedData;
                    }
                    timer.Stop();

                    childSA.UpdateTrafficStatistics(prevPayloadLength, timer.Cycles, timer.Duration);

                    if (childSA.Protocol == IKE_PROTOCOLS.AH)
                    {
                        if (ProtectedPacketSent != null)
                            ProtectedPacketSent(this, new ProtectedPacketSentEventArgs(childSA.ChildSAKey, IPDatagram.SourceIP, IPDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, 0, 0, ah.IntegrityCheckCycles, ah.IntegrityCheckTime, ah.SequenceNumber, null, ah.GetProcessedData.ToBytes(), ah.PaddingLength, ah.NextProtocol, ah.MAC, ah.MACLength));
                    }
                    else
                    {
                        if (ProtectedPacketSent != null)
                            ProtectedPacketSent(this, new ProtectedPacketSentEventArgs(childSA.ChildSAKey, IPDatagram.SourceIP, IPDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, esp.EncryptionCycles, esp.EncryptionTime, esp.IntegrityCheckCycles, esp.IntegrityCheckTime, esp.SequenceNumber, esp.IV, IPDatagram.Payload, esp.PaddingLength, esp.NextProtocol, esp.MAC, esp.MACLength));
                    }
                }
            }
            switch (result)
            {
                case PROTECTION_RESULTS.OK:
                    int r = rnd.Next(0, 100);
                    if (r >= APP_CONFIG.PACKET_LOSS_PERCENT)
                    {
                        r = rnd.Next(0, 100);
                        if (r < APP_CONFIG.CORRUPTED_PACKET_PERCENT)
                        {
                            r = rnd.Next(0, IPDatagram.Payload.Length);
                            IPDatagram.UpdatePayload(r, (byte)(r >> 1));
                        }
                        Router(IPDatagram);
                    }
                    break;
                case PROTECTION_RESULTS.NONE:
                    Router(IPDatagram);
                    break;
            }
        }

        public void Send(IPDatagram IPDatagram)
        {
            PROTECTION_RESULTS result = PROTECTION_RESULTS.NONE;
            bool shouldProtect = true;
            if (IPDatagram.Protocol == PROTOCOLS.UDP)
            {
                if ((Utils.BytesToShort(IPDatagram.Payload[2], IPDatagram.Payload[3])) == APP_CONFIG.ISKAMP)
                    shouldProtect = false;
            }
            if (shouldProtect)
            {
                long saKey = SPD.GetSA(Utils.ToShortStringIP(IPDatagram.DestinationIP));
                if (saKey == 0)
                {
                    SA sa = SA.GetSAByIP(Utils.ToShortStringIP(IPDatagram.DestinationIP));
                    if (sa != null)
                        saKey = sa.SAKey;
                    else
                        shouldProtect = false;
                }
                if (shouldProtect)
                {
                    DateTime arrivalTime = DateTime.Now;
                    HiPerfTimer timer = new HiPerfTimer();
                    timer.Start();

                    ESP esp = null;
                    AH ah = null;

                    CHILD_SA_TYPE childSA = SA.GetChildSA(saKey, true);

                    if (childSA != null)
                    {
                        int prevPayloadLength = IPDatagram.Payload.Length;
                        if (childSA.Protocol == IKE_PROTOCOLS.AH)
                        {
                            ah = new AH(IPDatagram, childSA, true);
                            result = ah.Protect();
                            if (result == PROTECTION_RESULTS.OK)
                                IPDatagram = ah.GetProcessedData;
                        }
                        else
                        {
                            esp = new ESP(IPDatagram, childSA, true);
                            result = esp.Protect();
                            if (result == PROTECTION_RESULTS.OK)
                                IPDatagram = esp.GetProcessedData;
                        }
                        timer.Stop();

                        childSA.UpdateTrafficStatistics(prevPayloadLength, timer.Cycles, timer.Duration);

                        if (childSA.Protocol == IKE_PROTOCOLS.AH)
                        {
                            if (ProtectedPacketSent != null)
                                ProtectedPacketSent(this, new ProtectedPacketSentEventArgs(childSA.ChildSAKey, IPDatagram.SourceIP, IPDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, 0, 0, ah.IntegrityCheckCycles, ah.IntegrityCheckTime, ah.SequenceNumber, null, ah.GetProcessedData.ToBytes(), ah.PaddingLength, ah.NextProtocol, ah.MAC, ah.MACLength));
                        }
                        else
                        {
                            if (ProtectedPacketSent != null)
                                ProtectedPacketSent(this, new ProtectedPacketSentEventArgs(childSA.ChildSAKey, IPDatagram.SourceIP, IPDatagram.DestinationIP, arrivalTime, result, timer.Cycles, timer.Duration, esp.EncryptionCycles, esp.EncryptionTime, esp.IntegrityCheckCycles, esp.IntegrityCheckTime, esp.SequenceNumber, esp.IV, IPDatagram.Payload, esp.PaddingLength, esp.NextProtocol, esp.MAC, esp.MACLength));
                        }
                    }
                }
            }

            switch (result)
            {
                case PROTECTION_RESULTS.OK:
                    int r = rnd.Next(0, 100);
                    if (r >= APP_CONFIG.PACKET_LOSS_PERCENT)
                    {
                        r = rnd.Next(0, 100);
                        if (r < APP_CONFIG.CORRUPTED_PACKET_PERCENT)
                        {
                            r = rnd.Next(0, IPDatagram.Payload.Length);
                            IPDatagram.UpdatePayload(r, (byte)(r >> 1));
                        }
                        Router(IPDatagram);
                    }
                    break;
                case PROTECTION_RESULTS.NONE:
                    Router(IPDatagram);
                    break;
            }

        }
        public static IPDatagram TryParse(byte[] RawIPDatagram)
        {
            IPDatagram ipDatagram = new IPDatagram();
            try
            {
                if ((RawIPDatagram[0] >> 4) != 4)
                    throw new Exception("Invalid IP version.");

                ipDatagram.Version = (byte)(RawIPDatagram[0] >> 4);
                ipDatagram.HLen = (byte)(RawIPDatagram[0] & 0x0f);
                ipDatagram.ServiceType = RawIPDatagram[1];
                ipDatagram.TotalLength = Utils.BytesToShort(RawIPDatagram[2], RawIPDatagram[3]);
                ipDatagram.Identification = Utils.BytesToShort(RawIPDatagram[4], RawIPDatagram[5]);
                ipDatagram.Flags = (byte)(RawIPDatagram[6] & 0xf0);
                ipDatagram.FragmentOffest = (short)(RawIPDatagram[6] & 0x0f);
                ipDatagram.FragmentOffest <<= 8;
                ipDatagram.FragmentOffest += RawIPDatagram[7];
                ipDatagram.TimeToLive = RawIPDatagram[8];
                ipDatagram.Protocol = (PROTOCOLS)RawIPDatagram[9];
                ipDatagram.HeaderChecksum = RawIPDatagram[10];
                ipDatagram.HeaderChecksum <<= 8;
                ipDatagram.HeaderChecksum += RawIPDatagram[11];

                ipDatagram.SourceIP = new byte[4];
                ipDatagram.SourceIP[0] = RawIPDatagram[12];
                ipDatagram.SourceIP[1] = RawIPDatagram[13];
                ipDatagram.SourceIP[2] = RawIPDatagram[14];
                ipDatagram.SourceIP[3] = RawIPDatagram[15];

                ipDatagram.DestinationIP = new byte[4];
                ipDatagram.DestinationIP[0] = RawIPDatagram[16];
                ipDatagram.DestinationIP[1] = RawIPDatagram[17];
                ipDatagram.DestinationIP[2] = RawIPDatagram[18];
                ipDatagram.DestinationIP[3] = RawIPDatagram[19];

                if (RawIPDatagram.Length > 20)
                {
                    ipDatagram.Payload = new byte[ipDatagram.TotalLength - 20];
                    for (int i = 0; i < ipDatagram.Payload.Length; i++)
                        ipDatagram.Payload[i] = RawIPDatagram[i + 20];
                }

                return ipDatagram;
            }
            catch (Exception e)
            {
                ipDatagram = null;
                throw new Exception("Invalid raw IP data. " + e.Message);
            }
        }
    }

}
