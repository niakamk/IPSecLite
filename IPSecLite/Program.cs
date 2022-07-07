using System;
using System.Collections.Generic;
using System.Windows.Forms;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.IKEExchange;
using System.Threading;
using adabtek.IPsecLite.Constants;
namespace adabtek.IPsecLite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static Ethernet ethernet;
        static IP ip;
        static ICMP icmp;
        static UDP udp;
        static IKEv2Exchange ikev2Exchange;
        static IPTrafficForm ipTrafficForm;
        static IPsecLiteMasterForm ipsecLiteMasterForm;
        public static IPsecLiteMasterForm IPsecLiteMasterForm
        {
            get { return ipsecLiteMasterForm; }
        }
        public static IPTrafficForm IPTrafficForm
        {
            get { return ipTrafficForm; }
        }
        public static Ethernet Ethernet
        {
            get { return ethernet; }
        }
        public static IP IP
        {
            get { return ip; }
        }
        public static ICMP ICMP
        {
            get { return icmp; }
        }
        public static UDP UDP
        {
            get { return udp; }
        }
        public static IKEv2Exchange IKEv2Exchange
        {
            get { return ikev2Exchange; }
        }
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen frmSplash = new SplashScreen();
            frmSplash.Show();
            frmSplash.Update();
            Thread.Sleep(2000);
            frmSplash.Close();


            if (args.Length > 2)
            {
                APP_CONFIG.ETHERNET_IP = args[0];
                APP_CONFIG.ETHERNET_PORT = short.Parse(args[1]);
                APP_CONFIG.IS_GATEWAY = bool.Parse(args[2]);
                if (APP_CONFIG.IS_GATEWAY)
                {
                    string[] octets = args[3].Split('.');
                    byte[] host = new byte[4];
                    host[0] = byte.Parse(octets[0]);
                    host[1] = byte.Parse(octets[1]);
                    host[2] = byte.Parse(octets[2]);
                    host[3] = byte.Parse(octets[3]);
                    APP_CONFIG.HOSTS.Add(host);

                    octets = args[4].Split('.');
                    byte[] peerGateway = new byte[4];
                    peerGateway[0] = byte.Parse(octets[0]);
                    peerGateway[1] = byte.Parse(octets[1]);
                    peerGateway[2] = byte.Parse(octets[2]);
                    peerGateway[3] = byte.Parse(octets[3]);
                    APP_CONFIG.GATEWAY_IP = peerGateway;
                }
                else
                {
                    string[] octets = args[3].Split('.');
                    byte[] gateway = new byte[4];
                    gateway[0] = byte.Parse(octets[0]);
                    gateway[1] = byte.Parse(octets[1]);
                    gateway[2] = byte.Parse(octets[2]);
                    gateway[3] = byte.Parse(octets[3]);
                    APP_CONFIG.GATEWAY_IP = gateway;
                }
            }

            NetConfigForm netConfigForm = new NetConfigForm();
Retry:
            if (netConfigForm.ShowDialog() == DialogResult.OK)
            {
                ethernet = new Ethernet(APP_CONFIG.ETHERNET_IP, APP_CONFIG.ETHERNET_PORT);
                ip = new IP();
                icmp = new ICMP();
                udp = new UDP();
                ikev2Exchange = new IKEv2Exchange();

                ipsecLiteMasterForm = new IPsecLiteMasterForm();

                ipTrafficForm = new IPTrafficForm();
                if (APP_CONFIG.FRAMED)
                    ipTrafficForm.MdiParent = ipsecLiteMasterForm;
                ipTrafficForm.Show();

                Program.Ethernet.Start();

                try
                {

                    Application.Run(ipsecLiteMasterForm);
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred.\r\n" + e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
                if (netConfigForm.DialogResult == DialogResult.Retry)
                    goto Retry;
        }
        private static void DoSplash()
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.ShowDialog();
        }

    }
}
