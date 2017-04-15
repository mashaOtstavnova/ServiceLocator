using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Support.Design.Widget;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Nostra13.Universalimageloader.Core;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Entities;
using ServiceLocator.Core.IServices;
using MvvmCross.Platform;
using VKontakte;

using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class HomeMasterView : BaseView<HomeMasterViewModel>
    {
        private DrawerLayout _drawerLayout;
        private NavigationView _navigationView;
        protected override int LayoutResource => Resource.Layout.home_master_view;

        protected override async void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

                var config = ImageLoaderConfiguration.CreateDefault(ApplicationContext);
                ImageLoader.Instance.Init(config);
            }
            catch(Exception ex)
            {
                var m = ex.Message;
            }
            //// Initialize ImageLoader with configuration.
            var imageLoader = ImageLoader.Instance;
            var headerMenu = FindViewById<NavigationView>(Resource.Id.nav_view).GetHeaderView(0);

            var photoUser = headerMenu.FindViewById<ImageView>(Resource.Id.user_image);
            var nameUser = headerMenu.FindViewById<TextView>(Resource.Id.user_name);
            await ViewModel.Initialize()
                .ContinueWith(
                    task =>
                    {
                        RunOnUiThread(
                            () => { imageLoader.DisplayImage(ViewModel?.CurrentUser?.photo_max_orig, photoUser); });
                    });

            IProfileService profileService;
            Mvx.TryResolve(out profileService);

            var user = await profileService.GetUser();
            nameUser.Text = user.first_name + " " + user.last_name;
            imageLoader.DisplayImage(user.photo_max_orig, photoUser);

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            _navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_user:
                        ViewModel.ShowProfile();
                        break;
                    case Resource.Id.nav_friends:
                        ViewModel.ShowFriends();
                        break;
                    case Resource.Id.nav_home:
                        ViewModel.ShowMainPage();
                        break;
                    case Resource.Id.nav_info:
                        ViewModel.ShowInfo();
                        break;
                    case Resource.Id.nav_exit:
                        Logout();
                        break;
                }
                _drawerLayout.CloseDrawers();
            };
        }

        private void Logout()
        {
            VKSdk.Logout();
            if (!VKSdk.IsLoggedIn)
                ViewModel.ShowLogin();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
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
            var dialog = sender as AlertDialog;
            dialog?.Cancel();
            FinishAffinity();
        }
    }
}