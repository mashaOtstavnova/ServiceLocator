using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Com.Nostra13.Universalimageloader.Core;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Core.ViewModels;
using VKontakte;

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

                _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

                var config = ImageLoaderConfiguration.CreateDefault(ApplicationContext);
                if(!ImageLoader.Instance.IsInited)
                    ImageLoader.Instance.Init(config);
            }
            catch (Exception ex)
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
                            () => { imageLoader.DisplayImage(ViewModel?.CurrentUser?.photo_100, photoUser); });
                    });

            IProfileService profileService;
            Mvx.TryResolve(out profileService);

            var user = await profileService.GetUser();
            nameUser.Text = user.first_name + " " + user.last_name;
            imageLoader.DisplayImage(user.photo_100, photoUser);

            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            _navigationView.NavigationItemSelected +=NavigationViewOnNavigationItemSelected;
        }

        private void NavigationViewOnNavigationItemSelected(object o, NavigationView.NavigationItemSelectedEventArgs e)
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
                case Resource.Id.nav_new_record:
                    ViewModel.ShowNewRecord();
                    break;
                case Resource.Id.nav_schedule:
                    ViewModel.ShowSchedule();
                    break;
                case Resource.Id.nav_exit:
                    Logout();
                    break;
            }
            _drawerLayout.CloseDrawers();
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

        protected override void OnDestroy()
        {
            ImageLoader.Instance.Destroy();
            base.OnDestroy();
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