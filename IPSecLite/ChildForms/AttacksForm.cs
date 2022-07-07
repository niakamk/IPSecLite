using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite.Constants;

namespace adabtek.IPsecLite
{
    public partial class AttacksForm : Form
    {
        public AttacksForm()
        {
            InitializeComponent();
            this.Text += (APP_CONFIG.IS_GATEWAY ? " Gateway (" + APP_CONFIG.ETHERNET_IP + ") " : " Host (" + APP_CONFIG.ETHERNET_IP + ") with Gateway (" + Utils.ToShortStringIP(APP_CONFIG.GATEWAY_IP) + ")");

        }
        delegate void IPDatagramSentHandler(object sender, IPDatagramTrafficEventArgs e);

        IPDatagram ipDatagram;

        private void AttacksForm_Load(object sender, EventArgs e)
        {
            Program.IP.IPDatagramSent += new IPsecLite.NetworkProtocols.IP.IPDatagramSentHandler(IP_IPDatagramSent);
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
            if (e.IPDatagram.Protocol == IPsecLite.Constants.PROTOCOLS.ESP)
            {
                byte[] snBytes = new byte[4];
                Utils.MemCpy(e.IPDatagram.Payload, 4, ref snBytes, 0, 4);
                int sn = Utils.BytesToInt(snBytes);
                newSequenceNumberTextBox.Text = sn.ToString();

                replayButton.Enabled = true;
            }
            else
                replayButton.Enabled = false;

            ipHeaderChecksumTextBox.Text = e.IPDatagram.HeaderChecksum.ToString();
            ipSourceAddressTextBox.Text = e.IPDatagram.SourceIPToString;
            ipDestinationAddressTextBox.Text = e.IPDatagram.DestinationIPToString;

            sourceIPMaskedTextBox.Text = e.IPDatagram.SourceIPToString;
            payloadTextBox.Text = Utils.ToHexString(e.IPDatagram.Payload, false, 0);

            ipDatagram = e.IPDatagram;

            newSequenceNumberTextBox.Enabled = (ipDatagram.Protocol == IPsecLite.Constants.PROTOCOLS.ESP);

            sendButton.Enabled = true;

            rawMessageTextBox.Text = Utils.ToCharString(ipDatagram.Payload, false, 0);
        }
        void IP_IPDatagramSent(object sender, IPDatagramSentEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IPDatagramSentHandler(displayNewDatagram), sender, e);
            else
                displayNewDatagram(sender, e);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (sourceIPMaskedTextBox.Text.Trim().Length < 12)
            {
                MessageBox.Show("Invalid source address.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ipDatagram.Payload = Utils.HexStringToBytes(payloadTextBox.Text);
            if (newSequenceNumberTextBox.Enabled)
            {
                byte[] newSN = Utils.IntToBytes(int.Parse(newSequenceNumberTextBox.Text));
                for (byte i = 0; i < 4; i++)
                    ipDatagram.UpdatePayload((byte)(4 + i), newSN[i]);
            }
            byte[] sourceIP = new byte[4];
            string[] ip = sourceIPMaskedTextBox.Text.Split('.');
            sourceIP[0] = byte.Parse(ip[0]);
            sourceIP[1] = byte.Parse(ip[1]);
            sourceIP[2] = byte.Parse(ip[2]);
            sourceIP[3] = byte.Parse(ip[3]);

            ipDatagram.SourceIP = sourceIP;

            Program.IP.Router(ipDatagram);

        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            byte[] newSN = Utils.IntToBytes(int.Parse(newSequenceNumberTextBox.Text));
            for (byte i = 0; i < 4; i++)
                ipDatagram.UpdatePayload((byte)(4 + i), newSN[i]);
            Program.IP.Router(ipDatagram);

        }

    }
}
