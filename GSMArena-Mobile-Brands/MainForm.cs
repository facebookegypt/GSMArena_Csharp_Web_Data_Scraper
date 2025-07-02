using clsGsmar.Models;
using clsGsmar.Tools;
namespace GSMArena_Mobile_Brands
{
    public partial class MainForm : Form
    {
        string placeHolder = "Leave blank to scrap all";
        public MainForm()
        {
            InitializeComponent();
        }
        private ScraperService _scraper;
        private async void MainForm_Load(object sender, EventArgs e)
        {
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

        private async void ScrapBtn_Click(object sender, EventArgs e)
        {
            if (SearchTextBox.Text != placeHolder)
            { }
            else
            { //Scrap everything
            }
            bool isConnected = await ChkCon.IsInternetAvailableAsync();
            if (isConnected)
            {
                ScrapBtn.Enabled = true; tstlChkCon.Text = "Connected";
                tstlMessage.Text = "Ready";
            }
            else
            {
                ScrapBtn.Enabled = false;
                tstlChkCon.Text = "No Internet";
                tstlMessage.Text = "Please check your connection.";
            }
            tstlMessage.Text = "Starting scrape...";
            ScrapBtn.Enabled = false;
            ProgressBarSc.Style = ProgressBarStyle.Marquee;

            var progress = new Progress<string>(msg =>
            {
                tstlMessage.Text = msg;
            });

            List<Phone> results = new List<Phone>();

            try
            {
                if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
                {
                    // Scrape all brands
                    results = await _scraper.ScrapeAllPhonesAsync(progress);
                }
                else
                {
                    // Scrape by brand
                    results = await _scraper.ScrapePhonesByBrandAsync(SearchTextBox.Text.Trim(), progress);
                }

                DGVscrap.DataSource = results;
            }
            catch (Exception ex)
            {
                tstlMessage.Text = $"Error: {ex.Message}";
            }
            finally
            {
                ProgressBarSc.Style = ProgressBarStyle.Blocks;
                ScrapBtn.Enabled = true;
                tstlMessage.Text = $"Done. {results.Count} phones loaded.";
                _allPhones = results;
                DGVscrap.DataSource = _allPhones;

            }
        }
        private List<Phone> _allPhones = new List<Phone>();
        private void ApplySearchFilter()
        {
            if (_allPhones == null || !_allPhones.Any())
            {
                return;
            }

            string filter = SearchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(filter))
            {
                DGVscrap.DataSource = _allPhones;
            }
            else
            {
                var filtered = _allPhones
                    .Where(p => (p.Brand?.ToLower().Contains(filter) ?? false)
                             || (p.Model?.ToLower().Contains(filter) ?? false))
                    .ToList();

                DGVscrap.DataSource = filtered;
            }
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplySearchFilter();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

    }
}
    
    
    