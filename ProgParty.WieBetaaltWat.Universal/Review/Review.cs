using ProgParty.WieBetaaltWat.Universal.Storage;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace ProgParty.WieBetaaltWat.Universal.Review
{
    class Review
    {
        public Uri ReviewUri{ get; private set; }

        public Review()
        {
            var appId = CurrentApp.AppId.ToString();
            var reviewApp = "ms-windows-store:reviewapp";
            var uri = new Uri($"{reviewApp}?appid={appId}");
            ReviewUri = uri;
        }

        public async Task Execute()
        {
            var storage = new Storage.Storage();
            storage.StoreInRoaming(StorageKeys.ReviewDone, true);

            await Windows.System.Launcher.LaunchUriAsync(ReviewUri);
        }
    }
}
