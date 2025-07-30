namespace GSMArena_Mobile_Brands
{
    partial class TwaitForm
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
            panel1 = new Panel();
            btnStop = new Button();
            btnResume = new Button();
            btnPause = new Button();
            progressBar1 = new ProgressBar();
            PrgrssTextBox = new TextBox();
            lblStatus = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(btnStop);
            panel1.Controls.Add(btnResume);
            panel1.Controls.Add(btnPause);
            panel1.Controls.Add(progressBar1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 190);
            panel1.Name = "panel1";
            panel1.Size = new Size(576, 52);
            panel1.TabIndex = 2;
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
            btnStop.Click += btnCancel_Click;
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
            // progressBar1
            // 
            progressBar1.BackColor = Color.Azure;
            progressBar1.Dock = DockStyle.Left;
            progressBar1.Location = new Point(0, 0);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(329, 52);
            progressBar1.TabIndex = 0;
            // 
            // PrgrssTextBox
            // 
            PrgrssTextBox.BackColor = Color.Azure;
            PrgrssTextBox.ForeColor = Color.Chocolate;
            PrgrssTextBox.Location = new Point(12, 44);
            PrgrssTextBox.Multiline = true;
            PrgrssTextBox.Name = "PrgrssTextBox";
            PrgrssTextBox.PlaceholderText = "Reporting the process ...";
            PrgrssTextBox.ReadOnly = true;
            PrgrssTextBox.ScrollBars = ScrollBars.Both;
            PrgrssTextBox.Size = new Size(564, 134);
            PrgrssTextBox.TabIndex = 7;
            PrgrssTextBox.TextChanged += PrgrssTextBox_TextChanged;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.BackColor = Color.Azure;
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.Brown;
            lblStatus.ImageAlign = ContentAlignment.MiddleLeft;
            lblStatus.Location = new Point(0, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(576, 41);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "evry1falls@acc/ Ahmed Samir->https://adonetaccess2003.blogspot.com";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.UseCompatibleTextRendering = true;
            // 
            // TwaitForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(576, 242);
            Controls.Add(lblStatus);
            Controls.Add(PrgrssTextBox);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TwaitForm";
            Text = "TwaitForm";
            Load += TwaitForm_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnStop;
        private Button btnResume;
        private Button btnPause;
        private ProgressBar progressBar1;
        private TextBox PrgrssTextBox;
        private Label lblStatus;
    }
}