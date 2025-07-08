namespace GSMArena_Mobile_Brands
{
    partial class DisplayForm
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
            components = new System.ComponentModel.Container();
            TreeNode treeNode2 = new TreeNode("Models / Phones");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayForm));
            TRVmodels = new TreeView();
            groupBox1 = new GroupBox();
            TxtSearchSpecs = new TextBox();
            lblSearchSpecs = new Label();
            BtnReset = new Button();
            RTBspecs = new RichTextBox();
            Phon_Img = new PictureBox();
            TRVimglst = new ImageList(components);
            TxtFilter = new TextBox();
            LblFilter = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Phon_Img).BeginInit();
            SuspendLayout();
            // 
            // TRVmodels
            // 
            TRVmodels.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            TRVmodels.BackColor = Color.Azure;
            TRVmodels.BorderStyle = BorderStyle.None;
            TRVmodels.Font = new Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TRVmodels.ForeColor = SystemColors.Highlight;
            TRVmodels.FullRowSelect = true;
            TRVmodels.LineColor = Color.FromArgb(0, 102, 204);
            TRVmodels.Location = new Point(1, 41);
            TRVmodels.Name = "TRVmodels";
            treeNode2.Name = "Node0";
            treeNode2.NodeFont = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            treeNode2.Text = "Models / Phones";
            TRVmodels.Nodes.AddRange(new TreeNode[] { treeNode2 });
            TRVmodels.ShowNodeToolTips = true;
            TRVmodels.Size = new Size(366, 397);
            TRVmodels.TabIndex = 0;
            TRVmodels.AfterSelect += TRVmodels_AfterSelect;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(TxtSearchSpecs);
            groupBox1.Controls.Add(lblSearchSpecs);
            groupBox1.Controls.Add(BtnReset);
            groupBox1.Controls.Add(RTBspecs);
            groupBox1.Controls.Add(Phon_Img);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.Brown;
            groupBox1.Location = new Point(372, -1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(394, 439);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Details";
            // 
            // TxtSearchSpecs
            // 
            TxtSearchSpecs.BackColor = Color.Azure;
            TxtSearchSpecs.Font = new Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TxtSearchSpecs.ForeColor = Color.DimGray;
            TxtSearchSpecs.Location = new Point(145, 55);
            TxtSearchSpecs.Name = "TxtSearchSpecs";
            TxtSearchSpecs.Size = new Size(243, 28);
            TxtSearchSpecs.TabIndex = 8;
            TxtSearchSpecs.TextChanged += TxtSearchSpecs_TextChanged;
            TxtSearchSpecs.KeyDown += TxtSearchSpecs_KeyDown;
            // 
            // lblSearchSpecs
            // 
            lblSearchSpecs.BackColor = Color.Transparent;
            lblSearchSpecs.ForeColor = Color.IndianRed;
            lblSearchSpecs.Image = Properties.Resources.magnifier;
            lblSearchSpecs.ImageAlign = ContentAlignment.MiddleLeft;
            lblSearchSpecs.Location = new Point(145, 29);
            lblSearchSpecs.Name = "lblSearchSpecs";
            lblSearchSpecs.Size = new Size(148, 23);
            lblSearchSpecs.TabIndex = 7;
            lblSearchSpecs.Text = "Search Specs";
            lblSearchSpecs.TextAlign = ContentAlignment.MiddleRight;
            lblSearchSpecs.UseCompatibleTextRendering = true;
            // 
            // BtnReset
            // 
            BtnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BtnReset.BackColor = Color.Transparent;
            BtnReset.BackgroundImageLayout = ImageLayout.Zoom;
            BtnReset.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnReset.ForeColor = Color.Black;
            BtnReset.Image = Properties.Resources.Wizard;
            BtnReset.ImageAlign = ContentAlignment.MiddleRight;
            BtnReset.Location = new Point(6, 385);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(133, 48);
            BtnReset.TabIndex = 6;
            BtnReset.Text = "Reset";
            BtnReset.UseVisualStyleBackColor = false;
            BtnReset.Click += BtnReset_Click;
            // 
            // RTBspecs
            // 
            RTBspecs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RTBspecs.BackColor = Color.Azure;
            RTBspecs.BorderStyle = BorderStyle.None;
            RTBspecs.BulletIndent = 2;
            RTBspecs.Location = new Point(145, 89);
            RTBspecs.Name = "RTBspecs";
            RTBspecs.ReadOnly = true;
            RTBspecs.ScrollBars = RichTextBoxScrollBars.Vertical;
            RTBspecs.Size = new Size(243, 344);
            RTBspecs.TabIndex = 4;
            RTBspecs.Text = "";
            // 
            // Phon_Img
            // 
            Phon_Img.BackColor = Color.Transparent;
            Phon_Img.Location = new Point(3, 29);
            Phon_Img.Name = "Phon_Img";
            Phon_Img.Size = new Size(136, 184);
            Phon_Img.SizeMode = PictureBoxSizeMode.Zoom;
            Phon_Img.TabIndex = 3;
            Phon_Img.TabStop = false;
            // 
            // TRVimglst
            // 
            TRVimglst.ColorDepth = ColorDepth.Depth32Bit;
            TRVimglst.ImageStream = (ImageListStreamer)resources.GetObject("TRVimglst.ImageStream");
            TRVimglst.TransparentColor = Color.Transparent;
            TRVimglst.Images.SetKeyName(0, "Root");
            TRVimglst.Images.SetKeyName(1, "Brand");
            TRVimglst.Images.SetKeyName(2, "Phones");
            // 
            // TxtFilter
            // 
            TxtFilter.BackColor = Color.Azure;
            TxtFilter.ForeColor = Color.DarkGray;
            TxtFilter.Location = new Point(97, 12);
            TxtFilter.Name = "TxtFilter";
            TxtFilter.Size = new Size(269, 23);
            TxtFilter.TabIndex = 4;
            // 
            // LblFilter
            // 
            LblFilter.BackColor = Color.Transparent;
            LblFilter.Image = Properties.Resources.magnifier;
            LblFilter.ImageAlign = ContentAlignment.MiddleLeft;
            LblFilter.Location = new Point(12, 12);
            LblFilter.Name = "LblFilter";
            LblFilter.Size = new Size(79, 23);
            LblFilter.TabIndex = 5;
            LblFilter.Text = "Filter";
            LblFilter.TextAlign = ContentAlignment.MiddleCenter;
            LblFilter.UseCompatibleTextRendering = true;
            // 
            // DisplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(778, 450);
            Controls.Add(LblFilter);
            Controls.Add(TxtFilter);
            Controls.Add(groupBox1);
            Controls.Add(TRVmodels);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DisplayForm";
            Text = "GSMArena Scrapper by evry1falls (Acc. Ahmed Samir) @https://adonetaccess2003.blogspot.com";
            Load += DisplayForm_Load;
            KeyDown += DisplayForm_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Phon_Img).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView TRVmodels;
        private GroupBox groupBox1;
        private PictureBox Phon_Img;
        private ImageList TRVimglst;
        private RichTextBox RTBspecs;
        private TextBox TxtFilter;
        private Label LblFilter;
        private Button BtnReset;
        private Label lblSearchSpecs;
        private TextBox TxtSearchSpecs;
    }
}