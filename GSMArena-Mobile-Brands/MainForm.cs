using clsGsmar.Models;
using clsGsmar.Tools;
namespace GSMArena_Mobile_Brands
{
    public partial class MainForm : Form
    {
        private ScraperService _scraper;
        string placeHolder = "Leave blank to scrap all";
        public MainForm()
        {
            InitializeComponent();
        }
        private void DGVscrap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGVHelper.CellContentClick(DGVscrap, e);
        }
        private async void MainForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            AllRadio.Checked = true;
            DGVscrap.CellContentClick += DGVscrap_CellContentClick;
            tstlMessage.Text = "Checking internet connection...";

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
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SearchTextBox.Text == placeHolder)
            {
                SearchTextBox.Clear();
                SearchTextBox.ForeColor = Color.Black;
            }
        }
        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            if (SearchTextBox.Text == placeHolder)
            {
                SearchTextBox.ForeColor = Color.DarkGray;
            }
            else
            {
                SearchTextBox.ForeColor = Color.Black;
            }
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                SearchTextBox.Text = placeHolder;
                SearchTextBox.ForeColor = Color.DarkGray;
            }
        }
        private List<Phone> _allPhones = [];
        private async void ScrapBtn_Click(object sender, EventArgs e)
        {
            // Check internet connectivity
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
                //  Determine if we scrape all or search by brand
                if (string.IsNullOrWhiteSpace(SearchTextBox.Text) || SearchTextBox.Text == placeHolder)
                {
                    // Scrape ALL brands (names + phone counts)
                    var results = await _scraper.GetBrandsAsync(progress);

                    // Store for filtering
                    _allBrands = results;

                    // Bind to DataGridView with columns + header checkbox
                    DGVHelper.SetupDataGridViewColumns(DGVscrap);
                    DGVHelper.BindData(DGVscrap, _allBrands);
                    // First, disable all editing:
                    DGVscrap.ReadOnly = false; // Allow selective editing

                    foreach (DataGridViewColumn col in DGVscrap.Columns)
                    {
                        // Make all columns ReadOnly
                        col.ReadOnly = true;
                    }

                    // Enable editing only for the checkbox column
                    if (DGVscrap.Columns.Contains("ChkCell"))
                    {
                        DGVscrap.Columns["ChkCell"].ReadOnly = false;
                    }
                    DGVHelper.AddHeaderCheckBox(DGVscrap, HeaderCheckBox_Clicked);


                    tstlMessage.Text = $"Done. {results.Count} brands loaded.";
                }
                else
                {
                    // For now, no single-brand search for brand list
                    tstlMessage.Text = "Please leave search box blank to load all brands.";
                }
            }
            catch (Exception ex)
            {
                tstlMessage.Text = $"Error: {ex.Message}";
            }
            finally
            {
                ProgressBarSc.Style = ProgressBarStyle.Blocks;
                ScrapBtn.Enabled = true;
            }
        }
        private List<Brand> _allBrands = new List<Brand>();
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            DGVHelper.HeaderCheckBox_Clicked(sender, DGVscrap);
        }

        private void ApplySearchFilter()
        {
            if (_allPhones == null || _allPhones.Count == 0)
            {
                tstMsg.Text = "Scrap first then filter";
                return;
            }

            string filter = SearchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(filter) || filter == placeHolder.ToLower())
            {
                tstMsg.Text = "Specify a filter.";
                DGVscrap.DataSource = _allPhones;
                return;
            }

            List<Phone> filtered;

            if (BrandRadio.Checked)
            {
                // Filter only by brand
                filtered = _allPhones
                    .Where(p => p.Brand?.ToLower().Contains(filter) ?? false)
                    .ToList();
            }
            else if (ModelRadio.Checked)
            {
                // Filter only by model name
                filtered = _allPhones
                    .Where(p => p.Model?.ToLower().Contains(filter) ?? false)
                    .ToList();
            }
            else
            {
                // Default: filter both brand and model
                filtered = _allPhones
                    .Where(p =>
                        (p.Brand?.ToLower().Contains(filter) ?? false) ||
                        (p.Model?.ToLower().Contains(filter) ?? false))
                    .ToList();
            }

            DGVscrap.DataSource = filtered;
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplySearchFilter();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void SearchTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            SearchTextBox.SelectAll();
        }
    }
}
    
    
    