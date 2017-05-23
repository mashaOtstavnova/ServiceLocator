using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Entities;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;

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
            _dataLoader = Mvx.Resolve<IDataLoaderService>();
            if (CurrentUser != null)
            { 
                friends = (await _profileService.GetFriends()).items;
                _clientsIds = new List<int>();
                _clientsIds.Add(CurrentUser.id);
                ReloadRecord();
            }
            //_progressLoaderService.HideProgressBar();
        }

        //private void OpenPresentPage(Present present)
        //{
        //    ShowViewModel<PresentViewModel>(new { presentId = present.Id.ToString() });
        //}

        public void ShowInfo()
        {
            ShowViewModel<InfoViewModel>();
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
            ShowViewModel<NewRecordClientViewModel>(new { masterId = -1, recordId ="" });
        }

        public void ShowBrowse()
        {
            //ShowViewModel<PresentsBrowseViewModel>();
        }
        public void ShowSchedule()
        {
            ShowViewModel<ScheduleViewModel>(new { idMasters = -1});
        }
        public void ShowLogin()
        {

            ShowViewModel<LoginVKViewModel>();
        }
        private IDataLoaderService _dataLoader;
        private IProgressLoaderService _progressLoaderService;
        private List<Friend> _friends;
        private List<int> _clientsIds;
        private ObservableCollection<RecordItem> _records;
        private ScheduleType _scheduleType;
        private string _typeData;
        private List<RecordItem> _selectedItems;
        private bool _isMaster;

        


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
