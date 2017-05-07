using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Entities;
using Exception = System.Exception;
using String = System.String;

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
                save_button.Click += SaveButtonOnClick;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
            ViewModel.Hour = e.HourOfDay;
            ViewModel.Minute = e.Minute;
           // UpdateDisplay();
        }
        protected override Dialog OnCreateDialog(int id)
        {
            if (id == TIME_DIALOG_ID)
                return new TimePickerDialog(this, TimePickerCallback, ViewModel.Hour,ViewModel.Minute, false);

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