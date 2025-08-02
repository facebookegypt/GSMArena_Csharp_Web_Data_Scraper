using clsGsmar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Dropbox.Api.Team.MobileClientPlatform;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GSMArena_Mobile_Brands
{
    public partial class TwaitForm : Form
    {
        private int _totalCount;
        private int _completedCount;
        private string? msg;
        private List<Phone>? _phones;
        private ScraperService _scraper;
        private Func<string, string>? _parser;
            public void InitializeScraping(List<Phone> phones, ScraperService scraper, Func<string, string> parser)
            {
                _phones = phones ?? throw new ArgumentNullException(nameof(phones));
                _scraper = scraper ?? throw new ArgumentNullException(nameof(scraper));
                _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            }
        private async Task ScrapeLoopAsync(CancellationToken token)
        {
            var startTime = DateTime.Now;
            foreach (var phone in _phones)
            {
                // Pause or cancel

                try
                {
                    token.ThrowIfCancellationRequested();

                    // Log attempt
                    //AppendLog($"[{DateTime.Now:HH:mm:ss}] Fetching: {phone.Brand} - {phone.Model}");
                    // Fetch HTML
                    string html = await _scraper.GetHtmlSmartAsync(phone.Url, new Progress<string>(msg => AppendLog($"[{DateTime.Now:HH:mm:ss}] {msg}")));

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
                    progressBar1.Value = percent;
                    lblStatus.Text = $"{_completedCount}/{_totalCount} completed";
                    //lblETA.Text = $@"ETA: {eta:hh\:mm\:ss}";
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
            if (this.PrgrssTextBox.InvokeRequired)
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
        public TwaitForm()
        {
            InitializeComponent();
            AsyncTaskController.PauseEvent = new ManualResetEventSlim(true);
            AsyncTaskController.Cts = new CancellationTokenSource();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            AsyncTaskController.PauseEvent.Reset(); // Pauses the task
            UpdateStatus("Paused...");
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            AsyncTaskController.PauseEvent.Set(); // Resumes the task
            UpdateStatus("Resumed...");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AsyncTaskController.Cts.Cancel(); // Cancels the task
            AsyncTaskController.PauseEvent.Set();
            UpdateStatus("Cancelling...");
        }

        public void UpdateStatus(string message)
        {
            if (!PrgrssTextBox.IsHandleCreated)
            {
                // Wait until handle is created before invoking
                PrgrssTextBox.HandleCreated += (s, e) =>
                {
                    PrgrssTextBox.Invoke((MethodInvoker)(() =>
                        PrgrssTextBox.Text += DateTime.Now.ToString("HH:mm:ss") + " ## " + message + Environment.NewLine));
                };
            }
            else
            {
                PrgrssTextBox.Invoke((MethodInvoker)(() =>
                    PrgrssTextBox.Text += DateTime.Now.ToString("HH:mm:ss") + " ## " + message + Environment.NewLine));
            }
        }

        public void UpdateProgress(int percent)
        {
            progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = percent));
        }

        private Task _scrapeTask;
        private void TwaitForm_Load(object sender, EventArgs e)
        {
            BringToFront();
            TopMost = true;
            PrgrssTextBox.Clear();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            if (_phones != null && _scraper != null && _parser != null)
            {
                _totalCount = _phones.Count;
                _completedCount = 0;
                _scrapeTask = Task.Run(() => ScrapeLoopAsync(AsyncTaskController.Cts.Token));
            }
            if (this.Name == "MainForm")
            { // Start scraping task
                
            }
        }
        public void AppendStatus(string message)
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => AppendStatus(message)));
            else
                PrgrssTextBox.AppendText(message + Environment.NewLine);
        }

        private void PrgrssTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
