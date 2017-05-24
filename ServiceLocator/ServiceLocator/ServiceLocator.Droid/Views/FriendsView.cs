using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using ServiceLocator.Core.ViewModels;

using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using MvvmCross.Plugins.Messenger;
using System.Collections.ObjectModel;
using Android.Widget;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;

namespace ServiceLocator.Droid.Views
{
    [Activity(Label = "Friends", ParentActivity = typeof(HomeMasterView))]
    public class FriendsView : BaseView<FriendsViewModel>
    {
        protected override int LayoutResource => Resource.Layout.friends_masters_view;

        private MvxSubscriptionToken _token;

        private AutoCompleteTextView _autocompleteTextView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _token = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<NeedSetAdapterMessage>(DeliveryAction);

            ViewModel.Title = "Друзья";
            Title = ViewModel.Title;
            _autocompleteTextView =
                            FindViewById<AutoCompleteTextView>(Resource.Id.new_record_master_autoCompleteInput);


            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        protected override void Dispose(bool disposing)
        {
            _token?.Dispose();
            _token = null;
            base.Dispose(disposing);
        }
        private void DeliveryAction(NeedSetAdapterMessage needSetAdapterMessage)
        {
            //var friends = ViewModel.Items.Select(
            //        f => new FriendItem {Image = f.photo_50, Name = f.first_name, SurName = f.last_name})
            //    .ToList();

            try
            {
                _autocompleteTextView.Adapter =
                    new FriendsFilteringAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line,
                        new ObservableCollection<FriendItem>(ViewModel.ItemsAutoComText));
                _autocompleteTextView.ItemClick += AutocompleteTextViewOnItemClick;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async void AutocompleteTextViewOnItemClick(object sender1, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            var item = (_autocompleteTextView.Adapter as FriendsFilteringAdapter)?.FilteredItems[itemClickEventArgs.Position];
            _autocompleteTextView.Text = item?.ToString();

            _profileService = Mvx.Resolve<IProfileService>();
            ViewModel.OnItemAutoSelect(item.Id);
            //ViewModel.IdMaster = item.Id;
            //ViewModel.Master = await _profileService.GetUserById(item.Id);
        }

        private IProfileService _profileService;

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