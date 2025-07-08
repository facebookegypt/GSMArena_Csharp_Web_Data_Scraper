using clsGsmar.Models;
using clsGsmar.Tools;
using GSMArena_Mobile_Brands.Properties;
using static System.Windows.Forms.AxHost;

namespace GSMArena_Mobile_Brands
{
    public partial class MainForm : Form
    {
        private ScraperService _scraper;
        private readonly string placeHolder = "Leave blank to scrap all";
        private readonly string TstGetplaceHolder = "Scrap Selected Brands";
        private List<Brand> _allBrands = new List<Brand>();
        private Image _loadingGif;

        public MainForm()
        {
            InitializeComponent();
        }
        private List<Brand> GetCheckedBrands()
        {
            var checkedBrands = new List<Brand>();

            foreach (DataGridViewRow row in DGVscrap.Rows)
            {
                if (row.IsNewRow) continue;

                bool isChecked = Convert.ToBoolean(row.Cells["ChkCell"].Value);
                if (isChecked)
                {
                    checkedBrands.Add(new Brand
                    {
                        Name = row.Cells["BrName"].Value?.ToString(),
                        Url = row.Cells["BrUrl"].Value?.ToString(),
                        PhoneCount = int.TryParse(row.Cells["Pcnt"].Value?.ToString(), out var pcnt) ? pcnt : 0
                    });
                }
            }

            return checkedBrands;
        }

        // Handles form load event
        private async void MainForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true; // Allows form to capture Esc key globally
            AllRadio.Checked = true; // Default mode
            DGVscrap.CellContentClick += DGVscrap_CellContentClick;
            
            tstlMessage.Text = "Checking internet connection...";
            groupBox1.Enabled = false;
            _loadingGif = Resources.loading;
            ScrapBtn.Enabled = false;
            bool isConnected = await ChkCon.IsInternetAvailableAsync();
            if (isConnected)
            {
                tstlChkCon.Text = "Connected";
                tstlMessage.Text = "Ready";
                ScrapBtn.Enabled = true;
                _scraper = new ScraperService();
            }
            else
            {
                tstlChkCon.Text = "No Internet";
                tstlMessage.Text = "Please check your connection.";
                ScrapBtn.Enabled = false;
            }
        }

        // Click event for the Scrape button
        private async void ScrapBtn_Click(object sender, EventArgs e)
        {
            // Check internet before starting
            bool isConnected = await ChkCon.IsInternetAvailableAsync();
            if (!isConnected)
            {
                ScrapBtn.Enabled = false;
                tstlChkCon.Text = "No Internet";
                tstlMessage.Text = "Please check your connection.";
                return;
            }

            ScrapBtn.Enabled = false;
            tstlChkCon.Text = "Connected";
            tstlMessage.Text = "Starting scrape...";
            ProgressBarSc.Style = ProgressBarStyle.Marquee;

            var progress = new Progress<string>(msg =>
            {
                tstlMessage.Text = msg;
            });

            try
            {
                // Get all brands with counts
                var results = await _scraper.GetBrandsAsync(progress);
                _allBrands = results;

                // Setup and bind DataGridView
                DGVHelper.SetupDataGridViewColumns(DGVscrap);
                DGVHelper.BindData(DGVscrap, _allBrands);
                UpdateTstGetEnabledState();

                // Make all columns readonly except for the checkbox column
                DGVscrap.ReadOnly = false;
                foreach (DataGridViewColumn col in DGVscrap.Columns)
                    col.ReadOnly = true;

                if (DGVscrap.Columns.Contains("ChkCell"))
                    DGVscrap.Columns["ChkCell"].ReadOnly = false;

                DGVHelper.AddHeaderCheckBox(DGVscrap, HeaderCheckBox_Clicked);

                tstlMessage.Text = $"Done. {results.Count} brands loaded.";
            }
            catch (Exception ex)
            {
                tstlMessage.Text = $"Error: {ex.Message}";
            }
            finally
            {
                ProgressBarSc.Style = ProgressBarStyle.Blocks;
                ScrapBtn.Enabled = true;
                groupBox1.Enabled = true;
            }
        }

        // Live filtering by brand name as user types
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (DGVscrap.Rows.Count == 0)
                return;

            string filter = SearchTextBox.Text.Trim().ToLower();

            foreach (DataGridViewRow row in DGVscrap.Rows)
            {
                if (row.IsNewRow) continue;

                if (AllRadio.Checked)
                {
                    row.Visible = true;
                }
                else if (BrandRadio.Checked)
                {
                    var brandName = row.Cells["BrName"]?.Value?.ToString()?.ToLower() ?? "";

                    if (string.IsNullOrEmpty(filter) || filter == placeHolder.ToLower())
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = brandName.Contains(filter);
                    }
                }
            }
        }

        // Restore placeholder styling when leaving Search box
        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text) || SearchTextBox.Text == placeHolder)
            {
                SearchTextBox.Text = placeHolder;
                SearchTextBox.ForeColor = Color.DarkGray;
            }
            else
            {
                SearchTextBox.ForeColor = Color.Black;
            }
        }

        // Select-all header checkbox click
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            DGVHelper.HeaderCheckBox_Clicked(sender, DGVscrap);
            UpdateTstGetEnabledState();
        }

        // Cell checkbox click
        private void DGVscrap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGVHelper.CellContentClick(DGVscrap, e);
            UpdateTstGetEnabledState();
            UpdateTstGetText();
        }

        // Search box click selects all text for easy replace
        private void SearchTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            SearchTextBox.SelectAll();
        }

        // Radio button: All mode disables filtering
        private void AllRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (AllRadio.Checked)
            {
                SearchTextBox.Clear();
                SearchTextBox.ReadOnly = true;

                // Reset all rows visible
                foreach (DataGridViewRow row in DGVscrap.Rows)
                    row.Visible = true;
            }
        }

        // Radio button: Brand mode enables filter textbox
        private void BrandRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (BrandRadio.Checked)
            {
                SearchTextBox.ReadOnly = false;
                SearchTextBox.Clear();
                SearchTextBox.Select();
            }
        }

        // Form-level keydown for Esc to close
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void UpdateTstGetEnabledState()
        {
            int checkedCount = 0;

            foreach (DataGridViewRow row in DGVscrap.Rows)
            {
                if (Convert.ToBoolean(row.Cells["ChkCell"]?.Value) == true)
                {
                    checkedCount++;
                }
            }
            TstGet.Enabled = checkedCount > 0;
        }

        private void TstGet_MouseHover(object sender, EventArgs e)
        {
            TstGet.ForeColor = Color.DarkGreen;
            TstGet.Font = new Font(TstGet.Font, FontStyle.Bold);
        }

        private void TstGet_MouseDown(object sender, MouseEventArgs e)
        {
            TstGet.Font = new Font(TstGet.Font, FontStyle.Bold);
        }

        private void TstGet_MouseLeave(object sender, EventArgs e)
        {
            TstGet.ForeColor = Color.Red;
            TstGet.Font = new Font(TstGet.Font, FontStyle.Regular);
        }

        private void TstGet_MouseEnter(object sender, EventArgs e)
        {
            TstGet.ForeColor = Color.DarkGreen;
            TstGet.Font = new Font(TstGet.Font, FontStyle.Bold);
        }
        private void UpdateTstGetText()
        {
            var selectedBrands = new List<string>();

            foreach (DataGridViewRow row in DGVscrap.Rows)
            {
                if (!row.IsNewRow && Convert.ToBoolean(row.Cells["ChkCell"]?.Value) == true)
                {
                    string brandName = row.Cells["BrName"]?.Value?.ToString() ?? "";
                    if (!string.IsNullOrWhiteSpace(brandName))
                        selectedBrands.Add(brandName);
                }
            }

            if (selectedBrands.Count == 0)
            {
                TstGet.Text = TstGetplaceHolder;
            }
            else if (selectedBrands.Count == DGVscrap.Rows.Count)
            {
                TstGet.Text = "Scrap All Brands";
            }
            else
            {
                string joined = string.Join(", ", selectedBrands);
                TstGet.Text = $"{TstGetplaceHolder} ({joined})";
            }
        }
        private async Task<Dictionary<string, List<Phone>>> FetchWithWaitAsync(List<Brand> selectedBrands)
        {
            // Create the wait form
            var waitForm = new WaitForm(this);
            waitForm.Setup("Fetching selected brands...", _loadingGif);
            waitForm.StartPosition = FormStartPosition.CenterParent;

            // Show the wait form modelessly on the UI thread
            waitForm.Show();

            try
            {
                // Start scraping in background
                var results = await Task.Run(() => _scraper.ScrapeSelectedBrandsAsync(selectedBrands));

                return results;
            }
            finally
            {
                // Ensure the wait form closes on the UI thread
                if (waitForm.InvokeRequired)
                    waitForm.Invoke(new Action(() => waitForm.Close()));
                else
                    waitForm.Close();
            }
        }

        private async void TstGet_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable controls
                ScrapBtn.Enabled = false;
                DGVscrap.Enabled = false;
                TstGet.Enabled = false;

                // Get selected brands
                var selectedBrands = GetCheckedBrands();
                if (selectedBrands.Count == 0)
                {
                    MessageBox.Show("Please select at least one brand.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Start WaitForm (modeless)
                var waitForm = new WaitForm(this);
                waitForm.Location = new Point(
this.Location.X + (this.Width - waitForm.Width) / 2,
this.Location.Y + (this.Height - waitForm.Height) / 2
);
                waitForm.Show(this);

                // Actually do the scraping in background
                Dictionary<string, List<Phone>> results = null;
                await Task.Run(async () =>
                {
                    results = await _scraper.ScrapeSelectedBrandsAsync(selectedBrands);
                });

                // Close wait form safely
                waitForm.Invoke(new Action(() => waitForm.Close()));

                // Show result
                if (results != null)
                {
                    var displayForm = new DisplayForm(results);
                    displayForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //  Re-enable controls
                ScrapBtn.Enabled = true;
                DGVscrap.Enabled = true;
                TstGet.Enabled = true;
            }
        }



    }
}