using ProgParty.WieBetaaltWat.Api.Result;
using ProgParty.WieBetaaltWat.Api.Scrape;
using System;

namespace ProgParty.WieBetaaltWat.Api.Execute
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
