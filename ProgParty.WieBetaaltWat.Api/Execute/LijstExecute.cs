﻿using ProgParty.WieBetaaltWat.Api.Result;
using ProgParty.WieBetaaltWat.Api.Scrape;
using System;
using System.Collections.Generic;

namespace ProgParty.WieBetaaltWat.Api.Execute
{
    public class LijstExecute
    {
        public Parameter.LijstParameter Parameters = new Parameter.LijstParameter();

        public List<LijstResult> Result;

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
