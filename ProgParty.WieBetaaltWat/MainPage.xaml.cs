using ProgParty.Core;
using ProgParty.Core.Storage;
using ProgParty.WieBetaaltWat.Api.Execute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ProgParty.WieBetaaltWat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public WieBetaaltWatDataContext PageDataContext { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            Config.Instance = new Config(this)
            {
                Pivot = searchPivot,
                AppName = "WieBetaaltWat",
                Ad = new ConfigAd()
                {
                    AdHolder = AdHolder,
                    AdApplicationId = "de76f64c-70b2-49d7-99b6-924257121127",
                    SmallAdUnitId = "11569201",
                    MediumAdUnitId = "11569200",
                    LargeAdUnitId = "11569199"
                }
            };

#if DEBUG
            Core.Config.Instance.LicenseInformation = CurrentAppSimulator.LicenseInformation;
#else
            Core.Config.Instance.LicenseInformation = CurrentApp.LicenseInformation;
#endif

            Core.License.LicenseInfo.SetLicenseInformation();

            Register.Execute();

            PageDataContext = new WieBetaaltWatDataContext();
            DataContext = PageDataContext;



            //AppBarButton btnHome = new AppBarButton();
            //btnHome.Label = "Nieuwe invoer";
            //btnHome.Icon = new SymbolIcon(Symbol.Add);

            //this.CommandBar.PrimaryCommands.Add(btnHome);

            //btnHome.IsEnabled = true;
            //< AppBarButton Label = "Nieuwe invoer" Icon = "Add" />
        }

        private void RedirectPageForLoggedin()
        {
            if (Api.Auth.IsLoggedIn)
                return;

            var storage = new Storage();

            string loggedInName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString() ?? string.Empty;
            string loggedInPassword = storage.LoadFromLocal(StorageKeys.LoggedInPassword)?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(loggedInName) || string.IsNullOrEmpty(loggedInPassword))
            {
                Frame.Navigate(typeof(Login));
                return;
            }

            var loginScrape = new Api.Authentication.LoginScrape(loggedInName, loggedInPassword);
            bool isLoggedIn = loginScrape.Execute();

            if (!isLoggedIn)
            {
                Frame.Navigate(typeof(Login));
                return;
            }
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e) => Register.RegisterOnNavigatedTo(Config.Instance.LicenseInformation);
        
        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            RedirectPageForLoggedin();

            ShowMyLists();

            Register.RegisterOnLoaded();
        }

        private void ShowMyLists()
        {
            var storage = new Storage();
            var param = new Api.Parameter.OverviewParameter();
            param.LoginName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString() ?? string.Empty;
            param.LoginPassword = storage.LoadFromLocal(StorageKeys.LoggedInPassword)?.ToString() ?? string.Empty;

            OverviewExecute overview = new OverviewExecute() { Parameters = param };
            overview.Execute();
            
            var result = overview.Result;

            PageDataContext.SetResult(result);
        }
        
        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Core.Pages.Contact));
        }

        private void BuyBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Core.Pages.Shop));
        }

        private void GalleryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var storage = new Storage();
            var param = new Api.Parameter.LijstParameter();
            param.Url = PageDataContext.Lijsten[(sender as ListView).SelectedIndex].ListUrl;
            param.LoginName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString() ?? string.Empty;
            param.LoginPassword = storage.LoadFromLocal(StorageKeys.LoggedInPassword)?.ToString() ?? string.Empty;
            LijstExecute lijst = new LijstExecute() { Parameters = param };
            lijst.Execute();
            //OverviewExecute overview = new OverviewExecute() { Parameters = param };
            //overview.Execute();

            var result = lijst.Result;

            //PageDataContext.SetLijstResult(result);

            searchPivot.SelectedIndex = 1;
        }

        private void LoadMoreGalleries_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }

        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            Button addEntry = (Button)sender;
            string value = addEntry.CommandParameter.ToString();

            PageDataContext.Lijsten.FirstOrDefault(c => c.ProjectId == value);
        }

        private void LijstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
