using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.Utilities;
namespace adabtek.IPsecLite
{
    public partial class NetConfigForm : Form
    {
        public NetConfigForm()
        {
            InitializeComponent();
        }

        private void EndPointForm_Load(object sender, EventArgs e)
        {
            IPAddress[] hostEntry = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipAddress in hostEntry)
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipListComboBox.Items.Add(ipAddress.ToString());
                    host1IPComboBox.Items.Add(ipAddress.ToString());
                    host2IPComboBox.Items.Add(ipAddress.ToString());
                    host3IPComboBox.Items.Add(ipAddress.ToString());

                    peerGatewayIPComboBox.Items.Add(ipAddress.ToString());
                    gatewayIPComboBox.Items.Add(ipAddress.ToString());
                }

            packetsLossPercentTextBox.Text = APP_CONFIG.PACKET_LOSS_PERCENT.ToString();
            outOfSequencePercentageTextBox.Text = APP_CONFIG.SEND_OUT_OF_SEQUENCE_PERCENT.ToString();
            corruptedPacketPercentTextBox.Text = APP_CONFIG.CORRUPTED_PACKET_PERCENT.ToString();

        }
        
        private void startButton_Click(object sender, EventArgs e)
        {

            bool ok = true;
            bool noHosts = true;
            string[] IPs = {"", "", "", "", ""};

            if ((ipListComboBox.Text.Length == 0) || (portTextBox.Text.Trim().Length == 0))
            {
                MessageBox.Show("IP address or Port for the node is not specified.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Retry;
                ok = false;
            }
            else
            {

                APP_CONFIG.ETHERNET_PORT = short.Parse(portTextBox.Text);
                APP_CONFIG.ETHERNET_IP = ipListComboBox.Text;

                IPs[0] = ipListComboBox.Text;

                APP_CONFIG.IS_GATEWAY = gatewayRadioButton.Checked;

                if (APP_CONFIG.IS_GATEWAY)
                {

                    if (peerGatewayIPComboBox.Text.Length == 0)
                    {
                        MessageBox.Show("Peer gateway is not specified.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Retry;
                        ok = false;

                    }
                    else
                    {

                        APP_CONFIG.GATEWAY_IP = Utils.IPToBytes(peerGatewayIPComboBox.Text);
                        IPs[1] = peerGatewayIPComboBox.Text;

                        if (host1IPComboBox.Text.Trim().Length > 0)
                        {
                            APP_CONFIG.HOSTS.Add(Utils.IPToBytes(host1IPComboBox.Text));
                            IPs[2] = host1IPComboBox.Text;
                            noHosts = false;
                        }

                        if (host2IPComboBox.Text.Trim().Length > 0)
                        {
                            APP_CONFIG.HOSTS.Add(Utils.IPToBytes(host2IPComboBox.Text));
                            IPs[3] = host2IPComboBox.Text;
                            noHosts = false;
                        }

                        if (host3IPComboBox.Text.Trim().Length > 0)
                        {
                            APP_CONFIG.HOSTS.Add(Utils.IPToBytes(host3IPComboBox.Text));
                            IPs[4] = host3IPComboBox.Text;
                            noHosts = false;
                        }

                        if (noHosts)
                        {
                            MessageBox.Show("At least one host must be specified.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.Retry;
                            ok = false;
                        }
                        else
                        {
                            int i;
                            int j;

                            i = 0;
                            while ((i < 5) && ok)
                            {
                                j = i + 1;
                                while ((j < 5) && ok)
                                {
                                    if ((IPs[i] == IPs[j]) && (IPs[i] != ""))
                                        ok = false;
                                    j++;
                                }
                                i++;
                            }
                            if (!ok)
                            {
                                MessageBox.Show("At least one IP address is used more than once.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.DialogResult = DialogResult.Retry;
                            }
                        }
                    }
                }
                else
                {
                    if (gatewayIPComboBox.Text.Length == 0)
                    {
                        MessageBox.Show("Gateway address is not specified.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Retry;
                        ok = false;
                    }
                    else
                    {
                        if (gatewayIPComboBox.Text == ipListComboBox.Text)
                        {
                            MessageBox.Show("The host and gateway addresses cannot be the same.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.Retry;
                            ok = false;
                        }
                        else
                            APP_CONFIG.GATEWAY_IP = Utils.IPToBytes(gatewayIPComboBox.Text);
                    }
                }

                if (ok)
                {
                    APP_CONFIG.PACKET_LOSS_PERCENT = byte.Parse(packetsLossPercentTextBox.Text);
                    APP_CONFIG.SEND_OUT_OF_SEQUENCE_PERCENT = byte.Parse(outOfSequencePercentageTextBox.Text);
                    APP_CONFIG.CORRUPTED_PACKET_PERCENT = byte.Parse(corruptedPacketPercentTextBox.Text);

                    APP_CONFIG.FRAMED = (framedCheckBox.Checked);
                }
            }
        }
    }
}
