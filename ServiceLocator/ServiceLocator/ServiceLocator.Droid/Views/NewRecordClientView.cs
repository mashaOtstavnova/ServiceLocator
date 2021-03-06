﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Core.ViewModels;

using Toolbar = Android.Support.V7.Widget.Toolbar;
using MvvmCross.Plugins.Messenger;
using System.Collections.ObjectModel;

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class NewRecordClientView : BaseView<NewRecordClientViewModel>
    {
        protected override int LayoutResource => Resource.Layout.new_record_client_view;
        private TextView time_display;
        private Button pick_button;
        private Button pickDate_button;
        DatePicker datePicker;

        private MvxSubscriptionToken _token;

        private AutoCompleteTextView _autocompleteTextView;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _token = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<NeedSetAdapterMessage>(DeliveryAction);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            _autocompleteTextView =
                              FindViewById<AutoCompleteTextView>(Resource.Id.new_record_master_autoCompleteInput);

            collapsingToolbar.SetCollapsedTitleTextColor(Color.White);
            collapsingToolbar.SetExpandedTitleColor(Color.White);
            var set = this.CreateBindingSet<NewRecordClientView, NewRecordClientViewModel>();
            set.Bind(collapsingToolbar).For(ctb => ctb.Title).To(vm => vm.NameMaster);
            this.CreateBinding().For("Title").To<NewRecordClientViewModel>(vm => vm.NameMaster).Apply();
            _autocompleteTextView.Text = ViewModel.NameMaster;
            set.Apply();
            
          
       
            try
            {
               

                pick_button = FindViewById<Button>(Resource.Id.pickTime);
                pickDate_button = FindViewById<Button>(Resource.Id.pickDate);
                var save_button = FindViewById<Button>(Resource.Id.save_button);

                pick_button.Click +=  DialogForTimeOnClick;
                pickDate_button.Click += (sender, e) => {
                    DateTime today = DateTime.Today;
                    DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                    dialog.DatePicker.MinDate = today.Millisecond;
                    dialog.Show();
                };
                save_button.Click += SaveButtonOnClick;

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
            ViewModel.IdMaster = item.Id;
            ViewModel.Master = await _profileService.GetUserById(item.Id);
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
        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.Date = e.Date;
        }
        private List<string> time;
        private AlertDialog dlgAlert;
        private IProfileService _profileService;

        private void DialogForTimeOnClick(object sender, EventArgs e)
        {
            IDataLoaderService dataLoaderService;
            Mvx.TryResolve(out dataLoaderService);
            //получение мастера
            ViewModel.IdMaster = 01;
            if (ViewModel.IdMaster != 0&ViewModel.Date!=new DateTime())
            {
                time = dataLoaderService.GetIsBusiRecordsMaster(ViewModel.IdMaster,ViewModel.Date);
                dlgAlert = (new AlertDialog.Builder(this)).Create();
                dlgAlert.SetTitle("Выбирите время");
                var listView = new ListView(this);
               
                listView.Adapter = new AlertListViewAdapter(this, time);
                listView.ItemClick += listViewItemClick;
                dlgAlert.SetView(listView);
               // dlgAlert.SetButton("OK", handllerNotingButton);
                dlgAlert.Show();
            }
            else if (ViewModel.IdMaster == 0)
            {
                var dlgAlert1 = (new AlertDialog.Builder(this)).Create();
                dlgAlert1.SetMessage("Выбирите мастера");
                dlgAlert1.SetTitle("Заполните поле");
                dlgAlert1.SetButton("OK", handllerNotingButton);
                dlgAlert1.Show();
            }else if (ViewModel.Date == new DateTime())
            {
                var dlgAlert1 = (new AlertDialog.Builder(this)).Create();
                dlgAlert1.SetMessage("Выбирите дату");
                dlgAlert1.SetTitle("Заполните поле");
                dlgAlert1.SetButton("OK", handllerNotingButton);
                dlgAlert1.Show();
            }
        }
        void handllerNotingButton(object sender, DialogClickEventArgs e)
        {
            AlertDialog objAlertDialog = sender as AlertDialog;
            Button btnClicked = objAlertDialog.GetButton(e.Which);
           // Toast.MakeText(this, "you clicked on " + btnClicked.Text, ToastLength.Long).Show();
        }
        void listViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, "Вы выбрали " + time[e.Position], ToastLength.Short).Show();
            ViewModel.TimeString = time[e.Position];
            dlgAlert.Cancel();
        }

        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    internal class AlertListViewAdapter : BaseAdapter<string>
    {
        Activity _context = null;
        List<String> _lstDataItem = null;

        public AlertListViewAdapter(Activity context, List<String> lstDataItem)
        {
            _context = context;
            _lstDataItem = lstDataItem;
        }

        #region implemented abstract members of BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
                convertView = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

            (convertView.FindViewById<TextView>(Android.Resource.Id.Text1))
                .SetText(this[position], TextView.BufferType.Normal);

            return convertView;
        }

        public override int Count
        {
            get
            {
                return _lstDataItem.Count;
            }
        }

        #endregion

        #region implemented abstract members of BaseAdapter

        public override string this[int index]
        {
            get
            {
                return _lstDataItem[index];
            }
        }

        #endregion

    }
}