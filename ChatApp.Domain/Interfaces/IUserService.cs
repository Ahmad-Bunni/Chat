using ChatApp.Domain.Models.Authentication;
using ChatApp.Domain.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces;

public interface IUserService
{
    Task LeaveGroup(string userId);

    Task JoinGroup(string groupName, string userId, string connectionId);

    Task<User> FindUser(string username);

    Task<IEnumerable<User>> GetAllGroupUsers(string groupName);

    Task<Authentication> Authenticate(string username);
}
