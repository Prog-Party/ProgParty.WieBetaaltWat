using System;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProgParty.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProgParty.WieBetaaltWat.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shop : Page
    {
        public Shop()
        {
            this.InitializeComponent();
            SetRemoveAdvertisements();
            SetRemoveCompanyAds();
            SetDonations();
        }

        private async void BuyRemoveAds_Click(object sender, RoutedEventArgs e)
        {
            string token = Common.InAppPurchase.TokenRemoveAdvertisement;
            AdsClick(token, () => { SetRemoveAdvertisements(); });
        }

        private void SetRemoveAdvertisements()
        {
            if (Config.Instance.LicenseInformation.ProductLicenses[Common.InAppPurchase.TokenRemoveAdvertisement].IsActive)
            {
                RemoveAdsButton.Visibility = Visibility.Collapsed;
                RemoveAdsPrice.Visibility = Visibility.Collapsed;
                RemoveAdsBought.Visibility = Visibility.Visible;
            } else
            {
                RemoveAdsBought.Visibility = Visibility.Collapsed;
            }
        }

        private async void BuyRemoveCompanyAds_Click(object sender, RoutedEventArgs e)
        {
            string token = Common.InAppPurchase.TokenRemoveCompanies;
            AdsClick(token, () => { SetRemoveCompanyAds(); });
           
        }

        private void SetRemoveCompanyAds()
        {
            if (Config.Instance.LicenseInformation.ProductLicenses[Common.InAppPurchase.TokenRemoveCompanies].IsActive)
            {
                RemoveCompanyAdsButton.Visibility = Visibility.Collapsed;
                RemoveCompanyAdsPrice.Visibility = Visibility.Collapsed;
                RemoveCompanyAdsBought.Visibility = Visibility.Visible;
            }
            else
            {
                RemoveCompanyAdsBought.Visibility = Visibility.Collapsed;
            }
        }

        private async void DonationButton_Click(object sender, RoutedEventArgs e)
        {
            string token = InAppPurchase.TokenDonation;
            AdsClick(token, () => { SetDonations(); });
        }

        private void SetDonations()
        {
            if (Config.Instance.LicenseInformation.ProductLicenses[InAppPurchase.TokenDonation].IsActive)
            {
                DonationButton.Visibility = Visibility.Collapsed;
                DonationPrice.Visibility = Visibility.Collapsed;
                DonationBought.Visibility = Visibility.Visible;
            }
            else
            {
                DonationBought.Visibility = Visibility.Collapsed;
            }
        }

        private async void AdsClick(string token, Action set)
        {
            if (!Config.Instance.LicenseInformation.ProductLicenses[token].IsActive)
            {
                try
                {

#if DEBUG
                    await CurrentAppSimulator.RequestProductPurchaseAsync(token);
#else
                await CurrentApp.RequestProductPurchaseAsync(token);
#endif
                    set();
                }
                catch (Exception)
                {
                    // The in-app purchase was not completed because 
                    // an error occurred.
                }
            }
            else
            {
                set();
            }
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
