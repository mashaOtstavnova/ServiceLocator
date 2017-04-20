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

using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;


namespace ServiceLocator.Droid.Views.Dialogs
{
    public class CategoryServiceDialog: MvxAppCompatDialogFragment
    {

        private AlertDialog _createdDialog;
        public override Dialog OnCreateDialog(Bundle savedState)
        {
            EnsureBindingContextSet(savedState);

            var view = this.BindingInflate(Resource.Layout.category_dialog, null);
            var mainLayout = view.FindViewById<LinearLayout>(Resource.Id.category_dialog_main);

            //var checkbox = new CheckBox(this.Context)
            //{
            //    Text = "",
            //    Checked = false,
            //};
            //checkbox.CheckedChange += addToArray;


            //var checkbox = new CheckBox(this.Context)
            //{
            //    Text = "",
            //    Checked = false,
            //};
            //checkbox.CheckedChange += addToArray;



            var builder = new AlertDialog.Builder(Activity);
            builder.SetView(view);

            //_selectedItemButton = view.FindViewById<TextView>(Resource.Id.printed_not_served_dialog_selectedItem);
            //_selectedItemButton.Click += SelectedItemButtonOnClick;

            //_allCourseButton = view.FindViewById<TextView>(Resource.Id.printed_not_served_dialog_allCourse);
            //_allCourseButton.Click += AllCourseButtonOnClick;

            _createdDialog = builder.Create();
            return _createdDialog;
        }
        public override void OnResume()
        {
            var width = (int)(Resources.DisplayMetrics.WidthPixels / 1.25);
            var @params = Dialog.Window.Attributes;
            @params.Width = width;
            @params.Height = ViewGroup.LayoutParams.WrapContent;
            Dialog.Window.Attributes = @params;
            base.OnResume();
        }
        protected override void Dispose(bool disposing)
        {
            //_allCourseButton.Click -= AllCourseButtonOnClick;
            //_selectedItemButton.Click -= SelectedItemButtonOnClick;
            base.Dispose(disposing);
        }
    }
}