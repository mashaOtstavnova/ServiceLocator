using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyServiceLocator.Core.ViewModels;

namespace MyServiceLocator.Droid.Views
{
    [Activity]
    public class LoginVKView : BaseView<LoginVKViewModel>
    {

        protected override int LayoutResource => Resource.Layout.loginVK;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            var vkLoginButton = FindViewById<Button>(Resource.Id.page_login_vkLogin);
            vkLoginButton.Click += VkLoginButtonOnClick;
        }
        private void VkLoginButtonOnClick(object sender, EventArgs eventArgs)
        {
            ViewModel.ShowTypeUserCommand.Execute();
    }

    }
}