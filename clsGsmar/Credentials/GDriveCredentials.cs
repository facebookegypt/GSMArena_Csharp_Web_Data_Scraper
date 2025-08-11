using System.IO;
using System.Text.Json;

namespace clsGsmar
{
    public class GDriveCredentials
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? RedirectUri { get; set; }
        public string? AuthUri { get; set; }
        public string? TokenUri { get; set; }

        public static GDriveCredentials Load()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "G_Credentials.json");
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Google credentials file not found: {filePath}");

            var json = File.ReadAllText(filePath);
            var root = JsonDocument.Parse(json).RootElement;

            var webSection = root.GetProperty("web");

            return new GDriveCredentials
            {
                ClientId = webSection.GetProperty("client_id").GetString(),
                ClientSecret = webSection.GetProperty("client_secret").GetString(),
                RedirectUri = webSection.GetProperty("redirect_uris")[0].GetString(),
                AuthUri = webSection.GetProperty("auth_uri").GetString(),
                TokenUri = webSection.GetProperty("token_uri").GetString()
            };
        }
    }
}
