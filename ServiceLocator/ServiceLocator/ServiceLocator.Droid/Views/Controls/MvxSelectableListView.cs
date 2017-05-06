using System.Collections;
using System.Linq;
using Android.Content;
using Android.Util;
using MvvmCross.Binding.Droid.Views;

namespace ServiceLocator.Droid.Views.Controls
{
    public class MvxSelectableListView : MvxListView
    {
        public MvxSelectableListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public MvxSelectableListView(Context context, IAttributeSet attrs, IMvxAdapter adapter) : base(context, attrs,
            adapter)
        {
        }

        public IList SelectedItems
        {
            set
            {
                var objects = Adapter.ItemsSource.Cast<object>().ToList();
                foreach (var item in objects)
                {
                    var position = objects.IndexOf(item);
                    var isChecked = value.Contains(item);
                    SetItemChecked(position, isChecked);
                }
            }
        }
    }
}