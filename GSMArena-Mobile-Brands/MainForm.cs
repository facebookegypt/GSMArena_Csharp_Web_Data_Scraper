using clsGsmar.Models;
using clsGsmar.Tools;

namespace GSMArena_Mobile_Brands
{
    public partial class MainForm : Form
    {
        private ScraperService _scraper;
        private readonly string placeHolder = "Leave blank to scrap all";
        private List<Brand> _allBrands = new List<Brand>();

        public MainForm()
        {
            InitializeComponent();
        }

        // Handles form load event
        private async void MainForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true; // Allows form to capture Esc key globally
            AllRadio.Checked = true; // Default mode
            DGVscrap.CellContentClick += DGVscrap_CellContentClick;

            tstlMessage.Text = "Checking internet connection...";
            groupBox1.Enabled = false;

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
        }

        // Cell checkbox click
        private void DGVscrap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGVHelper.CellContentClick(DGVscrap, e);
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
    }
}