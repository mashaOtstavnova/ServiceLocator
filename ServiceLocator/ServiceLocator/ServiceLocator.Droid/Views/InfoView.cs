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
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ServiceLocator.Droid.Views
{
    [Activity]
    public class InfoView : BaseView<InfoViewModel>
    {
        protected override int LayoutResource => Resource.Layout.info_view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ViewModel.Title = "О приложении";
            Title = ViewModel.Title;
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //if (string.IsNullOrWhiteSpace(image))
            //    image = friends[0].Image;

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);



            ViewModel.AppDiscription = "Мы помогаем упростить общение клиента с мастером!";
            FindViewById<TextView>(Resource.Id.app_description).Text = ViewModel.AppDiscription;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:

                    try
                    {
                        ViewModel.CloseThisVMCommand.Execute();
                        // NavUtils.NavigateUpFromSameTask(this);
                    }
                    catch (Exception ex)
                    {
                        var m = ex.Message;
                    }
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}