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
            TreeNode treeNode1 = new TreeNode("Models / Phones");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayForm));
            TRVmodels = new TreeView();
            groupBox1 = new GroupBox();
            btnCopy = new Button();
            TxtSearchSpecs = new TextBox();
            lblSearchSpecs = new Label();
            RTBspecs = new RichTextBox();
            Phon_Img = new PictureBox();
            TRVimglst = new ImageList(components);
            TxtFilter = new TextBox();
            LblFilter = new Label();
            panel1 = new Panel();
            Mnu = new MenuStrip();
            tsmExport = new ToolStripMenuItem();
            cSVToolStripMenuItem = new ToolStripMenuItem();
            jSONToolStripMenuItem = new ToolStripMenuItem();
            tXTToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            exportSettingsToolStripMenuItem = new ToolStripMenuItem();
            tsmShare = new ToolStripMenuItem();
            dropBoxToolStripMenuItem = new ToolStripMenuItem();
            oneDriveToolStripMenuItem = new ToolStripMenuItem();
            googleDriveToolStripMenuItem = new ToolStripMenuItem();
            tsmReset = new ToolStripMenuItem();
            StatusInfo = new StatusStrip();
            tstSelected = new ToolStripStatusLabel();
            TstShare = new ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Phon_Img).BeginInit();
            panel1.SuspendLayout();
            Mnu.SuspendLayout();
            StatusInfo.SuspendLayout();
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
            treeNode1.Name = "Node0";
            treeNode1.NodeFont = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            treeNode1.Text = "Models / Phones";
            TRVmodels.Nodes.AddRange(new TreeNode[] { treeNode1 });
            TRVmodels.ShowNodeToolTips = true;
            TRVmodels.Size = new Size(366, 384);
            TRVmodels.TabIndex = 0;
            TRVmodels.AfterCheck += TRVmodels_AfterCheck;
            TRVmodels.AfterSelect += TRVmodels_AfterSelect;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(btnCopy);
            groupBox1.Controls.Add(TxtSearchSpecs);
            groupBox1.Controls.Add(lblSearchSpecs);
            groupBox1.Controls.Add(RTBspecs);
            groupBox1.Controls.Add(Phon_Img);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.Brown;
            groupBox1.Location = new Point(372, -1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(518, 426);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Details";
            // 
            // btnCopy
            // 
            btnCopy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopy.AutoEllipsis = true;
            btnCopy.BackgroundImage = Properties.Resources.Clipboard;
            btnCopy.BackgroundImageLayout = ImageLayout.Stretch;
            btnCopy.FlatAppearance.BorderColor = Color.Azure;
            btnCopy.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 255, 255);
            btnCopy.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 255, 192);
            btnCopy.FlatStyle = FlatStyle.Flat;
            btnCopy.Font = new Font("Times New Roman", 11.25F, FontStyle.Bold);
            btnCopy.ForeColor = Color.OrangeRed;
            btnCopy.Location = new Point(480, 17);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(32, 35);
            btnCopy.TabIndex = 9;
            btnCopy.Tag = "Copy to Clipboard";
            btnCopy.UseCompatibleTextRendering = true;
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
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
            RTBspecs.Size = new Size(367, 331);
            RTBspecs.TabIndex = 4;
            RTBspecs.Text = "";
            RTBspecs.TextChanged += RTBspecs_TextChanged;
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
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(Mnu);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(896, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(80, 450);
            panel1.TabIndex = 6;
            // 
            // Mnu
            // 
            Mnu.BackColor = Color.Azure;
            Mnu.Dock = DockStyle.Right;
            Mnu.Items.AddRange(new ToolStripItem[] { tsmExport, tsmShare, tsmReset });
            Mnu.Location = new Point(-46, 0);
            Mnu.Name = "Mnu";
            Mnu.RightToLeft = RightToLeft.Yes;
            Mnu.Size = new Size(126, 450);
            Mnu.TabIndex = 13;
            Mnu.Text = "menuStrip1";
            // 
            // tsmExport
            // 
            tsmExport.AutoToolTip = true;
            tsmExport.BackgroundImageLayout = ImageLayout.Zoom;
            tsmExport.DropDownItems.AddRange(new ToolStripItem[] { cSVToolStripMenuItem, jSONToolStripMenuItem, tXTToolStripMenuItem, toolStripMenuItem2, toolStripMenuItem1, exportSettingsToolStripMenuItem });
            tsmExport.Font = new Font("Arial Rounded MT Bold", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tsmExport.ForeColor = Color.OrangeRed;
            tsmExport.Image = Properties.Resources.Wizard;
            tsmExport.ImageScaling = ToolStripItemImageScaling.None;
            tsmExport.Name = "tsmExport";
            tsmExport.RightToLeftAutoMirrorImage = true;
            tsmExport.Size = new Size(113, 81);
            tsmExport.Text = "&Export";
            tsmExport.TextDirection = ToolStripTextDirection.Vertical270;
            tsmExport.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // cSVToolStripMenuItem
            // 
            cSVToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            cSVToolStripMenuItem.AutoToolTip = true;
            cSVToolStripMenuItem.BackColor = Color.Azure;
            cSVToolStripMenuItem.Font = new Font("Times New Roman", 12F);
            cSVToolStripMenuItem.ForeColor = Color.IndianRed;
            cSVToolStripMenuItem.Image = (Image)resources.GetObject("cSVToolStripMenuItem.Image");
            cSVToolStripMenuItem.ImageAlign = ContentAlignment.MiddleRight;
            cSVToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            cSVToolStripMenuItem.Name = "cSVToolStripMenuItem";
            cSVToolStripMenuItem.RightToLeft = RightToLeft.No;
            cSVToolStripMenuItem.Size = new Size(180, 24);
            cSVToolStripMenuItem.Text = "CSV";
            cSVToolStripMenuItem.TextImageRelation = TextImageRelation.TextBeforeImage;
            cSVToolStripMenuItem.Click += cSVToolStripMenuItem_Click;
            // 
            // jSONToolStripMenuItem
            // 
            jSONToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            jSONToolStripMenuItem.AutoToolTip = true;
            jSONToolStripMenuItem.BackColor = Color.Azure;
            jSONToolStripMenuItem.Font = new Font("Times New Roman", 12F);
            jSONToolStripMenuItem.ForeColor = Color.IndianRed;
            jSONToolStripMenuItem.Image = (Image)resources.GetObject("jSONToolStripMenuItem.Image");
            jSONToolStripMenuItem.ImageAlign = ContentAlignment.MiddleRight;
            jSONToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            jSONToolStripMenuItem.Name = "jSONToolStripMenuItem";
            jSONToolStripMenuItem.RightToLeft = RightToLeft.No;
            jSONToolStripMenuItem.Size = new Size(180, 24);
            jSONToolStripMenuItem.Text = "JSON";
            jSONToolStripMenuItem.Click += jSONToolStripMenuItem_Click;
            // 
            // tXTToolStripMenuItem
            // 
            tXTToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            tXTToolStripMenuItem.AutoToolTip = true;
            tXTToolStripMenuItem.BackColor = Color.Azure;
            tXTToolStripMenuItem.Font = new Font("Times New Roman", 12F);
            tXTToolStripMenuItem.ForeColor = Color.IndianRed;
            tXTToolStripMenuItem.Image = (Image)resources.GetObject("tXTToolStripMenuItem.Image");
            tXTToolStripMenuItem.ImageAlign = ContentAlignment.MiddleRight;
            tXTToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            tXTToolStripMenuItem.Name = "tXTToolStripMenuItem";
            tXTToolStripMenuItem.RightToLeft = RightToLeft.No;
            tXTToolStripMenuItem.Size = new Size(180, 24);
            tXTToolStripMenuItem.Text = "TXT";
            tXTToolStripMenuItem.Click += tXTToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Alignment = ToolStripItemAlignment.Right;
            toolStripMenuItem2.AutoToolTip = true;
            toolStripMenuItem2.BackColor = Color.Azure;
            toolStripMenuItem2.Font = new Font("Times New Roman", 12F);
            toolStripMenuItem2.ForeColor = Color.IndianRed;
            toolStripMenuItem2.Image = (Image)resources.GetObject("toolStripMenuItem2.Image");
            toolStripMenuItem2.ImageAlign = ContentAlignment.MiddleRight;
            toolStripMenuItem2.ImageScaling = ToolStripItemImageScaling.None;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.RightToLeft = RightToLeft.No;
            toolStripMenuItem2.Size = new Size(180, 24);
            toolStripMenuItem2.Text = "SQL";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(177, 6);
            // 
            // exportSettingsToolStripMenuItem
            // 
            exportSettingsToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            exportSettingsToolStripMenuItem.AutoToolTip = true;
            exportSettingsToolStripMenuItem.BackColor = Color.Azure;
            exportSettingsToolStripMenuItem.Font = new Font("Times New Roman", 12F);
            exportSettingsToolStripMenuItem.ForeColor = Color.IndianRed;
            exportSettingsToolStripMenuItem.Image = (Image)resources.GetObject("exportSettingsToolStripMenuItem.Image");
            exportSettingsToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            exportSettingsToolStripMenuItem.Name = "exportSettingsToolStripMenuItem";
            exportSettingsToolStripMenuItem.RightToLeft = RightToLeft.No;
            exportSettingsToolStripMenuItem.Size = new Size(180, 24);
            exportSettingsToolStripMenuItem.Text = "Export Settings";
            exportSettingsToolStripMenuItem.Click += exportSettingsToolStripMenuItem_Click;
            // 
            // tsmShare
            // 
            tsmShare.DropDownItems.AddRange(new ToolStripItem[] { dropBoxToolStripMenuItem, oneDriveToolStripMenuItem, googleDriveToolStripMenuItem });
            tsmShare.Font = new Font("Arial Rounded MT Bold", 15.75F);
            tsmShare.ForeColor = Color.OrangeRed;
            tsmShare.Image = Properties.Resources.share;
            tsmShare.ImageScaling = ToolStripItemImageScaling.None;
            tsmShare.Name = "tsmShare";
            tsmShare.Size = new Size(113, 74);
            tsmShare.Text = "Sha&re";
            tsmShare.TextDirection = ToolStripTextDirection.Vertical270;
            tsmShare.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // dropBoxToolStripMenuItem
            // 
            dropBoxToolStripMenuItem.BackColor = Color.Azure;
            dropBoxToolStripMenuItem.Font = new Font("Times New Roman", 12F);
            dropBoxToolStripMenuItem.ForeColor = Color.IndianRed;
            dropBoxToolStripMenuItem.Image = Properties.Resources.dropbox;
            dropBoxToolStripMenuItem.Name = "dropBoxToolStripMenuItem";
            dropBoxToolStripMenuItem.RightToLeft = RightToLeft.No;
            dropBoxToolStripMenuItem.Size = new Size(160, 24);
            dropBoxToolStripMenuItem.Text = "DropBox";
            dropBoxToolStripMenuItem.TextAlign = ContentAlignment.MiddleLeft;
            dropBoxToolStripMenuItem.Click += dropBoxToolStripMenuItem_Click;
            // 
            // oneDriveToolStripMenuItem
            // 
            oneDriveToolStripMenuItem.BackColor = Color.Azure;
            oneDriveToolStripMenuItem.Font = new Font("Times New Roman", 12F);
            oneDriveToolStripMenuItem.ForeColor = Color.IndianRed;
            oneDriveToolStripMenuItem.Image = Properties.Resources.Onedrive;
            oneDriveToolStripMenuItem.Name = "oneDriveToolStripMenuItem";
            oneDriveToolStripMenuItem.RightToLeft = RightToLeft.No;
            oneDriveToolStripMenuItem.Size = new Size(160, 24);
            oneDriveToolStripMenuItem.Text = "OneDrive";
            oneDriveToolStripMenuItem.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // googleDriveToolStripMenuItem
            // 
            googleDriveToolStripMenuItem.BackColor = Color.Azure;
            googleDriveToolStripMenuItem.Font = new Font("Times New Roman", 12F);
            googleDriveToolStripMenuItem.ForeColor = Color.IndianRed;
            googleDriveToolStripMenuItem.Image = Properties.Resources.Gdrive;
            googleDriveToolStripMenuItem.Name = "googleDriveToolStripMenuItem";
            googleDriveToolStripMenuItem.RightToLeft = RightToLeft.No;
            googleDriveToolStripMenuItem.Size = new Size(160, 24);
            googleDriveToolStripMenuItem.Text = "Google Drive";
            googleDriveToolStripMenuItem.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tsmReset
            // 
            tsmReset.AutoToolTip = true;
            tsmReset.BackgroundImageLayout = ImageLayout.Zoom;
            tsmReset.Font = new Font("Arial Rounded MT Bold", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tsmReset.ForeColor = Color.OrangeRed;
            tsmReset.Image = Properties.Resources.Clear;
            tsmReset.ImageScaling = ToolStripItemImageScaling.None;
            tsmReset.Name = "tsmReset";
            tsmReset.RightToLeftAutoMirrorImage = true;
            tsmReset.Size = new Size(113, 71);
            tsmReset.Text = "R&eset";
            tsmReset.TextDirection = ToolStripTextDirection.Vertical270;
            tsmReset.TextImageRelation = TextImageRelation.TextBeforeImage;
            tsmReset.Click += tsmReset_Click;
            // 
            // StatusInfo
            // 
            StatusInfo.BackColor = Color.Transparent;
            StatusInfo.Items.AddRange(new ToolStripItem[] { tstSelected, TstShare });
            StatusInfo.Location = new Point(0, 428);
            StatusInfo.Name = "StatusInfo";
            StatusInfo.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            StatusInfo.ShowItemToolTips = true;
            StatusInfo.Size = new Size(896, 22);
            StatusInfo.SizingGrip = false;
            StatusInfo.TabIndex = 7;
            // 
            // tstSelected
            // 
            tstSelected.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tstSelected.ForeColor = Color.LightCoral;
            tstSelected.Name = "tstSelected";
            tstSelected.Size = new Size(0, 17);
            tstSelected.Click += tstSelected_Click;
            // 
            // TstShare
            // 
            TstShare.BorderSides = ToolStripStatusLabelBorderSides.Left;
            TstShare.ForeColor = Color.ForestGreen;
            TstShare.Image = Properties.Resources.Liner;
            TstShare.ImageAlign = ContentAlignment.MiddleLeft;
            TstShare.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            TstShare.Name = "TstShare";
            TstShare.Size = new Size(881, 20);
            TstShare.Spring = true;
            TstShare.Visible = false;
            // 
            // DisplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(976, 450);
            Controls.Add(StatusInfo);
            Controls.Add(panel1);
            Controls.Add(LblFilter);
            Controls.Add(TxtFilter);
            Controls.Add(groupBox1);
            Controls.Add(TRVmodels);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DisplayForm";
            Text = "Display ->GSMArena Scrapper by evry1falls (Acc. Ahmed Samir) @https://adonetaccess2003.blogspot.com";
            Load += DisplayForm_Load;
            KeyDown += DisplayForm_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Phon_Img).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            Mnu.ResumeLayout(false);
            Mnu.PerformLayout();
            StatusInfo.ResumeLayout(false);
            StatusInfo.PerformLayout();
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
        private Label lblSearchSpecs;
        private TextBox TxtSearchSpecs;
        private Panel panel1;
        private MenuStrip Mnu;
        private ToolStripMenuItem tsmExport;
        private ToolStripMenuItem cSVToolStripMenuItem;
        private ToolStripMenuItem jSONToolStripMenuItem;
        private ToolStripMenuItem tXTToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exportSettingsToolStripMenuItem;
        private ToolStripMenuItem tsmReset;
        private ToolStripMenuItem toolStripMenuItem2;
        private StatusStrip StatusInfo;
        private ToolStripStatusLabel tstSelected;
        private Button btnCopy;
        private ToolStripMenuItem tsmShare;
        private ToolStripMenuItem dropBoxToolStripMenuItem;
        private ToolStripMenuItem oneDriveToolStripMenuItem;
        private ToolStripMenuItem googleDriveToolStripMenuItem;
        private ToolStripStatusLabel TstShare;
    }
}