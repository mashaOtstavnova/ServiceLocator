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
    public class MoneyViewModel:BaseViewModel
    {
        private string _startDate;
        private string _endDate;
        private List<ListMoney> _list;
        private User _user;

        public List<ListMoney> Items
        {
            set
            {
                _list = value;
                RaisePropertyChanged(() => Items);
            }
            get
            {
                //он на каждый гет булет вызывать этот метод, ткак как  гет даже при пролистывании листвью ьбудет вызываться
                // InitializeMyList();
                return _list;
            }
        }
        public string StartDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_startDate))
                {
                    return "От";
                }
                else
                {
                    return _startDate;
                }
            }

            set
            {
                _startDate = value;
                if(string.IsNullOrWhiteSpace(value))
                {
                    _startDate = "От";
                }
                else
                {
                    _startDate = value;
                }
                RaisePropertyChanged(() => StartDate);
            }
        }
  public string All
        {
            get
            {
                
                    return _allMoney;
            }

            set
            {
                _allMoney = value;
                RaisePropertyChanged(() => All);
            }
        }

        IDataLoaderService _dataLoaderService;
        private string _allMoney;

        public async Task Init()
        {
            IProfileService profileService;
            Mvx.TryResolve(out profileService);

            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            Mvx.TryResolve(out _dataLoaderService);
            _user = await profileService.GetUser();
            ListFriends = await profileService.GetFriends();

        }

        public Friends ListFriends { get; set; }

        public MvxCommand GetMoneyClickCommand
        {
            get { return new MvxCommand(GetMoneyClick); }
        }

        private  IProgressLoaderService _progressLoaderService;
        private void GetMoneyClick()
        {
            _progressLoaderService.ShowProgressBar();
            var records = _dataLoaderService.GetMoneyMaster(StartDate, EndDate, _user.id);
            var list = new List<ListMoney>();
            decimal money = 0;
            foreach (var record in records)
            {
                string photoClient;
                if (record.IdClient==_user.id)
                {
                    photoClient = _user.photo_100;
                }
                else if (ListFriends.items.Where(t => t.id == record.IdClient).FirstOrDefault()!=null)
                {
                    photoClient
                        = ListFriends.items.Where(t => t.id == record.IdClient).FirstOrDefault().photo_100;
                }
                else
                {
                    photoClient = "http://guiaexcelenciascuba.com/Images/Utils/icon-user.png";
                }
                money += record.Money;
                list.Add(new ListMoney(record.Money.ToString(), photoClient,record.Service));
            }
            All = money.ToString();
            Items = new List<ListMoney>(list);
            _progressLoaderService.HideProgressBar();
        }
        public string EndDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_endDate))
                {
                    return "До";
                }
                else
                {
                    return _endDate;
                }
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _endDate = "До";
                }
                else
                {
                    _endDate = value;
                }
                RaisePropertyChanged(() => EndDate);
            }
        }
    }

    public class ListMoney
    {

        public ListMoney(string money, string photoClient, string service)
        {
            Money = money;
            PhotoClient = photoClient;
            Service = service;
        }

        public string Money { get; set; }
        public string Service { get; set; }
        public string PhotoClient { get; set; }
    }
}
