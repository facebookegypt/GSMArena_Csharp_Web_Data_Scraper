using System;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;

namespace GSMArena_Mobile_Brands
{
    public partial class WaitForm : Form
    {
        private int _elapsedSeconds = 0;
        public WaitForm(Form owner = null, string message = "Loading, please wait...")
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;

            if (owner != null)
            {
                this.Owner = owner;
                this.StartPosition = FormStartPosition.Manual;
                timer1.Interval = 1000;
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
            timer1.Interval = 1000;
            _elapsedSeconds = 0;
            timer1.Start();
        }
        public void StartCounter()
        {
            _elapsedSeconds = 0;
            timer1.Start();
        }
        public void StopCounter()
        {
            timer1.Stop();
        }

        private void WaitForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.BringToFront();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            _elapsedSeconds++;
            lblCounter.Text = _elapsedSeconds.ToString();
        }
        private void WaitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void picLoading_Click(object sender, EventArgs e)
        {

        }
    }
}