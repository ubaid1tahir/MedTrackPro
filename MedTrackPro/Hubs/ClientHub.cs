using DataLibrary.Models.Patient;
using MedTrackPro.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace MedTrackPro.Hubs
{
    public class ClientHub : Hub
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public ClientHub(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> SendMessage(string message, string receiverId, string senderName)
        {
            Message MsgModel = new()
            {
                ReceiverId = receiverId,
                Text = message
            };
            var sender = await _userManager.FindByNameAsync(senderName);
            await Clients.User(receiverId).SendAsync("OnSendMessage", MsgModel);
            if(sender != null)
            {
                _context.Messages.Add(
                new Message { Text = message, ReceiverId = receiverId, SenderId = sender.Id, SendTime = DateTime.Now });
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
