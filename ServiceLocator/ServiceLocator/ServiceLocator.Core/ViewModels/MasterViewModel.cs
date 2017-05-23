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
    public class MasterViewModel : BaseViewModel
    {
        private string _bdate;
        private string _city;
        private Friend _friend;
        private string _fullName;
        private bool _isMy;
        private string _mobile_phone;
        private string _home_phone;

        private bool _isPhone;
        private string _photo;

        private string _discription;
        private string _subcategory;
        private List<string> _categories;
        private string _servicesString;
        private string _contactsString;
        private bool _isRegistration;
        private Master _master;

        private User _user;
        public int UserId;

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

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged(() => City);
            }
        }
        public string ServicesString
        {
            get { return _servicesString; }
            set
            {
                _servicesString = value;
                RaisePropertyChanged(() => ServicesString);
            }
        }
        public string ContactsString
        {
            get { return _contactsString; }
            set
            {
                _contactsString = value;

                RaisePropertyChanged(() => ContactsString);
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
        public string Discription
        {
            get { return _discription; }
            set
            {
                _discription = value;
                RaisePropertyChanged(() => Discription);
            }
        }
        public string Subcategory
        {
            get { return _subcategory; }
            set
            {
                _subcategory = value;
                RaisePropertyChanged(() => Subcategory);
            }
        }
        public List<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged(() => Categories);
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
        public Master Master
        {
            get { return _master; }
            set
            {
                _master = value;
                if (value != null)
                {
                    Discription = $"{value.AboutMe}";
                    Subcategory = $"{value.Subcategory}";
                    Categories = value.Categories;
                    ServicesString = string.Join("; ", Categories);
                    IsRegistration = value.IsRegistration;
                }
                RaisePropertyChanged(() => Master);
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
                    IsMy = false;
                    FullName = $"{value.first_name} {value.last_name}";
                    Bdate = $"{value.bdate}";
                    HomePhone = $"{value.home_phone}";
                    MobilePhone = $"{value.mobile_phone}";
                    ContactsString = (string.IsNullOrWhiteSpace(value.home_phone) || string.IsNullOrWhiteSpace(value.mobile_phone)) ? $"{value.home_phone } {value.mobile_phone }" : "Нет контактов";
                    IsPhone = (string.IsNullOrWhiteSpace(HomePhone) & string.IsNullOrWhiteSpace(MobilePhone)) ? false : true;
                    City = $"{value.city?.title}";
                    Photo = value.photo_max_orig;
                }
                RaisePropertyChanged(() => Friend);
                RaisePropertyChanged(() => IsMy);
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
        }
        public string MobilePhone
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
                    ContactsString = (string.IsNullOrWhiteSpace(value.home_phone) || string.IsNullOrWhiteSpace(value.mobile_phone)) ? $"{value.home_phone } {value.mobile_phone }" : "Нет контактов";
                    IsPhone = (string.IsNullOrWhiteSpace(HomePhone) & string.IsNullOrWhiteSpace(MobilePhone)) ? false : true;
                    City = $"{value.city?.title}";
                    Photo = value.photo_max_orig;
                }
                RaisePropertyChanged(() => User);
                RaisePropertyChanged(() => IsMy);
            }
        }

        public IMvxCommand AddNewRecordCommand
        {
            get { return new MvxCommand(AddNewRecord); }
        }
        public IMvxCommand SheduleCommand
        {
            get { return new MvxCommand(Shedule); }
        }

        private void Shedule()
        {
            ShowViewModel<ScheduleViewModel>(new { idMasters = UserId });
        }

        public bool IsMy
        {
            get { return _isMy; }
            set
            {
                _isMy = value;
                RaisePropertyChanged(() => IsMy);
            }
        }

        public async Task Init(int idFriend)
        {
            UserId = idFriend;
            User = await Mvx.Resolve<IProfileService>().GetUser();
            Master = Mvx.Resolve<IDataLoaderService>().GetMaster(UserId);
            Friend = await Mvx.Resolve<IProfileService>().GetFriend(UserId);
        }

        private void AddNewRecord()
        {
            ShowViewModel<NewRecordClientViewModel>(new {masterId = UserId, recordId =""});
        }
    }
}
