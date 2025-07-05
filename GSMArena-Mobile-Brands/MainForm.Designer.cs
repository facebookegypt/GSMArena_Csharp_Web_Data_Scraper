namespace GSMArena_Mobile_Brands
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
            ModelRadio = new RadioButton();
            BrandRadio = new RadioButton();
            statusStripSc = new StatusStrip();
            tstlChkCon = new ToolStripStatusLabel();
            tstlMessage = new ToolStripStatusLabel();
            tstMsg = new ToolStripStatusLabel();
            ProgressBarSc = new ProgressBar();
            ScrapBtn = new Button();
            SearchTextBox = new TextBox();
            LblSearch = new Label();
            ((System.ComponentModel.ISupportInitialize)DGVscrap).BeginInit();
            groupBox1.SuspendLayout();
            statusStripSc.SuspendLayout();
            SuspendLayout();
            // 
            // DGVscrap
            // 
            DGVscrap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DGVscrap.BackgroundColor = Color.Azure;
            DGVscrap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVscrap.Location = new Point(12, 158);
            DGVscrap.Name = "DGVscrap";
            DGVscrap.Size = new Size(776, 416);
            DGVscrap.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.Azure;
            groupBox1.Controls.Add(AllRadio);
            groupBox1.Controls.Add(ModelRadio);
            groupBox1.Controls.Add(BrandRadio);
            groupBox1.Controls.Add(statusStripSc);
            groupBox1.Controls.Add(ProgressBarSc);
            groupBox1.Controls.Add(ScrapBtn);
            groupBox1.Controls.Add(SearchTextBox);
            groupBox1.Controls.Add(LblSearch);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 140);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Search (names, brands, description)";
            // 
            // AllRadio
            // 
            AllRadio.AutoSize = true;
            AllRadio.Location = new Point(359, 29);
            AllRadio.Name = "AllRadio";
            AllRadio.Size = new Size(39, 19);
            AllRadio.TabIndex = 8;
            AllRadio.TabStop = true;
            AllRadio.Text = "All";
            AllRadio.UseVisualStyleBackColor = true;
            // 
            // ModelRadio
            // 
            ModelRadio.AutoSize = true;
            ModelRadio.Location = new Point(214, 29);
            ModelRadio.Name = "ModelRadio";
            ModelRadio.Size = new Size(59, 19);
            ModelRadio.TabIndex = 7;
            ModelRadio.TabStop = true;
            ModelRadio.Text = "Model";
            ModelRadio.UseVisualStyleBackColor = true;
            // 
            // BrandRadio
            // 
            BrandRadio.AutoSize = true;
            BrandRadio.Location = new Point(96, 29);
            BrandRadio.Name = "BrandRadio";
            BrandRadio.Size = new Size(56, 19);
            BrandRadio.TabIndex = 6;
            BrandRadio.TabStop = true;
            BrandRadio.Text = "Brand";
            BrandRadio.UseVisualStyleBackColor = true;
            // 
            // statusStripSc
            // 
            statusStripSc.Items.AddRange(new ToolStripItem[] { tstlChkCon, tstlMessage, tstMsg });
            statusStripSc.Location = new Point(3, 115);
            statusStripSc.Name = "statusStripSc";
            statusStripSc.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            statusStripSc.Size = new Size(770, 22);
            statusStripSc.TabIndex = 5;
            statusStripSc.Text = "statusStrip1";
            // 
            // tstlChkCon
            // 
            tstlChkCon.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tstlChkCon.BorderStyle = Border3DStyle.Raised;
            tstlChkCon.Name = "tstlChkCon";
            tstlChkCon.Size = new Size(4, 17);
            // 
            // tstlMessage
            // 
            tstlMessage.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tstlMessage.BorderStyle = Border3DStyle.Raised;
            tstlMessage.Name = "tstlMessage";
            tstlMessage.Size = new Size(4, 17);
            // 
            // tstMsg
            // 
            tstMsg.Name = "tstMsg";
            tstMsg.Size = new Size(0, 17);
            // 
            // ProgressBarSc
            // 
            ProgressBarSc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ProgressBarSc.Location = new Point(529, 78);
            ProgressBarSc.Name = "ProgressBarSc";
            ProgressBarSc.Size = new Size(241, 23);
            ProgressBarSc.TabIndex = 4;
            // 
            // ScrapBtn
            // 
            ScrapBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ScrapBtn.Image = Properties.Resources.Edit;
            ScrapBtn.ImageAlign = ContentAlignment.MiddleLeft;
            ScrapBtn.Location = new Point(529, 15);
            ScrapBtn.Name = "ScrapBtn";
            ScrapBtn.Size = new Size(241, 57);
            ScrapBtn.TabIndex = 2;
            ScrapBtn.Text = "Scrap it!";
            ScrapBtn.UseVisualStyleBackColor = true;
            ScrapBtn.Click += ScrapBtn_Click;
            // 
            // SearchTextBox
            // 
            SearchTextBox.ForeColor = Color.DarkGray;
            SearchTextBox.Location = new Point(6, 49);
            SearchTextBox.Name = "SearchTextBox";
            SearchTextBox.Size = new Size(466, 23);
            SearchTextBox.TabIndex = 3;
            SearchTextBox.Text = "Leave blank to scrap all";
            SearchTextBox.MouseClick += SearchTextBox_MouseClick;
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
            SearchTextBox.KeyDown += SearchTextBox_KeyDown;
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
            LblSearch.Text = "Search";
            LblSearch.TextAlign = ContentAlignment.MiddleCenter;
            LblSearch.UseCompatibleTextRendering = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 586);
            Controls.Add(groupBox1);
            Controls.Add(DGVscrap);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "GSMArena Scrapper by evry1falls (Acc. Ahmed Samir) @https://adonetaccess2003.blogspot.com";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)DGVscrap).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            statusStripSc.ResumeLayout(false);
            statusStripSc.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DGVscrap;
        private GroupBox groupBox1;
        private Label LblSearch;
        private TextBox SearchTextBox;
        private Button ScrapBtn;
        private StatusStrip statusStripSc;
        private ProgressBar ProgressBarSc;
        private ToolStripStatusLabel tstlChkCon;
        private ToolStripStatusLabel tstlMessage;
        private RadioButton AllRadio;
        private RadioButton ModelRadio;
        private RadioButton BrandRadio;
        private ToolStripStatusLabel tstMsg;
    }
}
