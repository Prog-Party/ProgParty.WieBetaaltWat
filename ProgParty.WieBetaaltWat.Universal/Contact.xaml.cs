﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProgParty.WieBetaaltWat.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Contact : Page
    {
        public Contact()
        {
            this.InitializeComponent();

            SetFooterImage();
        }

        public Contact(SolidColorBrush foregroundColor, SolidColorBrush backgroundColor, bool imageMirrored = false)
        {
            this.InitializeComponent();

            SetColors(foregroundColor, backgroundColor);
            SetFooterImage(imageMirrored);
        }


        private void SetColors(SolidColorBrush foregroundColor, SolidColorBrush backgroundColor)
        {
            this.Background = backgroundColor;
            this.Foreground = foregroundColor;
        }

        private void SetFooterImage(bool imageMirrored = false)
        {
            BitmapImage footerImage = new BitmapImage();

            if (imageMirrored)
                footerImage.UriSource = new Uri("ms-appx:///Core/Assets/foto_jens_dennis_mirror.png");
            else
                footerImage.UriSource = new Uri("ms-appx:///Core/Assets/foto_jens_dennis.png");

            FooterImage.Source = footerImage;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void ButtonReview_Click(object sender, RoutedEventArgs e)
        {
            await new Review.Review().Execute();
        }

        private void FotoJensDennis_Loaded(object sender, RoutedEventArgs e)
        {
            FooterImage.Width = Window.Current.Bounds.Width;
        }

        private async void DennisMail_Click(object sender, RoutedEventArgs e)
        {
            //predefine Recipient
            Windows.ApplicationModel.Email.EmailRecipient sendTo = new Windows.ApplicationModel.Email.EmailRecipient()
            {
                Address = "dennis.rosenbaum@outlook.com"
            };
            Windows.ApplicationModel.Email.EmailRecipient sendCc = new Windows.ApplicationModel.Email.EmailRecipient()
            {
                Address = "jens.denbraber@gmail.com"
            };

            //generate mail object
            Windows.ApplicationModel.Email.EmailMessage mail = new Windows.ApplicationModel.Email.EmailMessage();
            mail.Subject = "Hi Prog Party!";
            mail.Body = "You are great";

            //add recipients to the mail object
            mail.To.Add(sendTo);
            mail.CC.Add(sendCc);

            //open the share contract with Mail only:
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
