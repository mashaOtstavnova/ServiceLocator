using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Info.Hoang8f.Android.Segmented;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Entities;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class ClientsRecordsView: BaseView<ClientsRecordsViewModel>
    {
        private ListView _eventsList;
        protected override int LayoutResource => Resource.Layout.record_clients_view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ViewModel.Title = "Ваши записи";
            Title = ViewModel.Title;
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
           
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
           
            //_eventsList= FindViewById<MvxListView>(Resource.Id.schedule_view_eventsList);

            //ViewModel.ReloadRecord(DateTime.Today);
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


    }
}