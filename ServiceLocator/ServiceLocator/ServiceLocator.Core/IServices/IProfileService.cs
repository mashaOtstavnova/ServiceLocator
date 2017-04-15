
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLocator.Entities;

namespace ServiceLocator.Core.IServices
{
    public interface IProfileService
    {
        Task<User> GetUser();
        Task<Friends> GetFriends();
        Task<Friend> GetFriend(int idFriend);
    }
}
