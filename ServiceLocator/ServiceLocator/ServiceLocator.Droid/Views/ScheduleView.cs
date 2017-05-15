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
    [Activity(Label = "Schedule")]
    public class ScheduleView : BaseView<ScheduleViewModel>
    {
        private CalendarView _calendar;
        private ListView _eventsList;
        private SegmentedGroup _segmentedControl;
        private SegmentedGroup tabSelect;
        private DateTime _calendarDate;
        protected override int LayoutResource => Resource.Layout.sheduli_view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ViewModel.Title = "Расписание";
            Title = ViewModel.Title;
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            var tabFree = FindViewById<RadioButton>(Resource.Id.schedule_view_segment_free);
             tabSelect = FindViewById<SegmentedGroup>(Resource.Id.schedule_view_segmentedControl);
            var tabBusy = FindViewById<RadioButton>(Resource.Id.schedule_view_segment_busy);
            tabSelect.Check(Resource.Id.schedule_view_segment_free);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            _calendarDate = DateTime.Now;
            _calendar = FindViewById<CalendarView>(Resource.Id.schedule_view_calendar);
            _calendar.DateChange += CalendarOnDateChange;

            _segmentedControl = FindViewById<SegmentedGroup>(Resource.Id.schedule_view_segmentedControl);
            _segmentedControl.CheckedChange += SegmentedControlOnCheckedChange;

            //_eventsList= FindViewById<MvxListView>(Resource.Id.schedule_view_eventsList);

            //ViewModel.ReloadRecord(DateTime.Today);
        }

        private void CalendarOnDateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            tabSelect.Check(Resource.Id.schedule_view_segment_free);
            _calendarDate = new DateTime(e.Year, e.Month + 1, e.DayOfMonth);
            ViewModel.ReloadRecord(_calendarDate);
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


        private void SegmentedControlOnCheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {

            switch (e.CheckedId)
            {
                case Resource.Id.schedule_view_segment_free:
                    ViewModel.ScheduleType = ScheduleType.Free;
                    ViewModel.ReloadRecord(_calendarDate);
                    break;
                case Resource.Id.schedule_view_segment_busy:
                    ViewModel.ScheduleType = ScheduleType.Busy;
                    ViewModel.ReloadRecord(_calendarDate);
                    break;
            }
        }
    }
}