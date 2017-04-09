using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace MyServiceLocator.Core.ViewModels
{
    public class LoginVKViewModel : BaseViewModel
    {
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
        public IMvxCommand ShowTypeUserCommand
        {
            get { return new MvxCommand(ShowTypeUser); }
        }


        private void ShowTypeUser()
        {
            ShowViewModel<TypeUserViewModel>();

        }
        public IMvxCommand ShowMainPageCommand
        {
            get { return new MvxCommand(ShowMainPage); }
        }

        private void ShowMainPage()
        {
            ShowViewModel<TypeUserViewModel>();
            //if (_repository.Users.Any(human => human.Login == Login && human.Password == Password))
            //{
            //    var user =
            //        FakeData.Users.FirstOrDefault(human => human.Login == Login && human.Password == Password);
            //    _repository.CurrentUser = user;
            //    switch (user.Type)
            //    {
            //        case UserType.Administrator:
            //            ShowViewModel<TeachersViewModel>();
            //            break;
            //        case UserType.Teacher:
            //            ShowViewModel<ScheduleViewModel>();
            //            break;
            //        case UserType.Student:
            //            ShowViewModel<ScheduleViewModel>();
            //            break;
            //    }
            //}
            //else
            //{
            //    _notificationService.ShowNotification("Ошибка авторизации",
            //        "Пользователя с такими данными не существует в базе!");
            //}
        }
    }
}
