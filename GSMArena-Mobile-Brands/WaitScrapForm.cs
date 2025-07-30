using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsGsmar.Models;
using clsGsmar.Services;

namespace GSMArena_Mobile_Brands
{
    public partial class WaitScrapForm : Form
    {
        private List<Phone> _phones;
        private ScraperService _scraper;
        private Func<string, string> _parser;

        private CancellationTokenSource _cts;
        public ManualResetEventSlim _pauseEvent;
        private Task _scrapeTask;

        private int _totalCount;
        private int _completedCount;
        public WaitScrapForm()
        {
            InitializeComponent();
            btnResume.Enabled = false;
            btnStop.Enabled = true;
        }

        /// <summary>
        /// Initialize phones list, scraper and parser.
        /// Call before ShowDialog or Show.
        /// </summary>
        public void InitializeScraping(List<Phone> phones, ScraperService scraper, Func<string, string> parser)
        {
            _phones = phones ?? throw new ArgumentNullException(nameof(phones));
            _scraper = scraper ?? throw new ArgumentNullException(nameof(scraper));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        private void WaitScrapForm_Load(object sender, EventArgs e)
        {
            // Prepare cancellation and pause control
            _cts = new CancellationTokenSource();
            _pauseEvent = new ManualResetEventSlim(true);

            _totalCount = _phones.Count;
            _completedCount = 0;

            ProgScrap.Minimum = 0;
            ProgScrap.Maximum = 100;
            ProgScrap.Value = 0;

            lblStatus.Text = "Starting scraping...";
            lblETA.Text = string.Empty;

            // Start scraping task
            _scrapeTask = Task.Run(() => ScrapeLoopAsync(_cts.Token));
        }

        private async Task ScrapeLoopAsync(CancellationToken token)
        {
            var startTime = DateTime.Now;
            foreach (var phone in _phones)
            {
                // Pause or cancel
                _pauseEvent.Wait(token);
                token.ThrowIfCancellationRequested();

                // Log attempt
                AppendLog($"[{DateTime.Now:HH:mm:ss}] Fetching: {phone.Brand} - {phone.Model}");

                try
                {
                    // Fetch HTML
                    string html = await _scraper.GetHtmlSmartAsync(phone.Url);//, new Progress<string>(msg => AppendLog($"[{DateTime.Now:HH:mm:ss}] {msg}")));

                    // string html = await _scraper.GetHtmlSmartAsync(phone.Url, new Progress<string>(msg => AppendLog($"[{DateTime.Now:HH:mm:ss}] {msg}")));
                    // Parse immediately
                    string specs = _parser(html);
                    phone.FormattedSpecs = specs;

                    AppendLog($"[{DateTime.Now:HH:mm:ss}] Success: {phone.Brand} - {phone.Model}");
                }
                catch (OperationCanceledException)
                {
                    AppendLog($"[{DateTime.Now:HH:mm:ss}] Scraping cancelled.");
                    break;
                }
                catch (Exception ex)
                {
                    AppendLog($"[{DateTime.Now:HH:mm:ss}] Error: {ex.Message}");
                }

                _completedCount++;
                // Update progress bar and ETA
                var percent = (int)((_completedCount / (double)_totalCount) * 100);
                var elapsed = DateTime.Now - startTime;
                var eta = TimeSpan.FromTicks(elapsed.Ticks * (_totalCount - _completedCount) / _completedCount);

                this.Invoke(() =>
                {
                    ProgScrap.Value = percent;
                    lblStatus.Text = $"{_completedCount}/{_totalCount} completed";
                    lblETA.Text = $@"ETA: {eta:hh\:mm\:ss}";
                });
            }

            // Finish
            this.Invoke(() =>
            {
                AppendLog($"[{DateTime.Now:HH:mm:ss}] ## {_completedCount} phones fetched.");
                DialogResult = DialogResult.OK;
                Close();
            });
        }

        private void AppendLog(string text)
        {
            if (PrgrssTextBox.InvokeRequired)
            {
                PrgrssTextBox.Invoke(new Action(() =>
                {
                    PrgrssTextBox.AppendText(text + Environment.NewLine);
                    PrgrssTextBox.ScrollToCaret();
                }));
            }
            else
            {
                PrgrssTextBox.AppendText(text + Environment.NewLine);
                PrgrssTextBox.ScrollToCaret();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnPause.Enabled = false;
            btnResume.Enabled = true;
            _pauseEvent.Reset();
            AppendLog($"[{DateTime.Now:HH:mm:ss}] Paused by user.");
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            btnPause.Enabled = true;
            btnResume.Enabled = false;
            _pauseEvent.Set();
            AppendLog($"[{DateTime.Now:HH:mm:ss}] Resumed by user.");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnPause.Enabled = false;
            btnResume.Enabled = false;
            btnStop.Enabled = false;
            _cts.Cancel();
            AppendLog($"[{DateTime.Now:HH:mm:ss}] Stop requested.");
            // Close form and return cancel result
            this.Invoke(new Action(() =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }));
        }

        private void WaitScrapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure cancellation
            _cts.Cancel();
            _pauseEvent.Set();
        }

        private void ProgScrap_Click(object sender, EventArgs e)
        {

        }
    }
}
