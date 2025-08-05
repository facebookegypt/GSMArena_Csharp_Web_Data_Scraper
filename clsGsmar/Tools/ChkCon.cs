using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace clsGsmar.Tools
{
    public static class ChkCon
    {
        private static readonly HttpClient _client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        /// <summary>
        /// Asynchronously tests if internet is available by performing a HEAD request.
        /// </summary>
        /// <param name="progress">Optional progress reporter</param>
        /// <param name="testUrl">Optional test URL (defaults to Google)</param>
        /// <returns>True if successful, False otherwise</returns>
        public static async Task<bool> IsInternetAvailableAsync(
            IProgress<string> ?progress = null,
            string testUrl = "https://www.google.com")
        {
            try
            {
                progress?.Report("Checking internet connectivity...");

                // HEAD is faster and lighter than GET
                using var request = new HttpRequestMessage(HttpMethod.Head, testUrl);
                request.Headers.UserAgent.ParseAdd("Mozilla/5.0");

                using var response = await _client.SendAsync(request);
                bool success = response.IsSuccessStatusCode;

                progress?.Report(success ? "Internet connection OK" : "Internet check failed");
                return success;
            }
            catch (Exception ex)
            {
                progress?.Report($"Internet check failed: {ex.Message}");
                return false;
            }
        }
    }
}