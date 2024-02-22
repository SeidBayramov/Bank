using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Core.Entities.Account;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<ChatHub> _logger;
        private readonly Dictionary<string, DateTime> _userLastMessageTime = new Dictionary<string, DateTime>();
        private const int MessageLimitInSeconds = 60; // Adjust the time limit as needed

        public ChatHub(UserManager<AppUser> userManager, ILogger<ChatHub> logger)
        {
            this._userManager = userManager;
            this._logger = logger;
        }

        public async Task SendMessage(string user, string message)
        {
            var userId = Context.User.Identity.Name;

            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing SendMessage for user {userId}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Identity.Name;

            try
            {
                var user = await _userManager.FindByNameAsync(userId);
                if (user != null)
                {
                    user.CoonectionId = Context.ConnectionId;
                    await _userManager.UpdateAsync(user);

                    bool isSeid2004 = user.UserName == "Seid2004";
                    await Clients.All.SendAsync("OnConnect", user.Id, isSeid2004);

                    _logger.LogInformation($"User connected: {userId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing OnConnectedAsync for user {userId}");
            }
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User.Identity.Name;

            try
            {
                var user = await _userManager.FindByNameAsync(userId);
                if (user != null)
                {
                    user.CoonectionId = null;
                    await _userManager.UpdateAsync(user);
                    await Clients.All.SendAsync("DisConnect", user.Id);

                    _logger.LogInformation($"User disconnected: {userId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing OnDisconnectedAsync for user {userId}");
            }
        }
    }
}
