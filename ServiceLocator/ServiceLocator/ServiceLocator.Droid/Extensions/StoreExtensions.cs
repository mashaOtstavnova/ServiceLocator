using Android.App;
using Android.Content;

namespace ServiceLocator.Droid.Extensions
{
    public static class StoreExtensions
    {
        public static void SetVkUserAuthorized(string packpageName)
        {
            var prefs = Application.Context.GetSharedPreferences(packpageName, FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutBoolean("isVkUserAuthorized", true);
            prefEditor.Commit();
        }

        public static bool IsVkUserAuthorized(string packpageName)
        {
            var preferences = Application.Context.GetSharedPreferences(packpageName, FileCreationMode.Private);
            var value = preferences.GetBoolean("isVkUserAuthorized", false);
            return value;
        }
    }
}