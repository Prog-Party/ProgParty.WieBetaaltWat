using HtmlAgilityPack;
using ProgParty.WieBetaaltWat.Api.Result;
using System.Linq;
using System.Net.Http;

namespace ProgParty.WieBetaaltWat.Api.Scrape
{
    internal class LijstScrape
    {
        private Parameter.LijstParameter Parameters { get; set; }

        public LijstScrape(Parameter.LijstParameter parameters)
        {
            Parameters = parameters;
        }

        public LijstResult Execute()
        {
            string url = Parameters.Url;
            string loggedInName = Parameters.LoginName;
            string loggedInPassword = Parameters.LoginPassword;


            var loginScrape = new Authentication.LoginScrape(loggedInName, loggedInPassword);
            bool isLoggedIn = loginScrape.Execute();

            //if (isLoggedIn)
            //    return ConvertToResult(loginScrape.HtmlContent);
            //return new LijstResult();


            using (var handler = new HttpClientHandler() { CookieContainer = Auth.CookieContainer })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Host = "www.wiebetaaltwat.nl";
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.125 Safari/537.36");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");

                    var response = client.GetAsync(url).Result;
                    var result = response.Content.ReadAsStringAsync().Result;

                    return ConvertToResult(result);
                }
            }
        }

        public LijstResult ConvertToResult(string result)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(result);
            var node = htmlDocument.DocumentNode;

            var tableNode = node.Descendants("table").FirstOrDefault(c => c.Attributes["is"]?.Value?.Contains("list") ?? false);

            var tableRows = tableNode.Descendants("td").ToList();
            
            LijstResult lijstResult = new LijstResult();
            lijstResult.PaidBy = tableRows[0]?.InnerText ?? string.Empty;
            lijstResult.Description = tableRows[1]?.InnerText ?? string.Empty;
            lijstResult.Amount = tableRows[2]?.InnerText ?? string.Empty;
            lijstResult.Date = tableRows[3]?.InnerText ?? string.Empty;
            lijstResult.WhoElse = tableRows[4]?.InnerText ?? string.Empty;
            
            return lijstResult;
        }
    }
}
