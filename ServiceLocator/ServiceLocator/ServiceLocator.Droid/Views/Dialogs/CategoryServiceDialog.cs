using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Entities;

namespace ServiceLocator.Droid.Views.Dialogs
{
    public class CategoryServiceDialog : MvxAppCompatDialogFragment
    {
        private readonly FirstMasterViewModel _firstMasterViewModel;
        private List<CheckBox> _checkBoxList;
        private Button _closeButton;

        private AlertDialog _createdDialog;

        public CategoryServiceDialog(FirstMasterViewModel firstMasterViewModel)
        {
            _firstMasterViewModel = firstMasterViewModel;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            EnsureBindingContextSet(savedState);

            var view = this.BindingInflate(Resource.Layout.category_dialog, null);
            var mainLayout = view.FindViewById<LinearLayout>(Resource.Id.category_dialog_main);

            _checkBoxList = new List<CheckBox>();

            foreach (var check in ServicesCategory.ServicesCategoryList)
            {
                var checkbox = new CheckBox(Context)
                {
                    Text = check,
                    Checked = false,
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
            return _createdDialog;
        }

        private void CloseButtonOnClick(object sender, EventArgs eventArgs)
        {
            var list = new List<string>();
            foreach (var checkBox in _checkBoxList)
                if (checkBox.Checked)
                    list.Add(checkBox.Text);
            _firstMasterViewModel.SelectedCategories = new List<string>(list);
            Dialog.Cancel();
        }

        public override void OnResume()
        {
            var width = (int) (Resources.DisplayMetrics.WidthPixels / 1.25);
            var @params = Dialog.Window.Attributes;
            @params.Width = width;
            @params.Height = ViewGroup.LayoutParams.WrapContent;
            Dialog.Window.Attributes = @params;
            base.OnResume();
        }

        protected override void Dispose(bool disposing)
        {
            _checkBoxList.Clear();
            _checkBoxList = null;
            _closeButton.Click -= CloseButtonOnClick;
            base.Dispose(disposing);
        }
    }
}