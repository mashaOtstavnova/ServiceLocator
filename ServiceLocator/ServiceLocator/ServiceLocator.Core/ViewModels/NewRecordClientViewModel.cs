using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;
using ServiceLocator.Entities;

namespace ServiceLocator.Core.ViewModels
{
    public class NewRecordClientViewModel:BaseViewModel
    {
        private int _idMaster;
        
        private string _dateString = "";
        private DateTime _date = new DateTime();
        private string _timeString="";

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
                var t = value.Split(':');
                Hour = t[0];
                Minute = t[1];
                _timeString = value;
                RaisePropertyChanged(() => TimeString);
            }
        }
        public string DateString
        {
            get
            {
                return (_dateString != "")
                    ? (_dateString)
                    : "Выбирите дату";
            }
            set
            {
                _dateString = value;
                RaisePropertyChanged(() => DateString);
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
        public int IdMaster
        {
            get => _idMaster;
            set
            {
                _idMaster = value;
                RaisePropertyChanged(() => IdMaster);
            }
        }
        public User Master
        {
            get => _master;
            set
            {
                Photo = $"{value.photo_100}";
                NameMaster = $"{value.first_name} {value.last_name}";
                _master = value;
                RaisePropertyChanged(() => Master);
            }
        }

        public string NameMaster
        {
            get => _nameMaster;
            set
            {
                _nameMaster = value;
                RaisePropertyChanged(() => NameMaster);
            }
        }

        private IDataLoaderService _dataLoaderService;
        private User _master;
        private Record _record;
        private string _service;
        private int _duration;
        private IProfileService _profileService;
        private decimal _money;
        public string _houre = "-1";
        public string _minute = "-1";
        private string _photo;
        private string _nameMaster;

        private  IProgressLoaderService _progressLoaderService;
        public async void Init(int masterId, string recordId)
        {

            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            _progressLoaderService.ShowProgressBar();
            _dataLoaderService = Mvx.Resolve<IDataLoaderService>();

            IdMaster = masterId;
            _profileService = Mvx.Resolve<IProfileService>();
            if (!string.IsNullOrWhiteSpace(recordId))
            {
                Record = _dataLoaderService.GetRecord(Guid.Parse(recordId));
                Master = await _profileService.GetUserById(Record.IdMaster);
            }
            else if (masterId != -1)
            {
                Master = await _profileService.GetUserById(masterId);

                // Client = _dataLoaderService.GetClient(clientId);
            }
            var ord = recordId;
            _progressLoaderService.HideProgressBar();
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
        public Record Record
        {
            get { return _record; }
            set
            {
                _record = value;
                if (value != null)
                {
                    IdMaster = value.IdMaster;
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
        public string Service
        {
            get => _service;
            set
            {
                _service = value;
                RaisePropertyChanged(() => Service);
            }
        }
        public string Photo
        {
            get {
                if(string.IsNullOrWhiteSpace(_photo))
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

    }

    
}
