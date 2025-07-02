using clsGsmar.Models;
using HtmlAgilityPack;

public class ScraperService
{
    private readonly HttpClient _httpClient;

    public ScraperService()
    {
        _httpClient = new HttpClient();
    }

    // Main public method
    public async Task<List<Phone>> ScrapeAllPhonesAsync(IProgress<string> progress = null)
    {
        progress?.Report("Starting scrape: All brands");

        var brands = await GetBrandsAsync(progress);
        progress?.Report($"Found {brands.Count} brands. Ready to scrape phones.");

        var allPhones = new List<Phone>();

        foreach (var brand in brands)
        {
            var phones = await GetPhonesForBrandAsync(brand.Url, brand.Name, progress);
            allPhones.AddRange(phones);
        }

        progress?.Report($"Scraping complete! Total phones found: {allPhones.Count}");
        return allPhones;
    }

    public async Task<List<Phone>> ScrapePhonesByBrandAsync(string brand, IProgress<string> progress = null)
    {
        progress?.Report($"Starting scrape: Brand = {brand}");
        await Task.Delay(1000);
        progress?.Report("Scraping complete!");
        return new List<Phone>();
    }

    // ✅ Here's the new private helper
    private async Task<List<Brand>> GetBrandsAsync(IProgress<string> progress = null)
    {
        var brands = new List<Brand>();
        var url = "https://www.gsmarena.com/makers.php3";

        progress?.Report("Downloading brand list...");

        try
        {
            var html = await _httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            //var brandLinks = doc.DocumentNode.SelectNodes("//div[@class='st-text']//ul//li//a");
            var brandLinks = doc.DocumentNode.SelectNodes("//div[contains(@class,'brandmenu-v2')]//a");


            if (brandLinks != null)
            {
                foreach (var link in brandLinks)
                {
                    var href = link.GetAttributeValue("href", "");
                    var name = link.InnerText.Trim();

                    if (!string.IsNullOrEmpty(href) && !string.IsNullOrEmpty(name))
                    {
                        brands.Add(new Brand
                        {
                            Name = System.Net.WebUtility.HtmlDecode(name),
                            Url = "https://www.gsmarena.com/" + href
                        });
                    }
                }
            }

            progress?.Report($"Found {brands.Count} brands.");
        }
        catch (Exception ex)
        {
            progress?.Report($"Error retrieving brands: {ex.Message}");
        }

        return brands;
    }
    private async Task<List<Phone>> GetPhonesForBrandAsync(string brandUrl, string brandName, IProgress<string> progress = null)
    {
        var phones = new List<Phone>();

        progress?.Report($"Downloading brand page: {brandName}");

        try
        {
            var html = await _httpClient.GetStringAsync(brandUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var phoneLinks = doc.DocumentNode.SelectNodes("//div[@class='makers']//ul//li//a");

            if (phoneLinks != null)
            {
                foreach (var link in phoneLinks)
                {
                    var href = link.GetAttributeValue("href", "");
                    var detailUrl = "https://www.gsmarena.com/" + href;

                    var imgNode = link.SelectSingleNode(".//img");
                    var imgUrl = imgNode?.GetAttributeValue("src", "");

                    var nameNode = link.SelectSingleNode(".//strong");
                    var modelName = nameNode?.InnerText.Trim() ?? "";

                    var specNode = link.SelectSingleNode(".//span");
                    var spec = specNode?.InnerText.Trim() ?? "";

                    if (!string.IsNullOrEmpty(modelName))
                    {
                        phones.Add(new Phone
                        {
                            Brand = brandName,
                            Model = System.Net.WebUtility.HtmlDecode(modelName),
                            Url = detailUrl,
                            ImageUrl = imgUrl,
                            SpecsPreview = spec
                        });
                    }
                }
            }
            progress?.Report($"Found {phones.Count} phones for {brandName}.");
        }
        catch (Exception ex)
        {
            progress?.Report($"Error scraping {brandName}: {ex.Message}");
        }

        return phones;
    }
}