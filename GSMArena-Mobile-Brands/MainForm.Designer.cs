﻿namespace GSMArena_Mobile_Brands
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DGVscrap = new DataGridView();
            groupBox1 = new GroupBox();
            AllRadio = new RadioButton();
            BrandRadio = new RadioButton();
            statusStripSc = new StatusStrip();
            tstlChkCon = new ToolStripStatusLabel();
            tstlMessage = new ToolStripStatusLabel();
            TstGet = new ToolStripStatusLabel();
            SearchTextBox = new TextBox();
            LblSearch = new Label();
            groupBox2 = new GroupBox();
            ScrapBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)DGVscrap).BeginInit();
            groupBox1.SuspendLayout();
            statusStripSc.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // DGVscrap
            // 
            DGVscrap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DGVscrap.BackgroundColor = Color.Azure;
            DGVscrap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVscrap.Location = new Point(12, 197);
            DGVscrap.Name = "DGVscrap";
            DGVscrap.Size = new Size(929, 377);
            DGVscrap.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSize = true;
            groupBox1.BackColor = Color.Azure;
            groupBox1.BackgroundImage = Properties.Resources.ImageNotFound;
            groupBox1.BackgroundImageLayout = ImageLayout.Zoom;
            groupBox1.Controls.Add(AllRadio);
            groupBox1.Controls.Add(BrandRadio);
            groupBox1.Controls.Add(statusStripSc);
            groupBox1.Controls.Add(SearchTextBox);
            groupBox1.Controls.Add(LblSearch);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(657, 179);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Search brnads by name";
            groupBox1.UseCompatibleTextRendering = true;
            // 
            // AllRadio
            // 
            AllRadio.AutoSize = true;
            AllRadio.BackColor = Color.Transparent;
            AllRadio.Location = new Point(304, 29);
            AllRadio.Name = "AllRadio";
            AllRadio.Size = new Size(78, 19);
            AllRadio.TabIndex = 8;
            AllRadio.TabStop = true;
            AllRadio.Text = "All Brands";
            AllRadio.UseVisualStyleBackColor = false;
            AllRadio.CheckedChanged += AllRadio_CheckedChanged;
            // 
            // BrandRadio
            // 
            BrandRadio.AutoSize = true;
            BrandRadio.BackColor = Color.Transparent;
            BrandRadio.Location = new Point(96, 29);
            BrandRadio.Name = "BrandRadio";
            BrandRadio.Size = new Size(195, 19);
            BrandRadio.TabIndex = 6;
            BrandRadio.TabStop = true;
            BrandRadio.Text = "Brand [i.e Acer, Samsung,....,etc]";
            BrandRadio.UseVisualStyleBackColor = false;
            BrandRadio.CheckedChanged += BrandRadio_CheckedChanged;
            // 
            // statusStripSc
            // 
            statusStripSc.BackColor = Color.Transparent;
            statusStripSc.Items.AddRange(new ToolStripItem[] { tstlChkCon, tstlMessage, TstGet });
            statusStripSc.Location = new Point(3, 151);
            statusStripSc.Name = "statusStripSc";
            statusStripSc.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            statusStripSc.Size = new Size(651, 25);
            statusStripSc.TabIndex = 5;
            statusStripSc.Text = "statusStrip1";
            // 
            // tstlChkCon
            // 
            tstlChkCon.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tstlChkCon.BorderStyle = Border3DStyle.Raised;
            tstlChkCon.Image = Properties.Resources.link;
            tstlChkCon.ImageAlign = ContentAlignment.MiddleLeft;
            tstlChkCon.ImageScaling = ToolStripItemImageScaling.None;
            tstlChkCon.Name = "tstlChkCon";
            tstlChkCon.Size = new Size(20, 20);
            // 
            // tstlMessage
            // 
            tstlMessage.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tstlMessage.BorderStyle = Border3DStyle.Raised;
            tstlMessage.Image = Properties.Resources.library;
            tstlMessage.ImageAlign = ContentAlignment.MiddleLeft;
            tstlMessage.ImageScaling = ToolStripItemImageScaling.None;
            tstlMessage.Name = "tstlMessage";
            tstlMessage.Size = new Size(20, 20);
            // 
            // TstGet
            // 
            TstGet.AutoToolTip = true;
            TstGet.BorderSides = ToolStripStatusLabelBorderSides.Right;
            TstGet.BorderStyle = Border3DStyle.Raised;
            TstGet.Enabled = false;
            TstGet.ForeColor = Color.Red;
            TstGet.Image = Properties.Resources.Pencil;
            TstGet.ImageAlign = ContentAlignment.MiddleLeft;
            TstGet.ImageScaling = ToolStripItemImageScaling.None;
            TstGet.ImageTransparentColor = Color.Azure;
            TstGet.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            TstGet.Name = "TstGet";
            TstGet.Size = new Size(596, 20);
            TstGet.Spring = true;
            TstGet.Text = "Scrap Selected Brands";
            TstGet.TextAlign = ContentAlignment.MiddleLeft;
            TstGet.Click += TstGet_Click;
            TstGet.MouseDown += TstGet_MouseDown;
            TstGet.MouseEnter += TstGet_MouseEnter;
            TstGet.MouseLeave += TstGet_MouseLeave;
            TstGet.MouseHover += TstGet_MouseHover;
            // 
            // SearchTextBox
            // 
            SearchTextBox.BackColor = Color.Azure;
            SearchTextBox.ForeColor = Color.DarkGray;
            SearchTextBox.Location = new Point(6, 49);
            SearchTextBox.Name = "SearchTextBox";
            SearchTextBox.Size = new Size(492, 23);
            SearchTextBox.TabIndex = 3;
            SearchTextBox.Text = "Leave blank to scrap all";
            SearchTextBox.MouseClick += SearchTextBox_MouseClick;
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
            SearchTextBox.Leave += SearchTextBox_Leave;
            // 
            // LblSearch
            // 
            LblSearch.BackColor = Color.Transparent;
            LblSearch.Image = Properties.Resources.magnifier;
            LblSearch.ImageAlign = ContentAlignment.MiddleLeft;
            LblSearch.Location = new Point(6, 31);
            LblSearch.Name = "LblSearch";
            LblSearch.Size = new Size(79, 15);
            LblSearch.TabIndex = 2;
            LblSearch.Text = "Filter";
            LblSearch.TextAlign = ContentAlignment.MiddleCenter;
            LblSearch.UseCompatibleTextRendering = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.BackColor = Color.Azure;
            groupBox2.Controls.Add(ScrapBtn);
            groupBox2.Location = new Point(675, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(266, 179);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Scrap All Brands";
            // 
            // ScrapBtn
            // 
            ScrapBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ScrapBtn.BackColor = Color.Transparent;
            ScrapBtn.BackgroundImage = Properties.Resources.TIME_IN;
            ScrapBtn.BackgroundImageLayout = ImageLayout.Zoom;
            ScrapBtn.Font = new Font("Times New Roman", 33.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ScrapBtn.ForeColor = Color.Black;
            ScrapBtn.ImageAlign = ContentAlignment.MiddleLeft;
            ScrapBtn.Location = new Point(3, 22);
            ScrapBtn.Name = "ScrapBtn";
            ScrapBtn.Size = new Size(257, 151);
            ScrapBtn.TabIndex = 5;
            ScrapBtn.Text = "Scrap it All!";
            ScrapBtn.UseVisualStyleBackColor = false;
            ScrapBtn.Click += ScrapBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(953, 586);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(DGVscrap);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "GSMArena Scrapper by evry1falls (Acc. Ahmed Samir) @https://adonetaccess2003.blogspot.com";
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)DGVscrap).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            statusStripSc.ResumeLayout(false);
            statusStripSc.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DGVscrap;
        private GroupBox groupBox1;
        private Label LblSearch;
        private TextBox SearchTextBox;
        private StatusStrip statusStripSc;
        private ToolStripStatusLabel tstlChkCon;
        private ToolStripStatusLabel tstlMessage;
        private RadioButton AllRadio;
        private RadioButton BrandRadio;
        private GroupBox groupBox2;
        private Button ScrapBtn;
        private ToolStripStatusLabel TstGet;
    }
}
