using System.Globalization;
using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Visibility;
namespace ServiceLocator.Droid.helpers
{
    public class MyVisibilityValueConverter : MvxBaseVisibilityValueConverter<bool>
    {
        protected override MvxVisibility Convert(bool value, object parameter, CultureInfo culture)
        {
            return value ? MvxVisibility.Visible : MvxVisibility.Collapsed;
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