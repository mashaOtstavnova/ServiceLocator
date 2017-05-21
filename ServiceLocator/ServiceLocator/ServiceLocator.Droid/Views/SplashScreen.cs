using Android.App;
using Android.Content.PM;
using Android.OS;
using Gcm.Client;
using MvvmCross.Droid.Views;

namespace ServiceLocator.Droid.Views
{
    [Activity(
        Label = "ServiceLocatorApplication.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenView : MvxSplashScreenActivity
    {
        public SplashScreenView()
            : base(Resource.Layout.SplashScreen)
        {
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Check to see that GCM is supported and that the manifest has the correct information
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            //Call to Register the device for Push Notifications
            GcmClient.Register(this, GcmBroadcastReceiver.SENDER_IDS);
        }
    }
}
