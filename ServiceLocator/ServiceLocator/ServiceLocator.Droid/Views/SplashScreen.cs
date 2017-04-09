using Android.App;
using Android.Content.PM;
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
    }
}
