using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.SADB;
using adabtek.IPsecLite.Stats;
using System.IO;
using adabtek.IPsecLite.Constants;
namespace adabtek.IPsecLite
{
    public partial class ProtectedOutgoingTrafficForm : Form
   {
        delegate void ProtectedPacketSentHandler(object sender, ProtectedPacketSentEventArgs e);

        Dictionary<string, ProtectedPacketSentEventArgs> packets = new Dictionary<string, ProtectedPacketSentEventArgs>(500);

        public ProtectedOutgoingTrafficForm()
        {
            InitializeComponent();
            this.Text += (APP_CONFIG.IS_GATEWAY ? " Gateway (" + APP_CONFIG.ETHERNET_IP + ") " : " Host (" + APP_CONFIG.ETHERNET_IP + ") with Gateway (" + Utils.ToShortStringIP(APP_CONFIG.GATEWAY_IP) + ")");

            Program.IP.ProtectedPacketSent += new IP.ProtectedPacketSentHandler(IP_ProtectedPacketSent);
            resetAH();
            resetESP();
        }
        void displayProtectedPacketSentMessage(object sender, ProtectedPacketSentEventArgs e)
        {
            CHILD_SA_TYPE childSA = SA.GetChildSA(e.SAKey);
            ListViewItem lvItem = new ListViewItem(Utils.HoursThroughMilliseconds(e.ArrivalTime));
            lvItem.SubItems.Add(childSA.SourceIP);
            lvItem.SubItems.Add(childSA.Protocol.ToString());
            lvItem.SubItems.Add(e.NextProtocol.ToString());
            lvItem.SubItems.Add(e.ProtectionResult.ToString());
            lvItem.SubItems.Add((e.Encrypted.Length - (e.IVLength + e.MACLength + e.PaddingLength + 2)).ToString());
            lvItem.SubItems.Add(e.IntegrityCheckCycles.ToString());
            lvItem.SubItems.Add((e.IntegrityCheckTime * 1000).ToString());
            lvItem.SubItems.Add(e.EncryptionCycles.ToString());
            lvItem.SubItems.Add((e.EncryptionTime * 1000).ToString());
            lvItem.SubItems.Add(e.ProcessingCycles.ToString());
            lvItem.SubItems.Add((e.ProcessingTime * 1000).ToString());

            lvItem.Tag = ++APP_CONST.EVENT_COUNTER;

            trafficListView.Items.Insert(0, lvItem);

            packets.Add(lvItem.Tag.ToString(), e);

            dataPointsTextBox.Text = trafficListView.Items.Count.ToString();

            warningLabel.Visible = true;

        }
        void IP_ProtectedPacketSent(object sender, ProtectedPacketSentEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new ProtectedPacketSentHandler(displayProtectedPacketSentMessage), sender, e);
            else
                displayProtectedPacketSentMessage(sender, e);
        }
        private void RefreshStats()
        {
            if (trafficListView.Items.Count == 0)
                return;

            Statistics s;

            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            double[] data = new double[trafficListView.Items.Count];

            //IC Cycle
            for (int i = 0; i < data.Length; i++)
                data[i] = double.Parse(trafficListView.Items[i].SubItems[6].Text);
            s = new Statistics(data);
            icCycleMeanTextBox.Text = s.Mean.ToString("############");
            icCycleStdTextBox.Text = s.Std.ToString("############");
            icCycleOutlierTextBox.Text = (data.Length - s.Length).ToString();
            //IC Time
            for (int i = 0; i < data.Length; i++)
                data[i] = double.Parse(trafficListView.Items[i].SubItems[7].Text);
            s = new Statistics(data);
            icTimeMeanTextBox.Text = s.Mean.ToString("#.#########");
            icTimeStdTextBox.Text = s.Std.ToString("#.#########");
            icTimeOutlierTextBox.Text = (data.Length - s.Length).ToString();
            //C Cycle
            for (int i = 0; i < data.Length; i++)
                data[i] = double.Parse(trafficListView.Items[i].SubItems[8].Text);
            s = new Statistics(data);
            cCycleMeanTextBox.Text = s.Mean.ToString("############");
            cCycleStdTextBox.Text = s.Std.ToString("############");
            cCycleOutlierTextBox.Text = (data.Length - s.Length).ToString();
            //C Time
            for (int i = 0; i < data.Length; i++)
                data[i] = double.Parse(trafficListView.Items[i].SubItems[9].Text);
            s = new Statistics(data);
            cTimeMeanTextBox.Text = s.Mean.ToString("#.#########");
            cTimeStdTextBox.Text = s.Std.ToString("#.#########");
            cTimeOutlierTextBox.Text = (data.Length - s.Length).ToString();
            //Total Cycle
            for (int i = 0; i < data.Length; i++)
                data[i] = double.Parse(trafficListView.Items[i].SubItems[10].Text);
            s = new Statistics(data);
            totalCycleMeanTextBox.Text = s.Mean.ToString("############");
            totalCycleStdTextBox.Text = s.Std.ToString("############");
            totalCycleOutlierTextBox.Text = (data.Length - s.Length).ToString();
            //Total Time
            for (int i = 0; i < data.Length; i++)
                data[i] = double.Parse(trafficListView.Items[i].SubItems[11].Text);
            s = new Statistics(data);
            totalTimeMeanTextBox.Text = s.Mean.ToString("#.#########");
            totalTimeStdTextBox.Text = s.Std.ToString("#.#########");
            totalTimeOutlierTextBox.Text = (data.Length - s.Length).ToString();

            warningLabel.Visible = false;

            Cursor.Current = c;

        }

        private void statsButton_Click(object sender, EventArgs e)
        {
            RefreshStats();
        }

        private void exportDataButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Utils.ExportListViewToTextFile(trafficListView, saveFileDialog.FileName);
                if (includeStats.Checked)
                {
                    if (warningLabel.Visible)
                        RefreshStats();

                    Cursor c = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;

                    StreamWriter sw = new StreamWriter(saveFileDialog.FileName, true);
                    sw.WriteLine();
                    sw.WriteLine("Traffic Statistics");
                    sw.WriteLine("         " + "\t" + "IC Cycles" + "\t" + "IC Time" + "\t" + "C Cycles" + "\t" + "C Time" + "\t" + "Total Cycles" + "\t" + "Total Time");
                    sw.WriteLine("Mean:    " + "\t" + icCycleMeanTextBox.Text + "\t" + icTimeMeanTextBox.Text + "\t" + cCycleMeanTextBox.Text + "\t" + cTimeMeanTextBox.Text + "\t" + totalCycleMeanTextBox.Text + "\t" + totalTimeMeanTextBox.Text);
                    sw.WriteLine("Std.:    " + "\t" + icCycleStdTextBox.Text + "\t" + icTimeStdTextBox.Text + "\t" + cCycleStdTextBox.Text + "\t" + cTimeStdTextBox.Text + "\t" + totalCycleStdTextBox.Text + "\t" + totalTimeStdTextBox.Text);
                    sw.WriteLine("Outlier:" + "\t" + icCycleOutlierTextBox.Text + "\t" + icTimeOutlierTextBox.Text + "\t" + cCycleOutlierTextBox.Text + "\t" + cTimeOutlierTextBox.Text + "\t" + totalCycleOutlierTextBox.Text + "\t" + totalTimeOutlierTextBox.Text);

                    sw.Flush();
                    sw.Close();
                    Cursor.Current = c;
                }
                MessageBox.Show(trafficListView.Items.Count.ToString() + " row(s) were exported to " + saveFileDialog.FileName + ".", "Export complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void resetESP()
        {
            spiTextBox.ForeColor = sequenceNumberTextBox.ForeColor = ivTextBox.ForeColor = macTextBox.ForeColor = Color.Black;

            spiTextBox.Text = "SPI";
            sequenceNumberTextBox.Text = "Sequence Number";
            ivTextBox.Text = "Initialization Vector (IV)";
            macTextBox.Text = "Message Authentication Code (MAC)";

            encryptedPayloadLabel.Text = "Encrypted Payload";
            paddingLabel.ForeColor = Color.Black;
            paddingLengthLabel.ForeColor = Color.Black;
            paddingLabel.Text = "Padding";
            paddingLengthLabel.Text = "Padding Length";
            nextProtocolLabel.Text = "Next Protocol";

            protocolTabControl.SelectTab(0);
        }
        private void resetAH()
        {
            ahSPITextBox.ForeColor = ahSequenceNumberTextBox.ForeColor = ahMacTextBox.ForeColor = Color.Black;

            ahNextProtocolTextBox.Text = "Next Protocol";
            ahPayloadLengthTextBox.Text = "Payload Length";
            ahSPITextBox.Text = "SPI";
            ahSequenceNumberTextBox.Text = "Sequence Number";
            ahMacTextBox.Text = "Message Authentication Code (MAC)";

            protocolTabControl.SelectTab(1);

        }
        private void trafficListView_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListView.SelectedListViewItemCollection lvItems = trafficListView.SelectedItems;
            foreach (ListViewItem lvItem in lvItems)
            {
                ProtectedPacketSentEventArgs packet;
                if (packets.TryGetValue(lvItem.Tag.ToString(), out packet))
                {
                    CHILD_SA_TYPE childSA = SA.GetChildSA(packet.SAKey);

                    if (childSA.Protocol == IPsecLite.IKEv2.IKE_PROTOCOLS.ESP)
                    {
                        resetESP();
                        espIPTextBox.Text = packet.SourceIP + " - " + packet.DestinationIP;

                        switch (packet.ProtectionResult)
                        {
                            case PROTECTION_RESULTS.INVALID_MAC:
                                spiTextBox.Text = Utils.ToHexString(packet.SPI);
                                sequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                macTextBox.Text = Utils.ToHexString(packet.MAC, false, 0);

                                macTextBox.ForeColor = Color.OrangeRed;
                                break;
                            case PROTECTION_RESULTS.REPLAYED:
                                spiTextBox.Text = Utils.ToHexString(packet.SPI);
                                sequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                sequenceNumberTextBox.ForeColor = Color.Red;
                                break;
                            case PROTECTION_RESULTS.TOO_OLD:
                                spiTextBox.Text = Utils.ToHexString(packet.SPI);
                                spiTextBox.ForeColor = Color.Pink;
                                sequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                sequenceNumberTextBox.ForeColor = Color.Pink;
                                break;
                            case PROTECTION_RESULTS.NO_SA:
                                spiTextBox.Text = Utils.ToHexString(packet.SPI);
                                spiTextBox.ForeColor = Color.Coral;
                                break;
                            case PROTECTION_RESULTS.INVALID_PAD:
                                spiTextBox.Text = Utils.ToHexString(packet.SPI);
                                sequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                ivTextBox.Text = Utils.ToHexString(packet.IV, false, 0);
                                paddingLabel.Text = "Invalid Padding";
                                paddingLabel.ForeColor = Color.Chocolate;
                                paddingLengthLabel.Text = packet.PaddingLength.ToString();
                                paddingLengthLabel.ForeColor = Color.Chocolate;
                                nextProtocolLabel.Text = packet.NextProtocol.ToString();
                                macTextBox.Text = Utils.ToHexString(packet.MAC, false, 0);
                                break;

                            case PROTECTION_RESULTS.OK:
                                spiTextBox.Text = Utils.ToHexString(packet.SPI);
                                sequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                ivTextBox.Text = Utils.ToHexString(packet.IV, false, 0);
                                if (packet.PaddingLength > 0)
                                    paddingLabel.Text = " 1-" + packet.PaddingLength.ToString();
                                paddingLengthLabel.Text = packet.PaddingLength.ToString();
                                nextProtocolLabel.Text = packet.NextProtocol.ToString();
                                macTextBox.Text = Utils.ToHexString(packet.MAC, false, 0);
                                encryptedPayloadLabel.Text = packet.NextProtocol.ToString() + " Packet";
                                break;

                        }
                    }
                    else
                    {
                        resetAH();
                        ahIPTextBox.Text = packet.SourceIP + " - " + packet.DestinationIP;
                        switch (packet.ProtectionResult)
                        {
                            case PROTECTION_RESULTS.INVALID_MAC:
                                ahSPITextBox.Text = Utils.ToHexString(packet.SPI);
                                ahSequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                ahMacTextBox.Text = Utils.ToHexString(packet.MAC, false, 0);

                                ahMacTextBox.ForeColor = Color.OrangeRed;
                                break;
                            case PROTECTION_RESULTS.REPLAYED:
                                ahSPITextBox.Text = Utils.ToHexString(packet.SPI);
                                ahSequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                ahSequenceNumberTextBox.ForeColor = Color.Red;
                                break;
                            case PROTECTION_RESULTS.TOO_OLD:
                                ahSPITextBox.Text = Utils.ToHexString(packet.SPI);
                                ahSPITextBox.ForeColor = Color.Pink;
                                ahSequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                ahSequenceNumberTextBox.ForeColor = Color.Pink;
                                break;
                            case PROTECTION_RESULTS.NO_SA:
                                ahSPITextBox.Text = Utils.ToHexString(packet.SPI);
                                ahSPITextBox.ForeColor = Color.Coral;
                                break;
                            case PROTECTION_RESULTS.INVALID_PAD:
                                ahSPITextBox.Text = Utils.ToHexString(packet.SPI);
                                ahSequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                ahNextProtocolTextBox.Text = packet.NextProtocol.ToString();
                                ahMacTextBox.Text = Utils.ToHexString(packet.MAC, false, 0);
                                break;

                            case PROTECTION_RESULTS.OK:
                                ahNextProtocolTextBox.Text = packet.NextProtocol.ToString();
                                ahSPITextBox.Text = Utils.ToHexString(packet.SPI);
                                ahSequenceNumberTextBox.Text = packet.SequenceNumber.ToString();
                                ahNextProtocolTextBox.Text = packet.NextProtocol.ToString();
                                ahMacTextBox.Text = Utils.ToHexString(packet.MAC, false, 0);
                                break;
                        }
                    }
                }
            }
        }
   }
}
