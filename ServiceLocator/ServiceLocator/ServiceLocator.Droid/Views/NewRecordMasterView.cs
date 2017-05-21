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

    public class NewRecordMasterView : BaseView<NewRecordMasterViewModel>
    {
        protected override int LayoutResource => Resource.Layout.new_record_master_view;
        private TextView time_display;
        private Button pick_button;
        private Button pickDate_button;
        DatePicker datePicker;

        private MvxSubscriptionToken _token;

        private AutoCompleteTextView _autocompleteTextView;
        const int TIME_DIALOG_ID = 0;
        private IProfileService _profileService;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _token = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<NeedSetAdapterMessage>(DeliveryAction);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);

            collapsingToolbar.SetCollapsedTitleTextColor(Color.White);
            collapsingToolbar.SetExpandedTitleColor(Color.White);
            var set = this.CreateBindingSet<NewRecordMasterView, NewRecordMasterViewModel>();
            set.Bind(collapsingToolbar).For(ctb => ctb.Title).To(vm => vm.NameClient);
            this.CreateBinding().For("Title").To<NewRecordMasterViewModel>(vm => vm.NameClient).Apply();
            set.Apply();
            _autocompleteTextView =
                                FindViewById<AutoCompleteTextView>(Resource.Id.new_record_master_autoCompleteInput);
            // var listName = ViewModel.Friends.Select(t=>t.Title);
          
          
            try
            {
                
                
                pick_button = FindViewById<Button>(Resource.Id.pickTime);
                pickDate_button = FindViewById<Button>(Resource.Id.pickDate);
                var save_button = FindViewById<Button>(Resource.Id.save_button);
                
                pick_button.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);
                pickDate_button.Click += (sender, e) => {
                    DateTime today = DateTime.Today;
                    DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                    dialog.DatePicker.MinDate = today.Millisecond;
                    dialog.Show();
                };
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
        private void SaveButtonOnClick(object sender1, EventArgs eventArgs)
        {
            var record = new Record();
            
            ViewModel.AddNewRecordCommand.Execute(record);
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.Date = e.Date;
        }

        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            ViewModel.Hour = e.HourOfDay.ToString();
            ViewModel.Minute = e.Minute.ToString();
           // UpdateDisplay();
        }
        protected override Dialog OnCreateDialog(int id)
        {
            if (id == TIME_DIALOG_ID)
                return new TimePickerDialog(this, TimePickerCallback, Convert.ToInt32(ViewModel.Hour), Convert.ToInt32(ViewModel.Minute), false);

            return null;
        }
        private void UpdateDisplay()
        {
            string time = string.Format("{0}:{1}", ViewModel.Hour, ViewModel.Minute.ToString().PadLeft(2, '0'));
            time_display.Text = time;
        }

        public class test
        {
            public string str;
            public int id;
        }
    }

   
}