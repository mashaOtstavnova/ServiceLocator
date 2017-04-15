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
using ServiceLocator.Core.ViewModels;

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class FirstMasterView : BaseView<FirstMasterViewModel>
    {
        protected override int LayoutResource => Resource.Layout.first_master_view;
    }
}