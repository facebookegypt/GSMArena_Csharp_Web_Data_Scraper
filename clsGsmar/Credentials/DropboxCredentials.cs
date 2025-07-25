using System.IO;
using System.Text.Json;

namespace clsGsmar
{
    public class DropboxCredentials
    {
        public string? AppKey { get; set; }
        public string? AppSecret { get; set; }
        public string? RedirectUri { get; set; }

        public static DropboxCredentials Load()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "credentials.json");
            var json = File.ReadAllText(filePath);
            var root = JsonDocument.Parse(json).RootElement;
            var section = root.GetProperty("Dropbox");

            return new DropboxCredentials
            {
                AppKey = section.GetProperty("AppKey").GetString(),
                AppSecret = section.GetProperty("AppSecret").GetString(),
                RedirectUri = section.GetProperty("RedirectUri").GetString()
            };
        }
    }
}
