using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.Constants;
namespace adabtek.IPsecLite
{
    public partial class UDPTrafficForm : Form
    {
        delegate void UDPPacketArrivedHandler(object sender,UDPPacketArrivedEventArgs e);
        delegate void UDPPacketSentHandler(object sender, UDPPacketSentEventArgs e);
        Dictionary<string, UDPPacketTrafficEventArgs> packets = new Dictionary<string, UDPPacketTrafficEventArgs>(100);

        public UDPTrafficForm()
        {
            InitializeComponent();
            this.Text += (APP_CONFIG.IS_GATEWAY ? " Gateway (" + APP_CONFIG.ETHERNET_IP + ") " : " Host (" + APP_CONFIG.ETHERNET_IP + ") with Gateway (" + Utils.ToShortStringIP(APP_CONFIG.GATEWAY_IP) + ")");

        }

        private void UDPTrafficForm_Load(object sender, EventArgs e)
        {
            Program.UDP.UDPPacketArrived += new UDP.UDPPacketArrivedHandler(UDP_UDPPacketArrived);
            Program.UDP.UDPPacketSent += new UDP.UDPPacketSentHandler(UDP_UDPPacketSent);
        }
        void displayNewPacket(object sender, UDPPacketTrafficEventArgs e)
        {
            ListViewItem networkDataLVI = new ListViewItem(Utils.HoursThroughMilliseconds(e.Time));
            networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.SourceIP));
            networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.DestinationIP));
            networkDataLVI.SubItems.Add(e.UDPPacket.SourcePort.ToString());
            networkDataLVI.SubItems.Add(e.UDPPacket.DestinationPort.ToString());
            networkDataLVI.SubItems.Add(Utils.ToCharString(e.UDPPacket.Payload, false, 0));

            networkDataLVI.Tag = ++APP_CONST.EVENT_COUNTER;

            networkDataListView.Items.Insert(0, networkDataLVI);

            packets.Add(networkDataLVI.Tag.ToString(), e);
        }
        void UDP_UDPPacketSent(object sender, UDPPacketSentEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new UDPPacketSentHandler(displayNewPacket), sender, e);
            else
                displayNewPacket(sender, e);
        }

        void UDP_UDPPacketArrived(object sender, UDPPacketArrivedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new UDPPacketArrivedHandler(displayNewPacket), sender, e);
            else
                displayNewPacket(sender, e);
        }

        private void networkDataListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = this.networkDataListView.SelectedItems;
            foreach (ListViewItem lvItem in items)
            {
                UDPPacketTrafficEventArgs udpTrafficEventArg;
                if (packets.TryGetValue(lvItem.Tag.ToString(), out udpTrafficEventArg))
                {
                    sourcePortTextBox.Text = udpTrafficEventArg.UDPPacket.SourcePort.ToString();
                    destinationPortTextBox.Text = udpTrafficEventArg.UDPPacket.DestinationPort.ToString();
                    lengthTextBox.Text = udpTrafficEventArg.UDPPacket.MessageLength.ToString();
                    checksumTextBox.Text = ((ushort)udpTrafficEventArg.UDPPacket.Checksum).ToString();
                    payloadTextBox.Text = Utils.ToHexString(udpTrafficEventArg.UDPPacket.Payload, false, 0);
                }
            }
        }
    }
}
