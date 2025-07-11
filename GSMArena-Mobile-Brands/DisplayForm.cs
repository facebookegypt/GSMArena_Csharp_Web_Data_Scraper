using clsGsmar.Models;
using clsGsmar.Services;
using HtmlAgilityPack;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace GSMArena_Mobile_Brands
{
    public partial class DisplayForm : Form
    {
        //==========================
        //Navigate through results in txtsearchSpecs
        private List<int> _specsMatchIndices = new List<int>();
        private int _currentSpecsMatchIndex = -1;
        private string _lastSearchTerm = "";
        // =========================
        // Embedded Custom Fonts
        // =========================
        private PrivateFontCollection _customFonts = new PrivateFontCollection();
        private FontFamily _fontNew;


        private void LoadCustomFonts()
        {
            AddFontFromBytes(Properties.Resources.COOPBL);
            _fontNew = _customFonts.Families[^1];
        }

        private void AddFontFromBytes(byte[] fontBytes)
        {
            IntPtr fontBuffer = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontBuffer, fontBytes.Length);
            _customFonts.AddMemoryFont(fontBuffer, fontBytes.Length);
            Marshal.FreeCoTaskMem(fontBuffer);
        }

        // =========================
        // State    
        // =========================
        private ScraperService _scraper;
        private Dictionary<string, List<Phone>> _brandPhones;

        // Icons from Resources
        private Image _rootImage = Properties.Resources.RootIcon;
        private Image _brandImage = Properties.Resources.BrandIcon;

        // =========================
        // Constructor
        // =========================
        public DisplayForm(Dictionary<string, List<Phone>> scrapedData)
        {
            InitializeComponent();
            _brandPhones = scrapedData ?? new Dictionary<string, List<Phone>>();
            if (_brandPhones != null && _brandPhones.Count > 0)
            {
                PopulateTreeView(_brandPhones);
            }
        }
        // =========================
        // Form Load
        // =========================
        private void DisplayForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            TRVmodels.CheckBoxes = true;
            TRVmodels.HideSelection = false;
            TRVmodels.FullRowSelect = true;
            _scraper = new ScraperService();

            // Load Custom Fonts
            LoadCustomFonts();

            // Hook filter events
            TxtFilter.TextChanged += TxtFilter_TextChanged;
            TxtFilter.KeyDown += TxtFilter_KeyDown;
            TxtFilter.Leave += TxtFilter_Leave;
        }
        private void TxtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string filter = TxtFilter.Text.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(filter))
                {
                    PopulateTreeView(_brandPhones);
                    return;
                }
                // Filter the brands with at least 1 match
                Dictionary<string, List<Phone>> filtered = new();

                foreach (var kvp in _brandPhones)
                {
                    var matchingPhones = kvp.Value
                        .Where(p => p.Model?.ToLower().Contains(filter) ?? false)
                        .ToList();

                    if (matchingPhones.Count > 0)
                    {
                        filtered.Add(kvp.Key, matchingPhones);
                    }
                }

                if (filtered.Count > 0)
                {
                    PopulateTreeView(filtered);
                }
                else
                {
                    TRVmodels.Nodes.Clear();
                    TRVmodels.Nodes.Add(new TreeNode("No matches found.") { ForeColor = Color.Red });
                }
            }
        }
        private void TxtFilter_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFilter.Text))
            {
                PopulateTreeView(_brandPhones);
            }
        }

        private void TxtFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = TxtFilter.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(filter))
            {
                // Reset highlighting when cleared
                foreach (TreeNode brandNode in TRVmodels.Nodes[0].Nodes)
                {
                    foreach (TreeNode phoneNode in brandNode.Nodes)
                    {
                        phoneNode.BackColor = TRVmodels.BackColor;
                        phoneNode.ForeColor = TRVmodels.ForeColor;
                        phoneNode.NodeFont = TRVmodels.Font;
                    }
                }
                return;
            }
            // Highlight matches live
            foreach (TreeNode brandNode in TRVmodels.Nodes[0].Nodes)
            {
                foreach (TreeNode phoneNode in brandNode.Nodes)
                {
                    if (phoneNode.Text.ToLower().Contains(filter))
                    {
                        phoneNode.BackColor = Color.LightYellow;
                        phoneNode.ForeColor = Color.DarkGreen;
                        phoneNode.NodeFont = new Font(TRVmodels.Font, FontStyle.Bold);
                    }
                    else
                    {
                        phoneNode.BackColor = TRVmodels.BackColor;
                        phoneNode.ForeColor = TRVmodels.ForeColor;
                        phoneNode.NodeFont = TRVmodels.Font;
                    }
                }
            }
        }
        // =========================
        // Populating TreeView
        // =========================
        public void PopulateTreeView(Dictionary<string, List<Phone>> scrapedData)
        {
            TRVmodels.Nodes.Clear();

            // Assign ImageList
            TRVmodels.ImageList = TRVimglst;

            // Root Node
            TreeNode rootNode = new TreeNode("Models / Phones", 0, 0);

            foreach (var kvp in scrapedData)
            {
                string brandName = kvp.Key;
                List<Phone> phones = kvp.Value;

                // Brand Node
                TreeNode brandNode = new TreeNode($"{brandName} ({phones.Count})", 1, 1);

                // Numbered Phones (no icons, just numbering)
                int counter = 1;
                foreach (var phone in phones)
                {
                    TreeNode phoneNode = new TreeNode($"{counter}. {phone.Model}", 2, 2)
                    {
                        Tag = phone,
                    };
                    brandNode.Nodes.Add(phoneNode);
                    counter++;
                }

                rootNode.Nodes.Add(brandNode);
            }

            TRVmodels.Nodes.Add(rootNode);
            TRVmodels.ExpandAll();
            tsmReset.Enabled = TRVmodels.Nodes.Count > 0;
        }
        // =========================
        // Handle Node Selection
        // =========================
        private async void TRVmodels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Phone phone)
            {
                try
                {
                    // Show user feedback
                    RTBspecs.Clear();
                    RTBspecs.AppendText("Loading specifications...\n");

                    // Load and show image
                    Phon_Img.Image = await LoadImageFromUrlAsync(phone.ImageUrl);

                    // Update group box title
                    groupBox1.Text = phone.Model;

                    // Fetch Specs HTML from phone page
                    string html = await _scraper.GetHtmlSmartAsync(phone.Url);
                    string specsText = ParseSpecsHtml(html);

                    // Format and display specs
                    if (!string.IsNullOrWhiteSpace(specsText))
                        FormatSpecsToRichTextBox(specsText);
                    else
                        RTBspecs.Text = "No specifications found.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching specs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RTBspecs.Text = "Error loading specifications.";
                }
            }
        }

        // =========================
        // Load Image from URL
        // =========================
        private async Task<Image> LoadImageFromUrlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            try
            {
                using var client = new HttpClient();
                var bytes = await client.GetByteArrayAsync(url);
                using var ms = new MemoryStream(bytes);
                return Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }

        // =========================
        // Parse Specs HTML
        // =========================
        private string ParseSpecsHtml(string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var sb = new StringBuilder();

            var specsDiv = doc.DocumentNode.SelectSingleNode("//div[@id='specs-list']");
            if (specsDiv == null)
                return "Specifications not found.";

            var sections = specsDiv.SelectNodes(".//table");
            if (sections != null)
            {
                foreach (var table in sections)
                {
                    var sectionHeader = table.SelectSingleNode(".//th")?.InnerText.Trim();
                    if (!string.IsNullOrEmpty(sectionHeader))
                    {
                        sb.AppendLine($" {sectionHeader}");
                        sb.AppendLine(new string('-', sectionHeader.Length + 4));
                    }

                    var rows = table.SelectNodes(".//tr");
                    if (rows != null)
                    {
                        foreach (var row in rows)
                        {
                            var specName = row.SelectSingleNode("./td[@class='ttl']")?.InnerText.Trim() ?? "";
                            var specValue = row.SelectSingleNode("./td[@class='nfo']")?.InnerText.Trim() ?? "";

                            if (!string.IsNullOrEmpty(specName) && !string.IsNullOrEmpty(specValue))
                            {
                                sb.AppendLine($"• {specName}: {specValue}");
                            }
                        }
                    }

                    sb.AppendLine();
                }
            }
            else
            {
                sb.AppendLine("No spec sections found.");
            }

            return sb.ToString();
        }

        // =========================
        // RichTextBox Formatting
        // =========================
        private void FormatSpecsToRichTextBox(string specs)
        {
            RTBspecs.Clear();
            //RTBspecs.Font = new Font(_fontDetail, 10); // default

            var lines = specs.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.Contains("----"))
                {
                    RTBspecs.AppendText(Environment.NewLine);
                    continue;
                }

                if (trimmed.Contains(':') || trimmed.Contains(','))
                {
                    // details - dark red
                    AppendStyledLine(trimmed, new Font("arial", 12, FontStyle.Regular), Color.DarkRed);
                }
                else
                {
                    // Heads - dark green
                    trimmed = Environment.NewLine + trimmed;
                    AppendStyledLine(trimmed, new Font(_fontNew, 16, FontStyle.Bold), Color.DarkGreen);
                }
            }
        }
        private void RemoveAllHighlights()
        {
            // Reset all text to default back color
            RTBspecs.SelectAll();
            RTBspecs.SelectionBackColor = RTBspecs.BackColor;
            RTBspecs.SelectionLength = 0;
        }

        private void TxtSearchSpecs_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = TxtSearchSpecs.Text.Trim();

            _lastSearchTerm = searchTerm;
            _currentSpecsMatchIndex = -1;

            if (string.IsNullOrEmpty(searchTerm))
            {
                RemoveAllHighlights();
                return;
            }

            HighlightAllSpecsMatches(searchTerm);
        }
        private void HighlightAllSpecsMatches(string searchTerm)
        {
            RemoveAllHighlights();
            _specsMatchIndices.Clear();

            int startIndex = 0;
            int matchIndex;

            while ((matchIndex = RTBspecs.Text.IndexOf(searchTerm, startIndex, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                _specsMatchIndices.Add(matchIndex);

                RTBspecs.Select(matchIndex, searchTerm.Length);
                RTBspecs.SelectionBackColor = Color.Yellow;

                startIndex = matchIndex + searchTerm.Length;
            }

            // Remove blinking caret selection
            RTBspecs.SelectionLength = 0;
        }
        private void TxtSearchSpecs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(_lastSearchTerm))
            {
                if (_specsMatchIndices.Count == 0)
                    return;

                _currentSpecsMatchIndex++;
                if (_currentSpecsMatchIndex >= _specsMatchIndices.Count)
                    _currentSpecsMatchIndex = 0;

                // Reset all highlights to yellow first
                HighlightAllSpecsMatches(_lastSearchTerm);

                // Highlight current in light green
                int matchPosition = _specsMatchIndices[_currentSpecsMatchIndex];
                RTBspecs.Select(matchPosition, _lastSearchTerm.Length);
                RTBspecs.SelectionBackColor = Color.LightGreen;

                // Scroll to selection
                RTBspecs.ScrollToCaret();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void AppendStyledLine(string text, Font font, Color color)
        {
            RTBspecs.SelectionFont = font;
            RTBspecs.SelectionColor = color;
            RTBspecs.AppendText(text + Environment.NewLine);
        }

        // =========================
        // Escape to Close
        // =========================
        private void DisplayForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        //Reset the Form
        private void tsmReset_Click(object sender, EventArgs e)
        {
            // 1. Clear the TextBox used for filtering
            TxtFilter.Clear();

            // 2. Clear the specs RichTextBox
            RTBspecs.Clear();

            // 3. Reset the phone image to default (from resources)
            Phon_Img.Image = Properties.Resources.ImageNotFound;

            // 4. Collapse all brands (Level1), so phones (Level2) are hidden
            foreach (TreeNode node in TRVmodels.Nodes)
            {
                foreach (TreeNode brand in node.Nodes)
                {
                    brand.Collapse();  // Collapse each brand
                }
            }

            // 5. Reselect nothing
            TRVmodels.SelectedNode = null;

            // 6. Optional: Reset tree appearance (if custom drawing used)
            TRVmodels.Refresh();

            //7. Clear Search Specs
            TxtSearchSpecs.Clear();
        }

        private void exportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ExportForm = new ExportSettingsForm();
            ExportForm.ShowDialog();
        }
        //Export CSV file.
        private async void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await HandleExportClickAsync("csv", Properties.Settings.Default.CSVpath);
        }

        private async void TRVmodels_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // Prevent recursive event firing
            TRVmodels.AfterCheck -= TRVmodels_AfterCheck;

            try
            {
                if (e.Node.Level == 0)
                {
                    // Root node (Models/Phones): apply to all brands
                    foreach (TreeNode brandNode in e.Node.Nodes)
                        SetNodeCheckedState(brandNode, e.Node.Checked);
                }
                else if (e.Node.Level == 1)
                {
                    // Brand node → propagate to all phones
                    foreach (TreeNode phoneNode in e.Node.Nodes)
                        phoneNode.Checked = e.Node.Checked;
                }
                else if (e.Node.Level == 2)
                {
                    // Individual phone node
                    await HandlePhoneNodeCheckAsync(e.Node);
                    UpdateBrandCheckState(e.Node.Parent);
                }
            }
            finally
            {
                TRVmodels.AfterCheck += TRVmodels_AfterCheck;
                UpdateSelectedStatus();
            }
        }
        private async Task HandlePhoneNodeCheckAsync(TreeNode phoneNode)
        {
            if (phoneNode?.Tag is Phone phone)
            {
                if (phoneNode.Checked)
                {
                    // Only load if not already cached
                    if (string.IsNullOrEmpty(phone.FormattedSpecs))
                    {
                        try
                        {
                            // Optional: show loading message somewhere in UI
                            tstSelected.Text = $"Fetching specs for {phone.Model}...";

                            string html = await _scraper.GetHtmlSmartAsync(phone.Url);
                            string parsed = ParseSpecsHtml(html);

                            phone.RawSpecsHtml = html;
                            phone.FormattedSpecs = parsed;

                            tstSelected.Text = $"Specs ready for {phone.Model}";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error fetching specs: {ex.Message}");
                        }
                    }
                }
            }
        }
        private void UpdateBrandCheckState(TreeNode brandNode)
        {
            if (brandNode == null) return;

            bool allChecked = true;
            foreach (TreeNode phoneNode in brandNode.Nodes)
            {
                if (!phoneNode.Checked)
                {
                    allChecked = false;
                    break;
                }
            }

            brandNode.Checked = allChecked;
        }

        private void SetNodeCheckedState(TreeNode node, bool isChecked)
        {
            node.Checked = isChecked;
            foreach (TreeNode child in node.Nodes)
            {
                SetNodeCheckedState(child, isChecked);
            }
        }
        public List<Phone> GetSelectedPhones()
        {
            List<Phone> selectedPhones = new List<Phone>();

            foreach (TreeNode brandNode in TRVmodels.Nodes[0].Nodes) // Skip root
            {
                foreach (TreeNode phoneNode in brandNode.Nodes)
                {
                    if (phoneNode.Checked && phoneNode.Tag is Phone phone)
                    {
                        selectedPhones.Add(phone);
                    }
                }
            }

            return selectedPhones;
        }
        private void UpdateSelectedStatus()
        {
            var sb = new StringBuilder();

            foreach (TreeNode brandNode in TRVmodels.Nodes[0].Nodes)
            {
                int checkedCount = 0;

                foreach (TreeNode phoneNode in brandNode.Nodes)
                {
                    if (phoneNode.Checked)
                        checkedCount++;
                }

                if (checkedCount > 0)
                {
                    sb.Append($"{brandNode.Text.Split('(')[0].Trim()}: {checkedCount} phone(s) - ");
                }
            }

            string status = sb.ToString().TrimEnd(' ', '-');

            if (string.IsNullOrEmpty(status))
                tstSelected.Text = "No phones selected.";
            else
                tstSelected.Text = status;
        }
        /// <summary>
        /// Collects all checked Phone objects from the TreeView.
        /// Assumes Level 2 nodes (phones) are checked.
        /// </summary>
        private List<Phone> GetCheckedPhones()
        {
            var selectedPhones = new List<Phone>();

            if (TRVmodels.Nodes.Count == 0) return selectedPhones;

            foreach (TreeNode brandNode in TRVmodels.Nodes[0].Nodes)
            {
                foreach (TreeNode phoneNode in brandNode.Nodes)
                {
                    if (phoneNode.Checked && phoneNode.Tag is Phone phone)
                    {
                        selectedPhones.Add(phone);
                    }
                }
            }

            return selectedPhones;
        }

        private async void tXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await HandleExportClickAsync("txt", Properties.Settings.Default.TXTpath);
        }

        private async void jSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await HandleExportClickAsync("json", Properties.Settings.Default.JSONpath);
        }
        private async Task HandleExportClickAsync(string format, string exportFolder)
        {
            if (!EnsureExportPath(format))
                return;

            var selectedPhones = GetCheckedPhones();
            if (selectedPhones.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "Please select at least one phone to export.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            await PopulateSpecsForSelectedPhonesAsync(selectedPhones);

            var exporter = new ExportServices(exportFolder);

            await exporter.ExportPhonesAsync(
                selectedPhones,
                format,
                progress: new Progress<string>(msg => tstSelected.Text = msg)
            );

            MessageBox.Show(
                this,
                $"Export to {format.ToUpper()} completed successfully.",
                "Export Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private async Task ExportPhonesAsync(string exportFolder, string Exportformat)
        {
            try
            {
                var selectedPhones = GetCheckedPhones();
                if (selectedPhones.Count == 0)
                {
                    MessageBox.Show(
                        this,
                        "Please select at least one phone to export.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                await PopulateSpecsForSelectedPhonesAsync(selectedPhones);
                var exporter = new ExportServices(exportFolder);
                switch (Exportformat.ToLower())
                {
                    case "csv":
                        await exporter.ExportPhonesAsync(
        selectedPhones,
        Exportformat,
        progress: new Progress<string>(msg => tstSelected.Text = msg)
    );
                        break;
                    case "txt":
                        await exporter.ExportPhonesAsync(
        selectedPhones,
        Exportformat,
        progress: new Progress<string>(msg => tstSelected.Text = msg)
    );
                        break;
                    case "json":
                        await exporter.ExportPhonesAsync(
        selectedPhones,
        Exportformat,
        progress: new Progress<string>(msg => tstSelected.Text = msg)
    );
                        break;
                    default:
                        MessageBox.Show($"Unsupported format: {Exportformat}");
                        break;
                }

                MessageBox.Show(
                    this,
                    $"Export to {Exportformat.ToUpper()} completed successfully.",
                    "Export Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Export failed: {ex.Message}",
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private async Task PopulateSpecsForSelectedPhonesAsync(List<Phone> phones)
        {
            Cursor.Current = Cursors.WaitCursor;
            tstSelected.Text = "Fetching specs for selected phones...";

            int total = phones.Count;
            int current = 1;

            foreach (var phone in phones)
            {
                if (string.IsNullOrWhiteSpace(phone.FormattedSpecs))
                {
                    try
                    {
                        tstSelected.Text = $"Fetching ({current}/{total}): {phone.Brand} - {phone.Model}";

                        string html = await _scraper.GetHtmlSmartAsync(phone.Url);
                        phone.RawSpecsHtml = html;
                        phone.FormattedSpecs = ParseSpecsHtml(html);

                        await Task.Delay(300);
                    }
                    catch
                    {
                        phone.FormattedSpecs = "Could not fetch specs.";
                    }
                }

                current++;
            }

            tstSelected.Text = "Specs fetching complete.";
            Cursor.Current = Cursors.Default;
        }

        private (string brandName, int phoneCount) GetExportSummary()
        {
            var selectedPhones = GetCheckedPhones();

            if (selectedPhones.Count == 0)
                return (null, 0);

            // For simplicity, take first brand
            string firstBrand = selectedPhones.First().Brand ?? "Brand";

            int phoneCount = selectedPhones.Count;

            return (firstBrand, phoneCount);
        }
        private string GenerateExportFileName(string brandName, int phoneCount, string extension)
        {
            string timestamp = DateTime.Now.ToString("HHmmssddMMyyyy");
            string safeBrand = string.Concat(brandName.Split(Path.GetInvalidFileNameChars()));
            return $"{safeBrand}-{phoneCount}-{timestamp}.{extension}";
        }
        private bool EnsureExportPath(string format)
        {
            string savedPath = GetSavedPathForFormat(format);

            if (string.IsNullOrWhiteSpace(savedPath) || !System.IO.Directory.Exists(savedPath))
            {
                MessageBox.Show(
                    this,
                    $"Export path for {format.ToUpper()} is not set or invalid.\nPlease set it in Export Settings.",
                    "Export Path Not Set",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                using (var settingsForm = new ExportSettingsForm())
                {
                    settingsForm.StartPosition = FormStartPosition.CenterParent;
                    settingsForm.ShowDialog(this);
                }

                savedPath = GetSavedPathForFormat(format);
                if (string.IsNullOrWhiteSpace(savedPath) || !System.IO.Directory.Exists(savedPath))
                {
                    MessageBox.Show(
                        this,
                        $"Export cancelled. No valid path was set for {format.ToUpper()}.",
                        "Export Cancelled",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return false;
                }
            }

            return true;
        }
        private string GetSavedPathForFormat(string format)
        {
            return format.ToLower() switch
            {
                "csv" => Properties.Settings.Default.CSVpath,
                "txt" => Properties.Settings.Default.TXTpath,
                "json" => Properties.Settings.Default.JSONpath,
                _ => ""
            };
        }
    }
}
