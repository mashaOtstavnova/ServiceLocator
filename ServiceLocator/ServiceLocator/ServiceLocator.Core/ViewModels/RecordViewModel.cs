using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Entities;

namespace ServiceLocator.Core.ViewModels
{
    public class RecordViewModel:BaseViewModel
    {
        private IDataLoaderService _dataLoaderService;
        private Friend _master;
        private Friend _client;
        private IProfileService _profileService;
        private Record _record;
        private string _photo;
        private string _services;
        private string _nameMaster;
        private string _nameClient;
        private string _time;
        private int _idClient;
        private int _idMaster;
        private User _currentUser;
        private User _masterUser;
        private User _clientUser;
        private bool _isMy;
        private bool _isMaster;
        private bool _isClient;


        public Record Record
        {
            get { return _record; }
            set
            {
                _record = value;
                if (value != null)
                {
                    IdClient = value.IdClient;
                    IdMaster = value.IdMaster;
                    Time = $"{value.Time.Date}";
                    Services = $"{value.Service}";
                }
                RaisePropertyChanged(() => Master);
            }
        }
        public string Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                RaisePropertyChanged(() => Photo);
            }
        }
        public string Services
        {
            get { return _services; }
            set
            {
                _services = value;
                RaisePropertyChanged(() => Services);
            }
        }

        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged(() => Time);
            }
        }

        public Friend Master
        {
            get { return _master; }
            set
            {
                _master = value;
                if (value != null)
                {
                    NameMaster = $"{value.first_name} {value.last_name}";
                    Photo = $"{value.photo_max_orig}";
                }
                RaisePropertyChanged(() => Master);
            }
        }
        public User MasterUser
        {
            get { return _masterUser; }
            set
            {
                _masterUser = value;
                if (value != null)
                {
                    NameMaster = $"{value.first_name} {value.last_name}";
                    Photo = $"{value.photo_max_orig}";
                    if (value.id == CurrentUser.id)
                    {
                        IsMy = true;
                        IsMaster = true;
                    }
                }
                RaisePropertyChanged(() => MasterUser);
            }
        }
        public bool IsMy
        {
            get { return _isMy; }
            set
            {
                _isMy = value;
                RaisePropertyChanged(() => IsMy);
            }
        }
        public bool IsMaster
        {
            get { return _isMaster; }
            set
            {
                _isMaster = value;
                RaisePropertyChanged(() => IsMaster);
            }
        }
        public string NameMaster
        {
            get { return _nameMaster; }
            set
            {
                _nameMaster = value;
                RaisePropertyChanged(() => NameMaster);
            }
        }

        public Friend Client
        {
            get { return _client; }
            set
            {
                _client = value;
                if (value != null)
                {
                    NameClient = $"{value.first_name} {value.last_name}";
                    
                }
                RaisePropertyChanged(() => Client);
            }
        }
        public User ClientUser
        {
            get { return _clientUser; }
            set
            {
                _clientUser = value;
                if (value != null)
                {
                    NameClient = $"{value.first_name} {value.last_name}";
                    if (value.id == CurrentUser.id)
                    {
                        IsMy = true;
                        IsClient = true;
                    }
                }
                RaisePropertyChanged(() => ClientUser);
            }
        }
        public string NameClient
        {
            get { return _nameClient; }
            set
            {
                _nameClient = value;
                RaisePropertyChanged(() => NameClient);
            }
        }
        public int IdClient
        {
            get { return _idClient; }
            set
            {
                _idClient = value;
                RaisePropertyChanged(() => IdClient);
            }
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
        public int IdMaster
        {
            get { return _idMaster; }
            set
            {
                _idMaster = value;
                RaisePropertyChanged(() => IdMaster);
            }
        }
        public bool IsClient
        {
            get { return _isClient; }
            set
            {
                _isClient = value;
                RaisePropertyChanged(() => IsClient);
            }
        }
        public IMvxCommand DellRecordCommand
        {
            get { return new MvxCommand(DellRecord); }
        }
       
        private void DellRecord()
        {
           _dataLoaderService.DellRecord(Record.Id);
        }
        public IMvxCommand UpdateRecordCommand
        {
            get { return new MvxCommand(UpdateRecord); }
        }
       
        private void UpdateRecord()
        {
            if (IsMaster)
            {
                ShowViewModel<NewRecordClientViewModel>(new { masterId =-1, recordId = 01 });
            }
            else if (IsClient)
            {
                ShowViewModel<NewRecordMasterViewModel>(new { clientId = -1, recordId = 01 });
            }
        }

        public async void Init(int idRecord)
        {
           
            _dataLoaderService = Mvx.Resolve<IDataLoaderService>();
            _profileService = Mvx.Resolve<IProfileService>();
            try
            {
                Record = _dataLoaderService.GetRecord(idRecord);
                CurrentUser = await _profileService.GetUser();
                MasterUser = await _profileService.GetUserById(IdMaster);
                ClientUser = await _profileService.GetUserById(IdClient);
                //if (CurrentUser != null & CurrentUser.id == IdMaster)
                //{
                //    MasterUser = await Mvx.Resolve<IProfileService>().GetUser();
                //    Client = await Mvx.Resolve<IProfileService>().GetFriend(IdClient);
                //}
                //else if (CurrentUser.id == IdClient)
                //{
                //    ClientUser = await Mvx.Resolve<IProfileService>().GetUser();
                //    Master = await Mvx.Resolve<IProfileService>().GetFriend(IdMaster);
                //}
            }
            catch (Exception ex)
            {
                
            }


        }
    }
}
