﻿using ProgParty.Core;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Register.RegisterOnNavigatedTo(Config.Instance.LicenseInformation);
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            Register.RegisterOnLoaded();
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
