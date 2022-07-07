namespace adabtek.IPsecLite
{
    partial class IPsecLiteMasterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.iPDatagramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uDPPacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.iPsecPacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPsecPacketsOutgoingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spdMenuPad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iCMPPingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.packetsReplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetsTamperingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPsecLitesWebPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutIPsecLiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.spdMenuPad,
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.helpToolStripMenuItem,
            this.exitsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iPDatagramsToolStripMenuItem,
            this.uDPPacketsToolStripMenuItem,
            this.toolStripSeparator1,
            this.iPsecPacketsToolStripMenuItem,
            this.iPsecPacketsOutgoingToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(104, 20);
            this.toolStripMenuItem4.Text = "&Traffic Monitors";
            // 
            // iPDatagramsToolStripMenuItem
            // 
            this.iPDatagramsToolStripMenuItem.Name = "iPDatagramsToolStripMenuItem";
            this.iPDatagramsToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.iPDatagramsToolStripMenuItem.Text = "I&P Datagrams";
            this.iPDatagramsToolStripMenuItem.Click += new System.EventHandler(this.iPDatagramsToolStripMenuItem_Click);
            // 
            // uDPPacketsToolStripMenuItem
            // 
            this.uDPPacketsToolStripMenuItem.Name = "uDPPacketsToolStripMenuItem";
            this.uDPPacketsToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.uDPPacketsToolStripMenuItem.Text = "&UDP Packets";
            this.uDPPacketsToolStripMenuItem.Click += new System.EventHandler(this.uDPPacketsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(227, 6);
            // 
            // iPsecPacketsToolStripMenuItem
            // 
            this.iPsecPacketsToolStripMenuItem.Name = "iPsecPacketsToolStripMenuItem";
            this.iPsecPacketsToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.iPsecPacketsToolStripMenuItem.Text = "Protected Packets (&Incoming)";
            this.iPsecPacketsToolStripMenuItem.Click += new System.EventHandler(this.iPsecPacketsToolStripMenuItem_Click);
            // 
            // iPsecPacketsOutgoingToolStripMenuItem
            // 
            this.iPsecPacketsOutgoingToolStripMenuItem.Name = "iPsecPacketsOutgoingToolStripMenuItem";
            this.iPsecPacketsOutgoingToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.iPsecPacketsOutgoingToolStripMenuItem.Text = "Protected Packets (&Outgoing)";
            this.iPsecPacketsOutgoingToolStripMenuItem.Click += new System.EventHandler(this.iPsecPacketsOutgoingToolStripMenuItem_Click);
            // 
            // spdMenuPad
            // 
            this.spdMenuPad.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.spdMenuPad.Name = "spdMenuPad";
            this.spdMenuPad.Size = new System.Drawing.Size(151, 20);
            this.spdMenuPad.Text = "&Policies and Associations";
            this.spdMenuPad.Click += new System.EventHandler(this.spdMenuPad_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iCMPPingToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(103, 20);
            this.toolStripMenuItem1.Text = "&Generate Traffic";
            // 
            // iCMPPingToolStripMenuItem
            // 
            this.iCMPPingToolStripMenuItem.Name = "iCMPPingToolStripMenuItem";
            this.iCMPPingToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.iCMPPingToolStripMenuItem.Text = "&ICMP Ping";
            this.iCMPPingToolStripMenuItem.Click += new System.EventHandler(this.iCMPPingToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packetsReplayToolStripMenuItem,
            this.packetsTamperingToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(58, 20);
            this.toolStripMenuItem3.Text = "&Attacks";
            // 
            // packetsReplayToolStripMenuItem
            // 
            this.packetsReplayToolStripMenuItem.Name = "packetsReplayToolStripMenuItem";
            this.packetsReplayToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.packetsReplayToolStripMenuItem.Text = "Packet &Replay";
            this.packetsReplayToolStripMenuItem.Click += new System.EventHandler(this.packetsReplayToolStripMenuItem_Click);
            // 
            // packetsTamperingToolStripMenuItem
            // 
            this.packetsTamperingToolStripMenuItem.Name = "packetsTamperingToolStripMenuItem";
            this.packetsTamperingToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.packetsTamperingToolStripMenuItem.Text = "&Packet Tampering";
            this.packetsTamperingToolStripMenuItem.Click += new System.EventHandler(this.packetsTamperingToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineHelpToolStripMenuItem,
            this.iPsecLitesWebPageToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutIPsecLiteToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.onlineHelpToolStripMenuItem.Text = "Online Help";
            this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.onlineHelpToolStripMenuItem_Click);
            // 
            // iPsecLitesWebPageToolStripMenuItem
            // 
            this.iPsecLitesWebPageToolStripMenuItem.Name = "iPsecLitesWebPageToolStripMenuItem";
            this.iPsecLitesWebPageToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.iPsecLitesWebPageToolStripMenuItem.Text = "Home Page";
            this.iPsecLitesWebPageToolStripMenuItem.Click += new System.EventHandler(this.iPsecLitesWebPageToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // aboutIPsecLiteToolStripMenuItem
            // 
            this.aboutIPsecLiteToolStripMenuItem.Name = "aboutIPsecLiteToolStripMenuItem";
            this.aboutIPsecLiteToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.aboutIPsecLiteToolStripMenuItem.Text = "About IPsecLite";
            this.aboutIPsecLiteToolStripMenuItem.Click += new System.EventHandler(this.aboutIPsecLiteToolStripMenuItem_Click);
            // 
            // exitsToolStripMenuItem
            // 
            this.exitsToolStripMenuItem.Name = "exitsToolStripMenuItem";
            this.exitsToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitsToolStripMenuItem.Text = "&Exit";
            this.exitsToolStripMenuItem.Click += new System.EventHandler(this.exitsToolStripMenuItem_Click);
            // 
            // IPsecLiteMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "IPsecLiteMasterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IPsecLite";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem spdMenuPad;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem exitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iCMPPingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem packetsReplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packetsTamperingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem iPDatagramsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uDPPacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem iPsecPacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPsecPacketsOutgoingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPsecLitesWebPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutIPsecLiteToolStripMenuItem;
    }
}



