using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController(ChatRepository chatRepository) : ControllerBase
    {
        private readonly ChatRepository _chatRepository = chatRepository;

        //Get chats by user
        [HttpGet("{userid}/chats")]
        public async Task<IActionResult> GetChatsByUser(int userid)
        {
            var userChats = await _chatRepository.GetChatsByUserId(userid);
            return userChats == null ?
                BadRequest("User chats cannot be loaded") : Ok(userChats);
        }

        //Get users by chat
        [HttpGet("{chatid}/users")]
        public async Task<IActionResult> GetUsersByChat(int chatid)
        {
            var chatUsers = await _chatRepository.GetUsersByChatId(chatid);
            return chatUsers == null ?
                BadRequest("Chat users cannot be loaded") : Ok(chatUsers);
        }

        //Get messages by chat
        [HttpGet("{chatid}/messages")]
        public async Task<IActionResult> GetMessagesByChat(int chatid)
        {
            var chatMessages = await _chatRepository.GetMessagesByChatId(chatid);
            return chatMessages == null ?
                BadRequest("Messages cannot be loaded") : Ok(chatMessages);
        }

        //Get chat pinned messages
        [HttpGet("{chatid}/pinnedmessages")]
        public async Task<IActionResult> GetChatPinnedMessages(int chatid)
        {
            var pinnedMessages = await _chatRepository.GetChatPinnedMessages(chatid);
            return pinnedMessages == null ?
                BadRequest("Pinned messages cannot be loaded") : Ok(pinnedMessages);
        }
    }
}