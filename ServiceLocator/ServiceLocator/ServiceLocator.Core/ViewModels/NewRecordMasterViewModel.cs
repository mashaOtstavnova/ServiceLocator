using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Entities;

namespace ServiceLocator.Core.ViewModels
{
    public class NewRecordMasterViewModel:BaseViewModel
    {

        public int _houre = -1;
        public int _minute= -1;
        public string _dateString= "";
        public DateTime _date= new DateTime();
        private int _duration;
        private decimal _money;
        private string _service;
        private Client _client;

        public int Minute
        {
            get => _minute;
            set
            {
                _minute = value;
                RaisePropertyChanged(() => Minute);
                RaisePropertyChanged(() => TimeString);
            }
        }
        public decimal Money
        {
            get => _money;
            set
            {
                _money = value;
                RaisePropertyChanged(() => Money);
            }
        }
        public int Hour
        {
            get => _houre;
            set
            {
                _houre = value;
                RaisePropertyChanged(() => Hour);
                RaisePropertyChanged(() => TimeString);
            }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                DateString = _date.ToString("dd/MM/yyyy");
                RaisePropertyChanged(() => Date);
                RaisePropertyChanged(() => DateString);
            }
        }
        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                RaisePropertyChanged(() => Duration);
            }
        }
        public Client Client
        {
            get => _client;
            set
            {
                _client = value;
                RaisePropertyChanged(() => Client);
            }
        }
        public string Service
        {
            get => _service;
            set
            {
                _service = value;
                RaisePropertyChanged(() => Service);
            }
        }
        public string TimeString
        {
            get
            {
                return (Hour != -1 & Minute != -1)
                    ? (Hour.ToString() + ":" + Minute.ToString())
                    : "Выбирите время";
            }
        }
        public string DateString
        {
            get
            {
                return (_dateString!="")
                    ? (_dateString)
                    : "Выбирите дату";
            }
            set
            {
                _dateString = value;
                RaisePropertyChanged(() => DateString);
            }
        }
        //public async void Init()
        ////{
        ////    IProfileService profileService;
        ////    Mvx.TryResolve(out profileService);

        ////    var users = await profileService.GetFriends();

        ////    var s =
        ////        users.items.Select(
        ////                friend => new ListItem(friend.first_name + " " + friend.last_name, friend.photo_50, friend.id))
        ////            .ToList();
        ////    Friends = new List<ListItem>(s);
        //}

        private IDataLoaderService _dataLoaderService;
        public void Init(int clientId)
        {
            _dataLoaderService = Mvx.Resolve<IDataLoaderService>();
            Client = _dataLoaderService.GetClient(clientId);
        }

        public List<ListItem> Friends { get; set; }
        public MvxCommand<Record> AddNewRecordCommand
        {
            get { return new MvxCommand<Record>(AddNewRecord); }
        }
        private void AddNewRecord(Record record)
        {
            
            //_dataLoader.AddNeweRecord(record);
        }
    }
}
