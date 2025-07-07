using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace GSMArena_Mobile_Brands
{
    public partial class WaitForm : Form
    {
        public WaitForm(Form owner = null, string message = "Loading, please wait...")
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;

            if (owner != null)
            {
                this.Owner = owner;
                this.StartPosition = FormStartPosition.CenterParent;
            }

            lblMessage.Text = message;
        }
        public void Setup(string message, Image loadingImage)
        {
            lblMessage.Text = message;
            picLoading.Image = loadingImage;
        }
        private void WaitForm_Load(object sender, EventArgs e)
        {
            // Optionally force focus
            this.BringToFront();
        }
    }
}