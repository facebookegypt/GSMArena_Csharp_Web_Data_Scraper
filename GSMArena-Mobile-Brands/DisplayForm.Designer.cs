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
            TreeNode treeNode2 = new TreeNode("Models / Phones");
            TRVmodels = new TreeView();
            groupBox1 = new GroupBox();
            Phon_Img = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Phon_Img).BeginInit();
            SuspendLayout();
            // 
            // TRVmodels
            // 
            TRVmodels.BackColor = Color.Azure;
            TRVmodels.BorderStyle = BorderStyle.None;
            TRVmodels.Dock = DockStyle.Left;
            TRVmodels.FullRowSelect = true;
            TRVmodels.Location = new Point(0, 0);
            TRVmodels.Name = "TRVmodels";
            treeNode2.Name = "Node0";
            treeNode2.NodeFont = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            treeNode2.Text = "Models / Phones";
            TRVmodels.Nodes.AddRange(new TreeNode[] { treeNode2 });
            TRVmodels.ShowNodeToolTips = true;
            TRVmodels.Size = new Size(179, 450);
            TRVmodels.TabIndex = 0;
            TRVmodels.AfterSelect += TRVmodels_AfterSelect;
            TRVmodels.NodeMouseDoubleClick += TRVmodels_NodeMouseDoubleClick;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(Phon_Img);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.Brown;
            groupBox1.Location = new Point(185, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(490, 216);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Details";
            // 
            // Phon_Img
            // 
            Phon_Img.Dock = DockStyle.Left;
            Phon_Img.Location = new Point(3, 29);
            Phon_Img.Name = "Phon_Img";
            Phon_Img.Size = new Size(117, 184);
            Phon_Img.SizeMode = PictureBoxSizeMode.Zoom;
            Phon_Img.TabIndex = 3;
            Phon_Img.TabStop = false;
            Phon_Img.MouseEnter += pictureBox1_MouseEnter;
            // 
            // DisplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(687, 450);
            Controls.Add(groupBox1);
            Controls.Add(TRVmodels);
            Name = "DisplayForm";
            Text = "GSMArena Scrapper by evry1falls (Acc. Ahmed Samir) @https://adonetaccess2003.blogspot.com";
            Load += DisplayForm_Load;
            KeyDown += DisplayForm_KeyDown;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Phon_Img).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TreeView TRVmodels;
        private GroupBox groupBox1;
        private PictureBox Phon_Img;
    }
}