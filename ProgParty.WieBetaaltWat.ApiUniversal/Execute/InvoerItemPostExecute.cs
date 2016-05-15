using ProgParty.WieBetaaltWat.ApiUniversal.Result;
using ProgParty.WieBetaaltWat.ApiUniversal.Scrape;
using System;

namespace ProgParty.WieBetaaltWat.ApiUniversal.Execute
{
    public class InvoerItemPostExecute
    {
        public Parameter.InvoerItemParameterPost Parameters = new Parameter.InvoerItemParameterPost();

        public InvoerItemPostResult Result;

        public bool Execute()
        {
            try
            {
                Result = new InvoerItemScrapePost(Parameters).Execute();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
