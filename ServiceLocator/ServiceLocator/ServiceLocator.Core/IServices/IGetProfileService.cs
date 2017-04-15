using ServiceLocator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLocator.Core.IServices
{
    public interface IGetProfileService
    {
        Task<User> GetUsers();
        Task<Friends> GetFriends();
        Task<Friend> GetFriend(int idFriend);

    }
}
