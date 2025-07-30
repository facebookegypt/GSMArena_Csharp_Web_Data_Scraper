using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Reflection;
using System.Resources;

namespace clsGsmar.Services
{
    public class FileReadServices
    {
        private readonly HttpClient _httpClient;

        public FileReadServices()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Returns true if the location is remote (http/https/ftp/ftps).
        /// </summary>
        public bool IsRemoteLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location)) return false;

            location = location.Trim().ToLowerInvariant();
            return location.StartsWith("http://") ||
                   location.StartsWith("https://") ||
                   location.StartsWith("ftp://") ||
                   location.StartsWith("ftps://");
        }

        /// <summary>
        /// Main public method to load lines from remote or local.
        /// </summary>
        /// <param name="location">Either remote URL or local file path</param>
        /// <param name="progress">Optional progress reporting</param>
        /// <returns>List of lines</returns>
        public async Task<List<string>> LoadLinesAsync(string location, IProgress<string> progress = null)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be empty.");

            if (IsRemoteLocation(location))
            {
                progress?.Report($"Downloading from remote: {location}");
                return await LoadRemoteLinesAsync(location, progress);
            }
            else
            {
                progress?.Report($"Reading local file: {location}");
                return await LoadLocalLinesAsync(location, progress);
            }
        }

        /// <summary>
        /// Downloads text content from a remote URL and splits it into lines.
        /// </summary>
        private async Task<List<string>> LoadRemoteLinesAsync(string url, IProgress<string> progress = null)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                progress?.Report($"Downloaded {lines.Count} lines.");
                return lines;
            }
            catch (Exception ex)
            {
                progress?.Report($"Error downloading: {ex.Message}");
                await LoadLocalLinesAsync(Application.StartupPath+"user_agents.txt",progress);
                throw;
            }
        }

        /// <summary>
        /// Reads text content from a local file and splits it into lines.
        /// </summary>
        private async Task<List<string>> LoadLocalLinesAsync(string path, IProgress<string> progress = null)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Local file not found.", path);

                var lines = await File.ReadAllLinesAsync(path);
                progress?.Report($"Loaded {lines.Length} lines from local file.");
                return lines.ToList();
            }
            else
            {
               var lines =  await LoadFallbackFromEmbeddedAsync(progress);
                progress?.Report($"Loaded {lines.Count} lines from local file.");
                return lines.ToList();
            }
            
        }
        public async Task<List<string>> LoadFallbackFromEmbeddedAsync(IProgress<string> progress = null)
        {
            try
            {
                progress?.Report("Loading fallback data from embedded resources...");
                

                // Access the embedded resource
                var assembly = Assembly.GetExecutingAssembly();
                using var stream = assembly.GetManifestResourceStream("clsGsmar.Resources.FallbackData.resources");

                if (stream == null)
                    throw new Exception("Embedded fallback data not found.");

                using var reader = new ResourceReader(stream);
                var data = reader.Cast<DictionaryEntry>()
                                  .FirstOrDefault(entry => entry.Key.ToString() == "ProxyAndUserAgentFallback").Value?.ToString();

                if (string.IsNullOrWhiteSpace(data))
                    throw new Exception("Fallback resource content is empty.");

                var lines = data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                progress?.Report($"Loaded {lines.Count} fallback lines.");
                return lines;
            }
            catch (Exception ex)
            {
                progress?.Report($"Error reading fallback: {ex.Message}");
                throw;
            }
        }
    }
}
