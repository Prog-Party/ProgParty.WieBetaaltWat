using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
            FotoJensDennis.Width = Window.Current.Bounds.Width;
        }

        private async void DennisMail_Click(object sender, RoutedEventArgs e)
        {
#if WINDOWS_PHONE_APP
            //predefine Recipient
            Windows.ApplicationModel.Email.EmailRecipient sendTo = new Windows.ApplicationModel.Email.EmailRecipient()
            {
                Address = "dennis.rosenbaum@outlook.com"
            };

            //generate mail object
            Windows.ApplicationModel.Email.EmailMessage mail = new Windows.ApplicationModel.Email.EmailMessage();
            mail.Subject = "Hoi Dennis!";
            mail.Body = "Je bent geweldig";

            //add recipients to the mail object
            mail.To.Add(sendTo);

            //open the share contract with Mail only:
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(mail);
#endif
        }
        private async void JensMail_Click(object sender, RoutedEventArgs e)
        {
#if WINDOWS_PHONE_APP
            //predefine Recipient
            Windows.ApplicationModel.Email.EmailRecipient sendTo = new Windows.ApplicationModel.Email.EmailRecipient()
            {
                Address = "jens.denbraber@gmail.com"
            };

            //generate mail object
            Windows.ApplicationModel.Email.EmailMessage mail = new Windows.ApplicationModel.Email.EmailMessage();
            mail.Subject = "Hoi Jens!";
            mail.Body = "Je bent geweldig";

            //add recipients to the mail object
            mail.To.Add(sendTo);

            //open the share contract with Mail only:
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(mail);
#endif
        }
    }
}
