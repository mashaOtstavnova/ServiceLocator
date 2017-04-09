using MvvmCross.Core.ViewModels;

namespace ServiceLocator.Core.ViewModels
{
    public class LoginVKViewModel : BaseViewModel
    {
        //private readonly INotificationService _notificationService;
        //private readonly Repository _repository;
        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged(() => Login);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public IMvxCommand ShowMainPageCommand
        {
            get { return new MvxCommand(ShowMainPage); }
        }

        private void ShowMainPage()
        {
            ShowViewModel<TypeUserViewModel>();
            
        }
    }
}