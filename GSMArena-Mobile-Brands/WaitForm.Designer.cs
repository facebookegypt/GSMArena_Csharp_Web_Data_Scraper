namespace GSMArena_Mobile_Brands
{
    partial class WaitForm
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox picLoading;
        private Label lblMessage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            picLoading = new PictureBox();
            lblMessage = new Label();
            ((System.ComponentModel.ISupportInitialize)picLoading).BeginInit();
            SuspendLayout();
            // 
            // picLoading
            // 
            picLoading.BackColor = Color.Transparent;
            picLoading.Dock = DockStyle.Top;
            picLoading.Image = Properties.Resources.loading;
            picLoading.Location = new Point(0, 0);
            picLoading.Name = "picLoading";
            picLoading.Size = new Size(463, 136);
            picLoading.SizeMode = PictureBoxSizeMode.Zoom;
            picLoading.TabIndex = 0;
            picLoading.TabStop = false;
            picLoading.UseWaitCursor = true;
            // 
            // lblMessage
            // 
            lblMessage.AutoEllipsis = true;
            lblMessage.BackColor = Color.Transparent;
            lblMessage.Dock = DockStyle.Bottom;
            lblMessage.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMessage.ForeColor = Color.Black;
            lblMessage.Location = new Point(0, 136);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(463, 38);
            lblMessage.TabIndex = 1;
            lblMessage.Text = "Loading, please wait...";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblMessage.UseCompatibleTextRendering = true;
            // 
            // WaitForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(463, 174);
            Controls.Add(picLoading);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WaitForm";
            Opacity = 0.8D;
            ShowIcon = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            TransparencyKey = Color.White;
            FormClosed += WaitForm_FormClosed;
            Load += WaitForm_Load;
            ((System.ComponentModel.ISupportInitialize)picLoading).EndInit();
            ResumeLayout(false);
        }
    }
}