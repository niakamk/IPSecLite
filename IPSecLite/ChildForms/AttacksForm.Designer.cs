namespace adabtek.IPsecLite
{
    partial class AttacksForm
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
            this.newSequenceNumberTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.payloadTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.sourceIPMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.helpLink = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.replayButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(801, 153);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " IP Datagram ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Payload:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "IP Header:";
            // 
            // rawMessageTextBox
            // 
            this.rawMessageTextBox.Location = new System.Drawing.Point(9, 72);
            this.rawMessageTextBox.Multiline = true;
            this.rawMessageTextBox.Name = "rawMessageTextBox";
            this.rawMessageTextBox.ReadOnly = true;
            this.rawMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rawMessageTextBox.Size = new System.Drawing.Size(781, 70);
            this.rawMessageTextBox.TabIndex = 0;
            // 
            // ipVersionTextBox
            // 
            this.ipVersionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipVersionTextBox.Location = new System.Drawing.Point(11, 33);
            this.ipVersionTextBox.Name = "ipVersionTextBox";
            this.ipVersionTextBox.ReadOnly = true;
            this.ipVersionTextBox.Size = new System.Drawing.Size(20, 20);
            this.ipVersionTextBox.TabIndex = 3;
            this.ipVersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipHLenTextBox
            // 
            this.ipHLenTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipHLenTextBox.Location = new System.Drawing.Point(30, 33);
            this.ipHLenTextBox.Name = "ipHLenTextBox";
            this.ipHLenTextBox.ReadOnly = true;
            this.ipHLenTextBox.Size = new System.Drawing.Size(20, 20);
            this.ipHLenTextBox.TabIndex = 4;
            this.ipHLenTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipServiceTypeTextBox
            // 
            this.ipServiceTypeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipServiceTypeTextBox.Location = new System.Drawing.Point(49, 33);
            this.ipServiceTypeTextBox.Name = "ipServiceTypeTextBox";
            this.ipServiceTypeTextBox.ReadOnly = true;
            this.ipServiceTypeTextBox.Size = new System.Drawing.Size(40, 20);
            this.ipServiceTypeTextBox.TabIndex = 5;
            this.ipServiceTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipTotalLengthTextBox
            // 
            this.ipTotalLengthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipTotalLengthTextBox.Location = new System.Drawing.Point(88, 33);
            this.ipTotalLengthTextBox.Name = "ipTotalLengthTextBox";
            this.ipTotalLengthTextBox.ReadOnly = true;
            this.ipTotalLengthTextBox.Size = new System.Drawing.Size(79, 20);
            this.ipTotalLengthTextBox.TabIndex = 6;
            this.ipTotalLengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipIdentificationTextBox
            // 
            this.ipIdentificationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipIdentificationTextBox.Location = new System.Drawing.Point(166, 33);
            this.ipIdentificationTextBox.Name = "ipIdentificationTextBox";
            this.ipIdentificationTextBox.ReadOnly = true;
            this.ipIdentificationTextBox.Size = new System.Drawing.Size(80, 20);
            this.ipIdentificationTextBox.TabIndex = 8;
            this.ipIdentificationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipFragmentOffsetTextBox
            // 
            this.ipFragmentOffsetTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipFragmentOffsetTextBox.Location = new System.Drawing.Point(264, 33);
            this.ipFragmentOffsetTextBox.Name = "ipFragmentOffsetTextBox";
            this.ipFragmentOffsetTextBox.ReadOnly = true;
            this.ipFragmentOffsetTextBox.Size = new System.Drawing.Size(60, 20);
            this.ipFragmentOffsetTextBox.TabIndex = 9;
            this.ipFragmentOffsetTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipFlagsTextBox
            // 
            this.ipFlagsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipFlagsTextBox.Location = new System.Drawing.Point(245, 33);
            this.ipFlagsTextBox.Name = "ipFlagsTextBox";
            this.ipFlagsTextBox.ReadOnly = true;
            this.ipFlagsTextBox.Size = new System.Drawing.Size(20, 20);
            this.ipFlagsTextBox.TabIndex = 15;
            this.ipFlagsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipHeaderChecksumTextBox
            // 
            this.ipHeaderChecksumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipHeaderChecksumTextBox.Location = new System.Drawing.Point(400, 33);
            this.ipHeaderChecksumTextBox.Name = "ipHeaderChecksumTextBox";
            this.ipHeaderChecksumTextBox.ReadOnly = true;
            this.ipHeaderChecksumTextBox.Size = new System.Drawing.Size(79, 20);
            this.ipHeaderChecksumTextBox.TabIndex = 10;
            this.ipHeaderChecksumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipDestinationAddressTextBox
            // 
            this.ipDestinationAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipDestinationAddressTextBox.Location = new System.Drawing.Point(633, 33);
            this.ipDestinationAddressTextBox.Name = "ipDestinationAddressTextBox";
            this.ipDestinationAddressTextBox.ReadOnly = true;
            this.ipDestinationAddressTextBox.Size = new System.Drawing.Size(158, 20);
            this.ipDestinationAddressTextBox.TabIndex = 14;
            this.ipDestinationAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipTimeToLiveTextBox
            // 
            this.ipTimeToLiveTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipTimeToLiveTextBox.Location = new System.Drawing.Point(323, 33);
            this.ipTimeToLiveTextBox.Name = "ipTimeToLiveTextBox";
            this.ipTimeToLiveTextBox.ReadOnly = true;
            this.ipTimeToLiveTextBox.Size = new System.Drawing.Size(39, 20);
            this.ipTimeToLiveTextBox.TabIndex = 11;
            this.ipTimeToLiveTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipSourceAddressTextBox
            // 
            this.ipSourceAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipSourceAddressTextBox.Location = new System.Drawing.Point(478, 33);
            this.ipSourceAddressTextBox.Name = "ipSourceAddressTextBox";
            this.ipSourceAddressTextBox.ReadOnly = true;
            this.ipSourceAddressTextBox.Size = new System.Drawing.Size(156, 20);
            this.ipSourceAddressTextBox.TabIndex = 13;
            this.ipSourceAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipProtocolTextBox
            // 
            this.ipProtocolTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipProtocolTextBox.Location = new System.Drawing.Point(361, 33);
            this.ipProtocolTextBox.Name = "ipProtocolTextBox";
            this.ipProtocolTextBox.ReadOnly = true;
            this.ipProtocolTextBox.Size = new System.Drawing.Size(40, 20);
            this.ipProtocolTextBox.TabIndex = 12;
            this.ipProtocolTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // newSequenceNumberTextBox
            // 
            this.newSequenceNumberTextBox.Location = new System.Drawing.Point(155, 29);
            this.newSequenceNumberTextBox.Name = "newSequenceNumberTextBox";
            this.newSequenceNumberTextBox.Size = new System.Drawing.Size(90, 20);
            this.newSequenceNumberTextBox.TabIndex = 43;
            // 
            // sendButton
            // 
            this.sendButton.Enabled = false;
            this.sendButton.Location = new System.Drawing.Point(375, 101);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 42;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.payloadTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.sourceIPMaskedTextBox);
            this.groupBox2.Controls.Add(this.sendButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 253);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(801, 134);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Packet Tampering ";
            // 
            // payloadTextBox
            // 
            this.payloadTextBox.Location = new System.Drawing.Point(136, 48);
            this.payloadTextBox.Multiline = true;
            this.payloadTextBox.Name = "payloadTextBox";
            this.payloadTextBox.Size = new System.Drawing.Size(654, 47);
            this.payloadTextBox.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(71, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "Payload:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Source Address:";
            // 
            // sourceIPMaskedTextBox
            // 
            this.sourceIPMaskedTextBox.Location = new System.Drawing.Point(136, 22);
            this.sourceIPMaskedTextBox.Mask = "###.###.###.###";
            this.sourceIPMaskedTextBox.Name = "sourceIPMaskedTextBox";
            this.sourceIPMaskedTextBox.Size = new System.Drawing.Size(90, 20);
            this.sourceIPMaskedTextBox.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "New Sequence Number:";
            // 
            // helpLink
            // 
            this.helpLink.AutoSize = true;
            this.helpLink.Location = new System.Drawing.Point(777, 9);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(35, 13);
            this.helpLink.TabIndex = 62;
            this.helpLink.TabStop = true;
            this.helpLink.Text = "Help?";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.replayButton);
            this.groupBox3.Controls.Add(this.newSequenceNumberTextBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(13, 179);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(800, 68);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Packet Replay";
            // 
            // replayButton
            // 
            this.replayButton.Enabled = false;
            this.replayButton.Location = new System.Drawing.Point(263, 27);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(75, 23);
            this.replayButton.TabIndex = 48;
            this.replayButton.Text = "Replay";
            this.replayButton.UseVisualStyleBackColor = true;
            this.replayButton.Click += new System.EventHandler(this.replayButton_Click);
            // 
            // AttacksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 422);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.helpLink);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AttacksForm";
            this.Text = "Attacks Simulator Tool";
            this.Load += new System.EventHandler(this.AttacksForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rawMessageTextBox;
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
        private System.Windows.Forms.TextBox newSequenceNumberTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox sourceIPMaskedTextBox;
        private System.Windows.Forms.TextBox payloadTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel helpLink;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button replayButton;
    }
}