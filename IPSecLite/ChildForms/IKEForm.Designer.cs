namespace adabtek.IPsecLite
{
    partial class IKEForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.spdDestinationIPTextBox = new System.Windows.Forms.MaskedTextBox();
            this.espICCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.spdSourceIPTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.spdModeComboBox = new System.Windows.Forms.ComboBox();
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
            this.label7 = new System.Windows.Forms.Label();
            this.spdApplicationComboBox = new System.Windows.Forms.ComboBox();
            this.spdListView = new System.Windows.Forms.ListView();
            this.spdDestinationIP = new System.Windows.Forms.ColumnHeader();
            this.spdSPI = new System.Windows.Forms.ColumnHeader();
            this.spdApplication = new System.Windows.Forms.ColumnHeader();
            this.spdMode = new System.Windows.Forms.ColumnHeader();
            this.spdProtocol = new System.Windows.Forms.ColumnHeader();
            this.spdConfidentiality = new System.Windows.Forms.ColumnHeader();
            this.spdDataIntegrity = new System.Windows.Forms.ColumnHeader();
            this.spdPRF = new System.Windows.Forms.ColumnHeader();
            this.spdDHGroup = new System.Windows.Forms.ColumnHeader();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.establishIPsecButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.spdTabPage = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.spdHelpLink = new System.Windows.Forms.LinkLabel();
            this.sadbTabPage = new System.Windows.Forms.TabPage();
            this.sadHelpLink = new System.Windows.Forms.LinkLabel();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ikeTraceTextBox = new System.Windows.Forms.TextBox();
            this.ikeMessageTreeView = new System.Windows.Forms.TreeView();
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
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.spdTabPage.SuspendLayout();
            this.sadbTabPage.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.spdDestinationIPTextBox);
            this.groupBox2.Controls.Add(this.espICCheckBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.spdSourceIPTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.spdModeComboBox);
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
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.spdApplicationComboBox);
            this.groupBox2.Location = new System.Drawing.Point(8, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(881, 137);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Security Policy Rule ";
            // 
            // spdDestinationIPTextBox
            // 
            this.spdDestinationIPTextBox.Location = new System.Drawing.Point(279, 22);
            this.spdDestinationIPTextBox.Mask = "###.###.###.###";
            this.spdDestinationIPTextBox.Name = "spdDestinationIPTextBox";
            this.spdDestinationIPTextBox.Size = new System.Drawing.Size(90, 20);
            this.spdDestinationIPTextBox.TabIndex = 36;
            // 
            // espICCheckBox
            // 
            this.espICCheckBox.AutoSize = true;
            this.espICCheckBox.Location = new System.Drawing.Point(460, 51);
            this.espICCheckBox.Name = "espICCheckBox";
            this.espICCheckBox.Size = new System.Drawing.Size(121, 17);
            this.espICCheckBox.TabIndex = 35;
            this.espICCheckBox.Text = "ESP Integrity Check";
            this.espICCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Source IP:";
            // 
            // spdSourceIPTextBox
            // 
            this.spdSourceIPTextBox.Location = new System.Drawing.Point(77, 28);
            this.spdSourceIPTextBox.Name = "spdSourceIPTextBox";
            this.spdSourceIPTextBox.ReadOnly = true;
            this.spdSourceIPTextBox.Size = new System.Drawing.Size(89, 20);
            this.spdSourceIPTextBox.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(412, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Mode:";
            // 
            // spdModeComboBox
            // 
            this.spdModeComboBox.FormattingEnabled = true;
            this.spdModeComboBox.Items.AddRange(new object[] {
            "Transport",
            "Tunnel"});
            this.spdModeComboBox.Location = new System.Drawing.Point(460, 78);
            this.spdModeComboBox.Name = "spdModeComboBox";
            this.spdModeComboBox.Size = new System.Drawing.Size(90, 21);
            this.spdModeComboBox.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(627, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "DH Group:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(659, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "PRF:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(606, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Data Integrity:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(603, 28);
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
            "GROUP 14: 2048 BIT MODP",
            "GROUP 15: 3072 BIT MODP",
            "GROUP 16: 4096 BIT MODP",
            "GROUP 17: 6144 BIT MODP",
            "GROUP 18: 8192 BIT MODP"});
            this.spdDHGroupComboBox.Location = new System.Drawing.Point(698, 105);
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
            this.spdPRFComboBox.Location = new System.Drawing.Point(698, 78);
            this.spdPRFComboBox.Name = "spdPRFComboBox";
            this.spdPRFComboBox.Size = new System.Drawing.Size(166, 21);
            this.spdPRFComboBox.TabIndex = 24;
            // 
            // spdIntgComboBox
            // 
            this.spdIntgComboBox.FormattingEnabled = true;
            this.spdIntgComboBox.Items.AddRange(new object[] {
            "HMAC-SHA1-96",
            "HMAC-MD5",
            "AES-XCBC-MAC-96"});
            this.spdIntgComboBox.Location = new System.Drawing.Point(698, 52);
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
            this.spdEncryptionComboBox.Location = new System.Drawing.Point(698, 25);
            this.spdEncryptionComboBox.Name = "spdEncryptionComboBox";
            this.spdEncryptionComboBox.Size = new System.Drawing.Size(166, 21);
            this.spdEncryptionComboBox.TabIndex = 22;
            // 
            // spdProtocolComboBox
            // 
            this.spdProtocolComboBox.FormattingEnabled = true;
            this.spdProtocolComboBox.Items.AddRange(new object[] {
            "AH",
            "ESP"});
            this.spdProtocolComboBox.Location = new System.Drawing.Point(460, 27);
            this.spdProtocolComboBox.Name = "spdProtocolComboBox";
            this.spdProtocolComboBox.Size = new System.Drawing.Size(90, 21);
            this.spdProtocolComboBox.TabIndex = 21;
            this.spdProtocolComboBox.SelectedIndexChanged += new System.EventHandler(this.spdProtocolComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(396, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Protocol:";
            // 
            // spdAddButton
            // 
            this.spdAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spdAddButton.Location = new System.Drawing.Point(403, 105);
            this.spdAddButton.Name = "spdAddButton";
            this.spdAddButton.Size = new System.Drawing.Size(90, 23);
            this.spdAddButton.TabIndex = 19;
            this.spdAddButton.Text = "Add/Update";
            this.spdAddButton.UseVisualStyleBackColor = true;
            this.spdAddButton.Click += new System.EventHandler(this.spdAddButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(183, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Destination IP:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(199, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Application:";
            // 
            // spdApplicationComboBox
            // 
            this.spdApplicationComboBox.FormattingEnabled = true;
            this.spdApplicationComboBox.Items.AddRange(new object[] {
            "ANY",
            "ICMP"});
            this.spdApplicationComboBox.Location = new System.Drawing.Point(279, 53);
            this.spdApplicationComboBox.Name = "spdApplicationComboBox";
            this.spdApplicationComboBox.Size = new System.Drawing.Size(90, 21);
            this.spdApplicationComboBox.TabIndex = 0;
            // 
            // spdListView
            // 
            this.spdListView.AutoArrange = false;
            this.spdListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.spdDestinationIP,
            this.spdSPI,
            this.spdApplication,
            this.spdMode,
            this.spdProtocol,
            this.spdConfidentiality,
            this.spdDataIntegrity,
            this.spdPRF,
            this.spdDHGroup});
            this.spdListView.FullRowSelect = true;
            this.spdListView.GridLines = true;
            this.spdListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.spdListView.Location = new System.Drawing.Point(6, 185);
            this.spdListView.MultiSelect = false;
            this.spdListView.Name = "spdListView";
            this.spdListView.Size = new System.Drawing.Size(883, 130);
            this.spdListView.TabIndex = 35;
            this.spdListView.UseCompatibleStateImageBehavior = false;
            this.spdListView.View = System.Windows.Forms.View.Details;
            // 
            // spdDestinationIP
            // 
            this.spdDestinationIP.Text = "Destination IP";
            this.spdDestinationIP.Width = 100;
            // 
            // spdSPI
            // 
            this.spdSPI.Text = "SPI";
            this.spdSPI.Width = 80;
            // 
            // spdApplication
            // 
            this.spdApplication.Text = "Application";
            // 
            // spdMode
            // 
            this.spdMode.Text = "Mode";
            // 
            // spdProtocol
            // 
            this.spdProtocol.Text = "Protocol";
            // 
            // spdConfidentiality
            // 
            this.spdConfidentiality.Text = "Confidentiality";
            this.spdConfidentiality.Width = 130;
            // 
            // spdDataIntegrity
            // 
            this.spdDataIntegrity.Text = "Data Integrity";
            this.spdDataIntegrity.Width = 130;
            // 
            // spdPRF
            // 
            this.spdPRF.Text = "PRF";
            this.spdPRF.Width = 130;
            // 
            // spdDHGroup
            // 
            this.spdDHGroup.Text = "DH Group";
            this.spdDHGroup.Width = 130;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(453, 321);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(90, 23);
            this.disconnectButton.TabIndex = 34;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // establishIPsecButton
            // 
            this.establishIPsecButton.Location = new System.Drawing.Point(357, 321);
            this.establishIPsecButton.Name = "establishIPsecButton";
            this.establishIPsecButton.Size = new System.Drawing.Size(90, 23);
            this.establishIPsecButton.TabIndex = 31;
            this.establishIPsecButton.Text = "Connect";
            this.establishIPsecButton.UseVisualStyleBackColor = true;
            this.establishIPsecButton.Click += new System.EventHandler(this.establishIPsecButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.spdTabPage);
            this.tabControl1.Controls.Add(this.sadbTabPage);
            this.tabControl1.Location = new System.Drawing.Point(2, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(907, 497);
            this.tabControl1.TabIndex = 43;
            // 
            // spdTabPage
            // 
            this.spdTabPage.Controls.Add(this.label3);
            this.spdTabPage.Controls.Add(this.spdListView);
            this.spdTabPage.Controls.Add(this.groupBox2);
            this.spdTabPage.Controls.Add(this.spdHelpLink);
            this.spdTabPage.Controls.Add(this.disconnectButton);
            this.spdTabPage.Controls.Add(this.establishIPsecButton);
            this.spdTabPage.Location = new System.Drawing.Point(4, 25);
            this.spdTabPage.Name = "spdTabPage";
            this.spdTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.spdTabPage.Size = new System.Drawing.Size(899, 468);
            this.spdTabPage.TabIndex = 0;
            this.spdTabPage.Text = "Security Policy Database";
            this.spdTabPage.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Security Policy Rules";
            // 
            // spdHelpLink
            // 
            this.spdHelpLink.AutoSize = true;
            this.spdHelpLink.Location = new System.Drawing.Point(855, 5);
            this.spdHelpLink.Name = "spdHelpLink";
            this.spdHelpLink.Size = new System.Drawing.Size(35, 13);
            this.spdHelpLink.TabIndex = 53;
            this.spdHelpLink.TabStop = true;
            this.spdHelpLink.Text = "Help?";
            // 
            // sadbTabPage
            // 
            this.sadbTabPage.Controls.Add(this.sadHelpLink);
            this.sadbTabPage.Controls.Add(this.label21);
            this.sadbTabPage.Controls.Add(this.groupBox7);
            this.sadbTabPage.Controls.Add(this.childSAListView);
            this.sadbTabPage.Location = new System.Drawing.Point(4, 25);
            this.sadbTabPage.Name = "sadbTabPage";
            this.sadbTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.sadbTabPage.Size = new System.Drawing.Size(899, 468);
            this.sadbTabPage.TabIndex = 1;
            this.sadbTabPage.Text = "Security Association Database";
            this.sadbTabPage.UseVisualStyleBackColor = true;
            // 
            // sadHelpLink
            // 
            this.sadHelpLink.AutoSize = true;
            this.sadHelpLink.Location = new System.Drawing.Point(855, 5);
            this.sadHelpLink.Name = "sadHelpLink";
            this.sadHelpLink.Size = new System.Drawing.Size(35, 13);
            this.sadHelpLink.TabIndex = 54;
            this.sadHelpLink.TabStop = true;
            this.sadHelpLink.Text = "Help?";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(9, 325);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 42;
            this.label21.Text = "Child SAs";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ikeTraceTextBox);
            this.groupBox7.Controls.Add(this.ikeMessageTreeView);
            this.groupBox7.Location = new System.Drawing.Point(6, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(887, 300);
            this.groupBox7.TabIndex = 45;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = " IKE Messages ";
            // 
            // ikeTraceTextBox
            // 
            this.ikeTraceTextBox.Location = new System.Drawing.Point(558, 19);
            this.ikeTraceTextBox.Multiline = true;
            this.ikeTraceTextBox.Name = "ikeTraceTextBox";
            this.ikeTraceTextBox.ReadOnly = true;
            this.ikeTraceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ikeTraceTextBox.Size = new System.Drawing.Size(323, 275);
            this.ikeTraceTextBox.TabIndex = 34;
            // 
            // ikeMessageTreeView
            // 
            this.ikeMessageTreeView.Location = new System.Drawing.Point(6, 19);
            this.ikeMessageTreeView.Name = "ikeMessageTreeView";
            this.ikeMessageTreeView.Size = new System.Drawing.Size(546, 275);
            this.ikeMessageTreeView.TabIndex = 33;
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
            this.childSAListView.Location = new System.Drawing.Point(6, 341);
            this.childSAListView.MultiSelect = false;
            this.childSAListView.Name = "childSAListView";
            this.childSAListView.Size = new System.Drawing.Size(881, 121);
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
            this.childSAProcessedBytes.Text = "Bytes";
            this.childSAProcessedBytes.Width = 80;
            // 
            // chidSAProcessCycles
            // 
            this.chidSAProcessCycles.Text = "Cycles";
            this.chidSAProcessCycles.Width = 80;
            // 
            // childSAProcessTime
            // 
            this.childSAProcessTime.Text = "Time";
            // 
            // ikeSASPI
            // 
            this.ikeSASPI.Text = "IKE SA SPI";
            this.ikeSASPI.Width = 100;
            // 
            // IKEForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 513);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "IKEForm";
            this.Text = "Security Policy Database/Security Association Database";
            this.Load += new System.EventHandler(this.PolicyDatabaseForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.spdTabPage.ResumeLayout(false);
            this.spdTabPage.PerformLayout();
            this.sadbTabPage.ResumeLayout(false);
            this.sadbTabPage.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox spdDHGroupComboBox;
        private System.Windows.Forms.ComboBox spdPRFComboBox;
        private System.Windows.Forms.ComboBox spdIntgComboBox;
        private System.Windows.Forms.ComboBox spdEncryptionComboBox;
        private System.Windows.Forms.ComboBox spdProtocolComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button spdAddButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox spdApplicationComboBox;
        private System.Windows.Forms.ListView spdListView;
        private System.Windows.Forms.ColumnHeader spdDestinationIP;
        private System.Windows.Forms.ColumnHeader spdSPI;
        private System.Windows.Forms.ColumnHeader spdApplication;
        private System.Windows.Forms.ColumnHeader spdProtocol;
        private System.Windows.Forms.ColumnHeader spdConfidentiality;
        private System.Windows.Forms.ColumnHeader spdDataIntegrity;
        private System.Windows.Forms.ColumnHeader spdPRF;
        private System.Windows.Forms.ColumnHeader spdDHGroup;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button establishIPsecButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage spdTabPage;
        private System.Windows.Forms.TabPage sadbTabPage;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ListView childSAListView;
        private System.Windows.Forms.ColumnHeader childSA1SourceIP;
        private System.Windows.Forms.ColumnHeader childSADestinationIP;
        private System.Windows.Forms.ColumnHeader childSAprotocol;
        private System.Windows.Forms.ColumnHeader childSAEncAlg;
        private System.Windows.Forms.ColumnHeader childSAAuthAlg;
        private System.Windows.Forms.ColumnHeader childSASPI;
        private System.Windows.Forms.ColumnHeader childSASequenceNumber;
        private System.Windows.Forms.ColumnHeader childSAProcessedBytes;
        private System.Windows.Forms.ColumnHeader chidSAProcessCycles;
        private System.Windows.Forms.ColumnHeader childSAProcessTime;
        private System.Windows.Forms.ColumnHeader ikeSASPI;
        private System.Windows.Forms.TextBox ikeTraceTextBox;
        private System.Windows.Forms.TreeView ikeMessageTreeView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox spdModeComboBox;
        private System.Windows.Forms.LinkLabel spdHelpLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel sadHelpLink;
        private System.Windows.Forms.ColumnHeader spdMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox spdSourceIPTextBox;
        private System.Windows.Forms.CheckBox espICCheckBox;
        private System.Windows.Forms.MaskedTextBox spdDestinationIPTextBox;
    }
}