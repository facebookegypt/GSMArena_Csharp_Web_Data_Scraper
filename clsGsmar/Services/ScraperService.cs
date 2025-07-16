using clsGsmar;
using clsGsmar.Models;
using clsGsmar.Services;
using HtmlAgilityPack;
using System.Net;
using System.Resources;

public class ScraperService
    {
        private readonly Random _random = new Random();
        private readonly List<string> _proxies = new();
        private readonly List<string> _userAgents = new();
        private void ParseProxyAndUserAgentLines(List<string> lines)
        {
            _proxies.Clear();
            _userAgents.Clear();

            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith("PROXY:", StringComparison.OrdinalIgnoreCase))
                {
                    var proxy = trimmed.Substring("PROXY:".Length).Trim();
                    if (!string.IsNullOrWhiteSpace(proxy))
                        _proxies.Add(proxy);
                }
                else if (trimmed.StartsWith("USERAGENT:", StringComparison.OrdinalIgnoreCase))
                {
                    var ua = trimmed.Substring("USERAGENT:".Length).Trim();
                    if (!string.IsNullOrWhiteSpace(ua))
                        _userAgents.Add(ua);
                }
            }
        }
        private readonly FileReadServices _fileReadServices;
        public ScraperService()
        {
            _fileReadServices = new FileReadServices();
        }
    private async Task<List<string>> LoadProxyUserAgentLinesAsync(string location, IProgress<string> progress)
    {
        if (!string.IsNullOrWhiteSpace(location))
        {
            try
            {
                progress?.Report($"Loading user-provided proxy/user-agent list from: {location}");
                return await _fileReadServices.LoadLinesAsync(location, progress);
            }
            catch (Exception ex)
            {
                progress?.Report($"Failed to load user-provided file. Will use built-in fallback. Error: {ex.Message}");
            }
        }

        progress?.Report("Loading built-in fallback proxy/user-agent list from resources.");
        return LoadEmbeddedFallbackLines();
    }

    private List<string> LoadEmbeddedFallbackLines()
    {
        var lines = new List<string>();
        var asm = typeof(ScraperService).Assembly;

        using (var stream = asm.GetManifestResourceStream("clsGsmar.user_agents.txt"))
        {
            var names = asm.GetManifestResourceNames();
            foreach (var name in names) Console.WriteLine(name);
            if (stream == null) return lines;

            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                    lines.Add(line.Trim());
            }
        }
        return lines;
    }


    //Plan A: No user-agents neither Proxy
    private bool IsBlockedOrEmpty(string html)
    {
        if (string.IsNullOrWhiteSpace(html)) return true;

        html = html.ToLower();

        return html.Contains("enable javascript") ||
               html.Contains("403 forbidden") ||
               html.Contains("robot check") ||
               html.Contains("are you human") ||
               html.Contains("access denied");
    }
    //private int GetTotalPages(string html)
    //{
    //    var doc = new HtmlAgilityPack.HtmlDocument();
    //    doc.LoadHtml(html);

    //    var nav = doc.DocumentNode.SelectSingleNode("//div[@class='nav-pages']");
    //    if (nav == null) return 1;

    //    var pageLinks = nav.SelectNodes(".//a");
    //    if (pageLinks == null) return 1;

    //    int maxPage = 1;
    //    foreach (var link in pageLinks)
    //    {
    //        if (int.TryParse(link.InnerText.Trim(), out int num))
    //        {
    //            if (num > maxPage) maxPage = num;
    //        }
    //    }
    //    return maxPage;
    //}


    private string GetPagedUrl(string brandUrl, int page)
    {
        if (page == 1)
            return brandUrl;

        var lastPart = brandUrl.Split('/').LastOrDefault();
        if (string.IsNullOrEmpty(lastPart)) return brandUrl;

        if (lastPart.EndsWith(".php"))
            lastPart = lastPart.Replace(".php", "");

        var parts = lastPart.Split('-').ToList();
        if (parts.Count < 2) return brandUrl;

        var brandId = parts.Last();
        parts.RemoveAt(parts.Count - 1);
        var prefix = string.Join("-", parts);

        var baseUrl = brandUrl.Substring(0, brandUrl.LastIndexOf('/'));
        return $"{baseUrl}/{prefix}-f-{brandId}-0-p{page}.php";
    }
    private List<Phone> ParsePhonesFromHtml(string html, string brandName)
    {
        var phones = new List<Phone>();
        var doc = new HtmlAgilityPack.HtmlDocument();
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
        return phones;
    }

    public async Task<string> GetHtmlSmartAsync(string url, IProgress<string> progress = null)
    {
        var _httpClient = new HttpClient();
    try
    {
        // Try default HttpClient first
        progress?.Report("Trying direct connection...");
        var html = await _httpClient.GetStringAsync(url);

        if (!IsBlockedOrEmpty(html))
        {
            return html;
        }

        progress?.Report("Blocked or empty response. Trying fallback...");
    }
    catch (HttpRequestException ex)
    {
        progress?.Report($"Direct connection failed: {ex.Message}");
    }
    catch (Exception ex)
    {
        progress?.Report($"Unexpected error on direct connection: {ex.Message}");
    }

    // Use fallback with proxies/user-agents
    return await TryGetHtmlWithFallbackAsync(url, progress);
}
    private readonly string _logFilePath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    $"ScrapingLog_{DateTime.Now:MMMMddyyyy_HHmmss}.txt"
);

    private async Task<string> TryGetHtmlWithFallbackAsync(string url, IProgress<string> progress = null)
    {
        await LoadProxyUserAgentLinesAsync(url, progress);

        foreach (var proxy in _proxies)
        {
            foreach (var agent in _userAgents)
            {
                for (int attempt = 1; attempt <= 2; attempt++)
                {
                    try
                    {
                        progress?.Report($"Trying Proxy: {proxy} | User-Agent: {agent.Substring(0, Math.Min(agent.Length, 50))} (Attempt {attempt})");

                        var handler = new HttpClientHandler
                        {
                            UseCookies = true,
                            CookieContainer = new CookieContainer(),
                            Proxy = new WebProxy(proxy),
                            UseProxy = true,
                            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                        };

                        using var client = new HttpClient(handler);

                        // Essential headers
                        client.DefaultRequestHeaders.Add("User-Agent", agent);
                        client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                        client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
                        client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br"); // ensure we accept compressed content
                        client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                        client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                        client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                        client.DefaultRequestHeaders.Add("Pragma", "no-cache");
                        client.DefaultRequestHeaders.Add("DNT", "1"); // Do Not Track
                        client.DefaultRequestHeaders.Referrer = new Uri("https://www.google.com"); // mimic organic traffic
                        client.DefaultRequestHeaders.Host = new Uri(url).Host;

                        await Task.Delay(_random.Next(1000, 3000));

                        var html = await client.GetStringAsync(url);

                        if (string.IsNullOrWhiteSpace(html) ||
                            html.Contains("enable JavaScript", StringComparison.OrdinalIgnoreCase) ||
                            html.Contains("are you human", StringComparison.OrdinalIgnoreCase))
                        {
                            progress?.Report($"Blocked content detected. Trying next combination...");
                            continue;
                        }

                        return html;
                    }
                    catch (HttpRequestException ex)
                    {
                        string msg = $"Request error via Proxy {proxy}: {ex.Message}";
                        progress?.Report(msg);
                        LogScrapingError(msg); // ← NEW
                        await Task.Delay(_random.Next(500, 1500));
                    }
                    catch (Exception ex)
                    {
                        string msg = $"Unexpected error: {ex.Message}";
                        progress?.Report(msg);
                        LogScrapingError(msg); // ← NEW
                    }

                }
            }
        }

        throw new Exception("All proxies and user-agents failed. Could not fetch usable HTML.");
    }
    private void LogScrapingError(string message)
    {
        try
        {
            string logDir = AppDomain.CurrentDomain.BaseDirectory;
            string logFile = Path.Combine(logDir, $"ScrapingLog_{DateTime.Now:MMMMddyyyy_HHmmss}.txt");

            // Append message with timestamp
            File.AppendAllText(_logFilePath, $"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");

        }
        catch
        {
            // Suppress logging errors
        }
    }

    // Public method to scrape all brands
    public async Task<List<Phone>> ScrapeAllPhonesAsync(IProgress<string> progress = null)
    {
        progress?.Report("Starting scrape: All brands");

        var brands = await GetBrandsAsync(progress);
        progress?.Report($"Found {brands.Count} brands. Ready to scrape phones.");

        var allPhones = new List<Phone>();

        foreach (var brand in brands)
        {
            // Add polite random delay between requests to avoid detection
            await Task.Delay(_random.Next(1200, 3000));

            var phones = await GetPhonesForBrandAsync(brand.Url, brand.Name, progress);
            allPhones.AddRange(phones);
        }

        progress?.Report($"Scraping complete! Total phones found: {allPhones.Count}");
        return allPhones;
    }

    // Public method to scrape a specific brand by name
    public async Task<List<Phone>> ScrapePhonesByBrandAsync(string brand, IProgress<string> progress = null)
    {
        progress?.Report($"Starting scrape for brand: {brand}");

        var brands = await GetBrandsAsync(progress);
        var matchedBrand = brands
            .FirstOrDefault(b => b.Name.Equals(brand, StringComparison.OrdinalIgnoreCase));

        if (matchedBrand == null)
        {
            progress?.Report($"Brand '{brand}' not found on GSMArena.");
            return new List<Phone>();
        }

        // Optional polite delay even for single request
        await Task.Delay(_random.Next(1200, 3000));

        return await GetPhonesForBrandAsync(matchedBrand.Url, matchedBrand.Name, progress);
    }

    // Private method to scrape the full brand list
    public async Task<List<Brand>> GetBrandsAsync(IProgress<string> progress = null)
    {
        var brands = new List<Brand>();
        var url = "https://www.gsmarena.com/makers.php3";

        progress?.Report("Downloading brand list...");

        try
        {
            string html = await GetHtmlSmartAsync(url, progress);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // GSMArena 'st-text' table rows
            var brandLinks = doc.DocumentNode.SelectNodes("//div[@class='st-text']//tr//a");

            if (brandLinks != null)
            {
                int idCounter = 1;
                foreach (var link in brandLinks)
                {
                    var href = link.GetAttributeValue("href", "");
                    var rawName = link.InnerText.Trim();

                    // Extract <span> content
                    var spanNode = link.SelectSingleNode(".//span");
                    int phoneCount = 0;
                    if (spanNode != null)
                    {
                        var countText = spanNode.InnerText.Replace("devices", "").Trim();
                        int.TryParse(countText.Split(' ')[0], out phoneCount);
                    }

                    // Clean name: Remove span text
                    var name = link.InnerText;
                    if (spanNode != null)
                    {
                        name = name.Replace(spanNode.InnerText, "").Trim();
                    }

                    if (!string.IsNullOrEmpty(href) && !string.IsNullOrEmpty(name))
                    {
                        brands.Add(new Brand
                        {
                            Id = idCounter++,
                            Name = System.Net.WebUtility.HtmlDecode(name),
                            Url = "https://www.gsmarena.com/" + href,
                            PhoneCount = phoneCount
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

    // Private method to scrape all phones for a given brand page
    private async Task<List<Phone>> GetPhonesForBrandAsync(string brandUrl, string brandName, IProgress<string> progress = null)
    {
        var allPhones = new List<Phone>();
        int currentPage = 1;
        bool morePages = true;

        while (morePages)
        {
            // Compute paged URL
            string pageUrl = GetPagedUrl(brandUrl, currentPage);
            progress?.Report($"Downloading page {currentPage} of {brandName}...");

            var html = await GetHtmlSmartAsync(pageUrl, progress);
            if (string.IsNullOrWhiteSpace(html))
            {
                progress?.Report($"No HTML returned for {pageUrl}");
                break;
            }

            // Parse phones from this page
            var phones = ParsePhonesFromHtml(html, brandName);
            if (phones.Count == 0)
            {
                progress?.Report($"No more phones found on page {currentPage} of {brandName}");
                break;
            }

            allPhones.AddRange(phones);
            currentPage++;

            // Check if this page had a "Next" link
            morePages = HasNextPage(html, currentPage);
            await Task.Delay(_random.Next(1200, 3000));
        }

        progress?.Report($"Finished {brandName}: {allPhones.Count} phones total.");
        return allPhones;
    }
    private bool HasNextPage(string html, int currentPage)
    {
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        var nav = doc.DocumentNode.SelectSingleNode("//div[@class='nav-pages']");
        if (nav == null) return false;

        var links = nav.SelectNodes(".//a");
        if (links == null) return false;

        // Look for next page link
        foreach (var link in links)
        {
            if (int.TryParse(link.InnerText.Trim(), out int pageNum))
            {
                if (pageNum >= currentPage)
                    return true;
            }
        }
        return false;
    }
    public async Task<Dictionary<string, List<Phone>>> ScrapeSelectedBrandsAsync(List<Brand> selectedBrands, IProgress<string> progress = null)
    {
        var result = new Dictionary<string, List<Phone>>();

        foreach (var brand in selectedBrands)
        {
            progress?.Report($"Scraping brand: {brand.Name}");
            var phones = await GetPhonesForBrandAsync(brand.Url, brand.Name, progress);
            result[brand.Name] = phones;
        }

        return result;
    }

}