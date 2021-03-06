﻿using System;
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

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class RecordView:BaseView<RecordViewModel>
    {
        protected override int LayoutResource => Resource.Layout.record_view;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);

            var set = this.CreateBindingSet<RecordView, RecordViewModel>();
            set.Bind(collapsingToolbar).For(ctb => ctb.Title).To(vm => vm.NameMaster);
            var delButton = FindViewById<Button>(Resource.Id.del_button);
            var updateButoon = FindViewById<Button>(Resource.Id.update_button);
            delButton.Click += DelButton_Click;
            updateButoon.Click += UpdateButoonOnClick;
            set.Bind(delButton).For(ctb => ctb.Visibility).To(vm => (vm.IsMy)).WithConversion("MyVisibility");
            set.Bind(updateButoon).For(ctb => ctb.Visibility).To(vm => vm.IsMy).WithConversion("MyVisibility");
            this.CreateBinding().For("Title").To<RecordViewModel>(vm => vm.NameMaster).Apply();
            set.Apply();
            
        }

        private void UpdateButoonOnClick(object sender, EventArgs eventArgs)
        {
            ViewModel.UpdateRecordCommand.Execute();
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
        private void DelButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnDestroy()
        {
            //var openPresentsButton = FindViewById<Button>(Resource.Id.button_present_user);
            //openPresentsButton.Click -= ButtonForPresent;
            base.OnDestroy();
        }
    }
}