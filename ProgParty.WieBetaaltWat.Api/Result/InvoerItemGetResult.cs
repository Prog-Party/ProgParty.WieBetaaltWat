using System.Collections.Generic;

namespace ProgParty.WieBetaaltWat.Api.Result
{
    public class InvoerItemGetResult
    {
        public List<InvoerItemPerson> Persons { get; set; } = new List<InvoerItemPerson>();
    }

    public class InvoerItemPerson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int ShareCount { get; set; } = 0;

        public override string ToString()
        {
            return Name;
        }
    }
}
