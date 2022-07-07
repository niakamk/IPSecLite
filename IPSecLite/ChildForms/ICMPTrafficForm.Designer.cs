namespace adabtek.IPsecLite
{
    partial class ICMPTrafficForm
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.corruptedPacketPercentTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.packetsLossPercentTextBox = new System.Windows.Forms.MaskedTextBox();
            this.outOfSequencePercentageTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.traceTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.destinationIPTextBox = new System.Windows.Forms.MaskedTextBox();
            this.useRandomMessageCheckBox = new System.Windows.Forms.CheckBox();
            this.randomLengthTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.sendMessageTextBox = new System.Windows.Forms.TextBox();
            this.helpLink = new System.Windows.Forms.LinkLabel();
            this.repeatTextbox = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.corruptedPacketPercentTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.packetsLossPercentTextBox);
            this.groupBox1.Controls.Add(this.outOfSequencePercentageTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 51);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Packet Delivery ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(322, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "Packet Corruption:";
            // 
            // corruptedPacketPercentTextBox
            // 
            this.corruptedPacketPercentTextBox.Location = new System.Drawing.Point(294, 16);
            this.corruptedPacketPercentTextBox.Mask = "00";
            this.corruptedPacketPercentTextBox.Name = "corruptedPacketPercentTextBox";
            this.corruptedPacketPercentTextBox.Size = new System.Drawing.Size(29, 20);
            this.corruptedPacketPercentTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(486, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Packet Loss:";
            // 
            // packetsLossPercentTextBox
            // 
            this.packetsLossPercentTextBox.Location = new System.Drawing.Point(459, 16);
            this.packetsLossPercentTextBox.Mask = "00";
            this.packetsLossPercentTextBox.Name = "packetsLossPercentTextBox";
            this.packetsLossPercentTextBox.Size = new System.Drawing.Size(29, 20);
            this.packetsLossPercentTextBox.TabIndex = 2;
            // 
            // outOfSequencePercentageTextBox
            // 
            this.outOfSequencePercentageTextBox.Location = new System.Drawing.Point(106, 16);
            this.outOfSequencePercentageTextBox.Mask = "00";
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
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(33, 175);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(45, 13);
            this.label22.TabIndex = 52;
            this.label22.Text = "Repeat:";
            // 
            // traceTextBox
            // 
            this.traceTextBox.Location = new System.Drawing.Point(12, 300);
            this.traceTextBox.Multiline = true;
            this.traceTextBox.Name = "traceTextBox";
            this.traceTextBox.ReadOnly = true;
            this.traceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.traceTextBox.Size = new System.Drawing.Size(527, 88);
            this.traceTextBox.TabIndex = 45;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(12, 284);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(44, 13);
            this.label20.TabIndex = 50;
            this.label20.Text = "Trace:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.repeatTextbox);
            this.groupBox3.Controls.Add(this.destinationIPTextBox);
            this.groupBox3.Controls.Add(this.useRandomMessageCheckBox);
            this.groupBox3.Controls.Add(this.randomLengthTextBox);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.sendMessageButton);
            this.groupBox3.Controls.Add(this.sendMessageTextBox);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Location = new System.Drawing.Point(12, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(527, 208);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Ping Request  ";
            // 
            // destinationIPTextBox
            // 
            this.destinationIPTextBox.Location = new System.Drawing.Point(84, 20);
            this.destinationIPTextBox.Mask = "###.###.###.###";
            this.destinationIPTextBox.Name = "destinationIPTextBox";
            this.destinationIPTextBox.Size = new System.Drawing.Size(88, 20);
            this.destinationIPTextBox.TabIndex = 3;
            // 
            // useRandomMessageCheckBox
            // 
            this.useRandomMessageCheckBox.AutoSize = true;
            this.useRandomMessageCheckBox.Location = new System.Drawing.Point(311, 22);
            this.useRandomMessageCheckBox.Name = "useRandomMessageCheckBox";
            this.useRandomMessageCheckBox.Size = new System.Drawing.Size(143, 17);
            this.useRandomMessageCheckBox.TabIndex = 4;
            this.useRandomMessageCheckBox.Text = "Use random data of size:";
            this.useRandomMessageCheckBox.UseVisualStyleBackColor = true;
            this.useRandomMessageCheckBox.CheckedChanged += new System.EventHandler(this.useRandomMessageCheckBox_CheckedChanged);
            // 
            // randomLengthTextBox
            // 
            this.randomLengthTextBox.Location = new System.Drawing.Point(459, 20);
            this.randomLengthTextBox.Mask = "####";
            this.randomLengthTextBox.Name = "randomLengthTextBox";
            this.randomLengthTextBox.Size = new System.Drawing.Size(38, 20);
            this.randomLengthTextBox.TabIndex = 5;
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(22, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "Data:";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(214, 172);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(90, 23);
            this.sendMessageButton.TabIndex = 8;
            this.sendMessageButton.Text = "Send";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // sendMessageTextBox
            // 
            this.sendMessageTextBox.Location = new System.Drawing.Point(25, 73);
            this.sendMessageTextBox.Multiline = true;
            this.sendMessageTextBox.Name = "sendMessageTextBox";
            this.sendMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sendMessageTextBox.Size = new System.Drawing.Size(486, 84);
            this.sendMessageTextBox.TabIndex = 6;
            // 
            // helpLink
            // 
            this.helpLink.AutoSize = true;
            this.helpLink.Location = new System.Drawing.Point(504, 5);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(35, 13);
            this.helpLink.TabIndex = 52;
            this.helpLink.TabStop = true;
            this.helpLink.Text = "Help?";
            // 
            // repeatTextbox
            // 
            this.repeatTextbox.Location = new System.Drawing.Point(84, 172);
            this.repeatTextbox.Mask = "####";
            this.repeatTextbox.Name = "repeatTextbox";
            this.repeatTextbox.Size = new System.Drawing.Size(37, 20);
            this.repeatTextbox.TabIndex = 53;
            this.repeatTextbox.Text = "1";
            // 
            // ICMPTrafficForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(551, 401);
            this.Controls.Add(this.helpLink);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.traceTextBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "ICMPTrafficForm";
            this.Text = " Network Traffic Generator Tool";
            this.Load += new System.EventHandler(this.ICMPTrafficForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox traceTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox useRandomMessageCheckBox;
        private System.Windows.Forms.MaskedTextBox randomLengthTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.TextBox sendMessageTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox packetsLossPercentTextBox;
        private System.Windows.Forms.MaskedTextBox outOfSequencePercentageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox corruptedPacketPercentTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel helpLink;
        private System.Windows.Forms.MaskedTextBox destinationIPTextBox;
        private System.Windows.Forms.MaskedTextBox repeatTextbox;
    }
}