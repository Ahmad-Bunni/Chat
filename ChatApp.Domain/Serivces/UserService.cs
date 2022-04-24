using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models.Authentication;
using ChatApp.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain.Serivces;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly AuthSettings _authSettings;

    public UserService(IUserRepository userRepository, AuthSettings authSettings)
    {
        _userRepository = userRepository;
        _authSettings = authSettings;
    }

    public async Task<Authentication> Authenticate(string username)
    {
        var user = await FindUser(username);

        // return null if user with the same username exists
        if (user != null)
            return null;

        await JoinGroup(null, username, null);

        //TODO authentication

        return default;
    }

    public async Task<User> FindUser(string username)
    {
        try
        {
            var users = await _userRepository.GetItemsAsync(x => x.Username.ToLower() == username.ToLower());

            return (users is not null && users.Any()) ? users.First() : null;
        }
        catch (Exception)
        {
            // TOOD logs
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetAllGroupUsers(string groupName)
    {
        try
        {
            return await _userRepository.GetItemsAsync(x => x.GroupName == groupName);
        }
        catch (Exception)
        {
            // TOOD logs
            throw;
        }
    }

    public async Task JoinGroup(string groupName, string userId, string connectionId)
    {
        try
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(), // premitive type
                GroupName = groupName,
                ConnectionId = connectionId
            };

            var users = await _userRepository.GetItemsAsync(x => x.Id == userId);

            if (users is not null && users.Any())
            {
                user = users.First() with
                {
                    GroupName = groupName,
                    ConnectionId = connectionId,
                };
            }

            await _userRepository.UpsertItemAsync(user);

        }
        catch (Exception)
        {
            // TOOD logs
            throw;
        }
    }

    public async Task LeaveGroup(string userId)
    {
        try
        {
            await _userRepository.DeleteItemAsync(userId);

        }
        catch (Exception)
        {
            // TOOD logs
            throw;
        }
    }
}
