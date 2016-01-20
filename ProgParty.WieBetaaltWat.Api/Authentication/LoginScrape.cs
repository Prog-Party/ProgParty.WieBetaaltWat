using HtmlAgilityPack;
using ProgParty.WieBetaaltWat.Api;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProgParty.WieBetaaltWat.Api.Authentication
{
    public class LoginScrape
    {
        private string Email { get; set; }
        private string Password { get; set; }

        public string HtmlContent { get; private set; }

        public LoginScrape(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public bool Execute()
        {
            bool worked = GoToLoginPage();
            if (!worked)
                return false;
            worked = DoLogin(Email, Password);
            return worked;
        }

        private bool GoToLoginPage()
        {
            using (var handler = new HttpClientHandler() { CookieContainer = Auth.CookieContainer })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Host = "wiebetaaltwat.nl";
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.125 Safari/537.36");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                    Task<HttpResponseMessage> response = client.GetAsync("https://wiebetaaltwat.nl/index.php");
                    var result = response.Result;

                    return result.IsSuccessStatusCode;
                }
            }
        }

        private bool DoLogin(string email, string password)
        {
            using (var handler = new HttpClientHandler() { CookieContainer = Auth.CookieContainer })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Host = "www.wiebetaaltwat.nl";
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Referer", "https://www.wiebetaaltwat.nl");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "nl-NL,nl;q=0.8,en-GB;q=0.5,en;q=0.3");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10240");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");

                    string uri = "https://www.wiebetaaltwat.nl";

                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("action", "login"),
                        new KeyValuePair<string, string>("username", email),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("login_submit", "Inloggen")
                    });

                    Task<HttpResponseMessage> response = client.PostAsync(uri, content);

                    if (response.Result.IsSuccessStatusCode || response.Result.StatusCode == System.Net.HttpStatusCode.Found)
                    {
                        var htmlContent = response.Result.Content.ReadAsStringAsync().Result;

                        HtmlDocument doc = new HtmlDocument();
                        doc.OptionFixNestedTags = true;
                        doc.OptionOutputAsXml = true;
                        doc.LoadHtml(htmlContent);
                        var loginError = doc.DocumentNode.Descendants("p").FirstOrDefault(t => t.Attributes["id"]?.Value == "message_8");

                        var success = loginError == null;
                        if (success)
                        {
                            Auth.IsLoggedIn = true;
                            //get list with details 
                            HtmlContent = htmlContent;
                        }

                        return success;
                    }
                }
            }
            return false;
        }
    }
}
