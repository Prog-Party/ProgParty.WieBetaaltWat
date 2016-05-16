using ProgParty.WieBetaaltWat.Universal.Storage;
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

namespace ProgParty.WieBetaaltWat.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();

            //ConfigButton.IsEnabled = false;

            //AddListButton.IsEnabled = false;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            var storage = new Storage.Storage();
            string loggedInName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString();

            if (string.IsNullOrEmpty(loggedInName))
                LoggedInNamePanel.Visibility = Visibility.Collapsed;
            else
            {
                LoginTextBox.Text = loggedInName;
                CurrentlyLoggedInName.Text = loggedInName;
                LoggedInNamePanel.Visibility = Visibility.Visible;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = LoginTextBox.Text;
            string password = PasswordTextBox.Password;

            //do login, if worked, save email and password
            var loginScrape = new ApiUniversal.Authentication.LoginScrape(email, password);
            bool isLoggedIn = loginScrape.Execute();
            if (isLoggedIn)
            {
                var storage = new Storage.Storage();
                storage.StoreInLocal(StorageKeys.LoggedInName, email);
                storage.StoreInLocal(StorageKeys.LoggedInPassword, password);

                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                //show message
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }
        
        private void ContactButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Contact));
        private void BuyBarButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Shop));

        private void LoginTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(Windows.System.VirtualKey.Enter))
                PasswordTextBox.Focus(FocusState.Programmatic);
        }

        private void PasswordTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(Windows.System.VirtualKey.Enter))
                LoginButton.Focus(FocusState.Programmatic);
        }
    }
}
