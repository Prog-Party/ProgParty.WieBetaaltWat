using ProgParty.WieBetaaltWat.Api.Result;
using ProgParty.WieBetaaltWat.Api.Scrape;
using System;

namespace ProgParty.WieBetaaltWat.Api.Execute
{
    public class ArticleExecute
    {
        public Parameter.LijstParameter Parameters = new Parameter.LijstParameter();

        public LijstResult Result;

        public bool Execute()
        {
            try
            {
                Result = new LijstScrape(Parameters).Execute();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
