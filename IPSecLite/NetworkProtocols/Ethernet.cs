using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.Utilities;
namespace adabtek.IPsecLite.NetworkProtocols
{
    public class Ethernet
    {

        byte[] buffer;
        UdpClient receiver;
        IPEndPoint sender;
        Thread rThread;

        public delegate void ErrorEventHandler(object sender, Exception e);
        public delegate void RawIPDatagramHandler(object sender, RawNetworkDataEventArgs e);
        //public delegate void IPDatagramArrivedHandler(object sender, IPDatagramArrivedEventArgs e);
        public event ErrorEventHandler ReceiveError;
        public event RawIPDatagramHandler RawIPDatagramArrived;
        //public event IPDatagramArrivedHandler IPDatagramArrived;

        private short communicationPort = 8088;

        public Ethernet(string IPAddress, short PortNumber)
        {
            buffer = new byte[1500];

            communicationPort = PortNumber;

            byte[] ipBytes = new byte[4];
            string[] ip = Utils.GetHostIPAddress().Split('.');
            ipBytes[0] = byte.Parse(ip[0]);
            ipBytes[1] = byte.Parse(ip[1]);
            ipBytes[2] = byte.Parse(ip[2]);
            ipBytes[3] = byte.Parse(ip[3]);
            IPAddress listenIPAddress = new IPAddress(ipBytes);
            sender = new IPEndPoint(listenIPAddress, communicationPort);
            receiver = new UdpClient(sender);
        }
        public void Start()
        {
            rThread = new Thread(receive);
            rThread.Start();
        }
        private void receive()
        {
            try
            {
                buffer = receiver.Receive(ref sender);
                if (RawIPDatagramArrived != null)
                    RawIPDatagramArrived(null, new RawNetworkDataEventArgs(buffer));
            }
            catch (Exception e)
            {
                if (ReceiveError != null)
                    ReceiveError(null, e);
            }
            finally
            {
                //The thread finished blocking, and ended, so we start again
                Start();
            }
        }
        public void Send(IPDatagram IPDatagram, byte[] DestinationIP)
        {
            string ip = "";
            ip = DestinationIP[0].ToString();
            ip += ".";
            ip += DestinationIP[1].ToString();
            ip += ".";
            ip += DestinationIP[2].ToString();
            ip += ".";
            ip += DestinationIP[3].ToString();

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), communicationPort);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            byte[] data = IPDatagram.ToBytes();

            client.Connect(ipep);
            client.Send(data, data.Length, SocketFlags.None);
            client.Close();
        }

    }
}
