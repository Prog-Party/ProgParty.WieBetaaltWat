using ProgParty.Core.Storage;
using ProgParty.WieBetaaltWat.Api.Execute;
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
            WieBetaaltWatDataContext dataContext = e.Parameter as WieBetaaltWatDataContext;

            var storage = new Storage();
            var param = new Api.Parameter.InvoerItemParameter();
            param.SingleList = dataContext.Lijsten[0];
            param.LoginName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString() ?? string.Empty;
            param.LoginPassword = storage.LoadFromLocal(StorageKeys.LoggedInPassword)?.ToString() ?? string.Empty;

            InvoerExecute invoerExecute = new InvoerExecute() { Parameters = param };
            invoerExecute.Execute();

            var result = invoerExecute.Result;

            dataContext.SetLijstPersons(result.Persons);
        }

        private void BetalingToevoegen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PaymentBy_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.SelectedIndex = 0;
        }
    }
}
