namespace GSMArena_Mobile_Brands
{
    partial class WaitScrapForm
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
            lblImg = new Label();
            panel1 = new Panel();
            btnStop = new Button();
            btnResume = new Button();
            btnPause = new Button();
            ProgScrap = new ProgressBar();
            lblETA = new Label();
            lblStatus = new Label();
            picLoading = new PictureBox();
            PrgrssTextBox = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLoading).BeginInit();
            SuspendLayout();
            // 
            // lblImg
            // 
            lblImg.AutoEllipsis = true;
            lblImg.AutoSize = true;
            lblImg.BackColor = Color.Transparent;
            lblImg.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblImg.ForeColor = Color.Brown;
            lblImg.Image = Properties.Resources.Download_32x32;
            lblImg.Location = new Point(1, 1);
            lblImg.Name = "lblImg";
            lblImg.Size = new Size(50, 41);
            lblImg.TabIndex = 0;
            lblImg.Text = "          \r\n           ";
            lblImg.TextAlign = ContentAlignment.MiddleLeft;
            lblImg.UseCompatibleTextRendering = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(btnStop);
            panel1.Controls.Add(btnResume);
            panel1.Controls.Add(btnPause);
            panel1.Controls.Add(ProgScrap);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 144);
            panel1.Name = "panel1";
            panel1.Size = new Size(578, 52);
            panel1.TabIndex = 1;
            // 
            // btnStop
            // 
            btnStop.BackgroundImage = Properties.Resources.Stop_24x24;
            btnStop.BackgroundImageLayout = ImageLayout.Center;
            btnStop.FlatStyle = FlatStyle.Popup;
            btnStop.Font = new Font("Times New Roman", 11.25F, FontStyle.Bold);
            btnStop.ForeColor = Color.OrangeRed;
            btnStop.Location = new Point(497, 3);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 46);
            btnStop.TabIndex = 3;
            btnStop.UseCompatibleTextRendering = true;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnResume
            // 
            btnResume.BackgroundImage = Properties.Resources.Play_24x24;
            btnResume.BackgroundImageLayout = ImageLayout.Center;
            btnResume.FlatStyle = FlatStyle.Popup;
            btnResume.Font = new Font("Times New Roman", 11.25F, FontStyle.Bold);
            btnResume.ForeColor = Color.OrangeRed;
            btnResume.Location = new Point(416, 3);
            btnResume.Name = "btnResume";
            btnResume.Size = new Size(75, 46);
            btnResume.TabIndex = 2;
            btnResume.UseCompatibleTextRendering = true;
            btnResume.UseVisualStyleBackColor = true;
            btnResume.Click += btnResume_Click;
            // 
            // btnPause
            // 
            btnPause.BackgroundImage = Properties.Resources.Pause_24x24;
            btnPause.BackgroundImageLayout = ImageLayout.Center;
            btnPause.FlatStyle = FlatStyle.Popup;
            btnPause.Font = new Font("Times New Roman", 11.25F, FontStyle.Bold);
            btnPause.ForeColor = Color.OrangeRed;
            btnPause.Location = new Point(335, 3);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(75, 46);
            btnPause.TabIndex = 1;
            btnPause.UseCompatibleTextRendering = true;
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // ProgScrap
            // 
            ProgScrap.BackColor = Color.Azure;
            ProgScrap.Dock = DockStyle.Left;
            ProgScrap.Location = new Point(0, 0);
            ProgScrap.Name = "ProgScrap";
            ProgScrap.Size = new Size(329, 52);
            ProgScrap.TabIndex = 0;
            // 
            // lblETA
            // 
            lblETA.AutoEllipsis = true;
            lblETA.BackColor = Color.Transparent;
            lblETA.Font = new Font("Arial Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblETA.ForeColor = Color.Red;
            lblETA.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            lblETA.Location = new Point(2, 115);
            lblETA.Name = "lblETA";
            lblETA.Size = new Size(48, 28);
            lblETA.TabIndex = 3;
            lblETA.Text = "000";
            lblETA.TextAlign = ContentAlignment.MiddleCenter;
            lblETA.UseCompatibleTextRendering = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.BackColor = Color.Azure;
            lblStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.Brown;
            lblStatus.ImageAlign = ContentAlignment.MiddleLeft;
            lblStatus.Location = new Point(57, 1);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(521, 41);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "evry1falls@acc/ Ahmed Samir->https://adonetaccess2003.blogspot.com";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.UseCompatibleTextRendering = true;
            // 
            // picLoading
            // 
            picLoading.BackColor = Color.Transparent;
            picLoading.Image = Properties.Resources.loading;
            picLoading.Location = new Point(0, 45);
            picLoading.Name = "picLoading";
            picLoading.Size = new Size(51, 93);
            picLoading.SizeMode = PictureBoxSizeMode.Zoom;
            picLoading.TabIndex = 5;
            picLoading.TabStop = false;
            picLoading.UseWaitCursor = true;
            // 
            // PrgrssTextBox
            // 
            PrgrssTextBox.BackColor = Color.Azure;
            PrgrssTextBox.ForeColor = Color.Chocolate;
            PrgrssTextBox.Location = new Point(57, 45);
            PrgrssTextBox.Multiline = true;
            PrgrssTextBox.Name = "PrgrssTextBox";
            PrgrssTextBox.PlaceholderText = "Reporting the process ...";
            PrgrssTextBox.ReadOnly = true;
            PrgrssTextBox.ScrollBars = ScrollBars.Both;
            PrgrssTextBox.Size = new Size(519, 93);
            PrgrssTextBox.TabIndex = 6;
            // 
            // WaitScrapForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(578, 196);
            ControlBox = false;
            Controls.Add(lblETA);
            Controls.Add(PrgrssTextBox);
            Controls.Add(lblStatus);
            Controls.Add(panel1);
            Controls.Add(lblImg);
            Controls.Add(picLoading);
            ForeColor = Color.Azure;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WaitScrapForm";
            Opacity = 0.8D;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            FormClosing += WaitScrapForm_FormClosing;
            Load += WaitScrapForm_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLoading).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblImg;
        private Panel panel1;
        private ProgressBar ProgScrap;
        private Button btnStop;
        private Button btnResume;
        private Button btnPause;
        private Label lblETA;
        private Label lblStatus;
        private PictureBox picLoading;
        private TextBox PrgrssTextBox;
    }
}