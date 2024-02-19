using Bank.Core.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Bank.Business.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task SendMessage(string user, string message)
        {
            var sender = await _userManager.FindByNameAsync(Context.User.Identity.Name);

            if (sender != null && sender.CoonectionId != null && (Context.User.IsInRole("Admin") || sender.IsOnline))
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
            else
            {
                
                await Clients.Caller.SendAsync("ErrorMessage", "You are not allowed to send messages.");
            }
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(Context.User.Identity.Name);
                if (user != null)
                {
                    user.CoonectionId = Context.ConnectionId;
                    user.IsOnline = true;
                    await _userManager.UpdateAsync(user);
                    await Clients.All.SendAsync("OnConnect", user.Id);
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(Context.User.Identity.Name);
                if (user != null)
                {
                    user.CoonectionId = null;
                    user.IsOnline = false;
                    await _userManager.UpdateAsync(user);
                    await Clients.All.SendAsync("DisConnect", user.Id);
                }
            }
        }
    }
}
