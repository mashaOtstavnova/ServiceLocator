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
    public class NewRecordMasterViewModel : BaseViewModel
    {
        private User _client;

        private string _currentTextHint;
        private IDataLoaderService _dataLoaderService;
        public DateTime _date;
        public string _dateString = "";
        private int _duration;
        private int _idClient;
        private List<FriendItem> _items;
        public string _minute = "-1";
        private decimal _money;
        public string _nameClient = "";
        private string _photo;
        private IProfileService _profileService;
        private Record _record;
        private string _searchTerm;
        private object _selectedObject;
        private string _service;
        private string _timeString;

        public string Houre = "-1";

        public NewRecordMasterViewModel()
        {
            Items = new List<FriendItem>();
        }

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
            get => Houre;
            set
            {
                Houre = value;
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
            get
            {
                if(string.IsNullOrWhiteSpace(_nameClient))
                {
                    return "Мастер";
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

        public string TimeString
        {
            get => (Hour != "-1") & (Minute != "-1")
                ? Hour + ":" + Minute
                : "Выбирите время";
            set
            {
                _timeString = value;
                RaisePropertyChanged(() => TimeString);
            }
        }

        public string DateString
        {
            get => _dateString != ""
                ? _dateString
                : "Выбирите дату";
            set
            {
                _dateString = value;
                RaisePropertyChanged(() => DateString);
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

        public Record Record
        {
            get => _record;
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

        public MvxCommand<Record> AddNewRecordCommand => new MvxCommand<Record>(AddNewRecord);

        public List<FriendItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        private IProgressLoaderService _progressLoaderService;
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
            }

            var friends = (await _profileService.GetFriends()).items;

            Items = friends.Select(
                    f => new FriendItem { Image = f.photo_100, Name = f.first_name, SurName = f.last_name,Id=f.id })
                .ToList();
            ;
            if (Items.Count > 0)
                Mvx.Resolve<IMvxMessenger>().Publish(new NeedSetAdapterMessage(this));
            _progressLoaderService.HideProgressBar();
        }

        private void AddNewRecord(Record record)
        {
            //_dataLoader.AddNeweRecord(record);
        }
    }

    public class FriendItem : INotifyPropertyChanged
    {
        private string _image;
        private string _name;
        private string _surName;
        private int _id;


        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string SurName
        {
            get => _surName;
            set
            {
                _surName = value;
                RaisePropertyChanged("SurName");
            }
        }

        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged("Image");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{Name} {SurName}";
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}