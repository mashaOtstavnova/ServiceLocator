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
    public class ClientViewModel : BaseViewModel
    {
        private string _bdate;
        private string _fullName;
        private string _photo;
        private string _mobile_phone;
        private bool _isPhone;
        private bool _isMy;
        private string _home_phone;
        private string _contactsString;
        private bool _isRegistration;
        private Friend _friend;
        private Client _client;
        public int UserId;
        private User _user;

        public bool IsMy
        {
            get { return _isMy; }
            set
            {
                _isMy = value;
                RaisePropertyChanged(() => IsMy);
            }
        }
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
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
        
        public string СontactsString
        {
            get { return _contactsString; }
            set
            {
                _contactsString = value;
               
                RaisePropertyChanged(() => СontactsString);
            }
        }
        public string Bdate
        {
            get { return _bdate; }
            set
            {
                _bdate = value;
                RaisePropertyChanged(() => Bdate);
            }
        }
        public string HomePhone
        {
            get { return _home_phone; }
            set
            {
                _home_phone = value;
                RaisePropertyChanged(() => HomePhone);
            }
        } public string MobilePhone
        {
            get { return _mobile_phone; }
            set
            {
                _mobile_phone = value;
                RaisePropertyChanged(() => MobilePhone);
            }
        }
        public bool IsPhone
        {
            get { return _isPhone; }
            set
            {
                _isPhone = value;
                RaisePropertyChanged(() => IsPhone);
            }
        }
        public bool IsRegistration
        {
            get { return _isRegistration; }
            set
            {
                _isRegistration = value;
                RaisePropertyChanged(() => IsRegistration);
            }
        }
        
        public Friend Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                if (value != null)
                {
                    FullName = $"{value.first_name} {value.last_name}";
                    Bdate = $"{value.bdate}";
                    IsMy = false;
                    HomePhone = $"{value.home_phone}";
                    MobilePhone = $"{value.mobile_phone}";
                    СontactsString = (value.home_phone != null || value.mobile_phone != null) ? $"{value.home_phone } {value.mobile_phone }" : "Нет контактов";
                    IsPhone = (HomePhone == null & MobilePhone == null) ? false : true;
                    Photo = value.photo_max_orig;
                }
                RaisePropertyChanged(() => Friend);
            }
        }
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                if (value != null)
                {
                    IsMy = true;
                    FullName = $"{value.first_name} {value.last_name}";
                    Bdate = $"{value.bdate}";
                    HomePhone = $"{value.home_phone}";
                    MobilePhone = $"{value.mobile_phone}";
                    СontactsString = (value.home_phone != null || value.mobile_phone != null) ? $"{value.home_phone } {value.mobile_phone }" : "Нет контактов";
                    IsPhone = (HomePhone == null & MobilePhone == null) ? false : true;
                   
                    Photo = value.photo_max_orig;
                }
                RaisePropertyChanged(() => User);
                RaisePropertyChanged(() => IsMy);
            }
        }
        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value;
                if (value != null)
                {
                    IsRegistration = value.IsRegistration;
                }
                RaisePropertyChanged(() => Client);
            }
        }
        public async Task Init(int idFriend)
        {
            UserId = idFriend;
            User = await Mvx.Resolve<IProfileService>().GetUser();
            Client = Mvx.Resolve<IDataLoaderService>().GetClient(UserId);
            Friend = await Mvx.Resolve<IProfileService>().GetFriend(idFriend);
        }
        public IMvxCommand AddNewRecordCommand
        {
            get { return new MvxCommand(AddNewRecord); }
        }

        private void AddNewRecord()
        {
            ShowViewModel<NewRecordMasterViewModel>(new { clientId = Client.Id , recordId = -1 });
        }
    }
}
