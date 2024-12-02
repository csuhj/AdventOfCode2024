using System.Net;
namespace Utilities;

public class DownloadHelper
{
    public const string BaseUrl = "https://adventofcode.com";

    public static async Task<string> DownloadInput(string cookieFilePath, string url)
    {
        string cookie;
        using (var stream = new FileStream(cookieFilePath, FileMode.Open))
            using (StreamReader sr = new StreamReader(stream))
                cookie = sr.ReadToEnd();

        var cookieContainer = new CookieContainer();
        using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
        {
            Uri baseUri = new Uri(BaseUrl);
            using (var client = new HttpClient(handler) { BaseAddress = baseUri })
            {
                cookieContainer.Add(baseUri, new Cookie("session", cookie));
                string content = await client.GetStringAsync(url);
                return content;
            }
        }
    }
}
