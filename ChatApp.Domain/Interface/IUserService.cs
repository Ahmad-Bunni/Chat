using ChatApp.Domain.Model.Authentication;
using ChatApp.Model.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interface;

public interface IUserService
{
    Task LeaveGroup(string userId);

    Task JoinGroup(string groupName, string userId, string connectionId);

    Task<User> FindUser(string username);

    Task<IEnumerable<User>> GetAllGroupUsers(string groupName);

    Task<Authentication> Authenticate(string username);
}
