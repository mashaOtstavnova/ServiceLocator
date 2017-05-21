using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;
using ServiceLocator.Core.ViewModels;

namespace ServiceLocator.Droid.Views
{
    public class FriendsFilteringAdapter : ArrayAdapter<FriendItem>
    {
        public FriendsFilteringAdapter(Context context, int textViewResourceId,
            ObservableCollection<FriendItem> items) : base(context, 0, items.ToArray())
        {
            Items = items;
            FilteredItems = new JavaList<FriendItem>();
        }

        public ObservableCollection<FriendItem> Items { get; set; }

        public JavaList<FriendItem> FilteredItems { get; set; }

        public override Filter Filter
        {
            get { return new FriendsFilter(this, Items); }
        }

        public override int Count
        {
            get
            {
                return FilteredItems.Size();
            }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var friend = FilteredItems[position];

            // Inflate your custom row layout as usual.
            var inflater = LayoutInflater.From(Context);
            convertView = inflater.Inflate(Resource.Layout.new_record_master_item, parent, false);

            var tvName = convertView.FindViewById<TextView>(Resource.Id.new_frienditem_name);
            tvName.Text = friend.ToString();
            var image = convertView.FindViewById<MvxAppCompatImageView>(Resource.Id.new_frienditem_image);
            image.ImageUrl = friend.Image;

            return convertView;
        }
    }

    public class FriendsFilter : Filter
    {
        public FriendsFilter(FriendsFilteringAdapter adapter, ObservableCollection<FriendItem> originalItems) : base()
        {
            Adapter = adapter;
            OriginalItems = new JavaList<FriendItem>(originalItems);
            FilteredItems = new JavaList<FriendItem>();
        }

        public FriendsFilteringAdapter Adapter { get; }
        public JavaList<FriendItem> OriginalItems { get; }
        public JavaList<FriendItem> FilteredItems { get; set; }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            FilteredItems.Clear();
            var results = new FilterResults();

            if (string.IsNullOrEmpty(constraint?.ToString()))
            {
                FilteredItems = new JavaList<FriendItem>(OriginalItems);
            }
            else
            {
                var filterPattern = constraint.ToString().ToLower().Trim();

                foreach (var item in OriginalItems)
                    if (item.ToString().ToLower().Contains(filterPattern))
                        FilteredItems.Add(item);
            }
            results.Values = FilteredItems;
            results.Count = FilteredItems.Size();
            return results;
        }

        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            Adapter.FilteredItems.Clear();
            Adapter.FilteredItems =//.AddAll(0, new JavaList {results.Values});//
                new JavaList<FriendItem>(
                    (IEnumerable<FriendItem>)results.Values);
            Adapter.NotifyDataSetChanged();
        }
    }
}