using ProgParty.WieBetaaltWat.ApiUniversal.Result;
using ProgParty.WieBetaaltWat.ApiUniversal.Scrape;
using System.Collections.Generic;

namespace ProgParty.WieBetaaltWat.ApiUniversal.Execute
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
    }
}
