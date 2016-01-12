using ProgParty.Core;
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



            AppBarButton btnHome = new AppBarButton();
            btnHome.Label = "Nieuwe invoer";
            btnHome.Icon = new SymbolIcon(Symbol.Add);

            this.CommandBar.PrimaryCommands.Add(btnHome);

            btnHome.IsEnabled = true;
            //< AppBarButton Label = "Nieuwe invoer" Icon = "Add" />
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void ComboBoxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Core.Pages.Contact));
        }

        private void BuyBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Core.Pages.Shop));
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void GalleryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoadMoreGalleries_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }
    }
}
