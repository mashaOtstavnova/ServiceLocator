//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using MvvmCross.Droid.Shared.Attributes;
//using MvvmCross.Droid.Support.V4;
//using ServiceLocator.Core.ViewModels;
//using ServiceLocator.Droid.Views;
//using ServiceLocator.Droid.Views.Controls;

//namespace ServiceLocator.Droid.Fragments
//{
//    [MvxFragment(typeof(HomeClientViewModel), Resource.Id.content_frame)]
//    [Register("servicelocator.droid.fragments.FriendsMasterFragment")]
//    public class FriendsMasterFragment : MvxFragment<FriendsMasterViewModel>
//    {

//        private List<FriendMaster> list = new List<FriendMaster>();
//        public List<FriendMaster> Items
//        {
//            set
//            {
//                list = value;
//            }
//            get
//            {
//                //он на каждый гет булет вызывать этот метод, ткак как  гет даже при пролистывании листвью ьбудет вызываться
//                // InitializeMyList();
//                return list;
//            }
//        }
//        public FriendsMasterFragment()
//        {
//            RetainInstance = true;
//        }
//        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//        {
//            base.OnCreateView(inflater, container, savedInstanceState);

//            var view = inflater.Inflate(Resource.Layout.friends_masters_view, null);
//            var grid = view.FindViewById<MvxSelectableListView>(Resource.Id.select);
//            //presents = new List<Present>
//            //{
//            //    new Present
//            //    {
//            //        Title = "Title",
//            //        Details = "Datails",
//            //        Id = 25,
//            //        Image = "https://cs7060.vk.me/c604316/v604316689/384b0/H9NOko56g6o.jpg"
//            //    }
//            //};
//            Items = ViewModel.Items;
//            grid.Adapter = new FriendsMastersAdapter(Activity, Items);
//            grid.ItemClick += GridOnItemClick;
//            return view;
//        }

//        private void GridOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
//        {
//            //var intent = new Intent(Activity, typeof(PresentsView));
//            //intent.PutExtra("Title", presents[itemClickEventArgs.Position].Title);
//            //intent.PutExtra("Image", presents[itemClickEventArgs.Position].Image);
//            //StartActivity(intent);
//        }
//    }
//}