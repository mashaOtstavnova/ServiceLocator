//using Android.OS;
//using Android.Support.Design.Widget;
//using Android.Support.V4.App;
//using Android.Support.V4.View;
//using Android.Support.V7.Widget;
//using Android.Views;
//using Java.Lang;
//using MvvmCross.Binding.BindingContext;
//using ServiceLocator.Core.ViewModels;
//using ServiceLocator.Droid.Fragments;


//namespace ServiceLocator.Droid.Views
//{
//    public class FriendsClientView:BaseView<FriendsClientViewModel>
//    {
//        protected override int LayoutResource => Resource.Layout.friends_client_view;
//        private TabLayout _tabs;
//        private ViewPager _viewPager;
//        protected override void OnCreate(Bundle bundle)
//        {
//            base.OnCreate(bundle);

//            _viewPager = FindViewById<BindableViewPager>(Resource.Id.viewPager);
//            _viewPager.OffscreenPageLimit = 2;
//            _tabs = FindViewById<TabLayout>(Resource.Id.tabs);
//            _tabs.TabMode = TabLayout.ModeScrollable;
//            var addButton = FindViewById<FloatingActionButton>(Resource.Id.fab);

//            _tabs.SetupWithViewPager(_viewPager);

//            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
//            SetSupportActionBar(toolbar);
//            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

//            //var set = this.CreateBindingSet<FriendsClientView, FriendsClientViewModel>();
//            //set.Bind(SupportActionBar).For(ctb => ctb.Title).To(vm => vm.FullName);
//            //set.Bind(addButton).For(ctb => ctb.Visibility).To(vm => vm.IsMy).WithConversion("MyVisibility");
//            //this.CreateBinding().For("Title").To<FriendsClientViewModel>(vm => vm.FullName).Apply();
//            //set.Apply();
//        }

        

//        public override void OnBackPressed()
//        {
//           // ViewModel.CloseCommand.Execute();
//        }

//        protected override void OnResume()
//        {
//            base.OnResume();
//           // ViewModel.UpdateData();
//        }

//        public override bool OnOptionsItemSelected(IMenuItem item)
//        {
//            switch (item.ItemId)
//            {
//                case Android.Resource.Id.Home:

//                    try
//                    {
//                        ViewModel.CloseThisVMCommand.Execute();
//                    }
//                    catch (Exception ex)
//                    {
//                        var m = ex.Message;
//                    }
//                    break;
//            }

//            return base.OnOptionsItemSelected(item);
//        }
//    }
//    public class FriendsMastersAdapter : FragmentPagerAdapter
//    {
//        private static readonly string[] Content =
//        {
//            "Желанные", "Нежеланные"
//        };

//        public FriendsMastersAdapter(FragmentManager p0)
//            : base(p0)
//        {
//        }

//        public override int Count
//        {
//            get { return Content.Length; }
//        }

//        public override Fragment GetItem(int index)
//        {
//            switch (index)
//            {
//                case 0:
//                    return new FriendsAllFragment();
//                case 1:
//                    return new FriendsMasterFragment();
//            }

//            return new FriendsAllFragment();
//        }

//        public override ICharSequence GetPageTitleFormatted(int p0)
//        {
//            return new String(Content[p0 % Content.Length].ToUpper());
//        }

//    }
//}