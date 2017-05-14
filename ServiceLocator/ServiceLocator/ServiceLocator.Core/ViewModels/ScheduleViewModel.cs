using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ServiceLocator.Core.IServices;
using ServiceLocator.Entities;

namespace ServiceLocator.Core.ViewModels
{
    public class ScheduleViewModel : BaseViewModel
    {
        private readonly IDataLoaderService _dataLoader;
        private readonly IProfileService _profileService;
        private List<Friend> _friends;
        private List<int> _masterIds;
        private ObservableCollection<RecordItem> _records;
        private ScheduleType _scheduleType;

        public ScheduleViewModel(IDataLoaderService dataLoaderService, IProfileService profileService)
        {
            _profileService = profileService;
            _dataLoader = dataLoaderService;
            ScheduleType = ScheduleType.Free;
            RecordItems = new ObservableCollection<RecordItem>();

            //TODO: make logic from NavDrawer or Screen for one master
            //if (если для одного и в др. сл. для всех)
            LoadData();
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
        

        private async void LoadData()
        {
            await _profileService.GetFriends()
                .ContinueWith(task =>
                {
                    _friends = task.Result.items;
                    _masterIds = _dataLoader.GetMastersByFriends(_friends.Select(f => f.id).ToList());
                    ReloadRecord(DateTime.Today);
                });
        }

        public async void ReloadRecord(DateTime date)
        {
            var data = _dataLoader.GetRecords(date, _masterIds).ToList();
            data = data.OrderBy(x => x.Time).ToList();
            var recordItems = new ObservableCollection<RecordItem>();
            var list = new List<Record>();

            switch (ScheduleType)
            {
                case ScheduleType.Free:

                    data = new List<Record>(data.Where(r => !r.IsBusy).ToList());
                    foreach (var item in data)
                    {
                        var recordItem = new RecordItem();
                        recordItem.Service = item.Service;
                        var master = await _profileService.GetUserById(item.IdMaster);
                        recordItem.NameMaster = $"{master.first_name} {master.last_name}";
                        recordItem.PhotoMaster = master.photo_100;
                        recordItem.Time = item.Time.Date.ToString("hh:mm");
                        recordItems.Add(recordItem);
                    }
                    RecordItems = new ObservableCollection<RecordItem>(recordItems);

                    break;
                case ScheduleType.Busy:
                    RecordItems = new ObservableCollection<RecordItem>();
                    data = new List<Record>(data.Where(r => r.IsBusy).ToList());
                    foreach (var item in data)
                    {
                        var recordItem = new RecordItem();
                        recordItem.Service = item.Service;
                        var master = await _profileService.GetUserById(item.IdMaster);
                        recordItem.NameMaster = $"{master.first_name} {master.last_name}";
                        recordItem.PhotoMaster = master.photo_100;
                        recordItem.Time = item.Time.Date.ToString("hh:mm");
                        recordItems.Add(recordItem);
                    }
                    RecordItems = new ObservableCollection<RecordItem>(recordItems);
                    break;
            }
        }
    }

    public class RecordItem
    {
        public string PhotoMaster { get; set; }
        public string NameMaster { get; set; }
        public string Time { get; set; }
        public string Service { get; set; }
    }
}