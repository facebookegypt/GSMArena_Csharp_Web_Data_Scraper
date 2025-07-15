namespace GSMArena_Mobile_Brands
{
    partial class ExportSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportSettingsForm));
            groupBox1 = new GroupBox();
            sqlRadio = new RadioButton();
            txtRadio = new RadioButton();
            jsonRadio = new RadioButton();
            csvRadio = new RadioButton();
            locationTextBox = new TextBox();
            locationLbl = new Label();
            groupBox2 = new GroupBox();
            locTextBox = new TextBox();
            BtnOpnLoc = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(sqlRadio);
            groupBox1.Controls.Add(txtRadio);
            groupBox1.Controls.Add(jsonRadio);
            groupBox1.Controls.Add(csvRadio);
            groupBox1.Controls.Add(locationTextBox);
            groupBox1.Controls.Add(locationLbl);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(637, 138);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Export location";
            // 
            // sqlRadio
            // 
            sqlRadio.AutoSize = true;
            sqlRadio.BackColor = Color.Transparent;
            sqlRadio.ForeColor = Color.Black;
            sqlRadio.Location = new Point(315, 18);
            sqlRadio.Name = "sqlRadio";
            sqlRadio.Size = new Size(46, 19);
            sqlRadio.TabIndex = 10;
            sqlRadio.TabStop = true;
            sqlRadio.Text = "SQL";
            sqlRadio.UseVisualStyleBackColor = false;
            sqlRadio.CheckedChanged += sqlRadio_CheckedChanged;
            // 
            // txtRadio
            // 
            txtRadio.AutoSize = true;
            txtRadio.BackColor = Color.Transparent;
            txtRadio.ForeColor = Color.Black;
            txtRadio.Location = new Point(253, 18);
            txtRadio.Name = "txtRadio";
            txtRadio.Size = new Size(46, 19);
            txtRadio.TabIndex = 9;
            txtRadio.TabStop = true;
            txtRadio.Text = "TXT";
            txtRadio.UseVisualStyleBackColor = false;
            txtRadio.CheckedChanged += txtRadio_CheckedChanged;
            // 
            // jsonRadio
            // 
            jsonRadio.AutoSize = true;
            jsonRadio.BackColor = Color.Transparent;
            jsonRadio.ForeColor = Color.Black;
            jsonRadio.Location = new Point(184, 18);
            jsonRadio.Name = "jsonRadio";
            jsonRadio.Size = new Size(53, 19);
            jsonRadio.TabIndex = 8;
            jsonRadio.TabStop = true;
            jsonRadio.Text = "JSON";
            jsonRadio.UseVisualStyleBackColor = false;
            jsonRadio.CheckedChanged += jsonRadio_CheckedChanged;
            // 
            // csvRadio
            // 
            csvRadio.AutoSize = true;
            csvRadio.BackColor = Color.Transparent;
            csvRadio.ForeColor = Color.Black;
            csvRadio.Location = new Point(122, 18);
            csvRadio.Name = "csvRadio";
            csvRadio.Size = new Size(46, 19);
            csvRadio.TabIndex = 7;
            csvRadio.TabStop = true;
            csvRadio.Text = "CSV";
            csvRadio.UseVisualStyleBackColor = false;
            csvRadio.CheckedChanged += csvRadio_CheckedChanged;
            // 
            // locationTextBox
            // 
            locationTextBox.BackColor = Color.Azure;
            locationTextBox.ForeColor = Color.DarkGray;
            locationTextBox.Location = new Point(122, 43);
            locationTextBox.Multiline = true;
            locationTextBox.Name = "locationTextBox";
            locationTextBox.PlaceholderText = "Define a local location";
            locationTextBox.ReadOnly = true;
            locationTextBox.ScrollBars = ScrollBars.Both;
            locationTextBox.Size = new Size(492, 89);
            locationTextBox.TabIndex = 5;
            // 
            // locationLbl
            // 
            locationLbl.BackColor = Color.Transparent;
            locationLbl.Dock = DockStyle.Left;
            locationLbl.Font = new Font("Arial Rounded MT Bold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            locationLbl.Image = Properties.Resources.Folder;
            locationLbl.ImageAlign = ContentAlignment.MiddleLeft;
            locationLbl.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            locationLbl.Location = new Point(3, 19);
            locationLbl.Name = "locationLbl";
            locationLbl.Size = new Size(113, 116);
            locationLbl.TabIndex = 4;
            locationLbl.Text = "Location";
            locationLbl.TextAlign = ContentAlignment.MiddleCenter;
            locationLbl.UseCompatibleTextRendering = true;
            locationLbl.Click += locationLbl_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.Transparent;
            groupBox2.Controls.Add(locTextBox);
            groupBox2.Controls.Add(BtnOpnLoc);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(0, 138);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(637, 138);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Import Settings";
            // 
            // locTextBox
            // 
            locTextBox.BackColor = Color.Azure;
            locTextBox.ForeColor = Color.IndianRed;
            locTextBox.Location = new Point(122, 43);
            locTextBox.Multiline = true;
            locTextBox.Name = "locTextBox";
            locTextBox.PlaceholderText = "Settings File location [Locally | Remotely]";
            locTextBox.ScrollBars = ScrollBars.Both;
            locTextBox.Size = new Size(492, 89);
            locTextBox.TabIndex = 5;
            locTextBox.Tag = "";
            // 
            // BtnOpnLoc
            // 
            BtnOpnLoc.BackColor = Color.Transparent;
            BtnOpnLoc.Font = new Font("Arial Rounded MT Bold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnOpnLoc.Image = Properties.Resources.Folder;
            BtnOpnLoc.ImageAlign = ContentAlignment.MiddleLeft;
            BtnOpnLoc.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            BtnOpnLoc.Location = new Point(3, 19);
            BtnOpnLoc.Name = "BtnOpnLoc";
            BtnOpnLoc.Size = new Size(113, 113);
            BtnOpnLoc.TabIndex = 4;
            BtnOpnLoc.Text = "&Open Location";
            BtnOpnLoc.TextAlign = ContentAlignment.MiddleCenter;
            BtnOpnLoc.UseCompatibleTextRendering = true;
            BtnOpnLoc.Click += BtnOpnLoc_Click;
            // 
            // ExportSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(637, 284);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ExportSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export Settings ->GSMArena Scrapper by evry1falls (Acc. Ahmed Samir) @https://adonetaccess2003.blogspot.com";
            FormClosing += ExportSettingsForm_FormClosing;
            Load += ExportSettingsForm_Load;
            KeyDown += ExportSettingsForm_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox locationTextBox;
        private Label locationLbl;
        private RadioButton sqlRadio;
        private RadioButton txtRadio;
        private RadioButton jsonRadio;
        private RadioButton csvRadio;
        private GroupBox groupBox2;
        private TextBox locTextBox;
        private Label BtnOpnLoc;
    }
}