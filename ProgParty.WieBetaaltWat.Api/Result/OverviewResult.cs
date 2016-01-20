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

        public override string ToString() => ListName;

        internal string ProjectId
        {
            get
            {
                if (string.IsNullOrEmpty(ListUrl))
                    return string.Empty;

                int index = ListUrl.IndexOf("lid");
                int index2 = ListUrl.IndexOf("&", index);
                if (index == -1 || index2 == -1)
                    return string.Empty; ;

                string projectId = ListUrl.Substring(index + 4, index2 - index - 4);
                return projectId;
            }
        }

        internal string ProjectIdssfjk
        {
            get
            {
                if (string.IsNullOrEmpty(ListUrl))
                    return string.Empty;

                int index = ListUrl.IndexOf("lid");
                int index2 = ListUrl.IndexOf("&", index);
                if (index == -1 || index2 == -1)
                    return string.Empty; ;

                string projectId = ListUrl.Substring(index + 4, index2 - index - 4);
                return projectId;
            }
        }
    }
}
