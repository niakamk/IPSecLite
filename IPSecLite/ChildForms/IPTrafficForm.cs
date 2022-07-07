using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.Constants;
namespace adabtek.IPsecLite
{
    public partial class IPTrafficForm : Form
    {
        delegate void IPDatagramArrivedHandler(object sender, IPDatagramArrivedEventArgs e);
        delegate void IPDatagramSentHandler(object sender, IPDatagramSentEventArgs e);
        Dictionary<string, IPDatagramTrafficEventArgs> ipDatagrams = new Dictionary<string, IPDatagramTrafficEventArgs>(1000);

        public IPTrafficForm()
        {
            InitializeComponent();
            this.Text += (APP_CONFIG.IS_GATEWAY ? " Gateway (" + APP_CONFIG.ETHERNET_IP + ") " : " Host (" + APP_CONFIG.ETHERNET_IP + ") with Gateway (" + Utils.ToShortStringIP(APP_CONFIG.GATEWAY_IP) + ")");


        }

        private void IPTrafficForm_Load(object sender, EventArgs e)
        {
            Program.IP.IPDatagramArrived += new IP.IPDatagramArrivedHandler(IP_IPDatagramArrived);
            Program.IP.IPDatagramSent += new IP.IPDatagramSentHandler(IP_IPDatagramSent);
            if (!APP_CONFIG.IS_GATEWAY)
                networkDataListView.Columns[4].Width = 0;

        }
        void displayNewDatagram(object sender, IPDatagramArrivedEventArgs e)
        {

            ListViewItem networkDataLVI = new ListViewItem(Utils.HoursThroughMilliseconds(e.Time));
            networkDataLVI.UseItemStyleForSubItems = false;
            networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.IPDatagram.SourceIP));
            networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.IPDatagram.DestinationIP));
            networkDataLVI.SubItems.Add(e.IPDatagram.Protocol.ToString());
            if (e.SentTo != null)
            {
                networkDataLVI.SubItems.Add("R");
                networkDataLVI.SubItems[4].BackColor = Color.Yellow;
            }
            else
                networkDataLVI.SubItems.Add("");
            networkDataLVI.SubItems.Add(Utils.ToCharString(e.IPDatagram.Payload, false, 0));

            networkDataLVI.Tag = ++APP_CONST.EVENT_COUNTER;

            networkDataListView.Items.Insert(0, networkDataLVI);

            ipDatagrams.Add(networkDataLVI.Tag.ToString(), e);

        }
        void displayNewDatagram(object sender, IPDatagramSentEventArgs e)
        {

            ListViewItem networkDataLVI = new ListViewItem(Utils.HoursThroughMilliseconds(e.Time));
            networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.IPDatagram.SourceIP));
            networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.IPDatagram.DestinationIP));
            networkDataLVI.SubItems.Add(e.IPDatagram.Protocol.ToString());
            if (e.SentTo != null)
            {
                networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.SentTo));
                networkDataLVI.SubItems[4].BackColor = Color.Orange;
            }
            else
                networkDataLVI.SubItems.Add("");
            networkDataLVI.SubItems.Add(Utils.ToCharString(e.IPDatagram.Payload, false, 0));

            networkDataLVI.Tag = ++APP_CONST.EVENT_COUNTER;

            networkDataListView.Items.Insert(0, networkDataLVI);


            ipDatagrams.Add(networkDataLVI.Tag.ToString(), e);

        }
        void IP_IPDatagramSent(object sender, IPDatagramSentEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IPDatagramSentHandler(displayNewDatagram), sender, e);
            else
                displayNewDatagram(sender, e);
        }

        void IP_IPDatagramArrived(object sender, IPDatagramArrivedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IPDatagramArrivedHandler(displayNewDatagram), sender, e);
            else
                displayNewDatagram(sender, e);
        }

        private void networkDataListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = this.networkDataListView.SelectedItems;
            foreach (ListViewItem lvItem in items)
            {
                IPDatagramTrafficEventArgs ipDatagramEventArg;

                if (ipDatagrams.TryGetValue(lvItem.Tag.ToString(), out ipDatagramEventArg))
                {
                    ipVersionTextBox.Text = ipDatagramEventArg.IPDatagram.Version.ToString();
                    ipHLenTextBox.Text = ipDatagramEventArg.IPDatagram.HLen.ToString();
                    ipServiceTypeTextBox.Text = ipDatagramEventArg.IPDatagram.ServiceType.ToString();
                    ipTotalLengthTextBox.Text = ipDatagramEventArg.IPDatagram.TotalLength.ToString();
                    ipIdentificationTextBox.Text = ipDatagramEventArg.IPDatagram.Identification.ToString();
                    ipFlagsTextBox.Text = ipDatagramEventArg.IPDatagram.Flags.ToString();
                    ipFragmentOffsetTextBox.Text = ipDatagramEventArg.IPDatagram.FragmentOffest.ToString();
                    ipTimeToLiveTextBox.Text = ipDatagramEventArg.IPDatagram.TimeToLive.ToString();
                    ipProtocolTextBox.Text = ipDatagramEventArg.IPDatagram.Protocol.ToString();
                    ipHeaderChecksumTextBox.Text = ((ushort)ipDatagramEventArg.IPDatagram.HeaderChecksum).ToString();
                    ipSourceAddressTextBox.Text = ipDatagramEventArg.IPDatagram.SourceIPToString;
                    ipDestinationAddressTextBox.Text = ipDatagramEventArg.IPDatagram.DestinationIPToString;
                    rawMessageTextBox.Text = Utils.ToHexString(ipDatagramEventArg.IPDatagram.Payload, false, 0);
                }
            }
        }
    }
}
