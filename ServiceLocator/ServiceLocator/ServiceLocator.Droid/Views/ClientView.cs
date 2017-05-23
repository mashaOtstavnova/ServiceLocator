using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using ServiceLocator.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using ServiceLocator.Core.IServices;
using MvvmCross.Platform;

namespace ServiceLocator.Droid.Views
{
    [Activity(Label = "Friend")]
    public class ClientView : BaseView<ClientViewModel>
    {
        protected override int LayoutResource => Resource.Layout.client_view;

        private  IProgressLoaderService _progressLoaderService;
        protected override void OnCreate(Bundle bundle)
        {

            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            _progressLoaderService.ShowProgressBar();
            base.OnCreate(bundle);
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
           
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var callButton = FindViewById<ImageButton>(Resource.Id.button_open_call);
            if (ViewModel.HomePhone == null & ViewModel.MobilePhone == null)
            {
                callButton.Visibility = ViewStates.Invisible;
            }
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            //collapsingToolbar.


            callButton.Click += OnButtonFoCall;

            var addNewRecordButoon = FindViewById<FloatingActionButton>(Resource.Id.addNewRecord);
            addNewRecordButoon.Click += OnAddNewRecord;

            var myRecordButoon = FindViewById<Button>(Resource.Id.shedule_button);
            myRecordButoon.Click += MyRecordButoon_Click; ;
            var set = this.CreateBindingSet<ClientView, ClientViewModel>();
            set.Bind(collapsingToolbar).For(ctb => ctb.Title).To(vm => vm.FullName);

            set.Bind(callButton).For(ctb => ctb.Visibility).To(vm => vm.IsPhone).WithConversion("MyVisibility");

            set.Bind(addNewRecordButoon).For(ctb => ctb.Visibility).To(vm => vm.IsMy).WithConversion("MyVisibilityFalse");
            set.Bind(myRecordButoon).For(ctb => ctb.Visibility).To(vm => vm.IsMyForRecord).WithConversion("MyVisibility");

            this.CreateBinding().For("Title").To<MasterViewModel>(vm => vm.FullName).Apply();
            set.Apply();
            if (ViewModel.Client != null && ViewModel.Client.IsRegistration)
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
        }

        private void MyRecordButoon_Click(object sender, EventArgs e)
        {
            ViewModel.ShowMyRecordCommand.Execute();
        }

        private void OnAddNewRecord(object sender, EventArgs e)
        {
            ViewModel.AddNewRecordCommand.Execute();
        }
        private void OnButtonFoCall(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionDial);
            intent.SetData(Android.Net.Uri.Parse("tel:" + ViewModel.ContactsString));
            StartActivity(intent);
        }


        protected override void OnDestroy()
        {
            //var openPresentsButton = FindViewById<Button>(Resource.Id.button_present_user);
            //openPresentsButton.Click -= ButtonForPresent;
            base.OnDestroy();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:

                    try
                    {
                        ViewModel.CloseThisVMCommand.Execute();
                    }
                    catch (Exception ex)
                    {
                        var m = ex.Message;
                    }
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}