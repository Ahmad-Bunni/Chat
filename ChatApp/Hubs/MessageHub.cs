using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("receiveMessage", $"{DateTime.UtcNow} {message.Username}: {message.Content}");
        }

        public async override Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("receiveMessage", $"-- Server -- {DateTime.UtcNow} User has connected! ");

            await base.OnConnectedAsync();
        }
    }
}
