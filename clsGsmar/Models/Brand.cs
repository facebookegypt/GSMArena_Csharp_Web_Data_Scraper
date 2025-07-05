namespace clsGsmar.Models
{
    public class Brand
    {
        public int Id { get; set; }          // Will set in MainForm for grid
        public string Name { get; set; }     // Brand name
        public string Url { get; set; }      // Link to brand page
        public int PhoneCount { get; set; }  // Extracted from <span>
    }
}
