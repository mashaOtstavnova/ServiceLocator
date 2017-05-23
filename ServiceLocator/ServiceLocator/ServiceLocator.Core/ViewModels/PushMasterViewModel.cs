using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using ServiceLocator.Core.IServices;
using ServiceLocator.Entities;

namespace ServiceLocator.Core.ViewModels
{
    public class PushMasterViewModel: BaseViewModel
    {
        private User _client;

        private string _currentTextHint;
        private IDataLoaderService _dataLoaderService;
       
        private int _idClient;
        private List<FriendItem> _items;
       
        public string _nameClient = "";
        private string _photo;
        private IProfileService _profileService;
        private string _searchTerm;
        private object _selectedObject;
        private string _titelPush;

        public string Houre = "-1";

        public PushMasterViewModel()
        {
            Items = new List<FriendItem>();
        }


     
        public User Client
        {
            get => _client;
            set
            {
                if(value!=null)
                {
                    _client = value;
                    Photo = $"{value.photo_100}";
                    NameClient = $"{value.first_name} {value.last_name}";
                    
                }
                else
                {
                    _client = null;
                    Photo = $"";
                    NameClient = $"";
                }
              RaisePropertyChanged(() => Client);
            }
        }

        public string Discription
        {
            get => _discription;
            set
            {
                _discription = value;
                RaisePropertyChanged(() => Discription);
            }
        }
        public string TitlePush
        {
            get => _titelPush;
            set
            {
                _titelPush = value;
                RaisePropertyChanged(() => TitlePush);
            }
        }
        public string NameClient
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_nameClient))
                {
                    return "Клиенты";
                }
                else
                {
                    return _nameClient;
                }
            }
            set
            {
                _nameClient = value;
                RaisePropertyChanged(() => NameClient);
            }
        }



        public string Photo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_photo))
                {
                    return "http://guiaexcelenciascuba.com/Images/Utils/icon-user.png";
                }
                else
                {
                    return _photo;

                }
            }
            set
            {
                _photo = value;
                RaisePropertyChanged(() => Photo);
            }
        }

       

        public int IdClient
        {
            get => _idClient;
            set
            {
                _idClient = value;
                //if (value != null)
                //{
                //    IdClient = value;
                //}
                RaisePropertyChanged(() => IdClient);
            }
        }

        public List<ListItem> Friends { get; set; }

        public MvxCommand PushCommand => new MvxCommand(Push);

        public List<FriendItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        private bool _all;
        public bool All
        {
            get => _all;
            set
            {
                _all = value;
                if(value)
                {
                    Photo = "http://guiaexcelenciascuba.com/Images/Utils/icon-user.png";
                    NameClient = "Клиенты";
                }
                RaisePropertyChanged(() => All);
            }
        }

        private IProgressLoaderService _progressLoaderService;
        private string _discription;

        public async void Init()
        {

            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            _progressLoaderService.ShowProgressBar();
            _dataLoaderService = Mvx.Resolve<IDataLoaderService>();
            _profileService = Mvx.Resolve<IProfileService>();
            _all = true;
            var friends = (await _profileService.GetFriends()).items;

            Items = friends.Select(
                    f => new FriendItem { Image = f.photo_100, Name = f.first_name, SurName = f.last_name, Id = f.id })
                .ToList();
            ;
            if (Items.Count > 0)
                Mvx.Resolve<IMvxMessenger>().Publish(new NeedSetAdapterMessage(this));
            _progressLoaderService.HideProgressBar();
        }

        private void Push()
        {
            //_dataLoader.AddNeweRecord(record);
        }
    }
}
