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
            picLoading.Dock = DockStyle.Top;
            picLoading.Image = Properties.Resources.loading;
            picLoading.Location = new Point(0, 0);
            picLoading.Name = "picLoading";
            picLoading.Size = new Size(240, 79);
            picLoading.SizeMode = PictureBoxSizeMode.Zoom;
            picLoading.TabIndex = 0;
            picLoading.TabStop = false;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 10F);
            lblMessage.Location = new Point(38, 92);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(141, 19);
            lblMessage.TabIndex = 1;
            lblMessage.Text = "Loading, please wait...";
            // 
            // WaitForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(240, 120);
            Controls.Add(picLoading);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WaitForm";
            StartPosition = FormStartPosition.CenterParent;
            Load += WaitForm_Load;
            ((System.ComponentModel.ISupportInitialize)picLoading).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}