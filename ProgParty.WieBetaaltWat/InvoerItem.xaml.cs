using ProgParty.Core.Storage;
using ProgParty.WieBetaaltWat.Api.Execute;
using ProgParty.WieBetaaltWat.Api.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ProgParty.WieBetaaltWat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InvoerItem : Page
    {
        private WieBetaaltWatDataContext _dataContext;

        public InvoerItem()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataContext = e.Parameter as WieBetaaltWatDataContext;
            DataContext = _dataContext;

            var storage = new Storage();
            var param = new Api.Parameter.InvoerItemParameter();
            param.SingleList = _dataContext.Lijsten.SingleOrDefault(p => p.ProjectId.Equals(_dataContext.ProjectId.ToString()));
            param.LoginName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString() ?? string.Empty;
            param.LoginPassword = storage.LoadFromLocal(StorageKeys.LoggedInPassword)?.ToString() ?? string.Empty;

            InvoerExecute invoerExecute = new InvoerExecute() { Parameters = param };
            invoerExecute.Execute();

            var result = invoerExecute.Result;

            _dataContext.SetLijstPersons(result.Persons);
        }
        
        private void BetalingToevoegen_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PaymentBy_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.SelectedIndex = 0;
        }

        private void PlusEntry_Click(object sender, RoutedEventArgs e)
        {        
            Button PlusEntry = (Button)sender;
            InvoerItemPerson invoerItemPerson = (InvoerItemPerson)PlusEntry.DataContext;
            invoerItemPerson.ShareCount++;

            Recalculate();
        }

        private void MinusEntry_Click(object sender, RoutedEventArgs e)
        {
            Button PlusEntry = (Button)sender;
            InvoerItemPerson invoerItemPerson = (InvoerItemPerson)PlusEntry.DataContext;
            if (invoerItemPerson.ShareCount > 0)
            {
                invoerItemPerson.ShareCount--;

                Recalculate();
            }
        }

        private double _totalAmount = 0;
        private void Recalculate()
        {
            var selectedPerson = PaymentBy.SelectedValue as InvoerItemPerson;

            foreach (var item in _dataContext.LijstPersons)
                item.Recalculate(_dataContext.LijstPersons.ToList(), selectedPerson.Id, _totalAmount);
        }

        private void PaymentBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recalculate();
        }

        private void Balance_TextChanged(object sender, TextChangedEventArgs e)
        {
            string balance = Balance.Text;
            balance = balance.Replace('.', ',');
            if (double.TryParse(balance, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("nl-NL"), out _totalAmount))
            {
                Balance.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            } else
            {
                Balance.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            }

            Recalculate();
        }
    }
}
