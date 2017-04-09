using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using ServiceLocator.Core.ViewModels;
using ServiceLocator.Droid.Views;

namespace ServiceLocator.Droid
{
    [Activity]
    public class TypeUserView: BaseView<TypeUserViewModel>
    {
        Button button;
        protected override int LayoutResource => Resource.Layout.typeUserView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            //bigIcon = (ImageView)FindViewById(Resource.Id.bigicon);
            //background = (ImageView)FindViewById(Resource.Id.background);
            button = (Button)FindViewById(Resource.Id.type_user_master);

            //Animation anim = AnimationUtils.LoadAnimation(ApplicationContext,
            //              Resource.Animation.fade_in);
            //button.StartAnimation(anim);
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    Animation anim = AnimationUtils.LoadAnimation(ApplicationContext,
        //                 Resource.Animation.fade_in);
        //    button.StartAnimation(anim);
        //}
    }
}