using System.Net.Http;
using System.Threading.Tasks;

namespace clsGsmar.Tools
{
    public static class ChkCon
    {
        private static readonly HttpClient _httpClient = new();

        /// <summary>
        /// Async method to check if internet is available.
        /// Returns true if successful, false otherwise.
        /// </summary>
        public static async Task<bool> IsInternetAvailableAsync()
        {
            try
            {
                using var response = await _httpClient.GetAsync("https://www.google.com");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}