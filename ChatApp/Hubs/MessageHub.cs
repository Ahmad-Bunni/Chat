using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class MessageHub : Hub
    {
        public static List<User> users = new List<User>();

        public async Task SendMessage(Message message)
        {
            message.IsServer = false;

            await Clients.All.SendAsync("receiveMessage", message);
        }

        public async override Task OnConnectedAsync()
        {
            users.Add(new User { Username = "user" });

            Message serverMessage = new Message()
            {
                IsServer = true,
                Content = "",
                Username = "Server"
            };

            await Clients.All.SendAsync("receiveUsers", users);

            await Clients.All.SendAsync("receiveMessage", serverMessage);

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            User user = users.FirstOrDefault(x => x.Username == "user");

            users.Remove(user);

            Message serverMessage = new Message()
            {
                IsServer = true,
                Content = "",
                Username = "Server"
            };

            await Clients.All.SendAsync("receiveUsers", users);

            await Clients.All.SendAsync("receiveMessage", serverMessage);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
