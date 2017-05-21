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
using ServiceLocator.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Visibility;

namespace ServiceLocator.Droid.Views
{
    [Activity ]
    public class MoneyView : BaseView<MoneyViewModel>
    {
        protected override int LayoutResource => Resource.Layout.money_master_view;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            ll = FindViewById<LinearLayout>(Resource.Id.contentForMoney);
            ll.Visibility = ViewStates.Gone;
            ViewModel.Title = "Выручка";
            Title = ViewModel.Title;
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var origin = new DateTime(1970, 1, 1);
            var startDateButton = FindViewById<Button>(Resource.Id.start);
            var endDateButton = FindViewById<Button>(Resource.Id.end);
            var enter = FindViewById<Button>(Resource.Id.enter);
            enter.Click += OnEnterDate;
                startDateButton.Click += (sender, e) => {
                   
                    DateTime today = DateTime.Today;
                    DateTime todayMin = new DateTime(today.Year,today.Month,today.Day - 1);
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSetStart, todayMin.Year, todayMin.Month - 1, todayMin.Day);
                
                dialog.DatePicker.MaxDate = (long)(DateTime.Now.Date.AddDays(0) - origin).TotalMilliseconds;
                    //dialog.DatePicker.MinDate = todayMin.Millisecond;
                    dialog.Show();
            };
            endDateButton.Click += (sender, e) => {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSetEnd, today.Year, today.Month - 1, today.Day);


                dialog.DatePicker.MaxDate = (long)(DateTime.Now.Date.AddDays(1) - origin).TotalMilliseconds;//dialog.DatePicker.MaxDate = today.Millisecond;
                dialog.Show();
            };
        }
        LinearLayout ll;
        void handllerNotingButton(object sender, DialogClickEventArgs e)
        {
            AlertDialog objAlertDialog = sender as AlertDialog;
            Button btnClicked = objAlertDialog.GetButton(e.Which);
            // Toast.MakeText(this, "you clicked on " + btnClicked.Text, ToastLength.Long).Show();
        }
        private void OnEnterDate(object sender, EventArgs e)
        {
            if (ViewModel.StartDate != "От" && ViewModel.EndDate != "До")
            {

                ViewModel.GetMoneyClickCommand.Execute();
                ll.Visibility = ViewStates.Visible;
            }
            else
            {
                var dlgAlert1 = (new AlertDialog.Builder(this)).Create();
                dlgAlert1.SetMessage("Выбирите дату");
                dlgAlert1.SetTitle("Заполните поле для промежутка");
                dlgAlert1.SetButton("OK", handllerNotingButton);
                dlgAlert1.Show();
            }
        }

        private void OnDateSetStart(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.StartDate = e.Date.ToString("d");
        }
        private void OnDateSetEnd(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.EndDate = e.Date.ToString("d");
        }
    }
}