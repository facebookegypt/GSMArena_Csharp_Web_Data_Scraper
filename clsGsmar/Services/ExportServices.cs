using System.Text;
using System.Text.Json;
using clsGsmar.Models;

namespace clsGsmar.Services
{
    public class ExportServices
    {
        private readonly string _exportFolder;

        public ExportServices(string exportFolder)
        {
            if (string.IsNullOrWhiteSpace(exportFolder) || !Directory.Exists(exportFolder))
                throw new ArgumentException("Export folder does not exist.", nameof(exportFolder));

            _exportFolder = exportFolder;
        }

        public async Task ExportPhonesAsync(
            List<Phone> phones,
            string format,
            string fileName = null,
            IProgress<string> progress = null)
        {
            if (phones == null || phones.Count == 0)
                throw new ArgumentException("No phones to export.");

            format = format?.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = $"export_{DateTime.Now:yyyyMMdd_HHmmss}.{format}";
            }

            string path = Path.Combine(_exportFolder, fileName);

            progress?.Report($"Exporting {phones.Count} phones as {format}...");

            switch (format)
            {
                case "csv":
                    await ExportAsCsvAsync(phones, path, progress);
                    break;

                case "txt":
                    await ExportAsTxtAsync(phones, path, progress);
                    break;

                case "json":
                    await ExportAsJsonAsync(phones, path, progress);
                    break;

                default:
                    throw new NotSupportedException($"Export format '{format}' is not supported.");
            }

            progress?.Report($"Export completed: {path}");
        }

        private async Task ExportAsCsvAsync(List<Phone> phones, string path, IProgress<string> progress)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                await writer.WriteLineAsync("Brand,Model,Specs");

                foreach (var phone in phones)
                {
                    string cleanSpecs = (phone.FormattedSpecs ?? "").Replace("\"", "'").Replace("\n", " ").Replace("\r", " ").Trim();
                    string line = $"\"{phone.Brand}\",\"{phone.Model}\",\"{cleanSpecs}\"";

                    await writer.WriteLineAsync(line);
                }
            }

            progress?.Report("CSV export finished.");
        }

        private async Task ExportAsTxtAsync(List<Phone> phones, string path, IProgress<string> progress)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (var phone in phones)
                {
                    await writer.WriteLineAsync($"Brand: {phone.Brand}");
                    await writer.WriteLineAsync($"Model: {phone.Model}");
                    await writer.WriteLineAsync($"Specs:\n{phone.FormattedSpecs}");
                    await writer.WriteLineAsync(new string('-', 50));
                }
            }

            progress?.Report("TXT export finished.");
        }

        private async Task ExportAsJsonAsync(List<Phone> phones, string path, IProgress<string> progress)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            using (var stream = File.Create(path))
            {
                await JsonSerializer.SerializeAsync(stream, phones, options);
            }

            progress?.Report("JSON export finished.");
        }
    }
}
