using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ServiceLocator.Core.ViewModels;

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class TypeUserView : BaseView<TypeUserViewModel>
    {
        private Button _buttonForMaster;
        private Button _buttonForClient;
        protected override int LayoutResource => Resource.Layout.typeUserView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            //bigIcon = (ImageView)FindViewById(Resource.Id.bigicon);
            //background = (ImageView)FindViewById(Resource.Id.background);
            _buttonForMaster = (Button) FindViewById(Resource.Id.type_user_master);
            _buttonForClient = (Button) FindViewById(Resource.Id.type_user_client);

            //Animation anim = AnimationUtils.LoadAnimation(ApplicationContext,
            //              Resource.Animation.fade_in);
            //button.StartAnimation(anim);

            _buttonForMaster.Click += OnButtonForMasterClic;
            _buttonForClient.Click += OnButtonForClientClick;
        }

        private void OnButtonForClientClick(object sender, EventArgs e)
        {
            ViewModel.SetTypeClientClickCommand.Execute();
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    Animation anim = AnimationUtils.LoadAnimation(ApplicationContext,
        //                 Resource.Animation.fade_in);
        //    button.StartAnimation(anim);
        //}
        private void OnButtonForMasterClic(object sender, EventArgs eventArgs)
        {
            ViewModel.SetTypeMasterClickCommand.Execute();
        }
    }
}