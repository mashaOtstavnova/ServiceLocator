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
using Java.Lang;
using MvvmCross.Binding.BindingContext;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Entities;
using Exception = System.Exception;
using String = System.String;
using System;
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
using ServiceLocator.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

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

        const int TIME_DIALOG_ID = 0;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

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

            var autoCompleteOptions = new String[]
                {"Hello", "Hey", "Heja", "Hi", "Hola", "Bonjour", "Gday", "Goodbye", "Sayonara", "Farewell", "Adios"};
           // var listName = ViewModel.Friends.Select(t=>t.Title);
            var textlest = new List<test>();
            textlest.Add(new test() {id = 89, str = "test"});
            textlest.Add(new test() {id = 98, str = "test1"});
            textlest.Add(new test() {id = 9, str = "test2"});
            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line,
                autoCompleteOptions);
            try
            {
                var autocompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
                autocompleteTextView.Adapter = autoCompleteAdapter;
                
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