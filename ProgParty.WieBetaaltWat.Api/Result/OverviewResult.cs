using System;

namespace ProgParty.WieBetaaltWat.Api.Result
{
    public class OverviewResult
    {
        public string ListName { get; internal set; }
        public string ListUrl { get; internal set; }
        public string MyBalance { get; internal set; }
        public string HighBalance { get; internal set; }
        public string LowBalance { get; internal set; }

        public override string ToString()
        {
            return ListName;
        }
    }
}
