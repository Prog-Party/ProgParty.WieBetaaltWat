using ProgParty.WieBetaaltWat.ApiUniversal.Result;
using ProgParty.WieBetaaltWat.ApiUniversal.Scrape;
using System;

namespace ProgParty.WieBetaaltWat.ApiUniversal.Execute
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
