using HtmlAgilityPack;
using ProgParty.WieBetaaltWat.Api.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgParty.WieBetaaltWat.Api.Scrape
{
    internal class OverviewScrape
    {
        private Parameter.OverviewParameter Parameters { get; set; }

        public OverviewScrape(Parameter.OverviewParameter parameters)
        {
            Parameters = parameters;
        }

        public List<OverviewResult> Execute()
        {
            string loggedInName = Parameters.LoginName;
            string loggedInPassword = Parameters.LoginPassword;
            

            var loginScrape = new Api.Authentication.LoginScrape(loggedInName, loggedInPassword);
            bool isLoggedIn = loginScrape.Execute();

            if(isLoggedIn)
            {
                return ConvertToResult(loginScrape.HtmlContent);
            }
            return new List<OverviewResult>();
        }

        public string ConstructUrl() => "https://wiebetaaltwat.nl";
        //public string ConstructUrl() => $"http://www.wiebetaaltwat.nl{GetBaseUrl()}/page/{Parameters.Paging}";
        public List<OverviewResult> ConvertToResult(string result)
        {
            List<OverviewResult> overviewResult = new List<OverviewResult>();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(result);
            var node = htmlDocument.DocumentNode;

            var tableNode = node.Descendants("table").FirstOrDefault(c => c.Attributes["class"]?.Value?.Contains("view-lists") ?? false);

            var lijsten = tableNode?.Descendants("tbody").FirstOrDefault()?.Descendants("tr") ?? new List<HtmlNode>();

            foreach (var lijst in lijsten)
                overviewResult.Add(ConvertSingleResult(lijst));

            return overviewResult;
        }

        public OverviewResult ConvertSingleResult(HtmlNode nodeTr)
        {
            var childNodes = nodeTr.Descendants("td").ToList();

            var o = new OverviewResult();
            o.ListName = childNodes[0]?.InnerText ?? string.Empty;
            o.ListUrl = "http://www.wiebetaaltwat.nl" + childNodes[0].Descendants("a").FirstOrDefault()?.Attributes["href"]?.Value + $"&p=1&rows={int.MaxValue}&sort_column=timestamp";

            o.MyBalance = Clean(childNodes[1]?.InnerText);
            o.HighBalance = Clean(childNodes[2]?.InnerText);
            o.LowBalance = Clean(childNodes[3]?.InnerText);
            return o;
        }

        private string Clean(string s) => System.Net.WebUtility.HtmlDecode(s ?? string.Empty);
    }
}
