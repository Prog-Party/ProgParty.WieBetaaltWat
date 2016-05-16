using ProgParty.Core;
using ProgParty.WieBetaaltWat.ApiUniversal.Execute;
using ProgParty.WieBetaaltWat.Universal.Storage;
using System.Linq;
using Windows.ApplicationModel.Store;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ProgParty.WieBetaaltWat.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public WieBetaaltWatDataContext PageDataContext { get; set; }
        public MainPage()
        {
            new Connection();
            if (!Connection.HasInternetAccess)
                new MessageDialog("Geen internet verbinding aanwezig").ShowAsync();

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

            DataContext = PageDataContext = new WieBetaaltWatDataContext();
        }

        private void RedirectPageForLoggedin()
        {
            if (ApiUniversal.Auth.IsLoggedIn)
                return;

            var storage = new Storage.Storage();

            string loggedInName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString() ?? string.Empty;
            string loggedInPassword = storage.LoadFromLocal(StorageKeys.LoggedInPassword)?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(loggedInName) || string.IsNullOrEmpty(loggedInPassword))
            {
                Frame.Navigate(typeof(Login));
                return;
            }

            var loginScrape = new ApiUniversal.Authentication.LoginScrape(loggedInName, loggedInPassword);
            bool isLoggedIn = loginScrape.Execute();

            if (!isLoggedIn)
            {
                Frame.Navigate(typeof(Login));
                return;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var context = e.Parameter as WieBetaaltWatDataContext;
            if(context != null)
                DataContext = PageDataContext = context;

            //Register.RegisterOnNavigatedTo(Config.Instance.LicenseInformation);

            searchPivot.SelectedIndex = 0;
            galleryList.SelectedItem = null;
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            RedirectPageForLoggedin();

            ShowMyLists();

            //Register.RegisterOnLoaded();
        }

        private void ShowMyLists()
        {
            var storage = new Storage.Storage();
            var param = new ApiUniversal.Parameter.OverviewParameter();
            param.LoginName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString() ?? string.Empty;
            param.LoginPassword = storage.LoadFromLocal(StorageKeys.LoggedInPassword)?.ToString() ?? string.Empty;

            OverviewExecute overview = new OverviewExecute() { Parameters = param };
            overview.Execute();
            
            var result = overview.Result;

            PageDataContext.SetResult(result);
        }
        
        
        private void GalleryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var storage = new Storage.Storage();
            var param = new ApiUniversal.Parameter.LijstParameter();

            var selectedIndex = (sender as ListView).SelectedIndex;
            if (PageDataContext.Lijsten.Any() && selectedIndex != -1)
            {
                param.ProjectId = int.Parse(PageDataContext.Lijsten[selectedIndex].ProjectId);
                param.Url = PageDataContext.Lijsten[(sender as ListView).SelectedIndex].ListUrl;
                param.LoginName = storage.LoadFromLocal(StorageKeys.LoggedInName)?.ToString() ?? string.Empty;
                param.LoginPassword = storage.LoadFromLocal(StorageKeys.LoggedInPassword)?.ToString() ?? string.Empty;
                LijstExecute lijst = new LijstExecute() { Parameters = param };
                lijst.Execute();
            
                PageDataContext.ProjectId = param.ProjectId;

                PageDataContext.SetLijstResult(lijst.Result);
            
                searchPivot.SelectedIndex = 1;
            }
        }
        private void ContactButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Contact));
        private void BuyBarButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Shop));
        private void AddListButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(InvoerItem), DataContext);
        private void SettingsButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Login));

        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            Button addEntry = (Button)sender;
            string value = addEntry.CommandParameter.ToString();

            PageDataContext.ProjectId = int.Parse(PageDataContext.Lijsten.FirstOrDefault(c => c.ProjectId == value).ProjectId);

            Frame.Navigate(typeof(InvoerItem), PageDataContext);
        }

        private void LijstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void searchPivot_PivotItemLoaded(Pivot sender, PivotItemEventArgs args)
        {
            if(searchPivot.SelectedIndex == 0)
            {
                BuyBarButton.Visibility = Visibility.Visible;
                ContactButton.Visibility = Visibility.Visible;
                AddItemButton.Visibility = Visibility.Collapsed;
                ConfigButton.Visibility = Visibility.Visible;
            }
            else if (searchPivot.SelectedIndex == 1)
            {
                BuyBarButton.Visibility = Visibility.Collapsed;
                ContactButton.Visibility = Visibility.Collapsed;
                AddItemButton.Visibility = Visibility.Visible;
                ConfigButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}