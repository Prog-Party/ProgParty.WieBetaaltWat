using ProgParty.WieBetaaltWat.Api.Result;
using ProgParty.WieBetaaltWat.Api.Scrape;
using System.Collections.Generic;

namespace ProgParty.WieBetaaltWat.Api.Execute
{
    public class OverviewExecute
    {
        public Parameter.OverviewParameter Parameters = new Parameter.OverviewParameter();

        public List<OverviewResult> Result;

        public bool Execute()
        {
            try
            {
                Result = new OverviewScrape(Parameters).Execute();
                return true;
            }
            catch(System.Exception e)
            {
                return false;
            }
        }

        public void Convert(string html)
        {
            Result = new OverviewScrape(Parameters).ConvertToResult(html);
        }
    }
}
