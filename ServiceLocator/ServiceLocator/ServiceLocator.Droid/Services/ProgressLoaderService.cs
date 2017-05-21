using Android.App;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using ServiceLocator.Core.IServices;

namespace ServiceLocator.Droid.Services
{
    public class ProgressLoaderService : IProgressLoaderService
    {
        private ProgressDialog _progressBar;

        public void ShowProgressBar()
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            activity.RunOnUiThread(
                () =>
                {
                    _progressBar = ProgressDialog.Show(activity, "Загрузка данных..", "Пожалуйста, подождите...", false);
                });
        }

        public void HideProgressBar()
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            activity.RunOnUiThread(
                () => { _progressBar.Hide(); });
        }
    }
}