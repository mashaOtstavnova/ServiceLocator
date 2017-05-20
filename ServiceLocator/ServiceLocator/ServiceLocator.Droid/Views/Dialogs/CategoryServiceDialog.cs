using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Entities;
using AlertDialog = Android.App.AlertDialog;

namespace ServiceLocator.Droid.Views.Dialogs
{
    public class CategoryServiceDialog : AppCompatDialogFragment
    {
        private readonly FirstMasterViewModel _firstMasterViewModel;
        private List<CheckBox> _checkBoxList;
        private Button _closeButton;

        private AlertDialog _createdDialog;

        public CategoryServiceDialog(FirstMasterViewModel firstMasterViewModel)
        {
            _firstMasterViewModel = firstMasterViewModel;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.category_dialog, null);
            Dialog.SetTitle("Выберите категории");
            var mainLayout = view.FindViewById<LinearLayout>(Resource.Id.category_dialog_main);
            _checkBoxList = new List<CheckBox>();

            for (var index = 0; index < ServicesCategory.ServicesCategoryList.Count; index++)
            {
                var check = ServicesCategory.ServicesCategoryList[index];
                var checkbox = new CheckBox(Context)
                {
                    Text = check,
                    Checked = _firstMasterViewModel.SelectedCategories.Exists(s => s.ToLower().Equals(check.ToLower())),
                    LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.WrapContent)
                };
                _checkBoxList.Add(checkbox);
                mainLayout.AddView(checkbox);
            }

            _closeButton = view.FindViewById<Button>(Resource.Id.category_dialog_closeButton);
            _closeButton.Click += CloseButtonOnClick;

            var builder = new AlertDialog.Builder(Activity);
            builder.SetView(view);

            _createdDialog = builder.Create();
            return view; // base.OnCreateView(inflater, container, savedInstanceState);
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Dialog?.SetTitle("Выберите категории");
        }

        private void CloseButtonOnClick(object sender, EventArgs eventArgs)
        {
            var list = new List<string>();
            foreach (var checkBox in _checkBoxList)
                if (checkBox.Checked)
                    list.Add(checkBox.Text);
            _firstMasterViewModel.SelectedCategories = new List<string>(list);
            Dialog.Dismiss();
        }


        protected override void Dispose(bool disposing)
        {
            _createdDialog.Dispose();
            _createdDialog = null;
            _checkBoxList.Clear();
            _checkBoxList = null;
            _closeButton.Click -= CloseButtonOnClick;
            base.Dispose(disposing);
        }
    }

    public class SheduleServiceDialog : AppCompatDialogFragment
    {
        private const int TIME_DIALOG_ID = 0;
        private readonly FirstMasterViewModel _firstMasterViewModel;
        private Button _che_c_btn;
        private Button _che_po_btn;
        private List<CheckBox> _checkBoxList;
        private Button _closeButton;

        private AlertDialog _createdDialog;
        private int _lastCclikedId;
        private int _lastPoclikedId;
        private Button _pn_c_btn;
        private Button _pn_po_btn;
        private Button _pt_c_btn;
        private Button _pt_po_btn;
        private Button _sb_c_btn;
        private Button _sb_po_btn;
        private Button _sr_c_btn;
        private Button _sr_po_btn;
        private Button _vs_c_btn;
        private Button _vs_po_btn;
        private Button _vt_c_btn;
        private Button _vt_po_btn;

        public SheduleServiceDialog(FirstMasterViewModel firstMasterViewModel)
        {
            _firstMasterViewModel = firstMasterViewModel;
        }

        public override void OnResume()
        {
            base.OnResume();
            Window window = Dialog.Window; 
            window.SetLayout(LinearLayout.LayoutParams.WrapContent, 1150);
            window.SetGravity(GravityFlags.Center);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.shedule_dialog, null);
            Dialog.SetTitle("Создайте расписание");
            var mainLayout = view.FindViewById<LinearLayout>(Resource.Id.category_dialog_main);
            var st = view.FindViewById<CheckBox>(Resource.Id.checkbox_pn);
            _checkBoxList = new List<CheckBox>();
            _checkBoxList.Add(view.FindViewById<CheckBox>(Resource.Id.checkbox_pn));
            _checkBoxList.Add(view.FindViewById<CheckBox>(Resource.Id.checkbox_vt));
            _checkBoxList.Add(view.FindViewById<CheckBox>(Resource.Id.checkbox_sr));
            _checkBoxList.Add(view.FindViewById<CheckBox>(Resource.Id.checkbox_che));
            _checkBoxList.Add(view.FindViewById<CheckBox>(Resource.Id.checkbox_pt));
            _checkBoxList.Add(view.FindViewById<CheckBox>(Resource.Id.checkbox_sb));
            _checkBoxList.Add(view.FindViewById<CheckBox>(Resource.Id.checkbox_vs));
            ///knopki C
            _pn_c_btn = view.FindViewById<Button>(Resource.Id.checkbox_pn_s);
            _pn_c_btn.Click += CBtnOnClick;
            _vt_c_btn = view.FindViewById<Button>(Resource.Id.checkbox_vt_s);
            _vt_c_btn.Click += CBtnOnClick;
            _sr_c_btn = view.FindViewById<Button>(Resource.Id.checkbox_sr_s);
            _sr_c_btn.Click += CBtnOnClick;
            _che_c_btn = view.FindViewById<Button>(Resource.Id.checkbox_che_s);
            _che_c_btn.Click += CBtnOnClick;
            _pt_c_btn = view.FindViewById<Button>(Resource.Id.checkbox_pt_s);
            _pt_c_btn.Click += CBtnOnClick;
            _sb_c_btn = view.FindViewById<Button>(Resource.Id.checkbox_sb_s);
            _sb_c_btn.Click += CBtnOnClick;
            _vs_c_btn = view.FindViewById<Button>(Resource.Id.checkbox_vs_s);
            _vs_c_btn.Click += CBtnOnClick;


            _pn_po_btn = view.FindViewById<Button>(Resource.Id.checkbox_pn_po);
            _pn_po_btn.Click += PoBtnOnClick;
            _vt_po_btn = view.FindViewById<Button>(Resource.Id.checkbox_vt_po);
            _vt_po_btn.Click += PoBtnOnClick;
            _sr_po_btn = view.FindViewById<Button>(Resource.Id.checkbox_sr_po);
            _sr_po_btn.Click += PoBtnOnClick;
            _che_po_btn = view.FindViewById<Button>(Resource.Id.checkbox_che_po);
            _che_po_btn.Click += PoBtnOnClick;
            _pt_po_btn = view.FindViewById<Button>(Resource.Id.checkbox_pt_po);
            _pt_po_btn.Click += PoBtnOnClick;
            _sb_po_btn = view.FindViewById<Button>(Resource.Id.checkbox_sb_po);
            _sb_po_btn.Click += PoBtnOnClick;
            _vs_po_btn = view.FindViewById<Button>(Resource.Id.checkbox_vs_po);
            _vs_po_btn.Click += PoBtnOnClick;


            //_checkBoxList = new List<CheckBox>();

            //for (var index = 0; index < ServicesCategory.DayOfWeekList.Count; index++)
            //{
            //    var lianerLayout = new LinearLayout(Context)
            //    {
            //        LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
            //            ViewGroup.LayoutParams.WrapContent)
            //    };
            //    var check = ServicesCategory.DayOfWeekList[index];
            //    var checkbox = new CheckBox(Context)
            //    {
            //        Text = check,
            //        Checked = _firstMasterViewModel.SelectedCategories.Exists(s => s.ToLower().Equals(check.ToLower())),
            //        LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
            //            ViewGroup.LayoutParams.WrapContent)
            //    };
            //    _checkBoxList.Add(checkbox);
            //    lianerLayout.AddView(checkbox);

            //    mainLayout.AddView(lianerLayout);
            //}

            _closeButton = view.FindViewById<Button>(Resource.Id.category_dialog_closeButton);
            _closeButton.Click += CloseButtonOnClick;

            var builder = new AlertDialog.Builder(Activity);
            builder.SetView(view);

            _createdDialog = builder.Create();
            return view; // base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void CBtnOnClick(object sender, EventArgs eventArgs)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                _lastCclikedId = btn.Id;
                var timePicker = new TimePickerDialog(Activity, CallBack, 9, 00, true);
                timePicker.Show();
            }
        }
        private void PoBtnOnClick(object sender, EventArgs eventArgs)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                _lastPoclikedId = btn.Id;
                var timePicker = new TimePickerDialog(Activity, CallBackPo,18, 00, true);
                timePicker.Show();
            }
        }
        private void CallBackPo(object sender, TimePickerDialog.TimeSetEventArgs args)
        {
            switch (_lastPoclikedId)
            {
                case Resource.Id.checkbox_pn_po:
                    _pn_po_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_vt_po:
                    _vt_po_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_sr_po:
                    _sr_po_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_che_po:
                    _che_po_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_pt_po:
                    _pt_po_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_sb_po:
                    _sb_po_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_vs_po:
                    _vs_po_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
            }
            _lastPoclikedId = 0;
        }
        private void CallBack(object sender, TimePickerDialog.TimeSetEventArgs args)
        {
            switch (_lastCclikedId)
            {
                case Resource.Id.checkbox_pn_s:
                    _pn_c_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_vt_s:
                    _vt_c_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_sr_s:
                    _sr_c_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_che_s:
                    _che_c_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_pt_s:
                    _pt_c_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_sb_s:
                    _sb_c_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
                case Resource.Id.checkbox_vs_s:
                    _vs_c_btn.Text = $"{args.HourOfDay}:{args.Minute}";
                    break;
            }
            _lastCclikedId = 0;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Dialog?.SetTitle("Выберите категории");
        }

        private void CloseButtonOnClick(object sender, EventArgs eventArgs)
        {
            var list = new List<string>();
            var listDay = new List<SheduleDay>();
            foreach (var checkBox in _checkBoxList)
                if (checkBox.Checked)
                { 
                switch (checkBox.Text)
                    {
                        case "ПН":
                            var s = _pn_c_btn.Text;
                            var e = _pn_po_btn.Text;
                            listDay.Add(new SheduleDay(){Day = "ПН", EndDay = e,StartDay = s});
                            break;
                        case "ВТ":
                            var s1 = _vt_c_btn.Text;
                            var e1 = _vt_po_btn.Text;
                            listDay.Add(new SheduleDay(){Day = "ВТ", EndDay = e1,StartDay = s1});
                            break;
                        case "СР":
                            var s2 = _sr_c_btn.Text;
                            var e2 = _sr_po_btn.Text;
                            listDay.Add(new SheduleDay(){Day = "СР", EndDay = e2,StartDay = s2});
                            break;
                        case "ЧТ":
                            var s3 = _che_c_btn.Text;
                            var e3 = _che_po_btn.Text;
                            listDay.Add(new SheduleDay(){Day = "ЧТ", EndDay = e3,StartDay = s3});
                            break;
                        case "ПТ":
                            var s4 = _pt_c_btn.Text;
                            var e4 = _pt_po_btn.Text;
                            listDay.Add(new SheduleDay(){Day = "ПТ", EndDay = e4,StartDay = s4});
                            break;
                        case "СБ":
                            var s5 = _sb_c_btn.Text;
                            var e5 = _sb_po_btn.Text;
                            listDay.Add(new SheduleDay(){Day = "СБ", EndDay = e5,StartDay = s5});
                            break;
                        case "ВС":
                            var s6 = _vs_c_btn.Text;
                            var e6 = _vs_po_btn.Text;
                            listDay.Add(new SheduleDay(){Day = "ВС", EndDay = e6,StartDay = s6});
                            break;
                    }
                 }
            
            _firstMasterViewModel.Shedule = new List<SheduleDay>(listDay);
            Dialog.Dismiss();
        }


        protected override void Dispose(bool disposing)
        {
            _createdDialog.Dispose();
            _createdDialog = null;
            _checkBoxList.Clear();
            _checkBoxList = null;
            _closeButton.Click -= CloseButtonOnClick;
            base.Dispose(disposing);
        }
    }
}