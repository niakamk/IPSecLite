namespace IPsecLite
{
    partial class IPsecLiteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rawMessageTextBox = new System.Windows.Forms.TextBox();
            this.ipVersionTextBox = new System.Windows.Forms.TextBox();
            this.ipHLenTextBox = new System.Windows.Forms.TextBox();
            this.ipServiceTypeTextBox = new System.Windows.Forms.TextBox();
            this.ipTotalLengthTextBox = new System.Windows.Forms.TextBox();
            this.ipIdentificationTextBox = new System.Windows.Forms.TextBox();
            this.ipFragmentOffsetTextBox = new System.Windows.Forms.TextBox();
            this.ipHeaderChecksumTextBox = new System.Windows.Forms.TextBox();
            this.ipTimeToLiveTextBox = new System.Windows.Forms.TextBox();
            this.ipProtocolTextBox = new System.Windows.Forms.TextBox();
            this.ipSourceAddressTextBox = new System.Windows.Forms.TextBox();
            this.ipDestinationAddressTextBox = new System.Windows.Forms.TextBox();
            this.ipFlagsTextBox = new System.Windows.Forms.TextBox();
            this.udpSourcePortTextBox = new System.Windows.Forms.TextBox();
            this.udpDestinationPortTextBox = new System.Windows.Forms.TextBox();
            this.udpChecksumTextBox = new System.Windows.Forms.TextBox();
            this.udpMessageLengthTextBox = new System.Windows.Forms.TextBox();
            this.ikeMessageTreeView = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.udpHeaderPanel = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.networkDataListView = new System.Windows.Forms.ListView();
            this.timeCol = new System.Windows.Forms.ColumnHeader();
            this.ipFromCol = new System.Windows.Forms.ColumnHeader();
            this.ipToCol = new System.Windows.Forms.ColumnHeader();
            this.protocolCol = new System.Windows.Forms.ColumnHeader();
            this.messageCol = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.ikeTraceTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.spdDHGroupComboBox = new System.Windows.Forms.ComboBox();
            this.spdPRFComboBox = new System.Windows.Forms.ComboBox();
            this.spdIntgComboBox = new System.Windows.Forms.ComboBox();
            this.spdEncryptionComboBox = new System.Windows.Forms.ComboBox();
            this.spdProtocolComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.spdAddButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.spdAddressMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.spdApplicationComboBox = new System.Windows.Forms.ComboBox();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.establishIPsecButton = new System.Windows.Forms.Button();
            this.childSAListView = new System.Windows.Forms.ListView();
            this.childSA1SourceIP = new System.Windows.Forms.ColumnHeader();
            this.childSADestinationIP = new System.Windows.Forms.ColumnHeader();
            this.childSAprotocol = new System.Windows.Forms.ColumnHeader();
            this.childSAEncAlg = new System.Windows.Forms.ColumnHeader();
            this.childSAAuthAlg = new System.Windows.Forms.ColumnHeader();
            this.childSASPI = new System.Windows.Forms.ColumnHeader();
            this.childSASequenceNumber = new System.Windows.Forms.ColumnHeader();
            this.childSAProcessedBytes = new System.Windows.Forms.ColumnHeader();
            this.chidSAProcessCycles = new System.Windows.Forms.ColumnHeader();
            this.childSAProcessTime = new System.Windows.Forms.ColumnHeader();
            this.ikeSASPI = new System.Windows.Forms.ColumnHeader();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.repeatSendingTextBox = new System.Windows.Forms.TextBox();
            this.dataInTraceCopyButton = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.dataOutTraceTextBox = new System.Windows.Forms.TextBox();
            this.useRandomMessageCheckBox = new System.Windows.Forms.CheckBox();
            this.randomLengthTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.sendMessageAddressMaskTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.sendMessageTextBox = new System.Windows.Forms.TextBox();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.spdTab = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.spdListView = new System.Windows.Forms.ListView();
            this.spdDestinationIP = new System.Windows.Forms.ColumnHeader();
            this.spdSPI = new System.Windows.Forms.ColumnHeader();
            this.spdApplication = new System.Windows.Forms.ColumnHeader();
            this.spdProtocol = new System.Windows.Forms.ColumnHeader();
            this.spdConfidentiality = new System.Windows.Forms.ColumnHeader();
            this.spdDataIntegrity = new System.Windows.Forms.ColumnHeader();
            this.spdPRF = new System.Windows.Forms.ColumnHeader();
            this.spdDHGroup = new System.Windows.Forms.ColumnHeader();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.saTab = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.packetTab = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataOutTraceCopyButton = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.dataInTraceTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.receiveMessageAddressMaskTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.receiveMessageTextBox = new System.Windows.Forms.TextBox();
            this.shapeContainer3 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape3 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.groupBox1.SuspendLayout();
            this.udpHeaderPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.spdTab.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.saTab.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.packetTab.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // rawMessageTextBox
            // 
            this.rawMessageTextBox.Location = new System.Drawing.Point(11, 169);
            this.rawMessageTextBox.Multiline = true;
            this.rawMessageTextBox.Name = "rawMessageTextBox";
            this.rawMessageTextBox.ReadOnly = true;
            this.rawMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rawMessageTextBox.Size = new System.Drawing.Size(391, 138);
            this.rawMessageTextBox.TabIndex = 0;
            // 
            // ipVersionTextBox
            // 
            this.ipVersionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipVersionTextBox.Location = new System.Drawing.Point(11, 38);
            this.ipVersionTextBox.Name = "ipVersionTextBox";
            this.ipVersionTextBox.ReadOnly = true;
            this.ipVersionTextBox.Size = new System.Drawing.Size(20, 20);
            this.ipVersionTextBox.TabIndex = 3;
            this.ipVersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipHLenTextBox
            // 
            this.ipHLenTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipHLenTextBox.Location = new System.Drawing.Point(30, 38);
            this.ipHLenTextBox.Name = "ipHLenTextBox";
            this.ipHLenTextBox.ReadOnly = true;
            this.ipHLenTextBox.Size = new System.Drawing.Size(20, 20);
            this.ipHLenTextBox.TabIndex = 4;
            this.ipHLenTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipServiceTypeTextBox
            // 
            this.ipServiceTypeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipServiceTypeTextBox.Location = new System.Drawing.Point(49, 38);
            this.ipServiceTypeTextBox.Name = "ipServiceTypeTextBox";
            this.ipServiceTypeTextBox.ReadOnly = true;
            this.ipServiceTypeTextBox.Size = new System.Drawing.Size(40, 20);
            this.ipServiceTypeTextBox.TabIndex = 5;
            this.ipServiceTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipTotalLengthTextBox
            // 
            this.ipTotalLengthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipTotalLengthTextBox.Location = new System.Drawing.Point(88, 38);
            this.ipTotalLengthTextBox.Name = "ipTotalLengthTextBox";
            this.ipTotalLengthTextBox.ReadOnly = true;
            this.ipTotalLengthTextBox.Size = new System.Drawing.Size(79, 20);
            this.ipTotalLengthTextBox.TabIndex = 6;
            this.ipTotalLengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipIdentificationTextBox
            // 
            this.ipIdentificationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipIdentificationTextBox.Location = new System.Drawing.Point(166, 38);
            this.ipIdentificationTextBox.Name = "ipIdentificationTextBox";
            this.ipIdentificationTextBox.ReadOnly = true;
            this.ipIdentificationTextBox.Size = new System.Drawing.Size(80, 20);
            this.ipIdentificationTextBox.TabIndex = 8;
            this.ipIdentificationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipFragmentOffsetTextBox
            // 
            this.ipFragmentOffsetTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipFragmentOffsetTextBox.Location = new System.Drawing.Point(264, 38);
            this.ipFragmentOffsetTextBox.Name = "ipFragmentOffsetTextBox";
            this.ipFragmentOffsetTextBox.ReadOnly = true;
            this.ipFragmentOffsetTextBox.Size = new System.Drawing.Size(60, 20);
            this.ipFragmentOffsetTextBox.TabIndex = 9;
            this.ipFragmentOffsetTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipHeaderChecksumTextBox
            // 
            this.ipHeaderChecksumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipHeaderChecksumTextBox.Location = new System.Drawing.Point(88, 57);
            this.ipHeaderChecksumTextBox.Name = "ipHeaderChecksumTextBox";
            this.ipHeaderChecksumTextBox.ReadOnly = true;
            this.ipHeaderChecksumTextBox.Size = new System.Drawing.Size(79, 20);
            this.ipHeaderChecksumTextBox.TabIndex = 10;
            this.ipHeaderChecksumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipTimeToLiveTextBox
            // 
            this.ipTimeToLiveTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipTimeToLiveTextBox.Location = new System.Drawing.Point(11, 57);
            this.ipTimeToLiveTextBox.Name = "ipTimeToLiveTextBox";
            this.ipTimeToLiveTextBox.ReadOnly = true;
            this.ipTimeToLiveTextBox.Size = new System.Drawing.Size(39, 20);
            this.ipTimeToLiveTextBox.TabIndex = 11;
            this.ipTimeToLiveTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipProtocolTextBox
            // 
            this.ipProtocolTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipProtocolTextBox.Location = new System.Drawing.Point(49, 57);
            this.ipProtocolTextBox.Name = "ipProtocolTextBox";
            this.ipProtocolTextBox.ReadOnly = true;
            this.ipProtocolTextBox.Size = new System.Drawing.Size(40, 20);
            this.ipProtocolTextBox.TabIndex = 12;
            this.ipProtocolTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipSourceAddressTextBox
            // 
            this.ipSourceAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipSourceAddressTextBox.Location = new System.Drawing.Point(11, 76);
            this.ipSourceAddressTextBox.Name = "ipSourceAddressTextBox";
            this.ipSourceAddressTextBox.ReadOnly = true;
            this.ipSourceAddressTextBox.Size = new System.Drawing.Size(156, 20);
            this.ipSourceAddressTextBox.TabIndex = 13;
            this.ipSourceAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipDestinationAddressTextBox
            // 
            this.ipDestinationAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipDestinationAddressTextBox.Location = new System.Drawing.Point(166, 76);
            this.ipDestinationAddressTextBox.Name = "ipDestinationAddressTextBox";
            this.ipDestinationAddressTextBox.ReadOnly = true;
            this.ipDestinationAddressTextBox.Size = new System.Drawing.Size(158, 20);
            this.ipDestinationAddressTextBox.TabIndex = 14;
            this.ipDestinationAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipFlagsTextBox
            // 
            this.ipFlagsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipFlagsTextBox.Location = new System.Drawing.Point(245, 38);
            this.ipFlagsTextBox.Name = "ipFlagsTextBox";
            this.ipFlagsTextBox.ReadOnly = true;
            this.ipFlagsTextBox.Size = new System.Drawing.Size(20, 20);
            this.ipFlagsTextBox.TabIndex = 15;
            this.ipFlagsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // udpSourcePortTextBox
            // 
            this.udpSourcePortTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udpSourcePortTextBox.Location = new System.Drawing.Point(6, 18);
            this.udpSourcePortTextBox.Name = "udpSourcePortTextBox";
            this.udpSourcePortTextBox.ReadOnly = true;
            this.udpSourcePortTextBox.Size = new System.Drawing.Size(77, 20);
            this.udpSourcePortTextBox.TabIndex = 18;
            this.udpSourcePortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // udpDestinationPortTextBox
            // 
            this.udpDestinationPortTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udpDestinationPortTextBox.Location = new System.Drawing.Point(82, 18);
            this.udpDestinationPortTextBox.Name = "udpDestinationPortTextBox";
            this.udpDestinationPortTextBox.ReadOnly = true;
            this.udpDestinationPortTextBox.Size = new System.Drawing.Size(79, 20);
            this.udpDestinationPortTextBox.TabIndex = 19;
            this.udpDestinationPortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // udpChecksumTextBox
            // 
            this.udpChecksumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udpChecksumTextBox.Location = new System.Drawing.Point(239, 18);
            this.udpChecksumTextBox.Name = "udpChecksumTextBox";
            this.udpChecksumTextBox.ReadOnly = true;
            this.udpChecksumTextBox.Size = new System.Drawing.Size(78, 20);
            this.udpChecksumTextBox.TabIndex = 21;
            this.udpChecksumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // udpMessageLengthTextBox
            // 
            this.udpMessageLengthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udpMessageLengthTextBox.Location = new System.Drawing.Point(160, 18);
            this.udpMessageLengthTextBox.Name = "udpMessageLengthTextBox";
            this.udpMessageLengthTextBox.ReadOnly = true;
            this.udpMessageLengthTextBox.Size = new System.Drawing.Size(80, 20);
            this.udpMessageLengthTextBox.TabIndex = 20;
            this.udpMessageLengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ikeMessageTreeView
            // 
            this.ikeMessageTreeView.Location = new System.Drawing.Point(12, 117);
            this.ikeMessageTreeView.Name = "ikeMessageTreeView";
            this.ikeMessageTreeView.Size = new System.Drawing.Size(331, 252);
            this.ikeMessageTreeView.TabIndex = 33;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.udpHeaderPanel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rawMessageTextBox);
            this.groupBox1.Controls.Add(this.networkDataListView);
            this.groupBox1.Controls.Add(this.ipVersionTextBox);
            this.groupBox1.Controls.Add(this.ipHLenTextBox);
            this.groupBox1.Controls.Add(this.ipServiceTypeTextBox);
            this.groupBox1.Controls.Add(this.ipTotalLengthTextBox);
            this.groupBox1.Controls.Add(this.ipIdentificationTextBox);
            this.groupBox1.Controls.Add(this.ipFragmentOffsetTextBox);
            this.groupBox1.Controls.Add(this.ipFlagsTextBox);
            this.groupBox1.Controls.Add(this.ipHeaderChecksumTextBox);
            this.groupBox1.Controls.Add(this.ipDestinationAddressTextBox);
            this.groupBox1.Controls.Add(this.ipTimeToLiveTextBox);
            this.groupBox1.Controls.Add(this.ipSourceAddressTextBox);
            this.groupBox1.Controls.Add(this.ipProtocolTextBox);
            this.groupBox1.Location = new System.Drawing.Point(724, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 561);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Network Communication ";
            // 
            // udpHeaderPanel
            // 
            this.udpHeaderPanel.Controls.Add(this.udpMessageLengthTextBox);
            this.udpHeaderPanel.Controls.Add(this.label23);
            this.udpHeaderPanel.Controls.Add(this.udpSourcePortTextBox);
            this.udpHeaderPanel.Controls.Add(this.udpDestinationPortTextBox);
            this.udpHeaderPanel.Controls.Add(this.udpChecksumTextBox);
            this.udpHeaderPanel.Location = new System.Drawing.Point(6, 104);
            this.udpHeaderPanel.Name = "udpHeaderPanel";
            this.udpHeaderPanel.Size = new System.Drawing.Size(330, 41);
            this.udpHeaderPanel.TabIndex = 44;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(4, 2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 13);
            this.label23.TabIndex = 43;
            this.label23.Text = "UDP Headers";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Network Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Network Data Content";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(474, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Data Processing Operations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "IP Headers";
            // 
            // networkDataListView
            // 
            this.networkDataListView.AutoArrange = false;
            this.networkDataListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeCol,
            this.ipFromCol,
            this.ipToCol,
            this.protocolCol,
            this.messageCol});
            this.networkDataListView.FullRowSelect = true;
            this.networkDataListView.GridLines = true;
            this.networkDataListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.networkDataListView.Location = new System.Drawing.Point(9, 337);
            this.networkDataListView.MultiSelect = false;
            this.networkDataListView.Name = "networkDataListView";
            this.networkDataListView.ShowGroups = false;
            this.networkDataListView.Size = new System.Drawing.Size(396, 218);
            this.networkDataListView.TabIndex = 37;
            this.networkDataListView.UseCompatibleStateImageBehavior = false;
            this.networkDataListView.View = System.Windows.Forms.View.Details;
            this.networkDataListView.SelectedIndexChanged += new System.EventHandler(this.rawIKEMsgListView_SelectedIndexChanged);
            // 
            // timeCol
            // 
            this.timeCol.Text = "Time";
            this.timeCol.Width = 80;
            // 
            // ipFromCol
            // 
            this.ipFromCol.Text = "From";
            this.ipFromCol.Width = 90;
            // 
            // ipToCol
            // 
            this.ipToCol.Text = "To";
            this.ipToCol.Width = 90;
            // 
            // protocolCol
            // 
            this.protocolCol.Text = "Protocol";
            // 
            // messageCol
            // 
            this.messageCol.Text = "Payload";
            this.messageCol.Width = 600;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "IKEv2 Messages";
            // 
            // ikeTraceTextBox
            // 
            this.ikeTraceTextBox.Location = new System.Drawing.Point(349, 117);
            this.ikeTraceTextBox.Multiline = true;
            this.ikeTraceTextBox.Name = "ikeTraceTextBox";
            this.ikeTraceTextBox.ReadOnly = true;
            this.ikeTraceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ikeTraceTextBox.Size = new System.Drawing.Size(331, 252);
            this.ikeTraceTextBox.TabIndex = 34;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressStatusLabel,
            this.errorStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1147, 22);
            this.statusStrip1.TabIndex = 36;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressStatusLabel
            // 
            this.progressStatusLabel.Name = "progressStatusLabel";
            this.progressStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // errorStatusLabel
            // 
            this.errorStatusLabel.Name = "errorStatusLabel";
            this.errorStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.spdDHGroupComboBox);
            this.groupBox2.Controls.Add(this.spdPRFComboBox);
            this.groupBox2.Controls.Add(this.spdIntgComboBox);
            this.groupBox2.Controls.Add(this.spdEncryptionComboBox);
            this.groupBox2.Controls.Add(this.spdProtocolComboBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.spdAddButton);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.spdAddressMaskedTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.spdApplicationComboBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(680, 137);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Security Policy Rule ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(418, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "DH Group:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(397, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "PRF Function:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(397, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Data Integrity:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(394, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Confidentiality:";
            // 
            // spdDHGroupComboBox
            // 
            this.spdDHGroupComboBox.FormattingEnabled = true;
            this.spdDHGroupComboBox.Items.AddRange(new object[] {
            "GROUP 1: 768   BIT MODP",
            "GROUP 2: 1024 BIT MODP",
            "GROUP 5: 1536 BIT MODP",
            "GROUP 5: 2048 BIT MODP",
            "GROUP 5: 3072 BIT MODP",
            "GROUP 5: 4096 BIT MODP",
            "GROUP 5: 6144 BIT MODP",
            "GROUP 5: 8192 BIT MODP"});
            this.spdDHGroupComboBox.Location = new System.Drawing.Point(489, 107);
            this.spdDHGroupComboBox.Name = "spdDHGroupComboBox";
            this.spdDHGroupComboBox.Size = new System.Drawing.Size(166, 21);
            this.spdDHGroupComboBox.TabIndex = 25;
            // 
            // spdPRFComboBox
            // 
            this.spdPRFComboBox.FormattingEnabled = true;
            this.spdPRFComboBox.Items.AddRange(new object[] {
            "HMAC-SHA1",
            "HMAC-MD5"});
            this.spdPRFComboBox.Location = new System.Drawing.Point(489, 80);
            this.spdPRFComboBox.Name = "spdPRFComboBox";
            this.spdPRFComboBox.Size = new System.Drawing.Size(166, 21);
            this.spdPRFComboBox.TabIndex = 24;
            // 
            // spdIntgComboBox
            // 
            this.spdIntgComboBox.FormattingEnabled = true;
            this.spdIntgComboBox.Items.AddRange(new object[] {
            "HMAC-SHA1-96",
            "HMAC-MD5"});
            this.spdIntgComboBox.Location = new System.Drawing.Point(489, 54);
            this.spdIntgComboBox.Name = "spdIntgComboBox";
            this.spdIntgComboBox.Size = new System.Drawing.Size(166, 21);
            this.spdIntgComboBox.TabIndex = 23;
            // 
            // spdEncryptionComboBox
            // 
            this.spdEncryptionComboBox.AccessibleDescription = "";
            this.spdEncryptionComboBox.FormattingEnabled = true;
            this.spdEncryptionComboBox.Items.AddRange(new object[] {
            "AES-CBC-128",
            "DES-CBC",
            "3DES-CBC"});
            this.spdEncryptionComboBox.Location = new System.Drawing.Point(489, 27);
            this.spdEncryptionComboBox.Name = "spdEncryptionComboBox";
            this.spdEncryptionComboBox.Size = new System.Drawing.Size(166, 21);
            this.spdEncryptionComboBox.TabIndex = 22;
            // 
            // spdProtocolComboBox
            // 
            this.spdProtocolComboBox.FormattingEnabled = true;
            this.spdProtocolComboBox.Items.AddRange(new object[] {
            "ESP"});
            this.spdProtocolComboBox.Location = new System.Drawing.Point(276, 54);
            this.spdProtocolComboBox.Name = "spdProtocolComboBox";
            this.spdProtocolComboBox.Size = new System.Drawing.Size(90, 21);
            this.spdProtocolComboBox.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(212, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Protocol:";
            // 
            // spdAddButton
            // 
            this.spdAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spdAddButton.Location = new System.Drawing.Point(276, 105);
            this.spdAddButton.Name = "spdAddButton";
            this.spdAddButton.Size = new System.Drawing.Size(90, 23);
            this.spdAddButton.TabIndex = 19;
            this.spdAddButton.Text = "Add";
            this.spdAddButton.UseVisualStyleBackColor = true;
            this.spdAddButton.Click += new System.EventHandler(this.spdAddButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Address:";
            // 
            // spdAddressMaskedTextBox
            // 
            this.spdAddressMaskedTextBox.Location = new System.Drawing.Point(82, 30);
            this.spdAddressMaskedTextBox.Mask = "###.###.###.###";
            this.spdAddressMaskedTextBox.Name = "spdAddressMaskedTextBox";
            this.spdAddressMaskedTextBox.Size = new System.Drawing.Size(90, 20);
            this.spdAddressMaskedTextBox.TabIndex = 17;
            this.spdAddressMaskedTextBox.Text = "192168000105";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(196, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Application:";
            // 
            // spdApplicationComboBox
            // 
            this.spdApplicationComboBox.FormattingEnabled = true;
            this.spdApplicationComboBox.Items.AddRange(new object[] {
            "ANY"});
            this.spdApplicationComboBox.Location = new System.Drawing.Point(276, 27);
            this.spdApplicationComboBox.Name = "spdApplicationComboBox";
            this.spdApplicationComboBox.Size = new System.Drawing.Size(90, 21);
            this.spdApplicationComboBox.TabIndex = 0;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(317, 388);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(90, 23);
            this.disconnectButton.TabIndex = 34;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // establishIPsecButton
            // 
            this.establishIPsecButton.Location = new System.Drawing.Point(221, 388);
            this.establishIPsecButton.Name = "establishIPsecButton";
            this.establishIPsecButton.Size = new System.Drawing.Size(90, 23);
            this.establishIPsecButton.TabIndex = 31;
            this.establishIPsecButton.Text = "Connect";
            this.establishIPsecButton.UseVisualStyleBackColor = true;
            this.establishIPsecButton.Click += new System.EventHandler(this.establishIPsecButton_Click);
            // 
            // childSAListView
            // 
            this.childSAListView.AutoArrange = false;
            this.childSAListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.childSA1SourceIP,
            this.childSADestinationIP,
            this.childSAprotocol,
            this.childSAEncAlg,
            this.childSAAuthAlg,
            this.childSASPI,
            this.childSASequenceNumber,
            this.childSAProcessedBytes,
            this.chidSAProcessCycles,
            this.childSAProcessTime,
            this.ikeSASPI});
            this.childSAListView.FullRowSelect = true;
            this.childSAListView.GridLines = true;
            this.childSAListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.childSAListView.Location = new System.Drawing.Point(12, 401);
            this.childSAListView.MultiSelect = false;
            this.childSAListView.Name = "childSAListView";
            this.childSAListView.Size = new System.Drawing.Size(668, 126);
            this.childSAListView.TabIndex = 30;
            this.childSAListView.UseCompatibleStateImageBehavior = false;
            this.childSAListView.View = System.Windows.Forms.View.Details;
            // 
            // childSA1SourceIP
            // 
            this.childSA1SourceIP.Text = "Source IP";
            this.childSA1SourceIP.Width = 90;
            // 
            // childSADestinationIP
            // 
            this.childSADestinationIP.Text = "Destination IP";
            this.childSADestinationIP.Width = 90;
            // 
            // childSAprotocol
            // 
            this.childSAprotocol.Text = "Protocol";
            // 
            // childSAEncAlg
            // 
            this.childSAEncAlg.Text = "Confidentiality";
            this.childSAEncAlg.Width = 100;
            // 
            // childSAAuthAlg
            // 
            this.childSAAuthAlg.Text = "Data Integrity";
            this.childSAAuthAlg.Width = 100;
            // 
            // childSASPI
            // 
            this.childSASPI.Text = "SPI";
            this.childSASPI.Width = 80;
            // 
            // childSASequenceNumber
            // 
            this.childSASequenceNumber.Text = "Sequence";
            this.childSASequenceNumber.Width = 70;
            // 
            // childSAProcessedBytes
            // 
            this.childSAProcessedBytes.Text = "Processed Bytes";
            // 
            // chidSAProcessCycles
            // 
            this.chidSAProcessCycles.Text = "Process Cycles";
            // 
            // childSAProcessTime
            // 
            this.childSAProcessTime.Text = "Process Time";
            // 
            // ikeSASPI
            // 
            this.ikeSASPI.Text = "IKE SA SPI";
            this.ikeSASPI.Width = 100;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.repeatSendingTextBox);
            this.groupBox3.Controls.Add(this.dataInTraceCopyButton);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.dataOutTraceTextBox);
            this.groupBox3.Controls.Add(this.useRandomMessageCheckBox);
            this.groupBox3.Controls.Add(this.randomLengthTextBox);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.sendMessageAddressMaskTextBox);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.sendMessageButton);
            this.groupBox3.Controls.Add(this.sendMessageTextBox);
            this.groupBox3.Location = new System.Drawing.Point(5, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 450);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Send Message ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 198);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(85, 13);
            this.label22.TabIndex = 52;
            this.label22.Text = "Repeat sending:";
            // 
            // repeatSendingTextBox
            // 
            this.repeatSendingTextBox.Location = new System.Drawing.Point(106, 195);
            this.repeatSendingTextBox.Name = "repeatSendingTextBox";
            this.repeatSendingTextBox.Size = new System.Drawing.Size(20, 20);
            this.repeatSendingTextBox.TabIndex = 51;
            this.repeatSendingTextBox.Text = "1";
            // 
            // dataInTraceCopyButton
            // 
            this.dataInTraceCopyButton.Location = new System.Drawing.Point(282, 244);
            this.dataInTraceCopyButton.Name = "dataInTraceCopyButton";
            this.dataInTraceCopyButton.Size = new System.Drawing.Size(44, 23);
            this.dataInTraceCopyButton.TabIndex = 50;
            this.dataInTraceCopyButton.Text = "Copy";
            this.dataInTraceCopyButton.UseVisualStyleBackColor = true;
            this.dataInTraceCopyButton.Click += new System.EventHandler(this.dataInTraceCopyButton_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(6, 255);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 13);
            this.label17.TabIndex = 49;
            this.label17.Text = "Trace:";
            // 
            // dataOutTraceTextBox
            // 
            this.dataOutTraceTextBox.Location = new System.Drawing.Point(7, 271);
            this.dataOutTraceTextBox.Multiline = true;
            this.dataOutTraceTextBox.Name = "dataOutTraceTextBox";
            this.dataOutTraceTextBox.ReadOnly = true;
            this.dataOutTraceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataOutTraceTextBox.Size = new System.Drawing.Size(320, 172);
            this.dataOutTraceTextBox.TabIndex = 42;
            // 
            // useRandomMessageCheckBox
            // 
            this.useRandomMessageCheckBox.AutoSize = true;
            this.useRandomMessageCheckBox.Location = new System.Drawing.Point(84, 49);
            this.useRandomMessageCheckBox.Name = "useRandomMessageCheckBox";
            this.useRandomMessageCheckBox.Size = new System.Drawing.Size(88, 17);
            this.useRandomMessageCheckBox.TabIndex = 48;
            this.useRandomMessageCheckBox.Text = "Use Random";
            this.useRandomMessageCheckBox.UseVisualStyleBackColor = true;
            this.useRandomMessageCheckBox.CheckedChanged += new System.EventHandler(this.useRandomMessageCheckBox_CheckedChanged);
            // 
            // randomLengthTextBox
            // 
            this.randomLengthTextBox.Location = new System.Drawing.Point(211, 47);
            this.randomLengthTextBox.Mask = "####";
            this.randomLengthTextBox.Name = "randomLengthTextBox";
            this.randomLengthTextBox.Size = new System.Drawing.Size(38, 20);
            this.randomLengthTextBox.TabIndex = 47;
            this.randomLengthTextBox.Text = "255";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(178, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 46;
            this.label15.Text = "Size:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(22, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 44;
            this.label14.Text = "Address:";
            // 
            // sendMessageAddressMaskTextBox
            // 
            this.sendMessageAddressMaskTextBox.Location = new System.Drawing.Point(84, 23);
            this.sendMessageAddressMaskTextBox.Mask = "###.###.###.###";
            this.sendMessageAddressMaskTextBox.Name = "sendMessageAddressMaskTextBox";
            this.sendMessageAddressMaskTextBox.Size = new System.Drawing.Size(90, 20);
            this.sendMessageAddressMaskTextBox.TabIndex = 43;
            this.sendMessageAddressMaskTextBox.Text = "192168000105";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(5, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "Message:";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(104, 222);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(90, 23);
            this.sendMessageButton.TabIndex = 41;
            this.sendMessageButton.Text = "Send";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // sendMessageTextBox
            // 
            this.sendMessageTextBox.Location = new System.Drawing.Point(6, 104);
            this.sendMessageTextBox.Multiline = true;
            this.sendMessageTextBox.Name = "sendMessageTextBox";
            this.sendMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sendMessageTextBox.Size = new System.Drawing.Size(320, 84);
            this.sendMessageTextBox.TabIndex = 40;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.mainTabControl.Controls.Add(this.spdTab);
            this.mainTabControl.Controls.Add(this.saTab);
            this.mainTabControl.Controls.Add(this.packetTab);
            this.mainTabControl.ItemSize = new System.Drawing.Size(100, 30);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(706, 603);
            this.mainTabControl.TabIndex = 43;
            // 
            // spdTab
            // 
            this.spdTab.Controls.Add(this.groupBox6);
            this.spdTab.Location = new System.Drawing.Point(4, 34);
            this.spdTab.Name = "spdTab";
            this.spdTab.Padding = new System.Windows.Forms.Padding(3);
            this.spdTab.Size = new System.Drawing.Size(698, 565);
            this.spdTab.TabIndex = 0;
            this.spdTab.Text = "Security Policies";
            this.spdTab.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox2);
            this.groupBox6.Controls.Add(this.spdListView);
            this.groupBox6.Controls.Add(this.disconnectButton);
            this.groupBox6.Controls.Add(this.establishIPsecButton);
            this.groupBox6.Controls.Add(this.shapeContainer1);
            this.groupBox6.Location = new System.Drawing.Point(3, 1);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(692, 540);
            this.groupBox6.TabIndex = 41;
            this.groupBox6.TabStop = false;
            // 
            // spdListView
            // 
            this.spdListView.AutoArrange = false;
            this.spdListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.spdDestinationIP,
            this.spdSPI,
            this.spdApplication,
            this.spdProtocol,
            this.spdConfidentiality,
            this.spdDataIntegrity,
            this.spdPRF,
            this.spdDHGroup});
            this.spdListView.FullRowSelect = true;
            this.spdListView.GridLines = true;
            this.spdListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.spdListView.Location = new System.Drawing.Point(6, 252);
            this.spdListView.MultiSelect = false;
            this.spdListView.Name = "spdListView";
            this.spdListView.Size = new System.Drawing.Size(680, 130);
            this.spdListView.TabIndex = 35;
            this.spdListView.UseCompatibleStateImageBehavior = false;
            this.spdListView.View = System.Windows.Forms.View.Details;
            // 
            // spdDestinationIP
            // 
            this.spdDestinationIP.Text = "Destination IP";
            this.spdDestinationIP.Width = 90;
            // 
            // spdSPI
            // 
            this.spdSPI.Text = "SPI";
            this.spdSPI.Width = 80;
            // 
            // spdApplication
            // 
            this.spdApplication.Text = "Application";
            this.spdApplication.Width = 50;
            // 
            // spdProtocol
            // 
            this.spdProtocol.Text = "Protocol";
            this.spdProtocol.Width = 50;
            // 
            // spdConfidentiality
            // 
            this.spdConfidentiality.Text = "Confidentiality";
            this.spdConfidentiality.Width = 100;
            // 
            // spdDataIntegrity
            // 
            this.spdDataIntegrity.Text = "Data Integrity";
            this.spdDataIntegrity.Width = 100;
            // 
            // spdPRF
            // 
            this.spdPRF.Text = "PRF";
            this.spdPRF.Width = 100;
            // 
            // spdDHGroup
            // 
            this.spdDHGroup.Text = "DH Group";
            this.spdDHGroup.Width = 100;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(686, 521);
            this.shapeContainer1.TabIndex = 41;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.Color.White;
            this.rectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape1.BorderColor = System.Drawing.Color.White;
            this.rectangleShape1.FillColor = System.Drawing.Color.White;
            this.rectangleShape1.FillGradientColor = System.Drawing.Color.White;
            this.rectangleShape1.Location = new System.Drawing.Point(4, 6);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(676, 72);
            // 
            // saTab
            // 
            this.saTab.Controls.Add(this.groupBox7);
            this.saTab.Location = new System.Drawing.Point(4, 34);
            this.saTab.Name = "saTab";
            this.saTab.Padding = new System.Windows.Forms.Padding(3);
            this.saTab.Size = new System.Drawing.Size(698, 565);
            this.saTab.TabIndex = 1;
            this.saTab.Text = "Security Associations";
            this.saTab.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.childSAListView);
            this.groupBox7.Controls.Add(this.ikeTraceTextBox);
            this.groupBox7.Controls.Add(this.ikeMessageTreeView);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.shapeContainer2);
            this.groupBox7.Location = new System.Drawing.Point(3, 1);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(692, 540);
            this.groupBox7.TabIndex = 43;
            this.groupBox7.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(9, 385);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 42;
            this.label21.Text = "Child SAs";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(346, 101);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 13);
            this.label16.TabIndex = 41;
            this.label16.Text = "IKEv2 Trace";
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape2});
            this.shapeContainer2.Size = new System.Drawing.Size(686, 521);
            this.shapeContainer2.TabIndex = 40;
            this.shapeContainer2.TabStop = false;
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.Color.White;
            this.rectangleShape2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape2.BorderColor = System.Drawing.Color.White;
            this.rectangleShape2.FillColor = System.Drawing.Color.White;
            this.rectangleShape2.FillGradientColor = System.Drawing.Color.White;
            this.rectangleShape2.Location = new System.Drawing.Point(4, 6);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(676, 72);
            // 
            // packetTab
            // 
            this.packetTab.Controls.Add(this.groupBox8);
            this.packetTab.Location = new System.Drawing.Point(4, 34);
            this.packetTab.Name = "packetTab";
            this.packetTab.Size = new System.Drawing.Size(698, 565);
            this.packetTab.TabIndex = 2;
            this.packetTab.Text = "Send/Receive Packets";
            this.packetTab.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.groupBox5);
            this.groupBox8.Controls.Add(this.groupBox3);
            this.groupBox8.Controls.Add(this.shapeContainer3);
            this.groupBox8.Location = new System.Drawing.Point(3, 1);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(692, 554);
            this.groupBox8.TabIndex = 44;
            this.groupBox8.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataOutTraceCopyButton);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.dataInTraceTextBox);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.receiveMessageAddressMaskTextBox);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.receiveMessageTextBox);
            this.groupBox5.Location = new System.Drawing.Point(351, 98);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(332, 450);
            this.groupBox5.TabIndex = 43;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Received Message ";
            // 
            // dataOutTraceCopyButton
            // 
            this.dataOutTraceCopyButton.Location = new System.Drawing.Point(282, 242);
            this.dataOutTraceCopyButton.Name = "dataOutTraceCopyButton";
            this.dataOutTraceCopyButton.Size = new System.Drawing.Size(44, 23);
            this.dataOutTraceCopyButton.TabIndex = 51;
            this.dataOutTraceCopyButton.Text = "Copy";
            this.dataOutTraceCopyButton.UseVisualStyleBackColor = true;
            this.dataOutTraceCopyButton.Click += new System.EventHandler(this.dataOutTraceCopyButton_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(6, 253);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(44, 13);
            this.label20.TabIndex = 50;
            this.label20.Text = "Trace:";
            // 
            // dataInTraceTextBox
            // 
            this.dataInTraceTextBox.Location = new System.Drawing.Point(6, 269);
            this.dataInTraceTextBox.Multiline = true;
            this.dataInTraceTextBox.Name = "dataInTraceTextBox";
            this.dataInTraceTextBox.ReadOnly = true;
            this.dataInTraceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataInTraceTextBox.Size = new System.Drawing.Size(320, 172);
            this.dataInTraceTextBox.TabIndex = 45;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(22, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 13);
            this.label18.TabIndex = 44;
            this.label18.Text = "Address:";
            // 
            // receiveMessageAddressMaskTextBox
            // 
            this.receiveMessageAddressMaskTextBox.Location = new System.Drawing.Point(84, 23);
            this.receiveMessageAddressMaskTextBox.Mask = "###.###.###.###";
            this.receiveMessageAddressMaskTextBox.Name = "receiveMessageAddressMaskTextBox";
            this.receiveMessageAddressMaskTextBox.ReadOnly = true;
            this.receiveMessageAddressMaskTextBox.Size = new System.Drawing.Size(90, 20);
            this.receiveMessageAddressMaskTextBox.TabIndex = 43;
            this.receiveMessageAddressMaskTextBox.Text = "192168000105";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 88);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 13);
            this.label19.TabIndex = 42;
            this.label19.Text = "Message:";
            // 
            // receiveMessageTextBox
            // 
            this.receiveMessageTextBox.Location = new System.Drawing.Point(6, 104);
            this.receiveMessageTextBox.Multiline = true;
            this.receiveMessageTextBox.Name = "receiveMessageTextBox";
            this.receiveMessageTextBox.ReadOnly = true;
            this.receiveMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.receiveMessageTextBox.Size = new System.Drawing.Size(320, 84);
            this.receiveMessageTextBox.TabIndex = 40;
            // 
            // shapeContainer3
            // 
            this.shapeContainer3.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer3.Name = "shapeContainer3";
            this.shapeContainer3.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape3});
            this.shapeContainer3.Size = new System.Drawing.Size(686, 535);
            this.shapeContainer3.TabIndex = 44;
            this.shapeContainer3.TabStop = false;
            // 
            // rectangleShape3
            // 
            this.rectangleShape3.BackColor = System.Drawing.Color.White;
            this.rectangleShape3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape3.BorderColor = System.Drawing.Color.White;
            this.rectangleShape3.FillColor = System.Drawing.Color.White;
            this.rectangleShape3.FillGradientColor = System.Drawing.Color.White;
            this.rectangleShape3.Location = new System.Drawing.Point(4, 6);
            this.rectangleShape3.Name = "rectangleShape3";
            this.rectangleShape3.Size = new System.Drawing.Size(676, 72);
            // 
            // IPsecLiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 645);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "IPsecLiteForm";
            this.Text = "IPsecLite";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.udpHeaderPanel.ResumeLayout(false);
            this.udpHeaderPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.spdTab.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.saTab.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.packetTab.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox rawMessageTextBox;
        private System.Windows.Forms.TextBox ipVersionTextBox;
        private System.Windows.Forms.TextBox ipHLenTextBox;
        private System.Windows.Forms.TextBox ipServiceTypeTextBox;
        private System.Windows.Forms.TextBox ipTotalLengthTextBox;
        private System.Windows.Forms.TextBox ipIdentificationTextBox;
        private System.Windows.Forms.TextBox ipFragmentOffsetTextBox;
        private System.Windows.Forms.TextBox ipHeaderChecksumTextBox;
        private System.Windows.Forms.TextBox ipTimeToLiveTextBox;
        private System.Windows.Forms.TextBox ipProtocolTextBox;
        private System.Windows.Forms.TextBox ipSourceAddressTextBox;
        private System.Windows.Forms.TextBox ipDestinationAddressTextBox;
        private System.Windows.Forms.TextBox ipFlagsTextBox;
        private System.Windows.Forms.TextBox udpSourcePortTextBox;
        private System.Windows.Forms.TextBox udpDestinationPortTextBox;
        private System.Windows.Forms.TextBox udpChecksumTextBox;
        private System.Windows.Forms.TextBox udpMessageLengthTextBox;
        private System.Windows.Forms.TreeView ikeMessageTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel errorStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel progressStatusLabel;
        private System.Windows.Forms.TextBox ikeTraceTextBox;
        private System.Windows.Forms.ListView networkDataListView;
        private System.Windows.Forms.ColumnHeader ipFromCol;
        private System.Windows.Forms.ColumnHeader messageCol;
        private System.Windows.Forms.ColumnHeader protocolCol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button spdAddButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox spdAddressMaskedTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox spdApplicationComboBox;
        private System.Windows.Forms.ComboBox spdProtocolComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox spdEncryptionComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox spdDHGroupComboBox;
        private System.Windows.Forms.ComboBox spdPRFComboBox;
        private System.Windows.Forms.ComboBox spdIntgComboBox;
        private System.Windows.Forms.ListView childSAListView;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ColumnHeader childSA1SourceIP;
        private System.Windows.Forms.ColumnHeader childSASPI;
        private System.Windows.Forms.Button establishIPsecButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.TextBox sendMessageTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MaskedTextBox sendMessageAddressMaskTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox randomLengthTextBox;
        private System.Windows.Forms.CheckBox useRandomMessageCheckBox;
        private System.Windows.Forms.ColumnHeader childSADestinationIP;
        private System.Windows.Forms.ColumnHeader childSASequenceNumber;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.ColumnHeader ikeSASPI;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage spdTab;
        private System.Windows.Forms.TabPage saTab;
        private System.Windows.Forms.TabPage packetTab;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.MaskedTextBox receiveMessageAddressMaskTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox receiveMessageTextBox;
        private System.Windows.Forms.TextBox dataOutTraceTextBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListView spdListView;
        private System.Windows.Forms.ColumnHeader spdDestinationIP;
        private System.Windows.Forms.ColumnHeader spdSPI;
        private System.Windows.Forms.ColumnHeader spdApplication;
        private System.Windows.Forms.ColumnHeader spdProtocol;
        private System.Windows.Forms.ColumnHeader spdConfidentiality;
        private System.Windows.Forms.ColumnHeader spdDataIntegrity;
        private System.Windows.Forms.ColumnHeader spdPRF;
        private System.Windows.Forms.ColumnHeader spdDHGroup;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer3;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox dataInTraceTextBox;
        private System.Windows.Forms.ColumnHeader timeCol;
        private System.Windows.Forms.ColumnHeader ipToCol;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button dataInTraceCopyButton;
        private System.Windows.Forms.Button dataOutTraceCopyButton;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox repeatSendingTextBox;
        private System.Windows.Forms.ColumnHeader childSAprotocol;
        private System.Windows.Forms.ColumnHeader childSAEncAlg;
        private System.Windows.Forms.ColumnHeader childSAAuthAlg;
        private System.Windows.Forms.ColumnHeader childSAProcessedBytes;
        private System.Windows.Forms.ColumnHeader chidSAProcessCycles;
        private System.Windows.Forms.ColumnHeader childSAProcessTime;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel udpHeaderPanel;

    }
}

