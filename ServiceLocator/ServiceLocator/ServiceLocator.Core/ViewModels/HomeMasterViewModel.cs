using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ServiceLocator.Entities;
using ServiceLocator.Core.IServices;

namespace ServiceLocator.Core.ViewModels
{
    public class HomeMasterViewModel : BaseViewModel
    {
        private readonly IDataLoaderService _loaderService;
        private readonly IProfileService _profileService;
        private User _currentUser;

        public HomeMasterViewModel()
        {
            _profileService = Mvx.Resolve<IProfileService>();
            _loaderService = Mvx.Resolve<IDataLoaderService>();
        }



        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }



        public async Task Initialize()
        {
            //var friends = new List<Friend>();
            //CurrentUser = await _profileService.GetUser();

            //if (CurrentUser != null)
            //{
            //    friends = (await _profileService.GetFriends()).items;


            //}
        }
        public void ShowInfo()
        {
            //ShowViewModel<InfoViewModel>();
        }

        public void ShowMainPage()
        {
            //ShowViewModel<HomeViewModel>();
        }

        public void ShowFriends()
        {
            //ShowViewModel<FriendsViewModel>();
        }

        public void ShowProfile()
        {
            //ShowViewModel<UserViewModel>(new { idFriend = CurrentUser.id });
        }

        public void ShowBrowse()
        {
            //ShowViewModel<PresentsBrowseViewModel>();
        }

        public void ShowLogin()
        {
            //ShowViewModel<FirstViewModel>();
        }
    }
}
