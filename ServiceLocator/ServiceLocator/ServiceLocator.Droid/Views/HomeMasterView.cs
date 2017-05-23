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
using Info.Hoang8f.Android.Segmented;
using ServiceLocator.Entities;

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class HomeMasterView : BaseView<HomeMasterViewModel>
    {
        private DrawerLayout _drawerLayout;
        private NavigationView _navigationView;
        private CalendarView _calendar;
        private ListView _eventsList;
        private SegmentedGroup _segmentedControl;
        private SegmentedGroup tabSelect;
        private DateTime _calendarDate;
        protected override int LayoutResource => Resource.Layout.home_master_view;

        protected override async void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
                ViewModel.Title = "Расписание";
                Title = ViewModel.Title;
                _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                var tabFree = FindViewById<RadioButton>(Resource.Id.schedule_view_segment_free);
                tabSelect = FindViewById<SegmentedGroup>(Resource.Id.schedule_view_segmentedControl);
                var tabBusy = FindViewById<RadioButton>(Resource.Id.schedule_view_segment_busy);
                tabSelect.Check(Resource.Id.schedule_view_segment_free);
              
                _calendarDate = DateTime.Now;
                _calendar = FindViewById<CalendarView>(Resource.Id.schedule_view_calendar);
                _calendar.DateChange += CalendarOnDateChange;

                _segmentedControl = FindViewById<SegmentedGroup>(Resource.Id.schedule_view_segmentedControl);
                _segmentedControl.CheckedChange += SegmentedControlOnCheckedChange;


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

        private IProgressLoaderService _progressLoaderService;
        private void CalendarOnDateChange(object sender, CalendarView.DateChangeEventArgs e)
        {

            _progressLoaderService.ShowProgressBar();
            tabSelect.Check(Resource.Id.schedule_view_segment_free);
            _calendarDate = new DateTime(e.Year, e.Month + 1, e.DayOfMonth);
            ViewModel.ReloadRecord(_calendarDate);

            _progressLoaderService.HideProgressBar();
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


        private void SegmentedControlOnCheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {

            switch (e.CheckedId)
            {
                case Resource.Id.schedule_view_segment_free:
                    ViewModel.ScheduleType = ScheduleType.Free;
                    ViewModel.ReloadRecord(_calendarDate);
                    break;
                case Resource.Id.schedule_view_segment_busy:
                    ViewModel.ScheduleType = ScheduleType.Busy;
                    ViewModel.ReloadRecord(_calendarDate);
                    break;
            }
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
                case Resource.Id.nav_money:
                    ViewModel.ShowMoney();
                    break;
                case Resource.Id.nav_push:
                    ViewModel.ShowNewPush();
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