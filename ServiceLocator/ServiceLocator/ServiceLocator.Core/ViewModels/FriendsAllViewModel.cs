using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLocator.Core.ViewModels
{
    public class FriendsAllViewModel:BaseViewModel
    {

        private List<FriendMaster> list = new List<FriendMaster>();
        public List<FriendMaster> Items
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
    }
    public class FriendMaster
    {
        public FriendMaster(string name, string photo, string services, int id)
        {
            Name = name;
            Photo = photo;
            Services = services;
            Id = id;
        }

        public string Name { get; set; }
        public string Services { get; set; }
        public int Id { get; set; }
        public string Photo { get; set; }
    }
}
