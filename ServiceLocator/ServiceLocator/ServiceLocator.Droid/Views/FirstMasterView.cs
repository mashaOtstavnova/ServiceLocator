using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using Com.Nostra13.Universalimageloader.Core;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Droid.Views.Dialogs;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ServiceLocator.Droid.Views
{
    [Activity(Label = "Master", ParentActivity = typeof(FirstMasterView))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "navdrawer.activities.FirstMasterView")]
    public class FirstMasterView : BaseView<FirstMasterViewModel>
    {
        private ImageLoader _imageLoader;
        protected override int LayoutResource => Resource.Layout.first_master_view;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // SetContentView(LayoutResource);
            //ViewModel.Title = "FriendView";
            //Title = ViewModel.Title;

            _imageLoader = ImageLoader.Instance;
            _imageLoader.Init(ImageLoaderConfiguration.CreateDefault(this));
            IProfileService profileService;
            var service = Mvx.TryResolve(out profileService);

            var user = await profileService.GetUser();

            //friends = Util.GenerateFriends();
            var title = user.first_name + " " + user.last_name;
            var birthday = user.bdate;

            title = string.IsNullOrWhiteSpace(title) ? "New Friend" : title;
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //if (string.IsNullOrWhiteSpace(image))
            //    image = friends[0].Image;

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            collapsingToolbar.SetTitle(title);
            collapsingToolbar.SetCollapsedTitleTextColor(Color.White);
            collapsingToolbar.SetExpandedTitleColor(Color.White);
            var t = FindViewById<ImageView>(Resource.Id.imageView1);
            // t.Background = user.photo_50;vo
            try
            {
                _imageLoader.DisplayImage(user.photo_100, FindViewById<ImageView>(Resource.Id.imageView1));
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }
            var buttonForCategory = (Button) FindViewById(Resource.Id.category);
            buttonForCategory.Click += OnCategoryClick;
        }

        private void OnCategoryClick(object sender, EventArgs eventArgs)
        {
            var dialog4 = new CategoryServiceDialog(ViewModel);
            dialog4.Show(SupportFragmentManager, "PrintedAndNotServedDialog");
        }
    }
}