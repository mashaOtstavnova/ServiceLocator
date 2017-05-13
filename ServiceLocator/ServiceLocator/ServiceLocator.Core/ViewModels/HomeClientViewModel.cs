﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Entities;

namespace ServiceLocator.Core.ViewModels
{
    public class HomeClientViewModel : BaseViewModel
    {
        private readonly IDataLoaderService _loaderService;

        private readonly IProfileService _profileService;

        // private ObservableCollection<Present> _allWantedPresents;
        private User _currentUser;
        //private readonly IProgressLoaderService _progressLoaderService;

        public HomeClientViewModel()
        {
            _profileService = Mvx.Resolve<IProfileService>();
            _loaderService = Mvx.Resolve<IDataLoaderService>();
            //_progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
        }

        //public ObservableCollection<Present> AllWantedPresents
        //{
        //    get { return _allWantedPresents; }
        //    set
        //    {
        //        _allWantedPresents = value;
        //        RaisePropertyChanged(() => AllWantedPresents);
        //    }
        //}

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }

        //public MvxCommand<Present> ItemClickCommand
        //{
        //    get { return new MvxCommand<Present>(OpenPresentPage); }
        //}

        public async Task Initialize()
        {
            //_progressLoaderService.ShowProgressBar();
            var friends = new List<Friend>();
            CurrentUser = await _profileService.GetUser();

            if (CurrentUser != null)
                friends = (await _profileService.GetFriends()).items;
            //_progressLoaderService.HideProgressBar();
        }

        //private void OpenPresentPage(Present present)
        //{
        //    ShowViewModel<PresentViewModel>(new { presentId = present.Id.ToString() });
        //}

        public void ShowInfo()
        {
            //ShowViewModel<InfoViewModel>();
        }

        public void ShowMainPage()
        {
            ShowViewModel<HomeClientViewModel>();
        }

        public void ShowFriends()
        {
            ShowViewModel<FriendsViewModel>();
        }

        public void ShowProfile()
        {
            ShowViewModel<ClientViewModel>(new { idFriend = CurrentUser.id });
        }
        public void ShowNewRecord()
        {
            ShowViewModel<NewRecordClientViewModel>();
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