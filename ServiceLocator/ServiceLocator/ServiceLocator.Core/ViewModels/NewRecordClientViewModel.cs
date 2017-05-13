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
                return (_timeString != "")
                    ? (_timeString)
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
        public Master Master
        {
            get => _master;
            set
            {
                _master = value;
                RaisePropertyChanged(() => Master);
            }
        }

        private IDataLoaderService _dataLoaderService;
        private Master _master;

        public void Init(int masterId, int recordId)
        {
            _dataLoaderService = Mvx.Resolve<IDataLoaderService>();
            IdMaster = masterId;
            var ord = recordId;
            Master = _dataLoaderService.GetMaster(masterId);
        }
    }
}
