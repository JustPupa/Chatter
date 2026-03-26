using Cozy_Chatter.DTO;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Cozy_Chatter.SignalR
{
    public class ChatHub(IChatService chatService) : Hub
    {
        private readonly IChatService _chatService = chatService;
        public async Task JoinChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        public async Task SendMessage(MessageDTO message)
        {
            var convertedMessage = message.ConvertToMessage();
            await _chatService.AddMessageAsync(convertedMessage);
            await Clients.Group(message.ChatId).SendAsync("ReceiveMessage", message);
        }
    }
}