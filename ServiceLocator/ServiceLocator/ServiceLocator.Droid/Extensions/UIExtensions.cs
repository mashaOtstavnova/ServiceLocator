using Android.App;
using Android.Graphics;

namespace ServiceLocator.Droid.Extensions
{
    public static class TypefaceExtensions
    {
        public static Typeface SonsieOneRegular = Typeface.CreateFromAsset(Application.Context.Assets,
            "Fonts/SonsieOne-Regular.ttf");
    }
}