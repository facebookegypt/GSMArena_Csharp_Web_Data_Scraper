using clsGsmar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GSMArena_Mobile_Brands
{
    public partial class TwaitForm : Form
    {
        private string ?msg;
        public TwaitForm()
        {
            InitializeComponent();
            AsyncTaskController.PauseEvent = new ManualResetEventSlim(true);
            AsyncTaskController.Cts = new CancellationTokenSource();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            AsyncTaskController.PauseEvent.Reset(); // Pauses the task
            UpdateStatus("Paused...");
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            AsyncTaskController.PauseEvent.Set(); // Resumes the task
            UpdateStatus("Resumed...");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AsyncTaskController.Cts.Cancel(); // Cancels the task
            AsyncTaskController.PauseEvent.Set();
            UpdateStatus("Cancelling...");
        }

        public void UpdateStatus(string message)
        {
            PrgrssTextBox.Invoke((MethodInvoker)(() => PrgrssTextBox.Text += DateTime.Now.ToString("HH:mm:ss") + "##" + message + Environment.NewLine));
            PrgrssTextBox.ScrollToCaret();
        }

        public void UpdateProgress(int percent)
        {
            progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = percent));
        }
        private void TwaitForm_Load(object sender, EventArgs e)
        {
            TopMost = true;
            PrgrssTextBox.Clear();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
        }
        private void PrgrssTextBox_TextChanged(object sender, EventArgs e)
        {
            PrgrssTextBox.ScrollToCaret();
        }
    }
}
