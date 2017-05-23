using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Entities;

namespace ServiceLocator.Core.ViewModels
{
    public class ClientsRecordsViewModel : BaseViewModel
    {

        private IDataLoaderService _dataLoader;
        private IProgressLoaderService _progressLoaderService;
        private IProfileService _profileService;
        private List<Friend> _friends;
        private List<int> _clientsIds;
        private ObservableCollection<RecordItem> _records;
        private ScheduleType _scheduleType;
        private string _typeData;
        private List<RecordItem> _selectedItems;
        private bool _isMaster;
        private User _currentUser;

        public ClientsRecordsViewModel()
        {

            ScheduleType = ScheduleType.Busy;
        }


        public ScheduleType ScheduleType
        {
            get => _scheduleType;
            set
            {
                _scheduleType = value;

                RaisePropertyChanged(() => ScheduleType);

            }
        }

        public ObservableCollection<RecordItem> RecordItems
        {
            get => _records;
            set
            {
                _records = value;
                RaisePropertyChanged(() => RecordItems);
            }
        }




        public async void ReloadRecord()
        {
            var data = _dataLoader.GetRecordsClients(_clientsIds).ToList();
            data = data.OrderBy(x => x.Time).ToList();
            var recordItems = new ObservableCollection<RecordItem>();
            var list = new List<Record>();

            foreach (var item in data)
            {
                var recordItem = new RecordItem();
                recordItem.Service = item.Service;
                var master = await _profileService.GetUserById(item.IdMaster);
                recordItem.NameMaster = $"{master.first_name} {master.last_name}";
                recordItem.PhotoMaster = master.photo_100;
                recordItem.IsBusy = item.IsBusy;
                recordItem.Id = item.Id;
                recordItem.Time = item.Time.Date.ToString();
                recordItems.Add(recordItem);
            }
            RecordItems = new ObservableCollection<RecordItem>(recordItems);
            
        }
        public IMvxCommand OnItemSelectCommand
        {
            get { return new MvxCommand<RecordItem>(OnItemSelect); }
        }

        private void OnItemSelect(RecordItem obj)
        {
            if (!obj.IsBusy)
            {
                ShowViewModel<RecordViewModel>(new { idRecord = obj.Id.ToString() });
            }
            else
            {
               
                    ShowViewModel<NewRecordClientViewModel>(new { masterId = -1, recordId = obj.Id.ToString() });
               
            }
        }

        public List<RecordItem> SelectedItems
        {
            get
            {
                if (_selectedItems == null)
                    _selectedItems = new List<RecordItem>();
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged(() => SelectedItems);
            }
        }
        public async Task Init(int idClients)
        {
            _clientsIds = new List<int>();
            _clientsIds.Add(idClients);
            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            _progressLoaderService.ShowProgressBar();
            _dataLoader = Mvx.Resolve<IDataLoaderService>();
            _profileService = Mvx.Resolve<IProfileService>();
            CurrentUser = await _profileService.GetUser();
            RecordItems = new ObservableCollection<RecordItem>();
            ReloadRecord();
            _progressLoaderService.HideProgressBar();
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                if (_currentUser != null)
                {
                    var typeUser = _dataLoader.GetType(_currentUser.id);
                    if (typeUser == "Master")
                    {
                        IsMaster = true;
                    }
                    else if (typeUser == "Client")
                    {
                        IsMaster = false;
                    }

                }
                RaisePropertyChanged(() => CurrentUser);
            }
        }

        public bool IsMaster
        {

            get => _isMaster;
            set
            {
                _isMaster = value;
                RaisePropertyChanged(() => IsMaster);
            }
        }
    }
}
