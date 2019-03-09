using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IHubContext<MessageHub> _hubContext;
        public ChatController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<IActionResult> SendMessage(Message message)
        {
            await _hubContext.Clients.All.SendAsync("receiveMessage", $"{DateTime.UtcNow} {message.Username}: {message.Content}");

            return Ok();
        }
    }
}
