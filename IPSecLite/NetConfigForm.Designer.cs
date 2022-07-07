namespace adabtek.IPsecLite
{
    partial class NetConfigForm
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
            this.ipListComboBox = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.helpLink = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.corruptedPacketPercentTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.packetsLossPercentTextBox = new System.Windows.Forms.MaskedTextBox();
            this.outOfSequencePercentageTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gatewayIPComboBox = new System.Windows.Forms.ComboBox();
            this.peerGatewayIPComboBox = new System.Windows.Forms.ComboBox();
            this.host3IPComboBox = new System.Windows.Forms.ComboBox();
            this.host2IPComboBox = new System.Windows.Forms.ComboBox();
            this.host1IPComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.hostRadioButton = new System.Windows.Forms.RadioButton();
            this.gatewayRadioButton = new System.Windows.Forms.RadioButton();
            this.framedCheckBox = new System.Windows.Forms.CheckBox();
            this.portTextBox = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipListComboBox
            // 
            this.ipListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ipListComboBox.FormattingEnabled = true;
            this.ipListComboBox.Location = new System.Drawing.Point(147, 26);
            this.ipListComboBox.Name = "ipListComboBox";
            this.ipListComboBox.Size = new System.Drawing.Size(97, 21);
            this.ipListComboBox.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.startButton.Location = new System.Drawing.Point(163, 342);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(244, 342);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.portTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ipListComboBox);
            this.groupBox1.Location = new System.Drawing.Point(7, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Real Traffic Endpoint ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(324, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 55;
            this.label8.Text = "Port:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(79, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 54;
            this.label7.Text = "IP Address:";
            // 
            // helpLink
            // 
            this.helpLink.AutoSize = true;
            this.helpLink.Location = new System.Drawing.Point(442, 4);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(35, 13);
            this.helpLink.TabIndex = 7;
            this.helpLink.TabStop = true;
            this.helpLink.Text = "Help?";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.corruptedPacketPercentTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.packetsLossPercentTextBox);
            this.groupBox2.Controls.Add(this.outOfSequencePercentageTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(7, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 71);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Packet Delivery ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(301, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "Packet Corruption:";
            // 
            // corruptedPacketPercentTextBox
            // 
            this.corruptedPacketPercentTextBox.Location = new System.Drawing.Point(270, 20);
            this.corruptedPacketPercentTextBox.Mask = "##";
            this.corruptedPacketPercentTextBox.Name = "corruptedPacketPercentTextBox";
            this.corruptedPacketPercentTextBox.Size = new System.Drawing.Size(29, 20);
            this.corruptedPacketPercentTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(443, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Packet Loss:";
            // 
            // packetsLossPercentTextBox
            // 
            this.packetsLossPercentTextBox.Location = new System.Drawing.Point(413, 19);
            this.packetsLossPercentTextBox.Mask = "##";
            this.packetsLossPercentTextBox.Name = "packetsLossPercentTextBox";
            this.packetsLossPercentTextBox.Size = new System.Drawing.Size(29, 20);
            this.packetsLossPercentTextBox.TabIndex = 2;
            // 
            // outOfSequencePercentageTextBox
            // 
            this.outOfSequencePercentageTextBox.Location = new System.Drawing.Point(106, 20);
            this.outOfSequencePercentageTextBox.Mask = "##";
            this.outOfSequencePercentageTextBox.Name = "outOfSequencePercentageTextBox";
            this.outOfSequencePercentageTextBox.Size = new System.Drawing.Size(29, 20);
            this.outOfSequencePercentageTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Out of Sequence:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gatewayIPComboBox);
            this.groupBox3.Controls.Add(this.peerGatewayIPComboBox);
            this.groupBox3.Controls.Add(this.host3IPComboBox);
            this.groupBox3.Controls.Add(this.host2IPComboBox);
            this.groupBox3.Controls.Add(this.host1IPComboBox);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.hostRadioButton);
            this.groupBox3.Controls.Add(this.gatewayRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(7, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(470, 149);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Network ";
            // 
            // gatewayIPComboBox
            // 
            this.gatewayIPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gatewayIPComboBox.FormattingEnabled = true;
            this.gatewayIPComboBox.Location = new System.Drawing.Point(146, 118);
            this.gatewayIPComboBox.Name = "gatewayIPComboBox";
            this.gatewayIPComboBox.Size = new System.Drawing.Size(98, 21);
            this.gatewayIPComboBox.TabIndex = 17;
            // 
            // peerGatewayIPComboBox
            // 
            this.peerGatewayIPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.peerGatewayIPComboBox.FormattingEnabled = true;
            this.peerGatewayIPComboBox.Location = new System.Drawing.Point(146, 68);
            this.peerGatewayIPComboBox.Name = "peerGatewayIPComboBox";
            this.peerGatewayIPComboBox.Size = new System.Drawing.Size(98, 21);
            this.peerGatewayIPComboBox.TabIndex = 16;
            // 
            // host3IPComboBox
            // 
            this.host3IPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.host3IPComboBox.FormattingEnabled = true;
            this.host3IPComboBox.Location = new System.Drawing.Point(359, 39);
            this.host3IPComboBox.Name = "host3IPComboBox";
            this.host3IPComboBox.Size = new System.Drawing.Size(99, 21);
            this.host3IPComboBox.TabIndex = 15;
            // 
            // host2IPComboBox
            // 
            this.host2IPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.host2IPComboBox.FormattingEnabled = true;
            this.host2IPComboBox.Location = new System.Drawing.Point(254, 39);
            this.host2IPComboBox.Name = "host2IPComboBox";
            this.host2IPComboBox.Size = new System.Drawing.Size(97, 21);
            this.host2IPComboBox.TabIndex = 14;
            // 
            // host1IPComboBox
            // 
            this.host1IPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.host1IPComboBox.FormattingEnabled = true;
            this.host1IPComboBox.Location = new System.Drawing.Point(146, 39);
            this.host1IPComboBox.Name = "host1IPComboBox";
            this.host1IPComboBox.Size = new System.Drawing.Size(98, 21);
            this.host1IPComboBox.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 121);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Gateway IP Address:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(55, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Peer IP Address:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Hosts IP addresses:";
            // 
            // hostRadioButton
            // 
            this.hostRadioButton.AutoSize = true;
            this.hostRadioButton.Checked = true;
            this.hostRadioButton.Location = new System.Drawing.Point(9, 96);
            this.hostRadioButton.Name = "hostRadioButton";
            this.hostRadioButton.Size = new System.Drawing.Size(91, 17);
            this.hostRadioButton.TabIndex = 5;
            this.hostRadioButton.TabStop = true;
            this.hostRadioButton.Text = "Host Instance";
            this.hostRadioButton.UseVisualStyleBackColor = true;
            // 
            // gatewayRadioButton
            // 
            this.gatewayRadioButton.AutoSize = true;
            this.gatewayRadioButton.Location = new System.Drawing.Point(11, 20);
            this.gatewayRadioButton.Name = "gatewayRadioButton";
            this.gatewayRadioButton.Size = new System.Drawing.Size(111, 17);
            this.gatewayRadioButton.TabIndex = 0;
            this.gatewayRadioButton.Text = "Gateway Instance";
            this.gatewayRadioButton.UseVisualStyleBackColor = true;
            // 
            // framedCheckBox
            // 
            this.framedCheckBox.AutoSize = true;
            this.framedCheckBox.Checked = true;
            this.framedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.framedCheckBox.Location = new System.Drawing.Point(7, 312);
            this.framedCheckBox.Name = "framedCheckBox";
            this.framedCheckBox.Size = new System.Drawing.Size(153, 17);
            this.framedCheckBox.TabIndex = 3;
            this.framedCheckBox.Text = "Display windows in a frame";
            this.framedCheckBox.UseVisualStyleBackColor = true;
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(359, 26);
            this.portTextBox.Mask = "#####";
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(54, 20);
            this.portTextBox.TabIndex = 56;
            this.portTextBox.Text = "8080";
            // 
            // NetConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 377);
            this.ControlBox = false;
            this.Controls.Add(this.framedCheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.helpLink);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NetConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IPsecLite Instance Configuration";
            this.Load += new System.EventHandler(this.EndPointForm_Load);
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

        private System.Windows.Forms.ComboBox ipListComboBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel helpLink;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox corruptedPacketPercentTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox packetsLossPercentTextBox;
        private System.Windows.Forms.MaskedTextBox outOfSequencePercentageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton hostRadioButton;
        private System.Windows.Forms.RadioButton gatewayRadioButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox framedCheckBox;
        private System.Windows.Forms.ComboBox host1IPComboBox;
        private System.Windows.Forms.ComboBox peerGatewayIPComboBox;
        private System.Windows.Forms.ComboBox host3IPComboBox;
        private System.Windows.Forms.ComboBox host2IPComboBox;
        private System.Windows.Forms.ComboBox gatewayIPComboBox;
        private System.Windows.Forms.MaskedTextBox portTextBox;
    }
}