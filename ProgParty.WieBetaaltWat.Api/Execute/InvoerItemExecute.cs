using ProgParty.WieBetaaltWat.Api.Result;
using ProgParty.WieBetaaltWat.Api.Scrape;
using System;

namespace ProgParty.WieBetaaltWat.Api.Execute
{
    public class InvoerExecute
    {
        public Parameter.InvoerItemParameter Parameters = new Parameter.InvoerItemParameter();

        public InvoerItemGetResult Result;

        public bool Execute()
        {
            try
            {
                Result = new InvoerItemScrape(Parameters).Execute();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
