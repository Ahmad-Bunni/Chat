using ChatApp.Domain.Interface;
using ChatApp.Domain.Model.Authentication;
using ChatApp.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain.Serivce;

public class UserService : IUserService
{
    private readonly ICosmosRepository<User> _cosmosRepository;
    private readonly AuthSettings _authSettings;

    public UserService(ICosmosRepository<User> cosmosRepository, AuthSettings authSettings)
    {
        _cosmosRepository = cosmosRepository;
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
            var users = await _cosmosRepository.GetItemsAsync(x => x.Username.ToLower() == username.ToLower());

            return (users is not null && users.Any()) ? users.First() : null;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetAllGroupUsers(string groupName)
    {
        try
        {
            return await _cosmosRepository.GetItemsAsync(x => x.GroupName == groupName);
        }
        catch (Exception ex)
        {
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

            var users = await _cosmosRepository.GetItemsAsync(x => x.Id == userId);

            if (users is not null && users.Any())
            {
                user = users.First() with
                {
                    GroupName = groupName,
                    ConnectionId = connectionId,
                };
            }

            await _cosmosRepository.UpsertItemAsync(user);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task LeaveGroup(string userId)
    {
        try
        {
            await _cosmosRepository.DeleteItemAsync(userId);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
