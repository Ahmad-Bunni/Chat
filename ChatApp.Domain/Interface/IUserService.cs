using ChatApp.Domain.Model;
using ChatApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interface
{
    public interface IUserService
    {
        Task LeaveGroup(string userId, string username);

        Task JoinGroup(string groupName, string username, string connectionId);

        Task<User> FindUser(string username);

        Task<IEnumerable<User>> GetAllGroupUsers(string groupName);

        Task<Authentication> Authenticate(string username);
    }
}
