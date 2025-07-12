namespace clsGsmar.Models
{
    public class Phone
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Url { get; set; }
        public string? ImageUrl { get; set; }

        public string? SpecsPreview { get; set; } // optional, used for UI preview
        public string? RawSpecsHtml { get; set; } // raw HTML, used internally
        public string? FormattedSpecs { get; set; } // plain-text specs, used internally
    }
}