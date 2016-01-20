using System;
using System.Collections.Generic;

namespace ProgParty.WieBetaaltWat.Api.Parameter
{
    public class InvoerItemParameterPost
    {
        public Result.OverviewResult SingleList { get; set; }

        public List<Result.InvoerItemPerson> Persons { get; set; }

        public string Description { get; set; }
        public Result.InvoerItemPerson PaymentBy { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }
    }

}
