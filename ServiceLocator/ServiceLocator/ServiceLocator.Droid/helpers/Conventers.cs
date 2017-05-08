using System.Globalization;
using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Visibility;
using System;
using System.Globalization;
using Android.App;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Content.Res;
using MvvmCross.Platform.Converters;

namespace ServiceLocator.Droid.helpers
{
    public class MyVisibilityValueConverter : MvxBaseVisibilityValueConverter<bool>
    {
        protected override MvxVisibility Convert(bool value, object parameter, CultureInfo culture)
        {
            return value ? MvxVisibility.Visible : MvxVisibility.Collapsed;
        }
    }
    public class MyVisibilityFalseValueConverter : MvxBaseVisibilityValueConverter<bool>
    {
        protected override MvxVisibility Convert(bool value, object parameter, CultureInfo culture)
        {
            return value ?  MvxVisibility.Collapsed:MvxVisibility.Visible ;
        }
    }
    public class MySwapImageValueConverter : MvxValueConverter<bool, string>
    {

        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
                if (value)
                {
                    return "res:ic_done";
                }
                else
                {
                    return "res:ic_clear";
                }
         }
    }
    public class LinkToVisibilityValueConverter : MvxBaseVisibilityValueConverter<string>
    {
        protected override MvxVisibility Convert(string value, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrEmpty(value) ? MvxVisibility.Visible : MvxVisibility.Collapsed;
        }
    }
}