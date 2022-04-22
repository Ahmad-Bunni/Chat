using ChatApp.Domain.Interface;
using ChatApp.Model.Messaging;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs;

public class MessageHub : Hub
{
    private readonly IUserService _userService;

    public MessageHub(IUserService userService)
    {
        _userService = userService;
    }

    public async Task SendMessage(Message message)
    {
        message = message with
        {
            Content = string.Format("{0} {1}: {2}", DateTime.UtcNow, Context.User.Identity.Name, message.Content),
            IsServer = false
        };

        await Clients.Group(message.GroupName).SendAsync("Send", message);
    }

    public async Task JoinGroup(string groupName, string userId)
    {
        try
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await _userService.JoinGroup(groupName, userId, Context.ConnectionId);

            var users = await _userService.GetAllGroupUsers(groupName);

            await Clients.Group(groupName).SendAsync("Users", users.Select(x => x.Username));

            await Clients.Group(groupName).SendAsync("Send"
                , new Message
                {
                    Content = $"{Context.User.Identity.Name} has joined the chat!",
                    IsServer = true,
                    Username = "Server"
                });
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async override Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public async override Task OnDisconnectedAsync(Exception exception)
    {
        var user = await _userService.FindUser(Context.User.Identity.Name);

        if (user != null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.GroupName);

            await _userService.LeaveGroup(user.Id);

            var users = await _userService.GetAllGroupUsers(user.GroupName);

            await Clients.Group(user.GroupName).SendAsync("Users", users.Select(x => x.Username));

            await Clients.Group(user.GroupName).SendAsync("Send", new Message
            {
                Content = $"{Context.User.Identity.Name} has left the chat!",
                IsServer = true,
                Username = "Server"
            });
        }

        await base.OnDisconnectedAsync(exception);
    }
}
