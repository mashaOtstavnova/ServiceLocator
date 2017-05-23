using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Entities;
using Object = Java.Lang.Object;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Views;
using ServiceLocator.Core.IServices;

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class PushMasterView:BaseView<PushMasterViewModel>
    {
        protected override int LayoutResource => Resource.Layout.add_new_push_masters;
        private MvxSubscriptionToken _token;

        private AutoCompleteTextView _autocompleteTextView;
     
        private IProfileService _profileService;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _token = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<NeedSetAdapterMessage>(DeliveryAction);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            var check = FindViewById<CheckBox>(Resource.Id.checkbox_all);
            check.Checked = true;
            check.Click += (s, e) =>
            {
                ViewModel.All = check.Checked;
                
            };
            _autocompleteTextView =
                                FindViewById<AutoCompleteTextView>(Resource.Id.new_record_master_autoCompleteInput);
            collapsingToolbar.SetCollapsedTitleTextColor(Color.White);
            collapsingToolbar.SetExpandedTitleColor(Color.White);
            _autocompleteTextView.Text = ViewModel.NameClient;
            var set = this.CreateBindingSet<PushMasterView, PushMasterViewModel>();
            set.Bind(collapsingToolbar).For(ctb => ctb.Title).To(vm => vm.NameClient);
            this.CreateBinding().For("Title").To<PushMasterViewModel>(vm => vm.NameClient).Apply();
            set.Bind(_autocompleteTextView).For(ctb => ctb.Visibility).To(vm => vm.All).WithConversion("MyVisibilityFalse");


            set.Apply();
           
            try
            {


             
                var save_button = FindViewById<Button>(Resource.Id.push_button);
                save_button.Click += Save_button_Click;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        
        private void Save_button_Click(object sender, EventArgs e)
        {
            ViewModel.PushCommand.Execute();
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
                        new ObservableCollection<FriendItem>(ViewModel.Items));
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
            ViewModel.IdClient = item.Id;
            ViewModel.Client = await _profileService.GetUserById(item.Id);
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