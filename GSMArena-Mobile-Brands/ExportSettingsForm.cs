using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GSMArena_Mobile_Brands
{
    public partial class ExportSettingsForm : Form
    {
        public ExportSettingsForm()
        {
            InitializeComponent();
            InitializeExportSettings();
        }
        private void InitializeExportSettings()
        {
            // Placeholder
            locationTextBox.ReadOnly = true;
            locationTextBox.Text = "Select an export format first...";
        }
        private void locationLbl_Click(object sender, EventArgs e)
        {
            string selectedFormat = GetSelectedFormat();
            if (string.IsNullOrEmpty(selectedFormat))
            {
                MessageBox.Show("Please select an export format (CSV, TXT, JSON, SQL).",
                                "No Format Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = $"Select folder for {selectedFormat} exports";
                dialog.UseDescriptionForTitle = true;
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    locationTextBox.Text = selectedPath;

                    switch (selectedFormat)
                    {
                        case "CSV":
                            Properties.Settings.Default.CSVpath = selectedPath;
                            break;
                        case "TXT":
                            Properties.Settings.Default.TXTpath = selectedPath;
                            break;
                        case "JSON":
                            Properties.Settings.Default.JSONpath = selectedPath;
                            break;
                        case "SQL":
                            Properties.Settings.Default.SQLpath = selectedPath;
                            break;
                    }
                }
            }
        }
        private string GetSelectedFormat()
        {
            if (csvRadio.Checked) return "CSV";
            if (txtRadio.Checked) return "TXT";
            if (jsonRadio.Checked) return "JSON";
            if (sqlRadio.Checked) return "SQL";
            return null;
        }
        private void csvRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (csvRadio.Checked)
            {
                locationTextBox.Text = Properties.Settings.Default.CSVpath ?? "No location set.";
                csvRadio.ForeColor = Color.OrangeRed;
            }
            else { csvRadio.ForeColor = Color.Black; }
        }
        private void txtRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (txtRadio.Checked)
            {
                locationTextBox.Text = Properties.Settings.Default.TXTpath ?? "No location set.";
                txtRadio.ForeColor = Color.OrangeRed;
            }
            else { txtRadio.ForeColor = Color.Black; }
        }
        private void jsonRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (jsonRadio.Checked)
            {
                locationTextBox.Text = Properties.Settings.Default.JSONpath ?? "No location set.";
                jsonRadio.ForeColor = Color.OrangeRed;
            }
            else { jsonRadio.ForeColor = Color.Black; }
        }
        private void sqlRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (sqlRadio.Checked)
            {
                locationTextBox.Text = Properties.Settings.Default.SQLpath ?? "No location set.";
                sqlRadio.ForeColor = Color.OrangeRed;
            }
            else
            {
                sqlRadio.ForeColor = Color.Black;
            }

        }
        private void ExportSettingsForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            // This ensures settings from previous session are read
            csvRadio.Checked = false;
            txtRadio.Checked = false;
            jsonRadio.Checked = false;
            sqlRadio.Checked = false;
            // Load existing location from user settings
            locTextBox.Text = Properties.Settings.Default.LocationSetting ?? "";
        }
        private void ExportSettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
        private void ExportSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the location to user settings
            Properties.Settings.Default.LocationSetting = locTextBox.Text;
            Properties.Settings.Default.Save(); // Save all settings
        }
        private void BtnOpnLoc_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Proxy/UserAgent TXT file";
                ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    locTextBox.Text = ofd.FileName;
                }
            }
        }
    }
}
