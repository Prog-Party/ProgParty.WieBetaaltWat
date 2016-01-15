﻿using System;

namespace ProgParty.WieBetaaltWat.Api.Result
{
    public class OverviewResult
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public int Index { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
