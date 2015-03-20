using System;
using System.Drawing;
using System.Windows.Forms;

namespace ACLMaster
{
    public partial class frmWaitForDomainInformation : Form
    {
        public frmWaitForDomainInformation()
        {
            InitializeComponent();
        }

        private void frmWait_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;

            //Create a worker thread and retreive domain information




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{

        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmWait_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape )
            {
                Close();
            }
        }
    }
}
