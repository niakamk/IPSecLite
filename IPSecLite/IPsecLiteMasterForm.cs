using System;
using System.Windows.Forms;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite;
namespace adabtek.IPsecLite
{
    public partial class IPsecLiteMasterForm : Form
    {
        private int childFormNumber = 0;

        public IPsecLiteMasterForm()
        {
            InitializeComponent();
            this.Text += (APP_CONFIG.IS_GATEWAY ? " Gateway (" + APP_CONFIG.ETHERNET_IP + ") " : " Host (" + APP_CONFIG.ETHERNET_IP + ") with Gateway (" + Utils.ToShortStringIP(APP_CONFIG.GATEWAY_IP) + ")");
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void exitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit IPsecLite?", "Exit IPsecLite", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void iCMPPingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICMPTrafficForm icmpTrafficForm = new ICMPTrafficForm();
            if (APP_CONFIG.FRAMED)
                icmpTrafficForm.MdiParent = this;
            icmpTrafficForm.Show();
        }

        private void spdMenuPad_Click(object sender, EventArgs e)
        {
            IKEForm ikeForm = new IKEForm();
            if (APP_CONFIG.FRAMED)
                ikeForm.MdiParent = this;
            ikeForm.Show();
        }

        private void packetsTamperingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttacksForm tamperingForm = new AttacksForm();
            if (APP_CONFIG.FRAMED)
                tamperingForm.MdiParent = this;
            tamperingForm.Show();
        }

        private void packetsReplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttacksForm tamperingForm = new AttacksForm();
            if (APP_CONFIG.FRAMED)
                tamperingForm.MdiParent = this;
            tamperingForm.Show();
        }


        private void iPsecPacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProtectedIncomingTrafficForm protectedIncomingTrafficForm = new ProtectedIncomingTrafficForm();
            if (APP_CONFIG.FRAMED)
                protectedIncomingTrafficForm.MdiParent = this;
            protectedIncomingTrafficForm.Show();
        }

        private void iPsecPacketsOutgoingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProtectedOutgoingTrafficForm protectedOutgoingTrafficForm = new ProtectedOutgoingTrafficForm();
            if (APP_CONFIG.FRAMED)
                protectedOutgoingTrafficForm.MdiParent = this;
            protectedOutgoingTrafficForm.Show();
        }

        private void uDPPacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UDPTrafficForm udpTrafficForm = new UDPTrafficForm();
            if (APP_CONFIG.FRAMED)
                udpTrafficForm.MdiParent = this;
            udpTrafficForm.Show();
        }

        private void iPDatagramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPTrafficForm ipTrafficForm = new IPTrafficForm();
            if (APP_CONFIG.FRAMED)
                ipTrafficForm.MdiParent = this;
            ipTrafficForm.Show();
        }

        private void aboutIPsecLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            if (APP_CONFIG.FRAMED)
                aboutForm.MdiParent = this;
            aboutForm.Show();
        }

        private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please visit the help pages at http://www.adabtek.com/ipsec/ipseclite", "IPsecLite Online Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void iPsecLitesWebPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please visit IPsecLite's web page at http://www.adabtek.com/ipsec/ipseclite", "IPsecLite's web page", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


    }
}
