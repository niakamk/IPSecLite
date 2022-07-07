namespace adabtek.IPsecLite
{
    partial class UDPTrafficForm
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
            this.helpLink = new System.Windows.Forms.LinkLabel();
            this.sourcePortTextBox = new System.Windows.Forms.TextBox();
            this.destinationPortTextBox = new System.Windows.Forms.TextBox();
            this.networkDataListView = new System.Windows.Forms.ListView();
            this.timeCol = new System.Windows.Forms.ColumnHeader();
            this.ipFromCol = new System.Windows.Forms.ColumnHeader();
            this.ipToCol = new System.Windows.Forms.ColumnHeader();
            this.sourcePortCol = new System.Windows.Forms.ColumnHeader();
            this.destinationPortCol = new System.Windows.Forms.ColumnHeader();
            this.messageCol = new System.Windows.Forms.ColumnHeader();
            this.payloadTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checksumTextBox = new System.Windows.Forms.TextBox();
            this.lengthTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // helpLink
            // 
            this.helpLink.AutoSize = true;
            this.helpLink.Location = new System.Drawing.Point(773, 3);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(35, 13);
            this.helpLink.TabIndex = 61;
            this.helpLink.TabStop = true;
            this.helpLink.Text = "Help?";
            // 
            // sourcePortTextBox
            // 
            this.sourcePortTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourcePortTextBox.Location = new System.Drawing.Point(10, 33);
            this.sourcePortTextBox.Name = "sourcePortTextBox";
            this.sourcePortTextBox.ReadOnly = true;
            this.sourcePortTextBox.Size = new System.Drawing.Size(156, 20);
            this.sourcePortTextBox.TabIndex = 13;
            this.sourcePortTextBox.Text = "Source Port";
            this.sourcePortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // destinationPortTextBox
            // 
            this.destinationPortTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destinationPortTextBox.Location = new System.Drawing.Point(165, 33);
            this.destinationPortTextBox.Name = "destinationPortTextBox";
            this.destinationPortTextBox.ReadOnly = true;
            this.destinationPortTextBox.Size = new System.Drawing.Size(158, 20);
            this.destinationPortTextBox.TabIndex = 14;
            this.destinationPortTextBox.Text = "Destination Port";
            this.destinationPortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // networkDataListView
            // 
            this.networkDataListView.AutoArrange = false;
            this.networkDataListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeCol,
            this.ipFromCol,
            this.ipToCol,
            this.sourcePortCol,
            this.destinationPortCol,
            this.messageCol});
            this.networkDataListView.FullRowSelect = true;
            this.networkDataListView.GridLines = true;
            this.networkDataListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.networkDataListView.Location = new System.Drawing.Point(7, 159);
            this.networkDataListView.MultiSelect = false;
            this.networkDataListView.Name = "networkDataListView";
            this.networkDataListView.ShowGroups = false;
            this.networkDataListView.Size = new System.Drawing.Size(801, 177);
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
            // sourcePortCol
            // 
            this.sourcePortCol.Text = "Source Port";
            this.sourcePortCol.Width = 70;
            // 
            // destinationPortCol
            // 
            this.destinationPortCol.Text = "Destination Port";
            this.destinationPortCol.Width = 70;
            // 
            // messageCol
            // 
            this.messageCol.Text = "Payload";
            this.messageCol.Width = 600;
            // 
            // payloadTextBox
            // 
            this.payloadTextBox.Location = new System.Drawing.Point(9, 72);
            this.payloadTextBox.Multiline = true;
            this.payloadTextBox.Name = "payloadTextBox";
            this.payloadTextBox.ReadOnly = true;
            this.payloadTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.payloadTextBox.Size = new System.Drawing.Size(781, 42);
            this.payloadTextBox.TabIndex = 0;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Payload";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Network Data";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checksumTextBox);
            this.groupBox1.Controls.Add(this.lengthTextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.payloadTextBox);
            this.groupBox1.Controls.Add(this.destinationPortTextBox);
            this.groupBox1.Controls.Add(this.sourcePortTextBox);
            this.groupBox1.Location = new System.Drawing.Point(7, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(801, 124);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " UDP Packet ";
            // 
            // checksumTextBox
            // 
            this.checksumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checksumTextBox.Location = new System.Drawing.Point(477, 33);
            this.checksumTextBox.Name = "checksumTextBox";
            this.checksumTextBox.ReadOnly = true;
            this.checksumTextBox.Size = new System.Drawing.Size(158, 20);
            this.checksumTextBox.TabIndex = 44;
            this.checksumTextBox.Text = "Checksum";
            this.checksumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lengthTextBox
            // 
            this.lengthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lengthTextBox.Location = new System.Drawing.Point(322, 33);
            this.lengthTextBox.Name = "lengthTextBox";
            this.lengthTextBox.ReadOnly = true;
            this.lengthTextBox.Size = new System.Drawing.Size(156, 20);
            this.lengthTextBox.TabIndex = 43;
            this.lengthTextBox.Text = "Message Length";
            this.lengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UDPTrafficForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 343);
            this.Controls.Add(this.helpLink);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.networkDataListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UDPTrafficForm";
            this.Text = "UDP Traffic Monitor";
            this.Load += new System.EventHandler(this.UDPTrafficForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel helpLink;
        private System.Windows.Forms.TextBox sourcePortTextBox;
        private System.Windows.Forms.TextBox destinationPortTextBox;
        private System.Windows.Forms.ListView networkDataListView;
        private System.Windows.Forms.ColumnHeader timeCol;
        private System.Windows.Forms.ColumnHeader ipFromCol;
        private System.Windows.Forms.ColumnHeader ipToCol;
        private System.Windows.Forms.ColumnHeader sourcePortCol;
        private System.Windows.Forms.ColumnHeader messageCol;
        private System.Windows.Forms.TextBox payloadTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox checksumTextBox;
        private System.Windows.Forms.TextBox lengthTextBox;
        private System.Windows.Forms.ColumnHeader destinationPortCol;
    }
}