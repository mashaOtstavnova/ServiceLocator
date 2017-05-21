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

        public string _houre = "-1";
        public string _minute= "-1";
        public string _dateString= "";
        public string _nameClient= "";
        public DateTime _date= new DateTime();
        private int _duration;
        private decimal _money;
        private string _service;
        private User _client;

        public string Minute
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
        public string Hour
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
        public User Client
        {
            get => _client;
            set
            {
                _client = value;
                Photo = $"{value.photo_100}";
                NameClient = $"{value.first_name} {value.last_name}";
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
        public string NameClient
        {
            get => _nameClient;
            set
            {
                _nameClient = value;
                RaisePropertyChanged(() => NameClient);
            }
        }
        public string TimeString
        {
            get
            {
               
                    return (Hour != "-1" & Minute != "-1")
                        ? (Hour + ":" + Minute)
                        : "Выбирите время";
            }
            set
            {
                _timeString = value;
                RaisePropertyChanged(() => TimeString);
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
        private Record _record;
        private int _idClient;
        private IProfileService _profileService;
        private string _photo;
        private string _timeString;

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

        private  IProgressLoaderService _progressLoaderService;
        public async void Init(int clientId, string recordId)
        {

            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            _progressLoaderService.ShowProgressBar();
            _dataLoaderService = Mvx.Resolve<IDataLoaderService>();
            _profileService = Mvx.Resolve<IProfileService>();
            if (!string.IsNullOrWhiteSpace(recordId))
            {
                Record = _dataLoaderService.GetRecord(Guid.Parse(recordId));
                Client = await _profileService.GetUserById(Record.IdClient);
            }
            else if (clientId != -1)
            {
               Client = await _profileService.GetUserById(clientId);
                
                // Client = _dataLoaderService.GetClient(clientId);
            }
            _progressLoaderService.HideProgressBar();
        }

        public Record Record {
            get { return _record; }
            set
            {
                _record = value;
                if (value != null)
                {
                    IdClient = value.IdClient;
                    //IdMaster = value.IdMaster;
                    Duration = value.Duration.Minutes;
                    Money = value.Money;
                    Hour = value.Time.Date.ToString("hh");
                    Minute = value.Time.Date.ToString("mm");
                    Date = value.Time;
                    Service = $"{value.Service}";
                }
                RaisePropertyChanged(() => Record);
            }
        }

        public int IdClient
        {
            get { return _idClient; }
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
