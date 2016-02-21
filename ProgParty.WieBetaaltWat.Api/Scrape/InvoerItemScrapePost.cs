using HtmlAgilityPack;
using ProgParty.WieBetaaltWat.Api.Result;
using ProgParty.Core.Extension;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgParty.WieBetaaltWat.Api.Scrape
{
    internal class InvoerItemScrapePost
    {
        private Parameter.InvoerItemParameterPost Parameters { get; set; }

        public InvoerItemScrapePost(Parameter.InvoerItemParameterPost parameters)
        {
            Parameters = parameters;
        }

        public InvoerItemPostResult Execute()
        {
            using (var handler = new HttpClientHandler() { CookieContainer = Auth.CookieContainer })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Host = "www.wiebetaaltwat.nl";
                    
//                    client.DefaultRequestHeaders.TryAddWithoutValidation("Referer", "https://www.wiebetaaltwat.nl");
//                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "nl-NL,nl;q=0.8,en-GB;q=0.5,en;q=0.3");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10240");
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                    //                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");

                    string uri = "https://www.wiebetaaltwat.nl/index.php";

                    var description = System.Net.WebUtility.UrlEncode(Parameters.Description);
                    string amount = Parameters.Amount.ToString().Replace(".", "%2C").Replace(",", "%2C");

                    var keyValPairs = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("action", "add_transaction"),
                        new KeyValuePair<string, string>("lid", Parameters.SingleList.ProjectId),
                        new KeyValuePair<string, string>("payment_by", Parameters.PaymentBy.Id),
                        new KeyValuePair<string, string>("description", description),
                        new KeyValuePair<string, string>("amount", amount),
                        new KeyValuePair<string, string>("date", Parameters.Date.ToString("dd-MM-yyyy")),
                        new KeyValuePair<string, string>("submit_add", "Verwerken")
                    };

                    foreach(InvoerItemPerson person in Parameters.Persons)
                    {
                        if (person.Amount == 0)
                            continue;

                        keyValPairs.Add(new KeyValuePair<string, string>("factor[" + person.Id + "]", person.ShareCount.ToString()));
                    }

                    foreach (var person in Parameters.Persons)
                    {
                        var factor = $"factor%5B{person.Id}%5D";

                        keyValPairs.Add(new KeyValuePair<string, string>(factor, person.Amount.ToString()));
                    }

                    var content = new FormUrlEncodedContent(keyValPairs);
                    
                    Task<HttpResponseMessage> response = client.PostAsync(uri, content);

                    if (response.Result.IsSuccessStatusCode || response.Result.StatusCode == System.Net.HttpStatusCode.Found)
                    {
                        var htmlContent = response.Result.Content.ReadAsStringAsync().Result;

                        return ConvertToResult(htmlContent);
                        
                    }
                }
            }
            return new InvoerItemPostResult();
        }

        public InvoerItemPostResult ConvertToResult(string html)
        {
            var result = new InvoerItemPostResult();
            if (html.Contains("De invoer is toegevoegd aan de lijst."))
                result.AddingWorked = true;
            return result;
        }
    }
}
