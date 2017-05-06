using System;
using System.Collections.Generic;
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
}