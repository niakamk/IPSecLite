namespace IPsecLite
{
    partial class ReplayForm
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
            this.changeSNCheckBox = new System.Windows.Forms.CheckBox();
            this.newSequenceNumberTextBox = new System.Windows.Forms.TextBox();
            this.replayButton = new System.Windows.Forms.Button();
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
            this.helpLink = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.changeSNCheckBox);
            this.groupBox1.Controls.Add(this.newSequenceNumberTextBox);
            this.groupBox1.Controls.Add(this.replayButton);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(801, 181);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " IP Datagram ";
            // 
            // changeSNCheckBox
            // 
            this.changeSNCheckBox.AutoSize = true;
            this.changeSNCheckBox.Location = new System.Drawing.Point(9, 149);
            this.changeSNCheckBox.Name = "changeSNCheckBox";
            this.changeSNCheckBox.Size = new System.Drawing.Size(170, 17);
            this.changeSNCheckBox.TabIndex = 44;
            this.changeSNCheckBox.Text = "Change Sequence Number to:";
            this.changeSNCheckBox.UseVisualStyleBackColor = true;
            // 
            // newSequenceNumberTextBox
            // 
            this.newSequenceNumberTextBox.Location = new System.Drawing.Point(185, 148);
            this.newSequenceNumberTextBox.Name = "newSequenceNumberTextBox";
            this.newSequenceNumberTextBox.Size = new System.Drawing.Size(100, 20);
            this.newSequenceNumberTextBox.TabIndex = 43;
            // 
            // replayButton
            // 
            this.replayButton.Enabled = false;
            this.replayButton.Location = new System.Drawing.Point(362, 148);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(75, 23);
            this.replayButton.TabIndex = 42;
            this.replayButton.Text = "Replay";
            this.replayButton.UseVisualStyleBackColor = true;
            this.replayButton.Click += new System.EventHandler(this.replayButton_Click);
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
            // helpLink
            // 
            this.helpLink.AutoSize = true;
            this.helpLink.Location = new System.Drawing.Point(777, 9);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(35, 13);
            this.helpLink.TabIndex = 61;
            this.helpLink.TabStop = true;
            this.helpLink.Text = "Help?";
            // 
            // ReplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 212);
            this.Controls.Add(this.helpLink);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReplayForm";
            this.Text = "Replay Attack Simulator";
            this.Load += new System.EventHandler(this.ReplayForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button replayButton;
        private System.Windows.Forms.TextBox newSequenceNumberTextBox;
        private System.Windows.Forms.CheckBox changeSNCheckBox;
        private System.Windows.Forms.LinkLabel helpLink;

    }
}