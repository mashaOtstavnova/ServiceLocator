using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using ServiceLocator.Core.ViewModels;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ServiceLocator.Droid.Views
{
    [Activity(Label = "Friends", ParentActivity = typeof(HomeMasterView))]
    public class FriendsView : BaseView<FriendsViewModel>
    {
        protected override int LayoutResource => Resource.Layout.friends_masters_view;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ViewModel.Title = "Друзья";
            Title = ViewModel.Title;
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
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