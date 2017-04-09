using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using ServiceLocator.Droid.Views;
using VKontakte;
using VKontakte.Utils;

namespace ServiceLocator.Droid
{
    [Application(Name = "otstavnova.ServiceLocator.ServiceLocator")]
    public class ServiceLocatorApplication : Application
    {
        private readonly TokenTracker _tokenTracker = new TokenTracker();

        protected ServiceLocatorApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            // list the app fingerprints
            var fingerprints = VKUtil.GetCertificateFingerprint(this, PackageName);
            foreach (var fingerprint in fingerprints)
            {
                Console.WriteLine("Detected Fingerprint: " + fingerprint);
            }

            // setup VK
            _tokenTracker.StartTracking();
            VKSdk.Initialize(this).WithPayments();
        }

        private class TokenTracker : VKAccessTokenTracker
        {
            public override void OnVKAccessTokenChanged(VKAccessToken oldToken, VKAccessToken newToken)
            {
                if (newToken == null)
                {                                                                             
                    Toast.MakeText(Context, "AccessToken invalidated", ToastLength.Long).Show();
                    var intent = new Intent(Context, typeof (LoginVKView));
                    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTop);
                    Context.StartActivity(intent);
                }
            }
        }
    }
}