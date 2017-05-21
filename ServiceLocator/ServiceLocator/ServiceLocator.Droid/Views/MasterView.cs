using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using ServiceLocator.Core.ViewModels;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using ServiceLocator.Core.IServices;
using MvvmCross.Platform;

namespace ServiceLocator.Droid.Views
{
    [Activity(Label = "Friend")]//, ParentActivity = typeof(HomeMasterView)
    //[MetaData("android.support.PARENT_ACTIVITY", Value = "navdrawer.activities.HomeMasterView")]
    public class MasterView : BaseView<MasterViewModel>
    {
        protected override int LayoutResource => Resource.Layout.master_view;

        private  IProgressLoaderService _progressLoaderService;

        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            _progressLoaderService.ShowProgressBar();
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            var addNewRecordButoon = FindViewById<FloatingActionButton>(Resource.Id.addNewRecord);
           
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var callButton = FindViewById<ImageButton>(Resource.Id.button_open_call);
            var sheduleButton = FindViewById<Button>(Resource.Id.shedule_button);
            sheduleButton.Click += OnButtonForShedule;

            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);

            var set = this.CreateBindingSet<MasterView, MasterViewModel>();
            set.Bind(collapsingToolbar).For(ctb => ctb.Title).To(vm => vm.FullName);

            callButton.Click += OnButtonFoCall;
            addNewRecordButoon.Click += OnAddNewRecord;
            try
            {
                set.Bind(callButton).For(ctb => ctb.Visibility).To(vm =>vm.IsPhone).WithConversion("MyVisibility");
                set.Bind(addNewRecordButoon).For(ctb => ctb.Visibility).To(vm =>vm.IsMy).WithConversion("MyVisibilityFalse");
            }
            catch (Exception e)
            {
                _progressLoaderService.HideProgressBar();
                Console.WriteLine(e);
                throw;
            }
          

            this.CreateBinding().For("Title").To<MasterViewModel>(vm => vm.FullName).Apply();
            set.Apply();
            if (ViewModel.Master != null && ViewModel.Master.IsRegistration)
            {
                var draw = GetDrawable(Resource.Drawable.ic_done);
                fab.SetImageDrawable(draw);
            }
            else
            {
                var draw = GetDrawable(Resource.Drawable.ic_clear);
                fab.SetImageDrawable(draw);
            }
            _progressLoaderService.HideProgressBar();
            //if (ViewModel.HomePhone == null & ViewModel.MobilePhone == null)
            //{
            //    callButton.Visibility = ViewStates.Invisible;
            //}
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    ViewModel.CloseThisVMCommand.Execute();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        private void OnButtonForShedule(object sender, EventArgs e)
        {
            ViewModel.SheduleCommand.Execute();
        }

        private void OnAddNewRecord(object sender, EventArgs e)
        {
            ViewModel.AddNewRecordCommand.Execute();
        }
        
        private void OnButtonFoCall(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionDial);
            intent.SetData(Android.Net.Uri.Parse("tel:" +ViewModel.ContactsString));
            StartActivity(intent);
        }
        protected override void OnDestroy()
        {
            //var openPresentsButton = FindViewById<Button>(Resource.Id.button_present_user);
            //openPresentsButton.Click -= ButtonForPresent;
            base.OnDestroy();
        }

       
    }
}