using HtmlAgilityPack;
using ProgParty.WieBetaaltWat.ApiUniversal.Result;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ProgParty.WieBetaaltWat.ApiUniversal.Scrape
{
    internal class LijstScrape
    {
        private Parameter.LijstParameter Parameters { get; set; }

        public LijstScrape(Parameter.LijstParameter parameters)
        {
            Parameters = parameters;
        }

        public List<LijstResult> Execute()
        {
            string url = Parameters.Url;
            string loggedInName = Parameters.LoginName;
            string loggedInPassword = Parameters.LoginPassword;


            var loginScrape = new Authentication.LoginScrape(loggedInName, loggedInPassword);
            bool isLoggedIn = loginScrape.Execute();

            using (var handler = new HttpClientHandler() { CookieContainer = Auth.CookieContainer })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Host = "www.wiebetaaltwat.nl";
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.125 Safari/537.36");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");

                    var response = client.GetAsync(url).Result;
                    var result = response.Content.ReadAsStringAsync().Result;

                    if (isLoggedIn)
                        return ConvertToResult(result);
                    return new List<LijstResult>();
                }
            }
        }

        public List<LijstResult> ConvertToResult(string result)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(result);
            var node = htmlDocument.DocumentNode;

            var tableNode = node.Descendants("table").FirstOrDefault(c => c.Attributes["id"]?.Value?.Contains("list") ?? false);

            var tableRows = tableNode?.Descendants("tbody").FirstOrDefault()?.Descendants("tr").ToList() ?? new List<HtmlNode>();
            
            List<LijstResult> lijstResults = new List<LijstResult>();

            foreach (var row in tableRows)
            {
                var tableTDs = row?.Descendants("td").ToList();

                if(tableTDs.Count < 5)
                {
                    continue;
                }

                LijstResult lijstResult = new LijstResult();
                lijstResult.PaidBy = tableTDs[0]?.InnerText ?? string.Empty;
                lijstResult.Description = tableTDs[1]?.InnerText ?? string.Empty;
                lijstResult.Amount = System.Net.WebUtility.HtmlDecode(tableTDs[2]?.InnerText) ?? string.Empty;
                lijstResult.Date = tableTDs[3]?.InnerText ?? string.Empty;
                lijstResult.WhoElse = tableTDs[4]?.InnerText ?? string.Empty;

                lijstResults.Add(lijstResult);
            }
            return lijstResults;
        }
    }
}
