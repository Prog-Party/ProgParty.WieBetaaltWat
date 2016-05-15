namespace ProgParty.WieBetaaltWat.ApiUniversal.Result
{
    public class LijstResult
    {
        public string PaidBy { get; internal set; }
        public string Description { get; internal set; }
        public string Amount { get; internal set; }
        public string Date { get; internal set; }
        public string WhoElse { get; internal set; }
        public string ListUrl { get; internal set; }

        public override string ToString() => PaidBy;

        public string PaymentId
        {
            get
            {
                if (string.IsNullOrEmpty(ListUrl))
                    return string.Empty;

                int index = ListUrl.IndexOf("lid");
                int index2 = ListUrl.IndexOf("&", index);
                if (index == -1 || index2 == -1)
                    return string.Empty; ;

                string paymentId = ListUrl.Substring(index + 4, index2 - index - 4);
                return paymentId;
            }
        }
    }
}
