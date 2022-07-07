namespace adabtek.IPsecLite
{
    partial class IPTrafficForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rawMessageTextBox = new System.Windows.Forms.TextBox();
            this.ipVersionTextBox = new System.Windows.Forms.TextBox();
            this.ipHLenTextBox = new System.Windows.Forms.TextBox();
            this.ipServiceTypeTextBox = new System.Windows.Forms.TextBox();
            this.ipTotalLengthTextBox = new System.Windows.Forms.TextBox();
            this.ipIdentificationTextBox = new System.Windows.Forms.TextBox();
            this.ipFragmentOffsetTextBox = new System.Windows.Forms.TextBox();
            this.ipFlagsTextBox = new System.Windows.Forms.TextBox();
            this.ipHeaderChecksumTextBox = new System.Windows.Forms.TextBox();
            this.ipDestinationAddressTextBox = new System.Windows.Forms.TextBox();
            this.ipTimeToLiveTextBox = new System.Windows.Forms.TextBox();
            this.ipSourceAddressTextBox = new System.Windows.Forms.TextBox();
            this.ipProtocolTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.networkDataListView = new System.Windows.Forms.ListView();
            this.timeCol = new System.Windows.Forms.ColumnHeader();
            this.ipFromCol = new System.Windows.Forms.ColumnHeader();
            this.ipToCol = new System.Windows.Forms.ColumnHeader();
            this.protocolCol = new System.Windows.Forms.ColumnHeader();
            this.sentToCol = new System.Windows.Forms.ColumnHeader();
            this.messageCol = new System.Windows.Forms.ColumnHeader();
            this.helpLink = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rawMessageTextBox);
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
            this.groupBox1.Location = new System.Drawing.Point(7, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(801, 123);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " IP Datagram ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Payload";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Header";
            // 
            // rawMessageTextBox
            // 
            this.rawMessageTextBox.Location = new System.Drawing.Point(9, 72);
            this.rawMessageTextBox.Multiline = true;
            this.rawMessageTextBox.Name = "rawMessageTextBox";
            this.rawMessageTextBox.ReadOnly = true;
            this.rawMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rawMessageTextBox.Size = new System.Drawing.Size(781, 42);
            this.rawMessageTextBox.TabIndex = 0;
            // 
            // ipVersionTextBox
            // 
            this.ipVersionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipVersionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipVersionTextBox.Location = new System.Drawing.Point(11, 33);
            this.ipVersionTextBox.Name = "ipVersionTextBox";
            this.ipVersionTextBox.ReadOnly = true;
            this.ipVersionTextBox.Size = new System.Drawing.Size(20, 18);
            this.ipVersionTextBox.TabIndex = 3;
            this.ipVersionTextBox.Text = "V";
            this.ipVersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipHLenTextBox
            // 
            this.ipHLenTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipHLenTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipHLenTextBox.Location = new System.Drawing.Point(30, 33);
            this.ipHLenTextBox.Name = "ipHLenTextBox";
            this.ipHLenTextBox.ReadOnly = true;
            this.ipHLenTextBox.Size = new System.Drawing.Size(20, 18);
            this.ipHLenTextBox.TabIndex = 4;
            this.ipHLenTextBox.Text = "HL";
            this.ipHLenTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipServiceTypeTextBox
            // 
            this.ipServiceTypeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipServiceTypeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipServiceTypeTextBox.Location = new System.Drawing.Point(49, 33);
            this.ipServiceTypeTextBox.Name = "ipServiceTypeTextBox";
            this.ipServiceTypeTextBox.ReadOnly = true;
            this.ipServiceTypeTextBox.Size = new System.Drawing.Size(40, 18);
            this.ipServiceTypeTextBox.TabIndex = 5;
            this.ipServiceTypeTextBox.Text = "ST";
            this.ipServiceTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipTotalLengthTextBox
            // 
            this.ipTotalLengthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipTotalLengthTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipTotalLengthTextBox.Location = new System.Drawing.Point(88, 33);
            this.ipTotalLengthTextBox.Name = "ipTotalLengthTextBox";
            this.ipTotalLengthTextBox.ReadOnly = true;
            this.ipTotalLengthTextBox.Size = new System.Drawing.Size(79, 18);
            this.ipTotalLengthTextBox.TabIndex = 6;
            this.ipTotalLengthTextBox.Text = "Total Length";
            this.ipTotalLengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipIdentificationTextBox
            // 
            this.ipIdentificationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipIdentificationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipIdentificationTextBox.Location = new System.Drawing.Point(166, 33);
            this.ipIdentificationTextBox.Name = "ipIdentificationTextBox";
            this.ipIdentificationTextBox.ReadOnly = true;
            this.ipIdentificationTextBox.Size = new System.Drawing.Size(80, 18);
            this.ipIdentificationTextBox.TabIndex = 8;
            this.ipIdentificationTextBox.Text = "Identification";
            this.ipIdentificationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipFragmentOffsetTextBox
            // 
            this.ipFragmentOffsetTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipFragmentOffsetTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipFragmentOffsetTextBox.Location = new System.Drawing.Point(264, 33);
            this.ipFragmentOffsetTextBox.Name = "ipFragmentOffsetTextBox";
            this.ipFragmentOffsetTextBox.ReadOnly = true;
            this.ipFragmentOffsetTextBox.Size = new System.Drawing.Size(60, 18);
            this.ipFragmentOffsetTextBox.TabIndex = 9;
            this.ipFragmentOffsetTextBox.Text = "Frg. Offset";
            this.ipFragmentOffsetTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipFlagsTextBox
            // 
            this.ipFlagsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipFlagsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipFlagsTextBox.Location = new System.Drawing.Point(245, 33);
            this.ipFlagsTextBox.Name = "ipFlagsTextBox";
            this.ipFlagsTextBox.ReadOnly = true;
            this.ipFlagsTextBox.Size = new System.Drawing.Size(20, 18);
            this.ipFlagsTextBox.TabIndex = 15;
            this.ipFlagsTextBox.Text = "F";
            this.ipFlagsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipHeaderChecksumTextBox
            // 
            this.ipHeaderChecksumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipHeaderChecksumTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipHeaderChecksumTextBox.Location = new System.Drawing.Point(400, 33);
            this.ipHeaderChecksumTextBox.Name = "ipHeaderChecksumTextBox";
            this.ipHeaderChecksumTextBox.ReadOnly = true;
            this.ipHeaderChecksumTextBox.Size = new System.Drawing.Size(79, 18);
            this.ipHeaderChecksumTextBox.TabIndex = 10;
            this.ipHeaderChecksumTextBox.Text = "Checksum";
            this.ipHeaderChecksumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipDestinationAddressTextBox
            // 
            this.ipDestinationAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipDestinationAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipDestinationAddressTextBox.Location = new System.Drawing.Point(633, 33);
            this.ipDestinationAddressTextBox.Name = "ipDestinationAddressTextBox";
            this.ipDestinationAddressTextBox.ReadOnly = true;
            this.ipDestinationAddressTextBox.Size = new System.Drawing.Size(158, 18);
            this.ipDestinationAddressTextBox.TabIndex = 14;
            this.ipDestinationAddressTextBox.Text = "Destination IP Address";
            this.ipDestinationAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipTimeToLiveTextBox
            // 
            this.ipTimeToLiveTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipTimeToLiveTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipTimeToLiveTextBox.Location = new System.Drawing.Point(323, 33);
            this.ipTimeToLiveTextBox.Name = "ipTimeToLiveTextBox";
            this.ipTimeToLiveTextBox.ReadOnly = true;
            this.ipTimeToLiveTextBox.Size = new System.Drawing.Size(39, 18);
            this.ipTimeToLiveTextBox.TabIndex = 11;
            this.ipTimeToLiveTextBox.Text = "TTL";
            this.ipTimeToLiveTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipSourceAddressTextBox
            // 
            this.ipSourceAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipSourceAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipSourceAddressTextBox.Location = new System.Drawing.Point(478, 33);
            this.ipSourceAddressTextBox.Name = "ipSourceAddressTextBox";
            this.ipSourceAddressTextBox.ReadOnly = true;
            this.ipSourceAddressTextBox.Size = new System.Drawing.Size(156, 18);
            this.ipSourceAddressTextBox.TabIndex = 13;
            this.ipSourceAddressTextBox.Text = "Source IP Address";
            this.ipSourceAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipProtocolTextBox
            // 
            this.ipProtocolTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipProtocolTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipProtocolTextBox.Location = new System.Drawing.Point(361, 33);
            this.ipProtocolTextBox.Name = "ipProtocolTextBox";
            this.ipProtocolTextBox.ReadOnly = true;
            this.ipProtocolTextBox.Size = new System.Drawing.Size(40, 18);
            this.ipProtocolTextBox.TabIndex = 12;
            this.ipProtocolTextBox.Text = "PRTCL";
            this.ipProtocolTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Network Data";
            // 
            // networkDataListView
            // 
            this.networkDataListView.AutoArrange = false;
            this.networkDataListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeCol,
            this.ipFromCol,
            this.ipToCol,
            this.protocolCol,
            this.sentToCol,
            this.messageCol});
            this.networkDataListView.FullRowSelect = true;
            this.networkDataListView.GridLines = true;
            this.networkDataListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.networkDataListView.Location = new System.Drawing.Point(6, 162);
            this.networkDataListView.MultiSelect = false;
            this.networkDataListView.Name = "networkDataListView";
            this.networkDataListView.ShowGroups = false;
            this.networkDataListView.Size = new System.Drawing.Size(802, 177);
            this.networkDataListView.TabIndex = 37;
            this.networkDataListView.UseCompatibleStateImageBehavior = false;
            this.networkDataListView.View = System.Windows.Forms.View.Details;
            this.networkDataListView.SelectedIndexChanged += new System.EventHandler(this.networkDataListView_SelectedIndexChanged);
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
            // sentToCol
            // 
            this.sentToCol.Text = "Sent To";
            this.sentToCol.Width = 100;
            // 
            // messageCol
            // 
            this.messageCol.Text = "Payload";
            this.messageCol.Width = 600;
            // 
            // helpLink
            // 
            this.helpLink.AutoSize = true;
            this.helpLink.Location = new System.Drawing.Point(774, 4);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(35, 13);
            this.helpLink.TabIndex = 60;
            this.helpLink.TabStop = true;
            this.helpLink.Text = "Help?";
            // 
            // IPTrafficForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 347);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.helpLink);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.networkDataListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "IPTrafficForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "IP Traffic Monitor";
            this.Load += new System.EventHandler(this.IPTrafficForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rawMessageTextBox;
        private System.Windows.Forms.ListView networkDataListView;
        private System.Windows.Forms.ColumnHeader timeCol;
        private System.Windows.Forms.ColumnHeader ipFromCol;
        private System.Windows.Forms.ColumnHeader ipToCol;
        private System.Windows.Forms.ColumnHeader protocolCol;
        private System.Windows.Forms.ColumnHeader messageCol;
        private System.Windows.Forms.TextBox ipVersionTextBox;
        private System.Windows.Forms.TextBox ipHLenTextBox;
        private System.Windows.Forms.TextBox ipServiceTypeTextBox;
        private System.Windows.Forms.TextBox ipTotalLengthTextBox;
        private System.Windows.Forms.TextBox ipIdentificationTextBox;
        private System.Windows.Forms.TextBox ipFragmentOffsetTextBox;
        private System.Windows.Forms.TextBox ipFlagsTextBox;
        private System.Windows.Forms.TextBox ipHeaderChecksumTextBox;
        private System.Windows.Forms.TextBox ipDestinationAddressTextBox;
        private System.Windows.Forms.TextBox ipTimeToLiveTextBox;
        private System.Windows.Forms.TextBox ipSourceAddressTextBox;
        private System.Windows.Forms.TextBox ipProtocolTextBox;
        private System.Windows.Forms.LinkLabel helpLink;
        private System.Windows.Forms.ColumnHeader sentToCol;
    }
}