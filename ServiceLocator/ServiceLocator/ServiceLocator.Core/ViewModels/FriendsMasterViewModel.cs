using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using ServiceLocator.Core.ViewModels;

namespace ServiceLocator.Core.ViewModels
{
    public class FriendsMasterViewModel:BaseViewModel
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
}
