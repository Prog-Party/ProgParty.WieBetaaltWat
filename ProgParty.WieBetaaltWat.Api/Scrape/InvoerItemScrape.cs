using HtmlAgilityPack;
using ProgParty.WieBetaaltWat.Api.Result;
using ProgParty.Core.Extension;
using System.Linq;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace ProgParty.WieBetaaltWat.Api.Scrape
{
    internal class InvoerItemScrape
    {
        private Parameter.InvoerItemParameter Parameters { get; set; }

        public InvoerItemScrape(Parameter.InvoerItemParameter parameters)
        {
            Parameters = parameters;
        }

        public InvoerItemGetResult Execute()
        {
            var url = ConstructUrl();

            using (var handler = new HttpClientHandler() { CookieContainer = Auth.CookieContainer })
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Host = "wiebetaaltwat.nl";
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.125 Safari/537.36");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                    var response = client.GetAsync(url).Result;
                    var result = response.Content.ReadAsStringAsync().Result;

                    return ConvertToResult(result);
                }
            }
        }

        private string ConstructUrl()
        {
            string projectId = Parameters.SingleList.ProjectId;
            return $"https://wiebetaaltwat.nl/index.php?lid={projectId}&page=transaction&type=add";
        }
        
        public InvoerItemGetResult ConvertToResult(string html)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var node = htmlDocument.DocumentNode;

            var selectNode = node.Descendants("select").FirstOrDefault(c => c.Attributes["id"]?.Value == "payment_by");

            var optionNodes = selectNode?.Descendants("option") ?? new List<HtmlNode>();

            var result = new InvoerItemGetResult();
            foreach(var option in optionNodes)
            {
                var person = new Api.Result.InvoerItemPerson();
                person.Id = option.Attributes["value"]?.Value;
                person.Name = option.InnerText;
                result.Persons.Add(person);
            }

            return result;
        }
    }
}
