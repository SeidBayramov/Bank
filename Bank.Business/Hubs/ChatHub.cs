using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Core.Entities.Account;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly Dictionary<string, DateTime> _userLastMessageTime = new Dictionary<string, DateTime>();
        private const int MessageLimitInSeconds = 60; // Adjust the time limit as needed
        private const int MaxMessagesPerMinute = 5;    // Adjust the message limit as needed

        public ChatHub(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task SendMessage(string user, string message)
        {
            var userId = Context.User.Identity.Name;

            if (!_userLastMessageTime.TryGetValue(userId, out var lastMessageTime))
            {
                lastMessageTime = DateTime.MinValue;
            }

            var timeSinceLastMessage = DateTime.Now - lastMessageTime;

            if (timeSinceLastMessage.TotalSeconds < MessageLimitInSeconds)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "System", $"You are sending messages too quickly. Please wait before sending another message.");
                return;
            }

            if (!_userLastMessageTime.ContainsKey(userId))
            {
                _userLastMessageTime.Add(userId, DateTime.Now);
            }
            else
            {
                _userLastMessageTime[userId] = DateTime.Now;
            }

            await Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.UtcNow);
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(Context.User.Identity.Name);
                if (user != null)
                {
                    user.CoonectionId = Context.ConnectionId;
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
                    await _userManager.UpdateAsync(user);
                    await Clients.All.SendAsync("DisConnect", user.Id);
                }
            }
        }
    }
}
