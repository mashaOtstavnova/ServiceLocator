using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ServiceLocator.Core.IServices;

namespace ServiceLocator.Core.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        private List<ListItem> _selectedItems;
        private List<ListItem> list = new List<ListItem>();

        public List<ListItem> Items
        {
            set
            {
                list = value;
                RaisePropertyChanged(() => Items);
            }
            get
            {
                //он на каждый гет булет вызывать этот метод, ткак как  гет даже при пролистывании листвью ьбудет вызываться
                // InitializeMyList();
                return list;
            }
        }

        public List<ListItem> SelectedItems
        {
            get
            {
                if (_selectedItems == null)
                    _selectedItems = new List<ListItem>();
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged(() => SelectedItems);
            }
        }

        public IMvxCommand OnItemSelectCommand
        {
            get { return new MvxCommand<ListItem>(OnItemSelect); }
        }

        private  IProgressLoaderService _progressLoaderService;
        public async void Init()
        {

            _progressLoaderService = Mvx.Resolve<IProgressLoaderService>();
            _progressLoaderService.ShowProgressBar();
            IProfileService profileService;
            Mvx.TryResolve(out profileService);

            IDataLoaderService dataLoaderService;
            Mvx.TryResolve(out dataLoaderService);
            var users = await profileService.GetFriends();
            var test = new List<ListItem>();
            Items = new List<ListItem>();
            foreach (var item in users.items)
            {
                var type = dataLoaderService.GetType(item.id);
                string services = "";
                if (type == "Master")
                {
                    var ser = dataLoaderService.GetMaster(item.id).Categories;
                    services = ser.Count > 0
                        ? string.Join("; ", ser)
                        : "Мастер";
                    ;
                }
                else
                {
                    services = "Клиент";
                }
                var t = new ListItem(item.first_name + " " + item.last_name, item.photo_50,services, item.id);
                Items.Add(t);
            }
            _progressLoaderService.HideProgressBar();
            //var s =
            //    users.items.Select(
            //            friend => new ListItem(friend.first_name + " " + friend.last_name, friend.photo_50, friend.id))
            //        .ToList();
            //Items = new List<ListItem>(s);
        }

        //private async void InitializeMyList()
        //{
        //    list = new List<ListItem>();
        //    IProfileService profileService;
        //    var service = Mvx.TryResolve(out profileService);

        //    var friends = await profileService.GetFriends();
        //    foreach (var friend in friends.items)
        //        list.Add(new ListItem(friend.first_name, friend.photo_50, friend.id));
        //}

        public void OnItemSelect(ListItem item)
        {
            IDataLoaderService dataLoaderService;
            var service = Mvx.TryResolve(out dataLoaderService);
            var type = dataLoaderService.GetType(item.Id);
            if (type == "Master")
            {
                ShowViewModel<MasterViewModel>(new { idFriend = item.Id });
            }
            else
            {
                ShowViewModel<ClientViewModel>(new { idFriend = item.Id });
            }
            //if (SelectedItems.Contains(item))
            //{
            //    SelectedItems.Remove(item);
            //}
            //else
            //{
            //    SelectedItems.Add(item);
            //}
            RaisePropertyChanged(() => SelectedItems);
        }
    }

    public class ListItem
    {
        public ListItem(string title, string photo, string services, int id)
        {
            Title = title;
            Photo = photo;
            Services = services;
            Id = id;
        }

        public string Title { get; set; }
        public string Services { get; set; }
        public int Id { get; set; }
        public string Photo { get; set; }
    }
}
