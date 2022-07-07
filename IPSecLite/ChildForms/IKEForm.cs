using System;
using System.Collections.Generic;
using System.Windows.Forms;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.IKEExchange;
using adabtek.IPsecLite.IKEv2;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.SADB;
using adabtek.IPsecLite.SPDB;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.Constants;
namespace adabtek.IPsecLite
{
    public partial class IKEForm : Form
    {
        delegate void SPDChangedHandler(SPDChangedEventArgs e);
        delegate void IKESAEstablishedHandler(object sender, IKESAEstablishedEventArgs e);
        delegate void IKEMessageArrivedHandler(object sender, IKEMessageArrivedEventArgs e);
        delegate void IKERawMessageArrivedHandler(object sender, NetworkDataEventArgs e);
        delegate void IKERawMessageSentHandler(object sender, NetworkDataEventArgs e);
        delegate void IKEMessageProcessingStartedHandler(object sender, IKEMessageProcessingStartedEventArgs e);
        delegate void IKEMessageProcessingFinishedHandler(object sender, IKEMessageProcessingFinishedEventArgs e);

        delegate void SADeletedHandler(object sender, long SAKey);
        delegate void ChildSACreatedHandler(ChildSACreatedEventArgs e1, ChildSACreatedEventArgs e2);
        delegate void ChildSADeletedHandler(ChildSADeletedEventArgs e1, ChildSADeletedEventArgs e2);

        delegate void ProtectedPacketArrivedHandler(object sender, ProtectedPacketArrivedEventArgs e);
        delegate void ProtectedPacketSentHandler(object sender, ProtectedPacketSentEventArgs e);

        public IKEForm()
        {
            InitializeComponent();
            this.Text += (APP_CONFIG.IS_GATEWAY ? " Gateway (" + APP_CONFIG.ETHERNET_IP + ") " : " Host (" + APP_CONFIG.ETHERNET_IP + ") with Gateway (" + Utils.ToShortStringIP(APP_CONFIG.GATEWAY_IP) + ")");

        }

        private void spdAddButton_Click(object sender, EventArgs e)
        {
            if (spdDestinationIPTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show(APP_CONST.ENTER_DESTINATION_IP, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            SPDEntry spdEntry = new SPDEntry();
            spdEntry.Key = Utils.MinimizeIPLength(spdDestinationIPTextBox.Text);
            spdEntry.Application = spdApplicationComboBox.Text;
            spdEntry.Mode = (IKE_MODE) spdModeComboBox.SelectedIndex;
            switch (spdProtocolComboBox.Text)
            {
                case "ESP":
                    spdEntry.Protocol = IKE_PROTOCOLS.ESP;
                    if (!espICCheckBox.Checked)
                        spdEntry.Protocol = IKE_PROTOCOLS.ESP_NO_ICV;
                    break;
                case "AH":
                    spdEntry.Protocol = IKE_PROTOCOLS.AH;
                    break;
            }
            switch (spdEncryptionComboBox.Text)
            {
                case "AES-CBC-128":
                    spdEntry.EncrAlg = IKE_ENCR_ALGS.ENCR_AES_CBC;
                    spdEntry.EncrKeyLength = 128;
                    spdEntry.EncrBlockSize = 128;
                    break;
                case "DES-CBC":
                    spdEntry.EncrAlg = IKE_ENCR_ALGS.ENCR_DES;
                    spdEntry.EncrKeyLength = 64;
                    spdEntry.EncrBlockSize = 64;
                    break;
                case "3DES-CBC":
                    spdEntry.EncrAlg = IKE_ENCR_ALGS.ENCR_DES3;
                    spdEntry.EncrKeyLength = 192;
                    spdEntry.EncrBlockSize = 64;
                    break;
            }
            switch (spdIntgComboBox.Text)
            {
                case "HMAC-SHA1-96":
                    spdEntry.IntgAlg = IKE_INTEG_ALGS.AUTH_HMAC_SHA1_96;
                    break;
                case "HMAC-MD5":
                    spdEntry.IntgAlg = IKE_INTEG_ALGS.AUTH_HMAC_MD5_96;
                    break;
                case "AES-XCBC-MAC-96":
                    spdEntry.IntgAlg = IKE_INTEG_ALGS.AUTH_AES_XCBC_96;
                    break;
            }
            switch (spdPRFComboBox.Text)
            {
                case "HMAC-SHA1":
                    spdEntry.PRF = IKE_PRFS.PRF_HMAC_SHA1;
                    break;
                case "HMAC-MD5":
                    spdEntry.PRF = IKE_PRFS.PRF_HMAC_MD5;
                    break;
            }
            switch (spdDHGroupComboBox.Text)
            {
                case "GROUP 1: 768   BIT MODP":
                    spdEntry.DHGroup = IKE_DH_GROUPS.GROUP1_768BIT_MODP;
                    break;
                case "GROUP 2: 1024 BIT MODP":
                    spdEntry.DHGroup = IKE_DH_GROUPS.GROUP2_1024BIT_MODP;
                    break;
                case "GROUP 5: 1536 BIT MODP":
                    spdEntry.DHGroup = IKE_DH_GROUPS.GROUP5_1536BIT_MODP;
                    break;
                case "GROUP 14: 2048 BIT MODP":
                    spdEntry.DHGroup = IKE_DH_GROUPS.GROUP5_2048BIT_MODP;
                    break;
                case "GROUP 15: 3072 BIT MODP":
                    spdEntry.DHGroup = IKE_DH_GROUPS.GROUP5_3072BIT_MODP;
                    break;
                case "GROUP 16: 4096 BIT MODP":
                    spdEntry.DHGroup = IKE_DH_GROUPS.GROUP5_4096BIT_MODP;
                    break;
                case "GROUP 17: 6144 BIT MODP":
                    spdEntry.DHGroup = IKE_DH_GROUPS.GROUP5_6144BIT_MODP;
                    break;
                case "GROUP 18: 8192 BIT MODP":
                    spdEntry.DHGroup = IKE_DH_GROUPS.GROUP5_8192BIT_MODP;
                    break;
            }
            if (spdModeComboBox.SelectedIndex == 0)
                spdEntry.Mode = IKE_MODE.TRANSPORT;
            else
                spdEntry.Mode = IKE_MODE.TUNNEL;
            SPD spd = new SPD();
            spd.AddPolicy(spdEntry);
        }
        void displayNewIKEMessage(object sender, IKESAEstablishedEventArgs e)
        {
            ListViewItem[] spdItems = spdListView.Items.Find(Utils.ToShortStringIP(e.InitiatorIP), false);
            if (spdItems.Length == 0)
                spdItems = spdListView.Items.Find(Utils.ToShortStringIP(e.ResponderIP), false);
            if (spdItems.Length != 0)
            {
                if (e.IsInitiatorSA)
                {
                    spdItems[0].SubItems[1].Text = Utils.ToHexString(e.InitiatorSPI);
                    spdItems[0].SubItems[1].Name = e.InitiatorSPI.ToString();
                }
                else
                {
                    spdItems[0].SubItems[1].Text = Utils.ToHexString(e.ResponderSPI);
                    spdItems[0].SubItems[1].Name = e.ResponderSPI.ToString();

                }
            }
            ikeTraceTextBox.Text = Utils.HoursThroughMilliseconds(e.Time) + " [SA established]\r\n" + ikeTraceTextBox.Text;
        }
        private void PolicyDatabaseForm_Load(object sender, EventArgs e)
        {
            SPD.SPDUpdated += new SPD.ChangedSPDHandler(SPD_SPDUpdated);

            Program.IP.ProtectedPacketArrived += new IP.ProtectedPacketArrivedHandler(IP_ProtectedPacketArrived);
            Program.IP.ProtectedPacketSent += new IP.ProtectedPacketSentHandler(IP_ProtectedPacketSent);

            Program.IKEv2Exchange.IKEMessageSent += new IKEv2Exchange.IKEMessageSentHandler(IKEv2Exchange_IKEMessageSent);
            Program.IKEv2Exchange.IKEMessageArrived += new IKEv2Exchange.IKEMessageArrivedHandler(IKEv2Exchange_IKEMessageArrived);
            Program.IKEv2Exchange.IKEMessageProcessingStarted += new IKEv2Exchange.IKEMessageProcessingStartedHandler(IKEv2Exchange_IKEMessageProcessingStarted);
            Program.IKEv2Exchange.IKEMessageProcessingFinished += new IKEv2Exchange.IKEMessageProcessingFinishedHandler(IKEv2Exchange_IKEMessageProcessingFinished);
            Program.IKEv2Exchange.IKESAEstablished += new IKEv2Exchange.IKESAEstablishedHandler(IKEv2Exchange_IKESAEstablished);
            Program.IKEv2Exchange.IKERawMessageArrived += new IKEv2Exchange.IKERawMessageArrivedHandler(IKEv2Exchange_IKERawMessageArrived);

            SA.ChildSACreated += new SA.ChildSACreatedHandler(SA_ChildSACreated);
            SA.ChildSADeleted += new SA.ChildSADeletedHandler(SA_ChildSADeleted);

            SA.SADeleted += new SA.SADeletedHandler(SA_SADeleted);

            foreach (KeyValuePair<string, SPDEntry> rule in SPD.SPDB)
                SPD_SPDUpdated(new SPDChangedEventArgs(rule.Value, 'A'));

            foreach (KeyValuePair<long, SA> sa in SA.SADB)
            {
                ChildSACreatedEventArgs[] childSAEvents = SA.GetChildSAEvents(sa.Key);
                if (childSAEvents != null)
                    displayNewChildSAs(childSAEvents[0], childSAEvents[1]);
            }

            spdSourceIPTextBox.Text = APP_CONFIG.ETHERNET_IP;

            spdApplicationComboBox.SelectedIndex = 0;
            spdProtocolComboBox.SelectedIndex = 0;
            spdModeComboBox.SelectedIndex = 0;
            spdEncryptionComboBox.SelectedIndex = 0;
            spdIntgComboBox.SelectedIndex = 0;
            spdPRFComboBox.SelectedIndex = 0;
            spdDHGroupComboBox.SelectedIndex = 3;
        }

        void updateSPDRule(object sender, long SPI)
        {
            ListViewItem[] spdItems = spdListView.Items.Find(SPI.ToString(), true);
            if (spdItems.Length > 0)
                if (spdItems[0].SubItems[1].Text.Length > 0)
                {
                    spdItems[0].SubItems[1].Text = "";
                    MessageBox.Show("IKE SA was deleted.", "SA Deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }
        void SA_SADeleted(object sender, long SAKey)
        {
            if (this.InvokeRequired)
                this.Invoke(new SADeletedHandler(updateSPDRule), sender, SAKey);
            else
                updateSPDRule(sender, SAKey);
        }
        void IKEv2Exchange_IKESAEstablished(object sender, IKESAEstablishedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IKESAEstablishedHandler(displayNewIKEMessage), sender, e);
            else
                displayNewIKEMessage(sender, e);
        }
        void SPD_SPDUpdated(SPDChangedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new SPDChangedHandler(displayNewSPDRule), e);
            else
                displayNewSPDRule(e);
        }
        void displayNewSPDRule(SPDChangedEventArgs e)
        {
            switch (e.UpdateType)
            {
                case 'A':
                    {
                        ListViewItem spdItem = new ListViewItem(e.Rule.Key);
                        spdItem.Name = e.Rule.Key;
                        if (e.Rule.SAKey != 0)
                            spdItem.SubItems.Add(Utils.ToHexString(e.Rule.SAKey));
                        else
                            spdItem.SubItems.Add("");
                        spdItem.SubItems.Add(e.Rule.Application);
                        spdItem.SubItems.Add(e.Rule.Mode.ToString());
                        spdItem.SubItems.Add(e.Rule.Protocol == IKE_PROTOCOLS.ESP_NO_ICV ? "ESP-ICV" : e.Rule.Protocol.ToString());
                        spdItem.SubItems.Add(e.Rule.EncrAlg + " (" + e.Rule.EncrKeyLength.ToString() + ")");
                        spdItem.SubItems.Add(e.Rule.IntgAlg.ToString());
                        spdItem.SubItems.Add(e.Rule.PRF.ToString());
                        spdItem.SubItems.Add(e.Rule.DHGroup.ToString());

                        spdListView.Items.Add(spdItem);

                        spdDestinationIPTextBox.Text = "";
                    }
                    break;
                case 'D':
                    {
                        ListViewItem[] spdItems = spdListView.Items.Find(e.Rule.Key, true);
                        spdListView.Items.Remove(spdItems[0]);
                    }
                    break;
                case 'N':
                    {
                        MessageBox.Show("There is an SA associated with this rule.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
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
        private void establishIPsecButton_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection spdItems = spdListView.SelectedItems;
            if (spdItems.Count > 0)
            {
                if (spdItems[0].SubItems[1].Text.Trim().Length == 0)
                {
                    Cursor c = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    tabControl1.SelectTab("sadbTabPage");
                    connect(spdItems[0].Text);
                    Cursor = c;
                }
                else
                    MessageBox.Show("An SA already exists for the selected rule.", "SA Exists!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Please select a destination.", "No Destination!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        void displayProtectedPacketSentMessage(object sender, ProtectedPacketSentEventArgs e)
        {
            ListViewItem[] item = childSAListView.Items.Find(e.SAKey, false);
            if (item.Length > 0)
            {
                CHILD_SA_TYPE childSA = SA.GetChildSA(e.SAKey);
                item[0].SubItems[6].Text = childSA.SequenceNumber.ToString();
                item[0].SubItems[7].Text = ((uint)childSA.TrafficStatistics.ProcessedBytes).ToString();
                item[0].SubItems[8].Text = ((ulong)childSA.TrafficStatistics.ProcessingCycles).ToString();
                item[0].SubItems[9].Text = childSA.TrafficStatistics.ProcessingTime.ToString();

            }
        }
        void IP_ProtectedPacketSent(object sender, ProtectedPacketSentEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new ProtectedPacketSentHandler(displayProtectedPacketSentMessage), sender, e);
            else
                displayProtectedPacketSentMessage(sender, e);
        }
        void displayProtectedPacketArrivedMessage(object sender, ProtectedPacketArrivedEventArgs e)
        {
            ListViewItem[] item = childSAListView.Items.Find(e.SAKey, false);
            if (item.Length > 0)
            {
                CHILD_SA_TYPE childSA = SA.GetChildSA(e.SAKey);
                item[0].SubItems[6].Text = e.SequenceNumber.ToString();
                item[0].SubItems[7].Text = ((uint)childSA.TrafficStatistics.ProcessedBytes).ToString();
                item[0].SubItems[8].Text = ((ulong)childSA.TrafficStatistics.ProcessingCycles).ToString();
                item[0].SubItems[9].Text = childSA.TrafficStatistics.ProcessingTime.ToString();
            }
        }
        void IP_ProtectedPacketArrived(object sender, ProtectedPacketArrivedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new ProtectedPacketArrivedHandler(displayProtectedPacketArrivedMessage), sender, e);
            else
                displayProtectedPacketArrivedMessage(sender, e);
        }
        void SA_ChildSADeleted(object sender, ChildSADeletedEventArgs e1, ChildSADeletedEventArgs e2)
        {
            if (this.InvokeRequired)
                this.Invoke(new ChildSADeletedHandler(updateChildSAs), e1, e2);
            else
                updateChildSAs(e1, e2);
        }
        void updateChildSAs(ChildSADeletedEventArgs e1, ChildSADeletedEventArgs e2)
        {
            ListViewItem[] items1 = childSAListView.Items.Find(e1.ChildSAKey, false);
            if (items1.Length > 0)
                childSAListView.Items.Remove(items1[0]);
            ListViewItem[] items2 = childSAListView.Items.Find(e2.ChildSAKey, false);
            if (items2.Length > 0)
                childSAListView.Items.Remove(items2[0]);
        }

        void displayNewChildSAs(ChildSACreatedEventArgs e1, ChildSACreatedEventArgs e2)
        {
            {
                ListViewItem item = childSAListView.Items.Add(e1.SourceIP);
                item.Name = e1.ChildSAKey;
                item.SubItems.Add(e1.DestinationIP);
                {
                    CHILD_SA_TYPE childSA = SA.GetChildSA(e1.ChildSAKey);
                    item.SubItems.Add(childSA.Protocol.ToString());
                    item.SubItems.Add(childSA.cryptoAlgs.EncrAlg.ToString());
                    item.SubItems.Add(childSA.cryptoAlgs.IntegAlg.ToString());
                    item.SubItems.Add(Utils.ToHexString(childSA.SPI));
                    item.SubItems.Add(childSA.SequenceNumber.ToString());
                    item.SubItems.Add(childSA.TrafficStatistics.ProcessedBytes.ToString());
                    item.SubItems.Add(childSA.TrafficStatistics.ProcessingCycles.ToString());
                    item.SubItems.Add((childSA.TrafficStatistics.ProcessingTime * 1000).ToString());
                    item.SubItems.Add(Utils.ToHexString(e1.IKESASPI));
                }
            }
            {
                ListViewItem item = childSAListView.Items.Add(e2.SourceIP);
                item.Name = e2.ChildSAKey;
                item.SubItems.Add(e2.DestinationIP);
                {
                    CHILD_SA_TYPE childSA = SA.GetChildSA(e2.ChildSAKey);
                    item.SubItems.Add(childSA.Protocol.ToString());
                    item.SubItems.Add(childSA.cryptoAlgs.EncrAlg.ToString());
                    item.SubItems.Add(childSA.cryptoAlgs.IntegAlg.ToString());
                    item.SubItems.Add(Utils.ToHexString(childSA.SPI));
                    item.SubItems.Add(childSA.SequenceNumber.ToString());
                    item.SubItems.Add(childSA.TrafficStatistics.ProcessedBytes.ToString());
                    item.SubItems.Add(childSA.TrafficStatistics.ProcessingCycles.ToString());
                    item.SubItems.Add((childSA.TrafficStatistics.ProcessingTime * 1000).ToString());
                    item.SubItems.Add(Utils.ToHexString(e2.IKESASPI));
                }
            }
        }
        void SA_ChildSACreated(object sender, ChildSACreatedEventArgs e1, ChildSACreatedEventArgs e2)
        {
            if (this.InvokeRequired)
                this.Invoke(new ChildSACreatedHandler(displayNewChildSAs), e1, e2);
            else
                displayNewChildSAs(e1, e2);
        }

        void displayNewIKEMessage(object sender, NetworkDataEventArgs e)
        {
            ListViewItem networkDataLVI = new ListViewItem(Utils.HoursThroughMilliseconds(e.Time));
            networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.SourceIP));
            networkDataLVI.SubItems.Add(Utils.ToShortStringIP(e.DestinationIP));
            networkDataLVI.SubItems.Add("IKE");
            networkDataLVI.SubItems.Add(Utils.ToCharString(e.NetworkData, false, 0));
        }
        void IKEv2Exchange_IKERawMessageArrived(object sender, NetworkDataEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IKERawMessageArrivedHandler(displayNewIKEMessage), sender, e);
            else
                displayNewIKEMessage(sender, e);
        }

        void IKEv2Exchange_IKEMessageSent(object sender, NetworkDataEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IKERawMessageSentHandler(displayNewIKEMessage), sender, e);
            else
                displayNewIKEMessage(sender, e);
        }
        void displayNewIKEMessage(object sender, IKEMessageArrivedEventArgs e)
        {
            TreeNode ikeNode = new TreeNode("IKE Message: " + e.IKEMessage.ExchangeType.ToString());
            ikeMessageTreeView.Nodes.Add(ikeNode);
            ikeNode.Nodes.Add(new TreeNode("Initiator SPI: " + e.IKEMessage.InitiatorSPIToHex));
            ikeNode.Nodes.Add(new TreeNode("Responder SPI: " + e.IKEMessage.ResponderSPIToHex));
            ikeNode.Nodes.Add(new TreeNode("Next Payload: " + e.IKEMessage.NextPayload.ToString()));
            ikeNode.Nodes.Add(new TreeNode("Version: " + ((e.IKEMessage.Version & 0xf0) >> 4).ToString() + "." + (e.IKEMessage.Version & 0x0f).ToString()));
            ikeNode.Nodes.Add(new TreeNode("Exchange Type: " + e.IKEMessage.ExchangeType.ToString()));
            ikeNode.Nodes.Add(new TreeNode("Flags: " + Utils.ToBinary(e.IKEMessage.Flags, 8)));
            ikeNode.Nodes.Add(new TreeNode("Message ID: " + e.IKEMessage.MessageID.ToString()));
            ikeNode.Nodes.Add(new TreeNode("Length: " + e.IKEMessage.Length.ToString()));

            TreeNode payloadsNode = new TreeNode("Payloads: ");
            ikeNode.Nodes.Add(payloadsNode);

            if (e.IKEMessage.NextPayload == IKE_PAYLOADS.ENCR)
                displayPayloads(payloadsNode, e.IKEMessage.EncryptedPayload.InnerPayloads);
            else
                displayPayloads(payloadsNode, e.IKEMessage.Payloads);
        }
        void IKEv2Exchange_IKEMessageArrived(object sender, IKEMessageArrivedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IKEMessageArrivedHandler(displayNewIKEMessage), sender, e);
            else
                displayNewIKEMessage(sender, e);
        }
        void IKEv2ExchangeUpdate(object sender, IKEMessageProcessingStartedEventArgs e)
        {
            ikeTraceTextBox.Text = Utils.HoursThroughMilliseconds(e.Time) + "  " + e.IKEMessageType.ToString() + " [processing started]" + "\r\n" + ikeTraceTextBox.Text;
        }
        void IKEv2ExchangeUpdate(object sender, IKEMessageProcessingFinishedEventArgs e)
        {
            ikeTraceTextBox.Text = Utils.HoursThroughMilliseconds(e.Time) + "  " + e.IKEMessageType.ToString() + " [processing finished]" + "\r\n" + ikeTraceTextBox.Text;
        }
        void IKEv2Exchange_IKEMessageProcessingFinished(object sender, IKEMessageProcessingFinishedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IKEMessageProcessingFinishedHandler(IKEv2ExchangeUpdate), sender, e);
            else
                IKEv2ExchangeUpdate(sender, e);
        }
        void IKEv2Exchange_IKEMessageProcessingStarted(object sender, IKEMessageProcessingStartedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new IKEMessageProcessingStartedHandler(IKEv2ExchangeUpdate), sender, e);
            else
                IKEv2ExchangeUpdate(sender, e);
        }
        void displayPayloads(TreeNode payloadsNode, LinkedList<PAYLOAD_NODE_TYPE> Payloads)
        {
            foreach (PAYLOAD_NODE_TYPE p in Payloads)
            {
                switch (p.PayloadType)
                {
                    case IKE_PAYLOADS.SA:
                        {
                            IKE_SA_Payload saPayload = (IKE_SA_Payload)p.Payload;
                            TreeNode node = new TreeNode(p.PayloadType.ToString());
                            payloadsNode.Nodes.Add(node);

                            if (saPayload.Proposals != null)
                            {
                                foreach (IKE_Proposal proposal in saPayload.Proposals)
                                {
                                    TreeNode proposalNode = new TreeNode("Proposal: " + proposal.Number.ToString());
                                    node.Nodes.Add(proposalNode);
                                    proposalNode.Nodes.Add(new TreeNode("Length: " + proposal.Length.ToString()));
                                    proposalNode.Nodes.Add(new TreeNode("Protocol: " + proposal.ProtocolID.ToString()));
                                    if ((proposal.ProtocolID == IKE_PROTOCOLS.ESP) || (proposal.ProtocolID == IKE_PROTOCOLS.AH))
                                        proposalNode.Nodes.Add(new TreeNode("SPI: " + proposal.SPIHex));
                                    TreeNode transformsNode = new TreeNode("Transforms: " + proposal.Transforms.Length.ToString());
                                    proposalNode.Nodes.Add(transformsNode);
                                    if (proposal.Transforms != null)
                                    {
                                        string nodeText = "";
                                        foreach (IKE_Transform transform in proposal.Transforms)
                                        {
                                            nodeText = transform.TransformType.ToString();
                                            switch (transform.TransformType)
                                            {
                                                case IKE_TRANSFORM_TYPES.DH:
                                                    switch (transform.TransformID)
                                                    {
                                                        case 1:
                                                            nodeText += ": Group 1 (768 bit MODP)";
                                                            break;
                                                        case 2:
                                                            nodeText += ": Group 2 (1024 bit MODP)";
                                                            break;
                                                        case 5:
                                                            nodeText += ": Group 5 (1536 bit MODP)";
                                                            break;
                                                        case 14:
                                                            nodeText += ": Group 14 (2048 bit MODP)";
                                                            break;
                                                        case 15:
                                                            nodeText += ": Group 15 (3072 bit MODP)";
                                                            break;
                                                        case 16:
                                                            nodeText += ": Group 16 (4096 bit MODP)";
                                                            break;
                                                        case 17:
                                                            nodeText += ": Group 17 (6144 bit MODP)";
                                                            break;
                                                        case 18:
                                                            nodeText += ": Group 18 (8192 bit MODP)";
                                                            break;
                                                    }
                                                    break;
                                                case IKE_TRANSFORM_TYPES.ENCR:
                                                    nodeText += ": " + ((IKE_ENCR_ALGS) transform.TransformID).ToString();
                                                    break;
                                                case IKE_TRANSFORM_TYPES.INTEG:
                                                    nodeText += ": " + ((IKE_INTEG_ALGS)transform.TransformID).ToString();
                                                    break;
                                                case IKE_TRANSFORM_TYPES.PRF:
                                                    nodeText += ": " + ((IKE_PRFS)transform.TransformID).ToString();
                                                    break;
                                            }
                                            TreeNode transformNode = new TreeNode(nodeText);
                                            transformsNode.Nodes.Add(transformNode);

                                            if (transform.Attributes != null)
                                            {
                                                foreach (IKE_Transform_Attribute attrribute in transform.Attributes)
                                                {
                                                    switch ((IKE_ATTRIBUTE_TYPES)attrribute.AttributeType)
                                                    {
                                                        case IKE_ATTRIBUTE_TYPES.KEY_LENGTH:
                                                            nodeText = "Key Length: " + Utils.BytesToShort(attrribute.Value[0], attrribute.Value[1]).ToString(); break;
                                                        case IKE_ATTRIBUTE_TYPES.BLOCK_SIZE:
                                                            nodeText = "Block Size: " + Utils.BytesToShort(attrribute.Value[0], attrribute.Value[1]).ToString();
                                                            break;
                                                    }
                                                    transformNode.Nodes.Add(nodeText);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case IKE_PAYLOADS.KE:
                        {
                            IKE_KE_Payload payload = (IKE_KE_Payload)p.Payload;

                            TreeNode node = new TreeNode(p.PayloadType.ToString());
                            payloadsNode.Nodes.Add(node);

                            node.Nodes.Add("Length: " + payload.Length.ToString());
                            node.Nodes.Add("DH Group: " + payload.DHGroup.ToString());
                        }
                        break;
                    case IKE_PAYLOADS.N:
                        {
                            IKE_Nonce_Payload payload = (IKE_Nonce_Payload)p.Payload;
                            TreeNode node = new TreeNode(p.PayloadType.ToString());
                            payloadsNode.Nodes.Add(node);
                            node.Nodes.Add("Length: " + payload.Length.ToString());
                        }
                        break;
                    case IKE_PAYLOADS.IDi:
                        {
                            IKE_Identification_Payload payload = (IKE_Identification_Payload)p.Payload;
                            TreeNode node = new TreeNode(p.PayloadType.ToString());
                            payloadsNode.Nodes.Add(node);
                            node.Nodes.Add("Length: " + payload.Length.ToString());
                        }
                        break;
                    case IKE_PAYLOADS.IDr:
                        {
                            IKE_Identification_Payload payload = (IKE_Identification_Payload)p.Payload;
                            TreeNode node = new TreeNode(p.PayloadType.ToString());
                            payloadsNode.Nodes.Add(node);
                            node.Nodes.Add("Length: " + payload.Length.ToString());
                        }
                        break;
                    case IKE_PAYLOADS.AUTH:
                        {
                            IKE_Auth_Payload payload = (IKE_Auth_Payload)p.Payload;
                            TreeNode node = new TreeNode(p.PayloadType.ToString());
                            payloadsNode.Nodes.Add(node);
                            node.Nodes.Add("Length: " + payload.Length.ToString());
                        }
                        break;
                    case IKE_PAYLOADS.DEL:
                        {
                            IKE_Delete_Payload payload = (IKE_Delete_Payload)p.Payload;
                            TreeNode node = new TreeNode(p.PayloadType.ToString());
                            payloadsNode.Nodes.Add(node);
                            node.Nodes.Add("Length: " + payload.Length.ToString());
                        }
                        break;
                }
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection spdItems = spdListView.SelectedItems;
            if (spdItems.Count > 0)
                Program.IKEv2Exchange.Disconnect(long.Parse(spdItems[0].SubItems[1].Name));
            else
                MessageBox.Show("Please select a destination.", "No Destination!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void spdProtocolComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            espICCheckBox.Enabled = (spdProtocolComboBox.SelectedIndex == 1);
            espICCheckBox.Checked = (spdProtocolComboBox.SelectedIndex == 1);
        }


    }
}
