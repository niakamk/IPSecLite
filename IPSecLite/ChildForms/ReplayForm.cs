using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite;

namespace IPsecLite
{
    public partial class ReplayForm : Form
    {
        delegate void IPDatagramSentHandler(object sender, IPDatagramTrafficEventArgs e);

        IPDatagram ipDatagram;

        public ReplayForm()
        {
            InitializeComponent();
        }

        private void ReplayForm_Load(object sender, EventArgs e)
        {
            Program.IP.IPDatagramSent += new adabtek.IPsecLite.NetworkProtocols.IP.IPDatagramSentHandler(IP_IPDatagramSent);
        }

        void displayNewDatagram(object sender, IPDatagramTrafficEventArgs e)
        {
            ipVersionTextBox.Text = e.IPDatagram.Version.ToString();
            ipHLenTextBox.Text = e.IPDatagram.HLen.ToString();
            ipServiceTypeTextBox.Text = e.IPDatagram.ServiceType.ToString();
            ipTotalLengthTextBox.Text = e.IPDatagram.TotalLength.ToString();
            ipIdentificationTextBox.Text = e.IPDatagram.Identification.ToString();
            ipFlagsTextBox.Text = e.IPDatagram.Flags.ToString();
            ipFragmentOffsetTextBox.Text = e.IPDatagram.FragmentOffest.ToString();
            ipTimeToLiveTextBox.Text = e.IPDatagram.TimeToLive.ToString();
            ipProtocolTextBox.Text = e.IPDatagram.Protocol.ToString();
            if (e.IPDatagram.Protocol == adabtek.IPsecLite.Constants.PROTOCOLS.ESP)
            {
                byte[] snBytes = new byte[4];
                Utils.MemCpy(e.IPDatagram.Payload, 4, ref snBytes, 0, 4);
                int sn = Utils.BytesToInt(snBytes);
                newSequenceNumberTextBox.Text = sn.ToString();
            }
            ipHeaderChecksumTextBox.Text = e.IPDatagram.HeaderChecksum.ToString();
            ipSourceAddressTextBox.Text = e.IPDatagram.SourceIPToString;
            ipDestinationAddressTextBox.Text = e.IPDatagram.DestinationIPToString;

            ipDatagram = e.IPDatagram;

            replayButton.Enabled = true;

            rawMessageTextBox.Text = Utils.ToCharString(ipDatagram.Payload, false, 0);
        }
        void IP_IPDatagramSent(object sender, IPDatagramSentEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IPDatagramSentHandler(displayNewDatagram), sender, e);
            else
                displayNewDatagram(sender, e);
        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            if (changeSNCheckBox.Checked)
            {
                byte[] newSN = Utils.IntToBytes(int.Parse(newSequenceNumberTextBox.Text));
                for(byte i = 0; i < 4; i++)
                    ipDatagram.UpdatePayload((byte)(4 + i), newSN[i]);
            }
            Program.Ethernet.Send(ipDatagram);
        }
    }
}
