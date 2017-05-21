using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Droid.Extensions;
using VKontakte;
using VKontakte.API;
using ServiceLocator.Droid.Views;
using ServiceLocator.Core.IServices;
using MvvmCross.Platform;

namespace ServiceLocator.Droid
{
    /// <summary>
    ///     Activity which displays a login screen to the user, offering registration as well.
    /// </summary>
    [Register("otstavnova.ServiceLocator.LoginVKView")]
    [IntentFilter(new[] { Intent.ActionView }, DataScheme = "vk5963678", Categories = new[]
    {
        Intent.CategoryBrowsable,
        Intent.CategoryDefault
    })]
    [Activity(WindowSoftInputMode = SoftInput.AdjustResize)]
    public class LoginVKView : BaseView<LoginVKViewModel>
    {
        // Scope is set of required permissions for your application
        // (see vk.com api permissions documentation: https://vk.com/dev/permissions)
        private static readonly string[] _myScopes =
        {
            VKScope.Friends,
            VKScope.Wall,
            VKScope.Photos,
            VKScope.Nohttps,
            VKScope.Messages,
            VKScope.Docs
        };

        private bool _isResumed;
        protected override int LayoutResource => Resource.Layout.loginVK;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            VKSdk.WakeUpSession(this, OnSucsessResult, OnErrorResult);

            var vkLoginButton = FindViewById<Button>(Resource.Id.page_login_vkLogin);
            vkLoginButton.Click += VkLoginButtonOnClick;
        }

        private  IProgressLoaderService _progressLoaderService;
        private void VkLoginButtonOnClick(object sender, EventArgs eventArgs)
        {
            _progressLoaderService.ShowProgressBar();
            ShowLogin();
            _progressLoaderService.HideProgressBar();
        }

        protected override void OnDestroy()
        {
            var vkLoginButton = FindViewById<Button>(Resource.Id.page_login_vkLogin);
            vkLoginButton.Click -= VkLoginButtonOnClick;
            base.OnDestroy();
        }

        private void OnErrorResult(VKError vkError)
        {
            //TODO: показывать ошибку диалогом
        }

        private void OnSucsessResult(VKSdk.LoginState loginState)
        {
            _progressLoaderService.ShowProgressBar();
            if (_isResumed)

                if (loginState == VKSdk.LoginState.LoggedOut)
                    ShowLogin();
                else if (loginState == VKSdk.LoginState.LoggedIn)
                    ViewModel.ShowMainPageCommand.Execute();
            _progressLoaderService.HideProgressBar();
        }

        private void ShowLogin()
        {
            VKSdk.Login(this, _myScopes);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _progressLoaderService.ShowProgressBar();
            if (StoreExtensions.IsVkUserAuthorized(PackageName))
            {
                _isResumed = true;
                if (VKSdk.IsLoggedIn)
                    ViewModel.ShowMainPageCommand.Execute();
            }
            _progressLoaderService.HideProgressBar();
        }

        protected override void OnPause()
        {
            _isResumed = false;

            base.OnPause();
        }

        public override void OnBackPressed()
        {
            var dialogBuilder = new AlertDialog.Builder(this);
            dialogBuilder.SetTitle("Завершение работы приложения");
            dialogBuilder.SetMessage("Вы хотите выйти из приложения?");
            dialogBuilder.SetCancelable(false);
            dialogBuilder.SetPositiveButton("Выйти", CloseDialog);
            dialogBuilder.SetNegativeButton("Отмена", (sender, args) => { (sender as AlertDialog).Cancel(); });
            dialogBuilder.Show();
        }

        private void CloseDialog(object sender, DialogClickEventArgs dialogClickEventArgs)
        {
            _progressLoaderService.ShowProgressBar();
            var dialog = sender as AlertDialog;
            dialog?.Cancel();
            FinishAffinity();
            _progressLoaderService.HideProgressBar();
        }
        
        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
           // _progressLoaderService.ShowProgressBar();
            bool vkResult;
            var task = VKSdk.OnActivityResultAsync(requestCode, resultCode, data, out vkResult);
            if (!vkResult)
                base.OnActivityResult(requestCode, resultCode, data);

            try
            {
                var token = await task;

                Console.WriteLine("User passed Authorization: " + token.AccessToken);

                ViewModel.ShowMainPageCommand.Execute();

                StoreExtensions.SetVkUserAuthorized(PackageName);
            }
            catch (VKException ex)
            {
                Console.WriteLine("User didn't pass Authorization: " + ex);
            }
            //_progressLoaderService.HideProgressBar(); 
        }
    }
}