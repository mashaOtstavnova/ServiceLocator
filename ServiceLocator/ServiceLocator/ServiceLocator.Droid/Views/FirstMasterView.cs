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
        private Button _buttonForCategory;
        private ImageLoader _imageLoader;
        private Button _saveButton;
        private Button _buttonForShedule;
        protected override int LayoutResource => Resource.Layout.first_master_view;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _imageLoader = ImageLoader.Instance;

            if (!_imageLoader.IsInited)
                _imageLoader.Init(ImageLoaderConfiguration.CreateDefault(this));

            IProfileService profileService;
            var service = Mvx.TryResolve(out profileService);

            var user = await profileService.GetUser();
            
            var title = user.first_name + " " + user.last_name;
            var birthday = user.bdate;

            title = string.IsNullOrWhiteSpace(title) ? "New Friend" : title;
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            collapsingToolbar.SetTitle(title);
            collapsingToolbar.SetCollapsedTitleTextColor(Color.White);
            collapsingToolbar.SetExpandedTitleColor(Color.White);
            try
            {
                _imageLoader.DisplayImage(user.photo_100, FindViewById<ImageView>(Resource.Id.imageView1));
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }
            _buttonForCategory = (Button) FindViewById(Resource.Id.category);
            _buttonForShedule = (Button) FindViewById(Resource.Id.schedule);
            _buttonForCategory.Click += OnCategoryClick;
            _buttonForShedule.Click += OnSheduleClick;
            _saveButton = FindViewById<Button>(Resource.Id.save_button);
            _saveButton.Click += SaveButtonOnClick;
        }

        protected override void OnDestroy()
        {
            _buttonForCategory.Click -= OnCategoryClick;
            _saveButton.Click -= SaveButtonOnClick;
            _imageLoader.Destroy();
            base.OnDestroy();
        }

        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            ViewModel.SaveInfoCommand.Execute();
        }
        private void OnSheduleClick(object sender, EventArgs eventArgs)
        {
            var dialog4 = new SheduleServiceDialog(ViewModel);
            dialog4.Show(SupportFragmentManager, "PrintedAndNotServedDialog");
        }
        private void OnCategoryClick(object sender, EventArgs eventArgs)
        {
            var dialog4 = new CategoryServiceDialog(ViewModel);
            dialog4.Show(SupportFragmentManager, "PrintedAndNotServedDialog");
        }
    }
}