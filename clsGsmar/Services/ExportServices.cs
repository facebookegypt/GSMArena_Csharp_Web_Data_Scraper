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

        /// <summary>
        /// Main entry point
        /// </summary>
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

            // Map and clean
            var exportList = MapPhonesForExport(phones);

            switch (format)
            {
                case "csv":
                    await ExportAsCsvAsync(exportList, path, progress);
                    break;

                case "txt":
                    await ExportAsTxtAsync(exportList, path, progress);
                    break;

                case "json":
                    await ExportAsJsonAsync(exportList, path, progress);
                    break;

                default:
                    throw new NotSupportedException($"Export format '{format}' is not supported.");
            }

            progress?.Report($"Export completed: {path}");
        }

        /// <summary>
        /// DTO for export - Only the required fields.
        /// </summary>
        private class PhoneExportDto
        {
            public string Brand { get; set; }
            public string Model { get; set; }
            public string ImageUrl { get; set; }
            public string ModelUrl { get; set; }
            public string Specs { get; set; }
        }

        /// <summary>
        /// Maps Phone objects to export DTO, with cleaning
        /// </summary>
        private List<PhoneExportDto> MapPhonesForExport(List<Phone> phones)
        {
            var list = new List<PhoneExportDto>();

            foreach (var p in phones)
            {
                string cleanedSpecs = (p.FormattedSpecs ?? "")
                    .Replace("•", "-")
                    .Replace("\r", " ")
                    .Replace("\n", " ")
                    .Replace("\t", " ")
                    .Replace("  ", " ")
                    .Trim();

                list.Add(new PhoneExportDto
                {
                    Brand = p.Brand,
                    Model = p.Model,
                    ImageUrl = p.ImageUrl,
                    ModelUrl = p.Url,
                    Specs = cleanedSpecs
                });
            }

            return list;
        }

        /// <summary>
        /// CSV Export
        /// </summary>
        private async Task ExportAsCsvAsync(List<PhoneExportDto> phones, string path, IProgress<string> progress)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                await writer.WriteLineAsync("Brand,Model,ImageUrl,ModelUrl,Specs");

                foreach (var p in phones)
                {
                    string line = $"\"{p.Brand}\",\"{p.Model}\",\"{p.ImageUrl}\",\"{p.ModelUrl}\",\"{p.Specs}\"";
                    await writer.WriteLineAsync(line);
                }
            }

            progress?.Report("CSV export finished.");
        }

        /// <summary>
        /// TXT Export
        /// </summary>
        private async Task ExportAsTxtAsync(List<PhoneExportDto> phones, string path, IProgress<string> progress)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (var p in phones)
                {
                    await writer.WriteLineAsync($"Brand: {p.Brand}");
                    await writer.WriteLineAsync($"Model: {p.Model}");
                    await writer.WriteLineAsync($"ImageUrl: {p.ImageUrl}");
                    await writer.WriteLineAsync($"ModelUrl: {p.ModelUrl}");
                    await writer.WriteLineAsync($"Specs: {p.Specs}");
                    await writer.WriteLineAsync(new string('-', 50));
                }
            }

            progress?.Report("TXT export finished.");
        }

        /// <summary>
        /// JSON Export
        /// </summary>
        private async Task ExportAsJsonAsync(List<PhoneExportDto> phones, string path, IProgress<string> progress)
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
