using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;

namespace ProgParty.WieBetaaltWat.ApiUniversal.Result
{
    public class InvoerItemGetResult
    {
        public List<InvoerItemPerson> Persons { get; set; } = new List<InvoerItemPerson>();
    }

    public class InvoerItemPerson : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }

        public Visibility MinIsVisible => ShareCount == 0 ? Visibility.Collapsed : Visibility.Visible;

        private int _shareCount = 0;
        public int ShareCount
        {
            get { return _shareCount; }
            set
            {
                _shareCount = value;
                OnPropertyChanged(nameof(ShareCount));
                OnPropertyChanged(nameof(MinIsVisible));
            }
        }

        public override string ToString() => Name;

        public void Recalculate(List<InvoerItemPerson> participants, string buyerId, double totalAmount)
        {
            int totalShare = participants.Sum(p => p.ShareCount);
            if (totalShare == 0)
                Amount = 0;
            else
            {
                double pricePerShare = totalAmount / (double)totalShare;

                if (Id == buyerId)
                {
                    Amount = (totalShare - ShareCount) * pricePerShare;
                }
                else
                {
                    Amount = ShareCount * -1 * pricePerShare;
                }
            }

            OnPropertyChanged(nameof(Amount));
        }
        
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
