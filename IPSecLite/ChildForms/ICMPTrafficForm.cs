using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adabtek.IPsecLite.SPDB;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.IKEExchange;
using System.Threading;
namespace adabtek.IPsecLite
{
    public partial class ICMPTrafficForm : Form
    {

        delegate void IKESAEstablishedHandler(object sender, IKESAEstablishedEventArgs e);

        delegate void ICMPPacketArrivedHandler(object sender, ICMPPacketArrivedEventArgs e);
        delegate void ICMPPacketSentHandler(object sender, ICMPPacketSentEventArgs e);

        static bool messagePending = false;
        public ICMPTrafficForm()
        {
            InitializeComponent();
            this.Text += (APP_CONFIG.IS_GATEWAY ? " Gateway (" + APP_CONFIG.ETHERNET_IP + ") " : " Host (" + APP_CONFIG.ETHERNET_IP + ") with Gateway (" + Utils.ToShortStringIP(APP_CONFIG.GATEWAY_IP) + ")");

            packetsLossPercentTextBox.Text = APP_CONFIG.PACKET_LOSS_PERCENT.ToString();
            outOfSequencePercentageTextBox.Text = APP_CONFIG.SEND_OUT_OF_SEQUENCE_PERCENT.ToString();
            corruptedPacketPercentTextBox.Text = APP_CONFIG.CORRUPTED_PACKET_PERCENT.ToString();

        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            if (destinationIPTextBox.Text.Trim().Length < 12)
            {
                MessageBox.Show("Invalid destination address.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                destinationIPTextBox.Focus();
                return;
            }

            if (repeatTextbox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Number of messages to send is required.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                repeatTextbox.Focus();
                return;
            }

            {
                APP_CONFIG.PACKET_LOSS_PERCENT = byte.Parse(packetsLossPercentTextBox.Text);
                APP_CONFIG.SEND_OUT_OF_SEQUENCE_PERCENT = byte.Parse(outOfSequencePercentageTextBox.Text);
                APP_CONFIG.CORRUPTED_PACKET_PERCENT = byte.Parse(corruptedPacketPercentTextBox.Text);
                if (sendMessageTextBox.Text.Length > 0)
                    sendMessage();
            }
        }
        private void connect(string PolicyID)
        {
            byte[] responderIP = new byte[4];
            string[] ip = PolicyID.Split('.');
            responderIP[0] = byte.Parse(ip[0]);
            responderIP[1] = byte.Parse(ip[1]);
            responderIP[2] = byte.Parse(ip[2]);
            responderIP[3] = byte.Parse(ip[3]);

            Program.IKEv2Exchange.SendINITi(responderIP, Utils.ToShortStringIP(responderIP));
        }
        private void sendMessage()
        {
            byte[] sourceIP = Utils.IPToBytes(Utils.GetHostIPAddress());

            byte[] destIP = new byte[4];
            string[] ipAddress = destinationIPTextBox.Text.Split('.');
            destIP[0] = byte.Parse(ipAddress[0]);
            destIP[1] = byte.Parse(ipAddress[1]);
            destIP[2] = byte.Parse(ipAddress[2]);
            destIP[3] = byte.Parse(ipAddress[3]);
            ICMPPacket icmp = new ICMPPacket(ICMP_TYPE.REQUEST, sendMessageTextBox.Text);
            short repeats = short.Parse(repeatTextbox.Text);
            for (int i = 0; i < repeats; i++)
            {
                Program.ICMP.Send(sourceIP, destIP, icmp);
                Thread.Sleep(100);
            }
        }

        private void ICMPTrafficForm_Load(object sender, EventArgs e)
        {
            Program.IKEv2Exchange.IKESAEstablished += new IKEv2Exchange.IKESAEstablishedHandler(IKEv2Exchange_IKESAEstablished);

           // Program.ICMP.ICMPPacketArrived += new ICMP.ICMPPacketArrivedHandler(ICMP_ICMPPacketArrived);
            Program.ICMP.ICMPPacketSent += new ICMP.ICMPPacketSentHandler(ICMP_ICMPPacketSent);
        }
        void IKEv2Exchange_IKESAEstablished(object sender, IKESAEstablishedEventArgs e)
        {
            if (messagePending)
            {
                sendMessage();
                messagePending = false;
            }

        }
        void displayICMPMessage(object sender, ICMPPacketSentEventArgs e)
        {
            traceTextBox.Text = Utils.HoursThroughMilliseconds(e.Time) + " SND ping to " + Utils.ToLongStringIP(e.DestinationIP) + " size: " + (e.ICMPPacket.Data.Length + 8).ToString() + "\r\n" + traceTextBox.Text;
        }
        void displayICMPMessage(object sender, ICMPPacketArrivedEventArgs e)
        {
            traceTextBox.Text = Utils.HoursThroughMilliseconds(e.Time) + " RCV response from " + Utils.ToLongStringIP(e.SourceIP) + " size: " + (e.ICMPPacket.Data.Length + 8).ToString() + "\r\n" + traceTextBox.Text;

        }
        void ICMP_ICMPPacketSent(object sender, ICMPPacketSentEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new ICMPPacketSentHandler(displayICMPMessage), sender, e);
            else
                displayICMPMessage(sender, e);
        }

        void ICMP_ICMPPacketArrived(object sender, ICMPPacketArrivedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new ICMPPacketArrivedHandler(displayICMPMessage), sender, e);
            else
                displayICMPMessage(sender, e);
        }

        private void useRandomMessageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useRandomMessageCheckBox.Checked)
            {
                if (randomLengthTextBox.Text.Trim().Length > 0)
                {
                    Cursor prev = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    Random rnd = new Random();
                    for (int i = 0; i < int.Parse(randomLengthTextBox.Text); i++)
                        sendMessageTextBox.Text += (char)rnd.Next(65, 90);
                    Cursor.Current = prev;
                }
                else
                {
                    MessageBox.Show("Data size must be specified.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    randomLengthTextBox.Focus();
                    useRandomMessageCheckBox.Checked = false;
                }
            }
            else
                sendMessageTextBox.Text = "";
        }



    }
}
