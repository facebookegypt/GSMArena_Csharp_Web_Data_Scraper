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
            TreeNode treeNode1 = new TreeNode("Models / Phones");
            TRVmodels = new TreeView();
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            treeNode1.Name = "Node0";
            treeNode1.NodeFont = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            treeNode1.Text = "Models / Phones";
            TRVmodels.Nodes.AddRange(new TreeNode[] { treeNode1 });
            TRVmodels.ShowNodeToolTips = true;
            TRVmodels.Size = new Size(179, 450);
            TRVmodels.TabIndex = 0;
            TRVmodels.AfterSelect += TRVmodels_AfterSelect;
            TRVmodels.NodeMouseDoubleClick += TRVmodels_NodeMouseDoubleClick;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.Brown;
            groupBox1.Location = new Point(185, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(490, 216);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Details";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Left;
            pictureBox1.Location = new Point(3, 29);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(117, 184);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TreeView TRVmodels;
        private GroupBox groupBox1;
        private PictureBox pictureBox1;
    }
}