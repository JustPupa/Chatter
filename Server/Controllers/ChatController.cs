using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        //Get chats by user
        [HttpGet("{userid}/chats")]
        public async Task<IActionResult> GetChatsByUser(int userid)
        {
            var userChats = await ChatRepository.GetChatsByUserId(userid);
            return userChats == null ?
                BadRequest("User chats cannot be loaded") : Ok(userChats);
        }

        //Get users by chat
        [HttpGet("{chatid}/users")]
        public async Task<IActionResult> GetUsersByChat(int chatid)
        {
            var chatUsers = await ChatRepository.GetUsersByChatId(chatid);
            return chatUsers == null ?
                BadRequest("Chat users cannot be loaded") : Ok(chatUsers);
        }

        //Get messages by chat
        [HttpGet("{chatid}/messages")]
        public async Task<IActionResult> GetMessagesByChat(int chatid)
        {
            var chatMessages = await ChatRepository.GetMessagesByChatId(chatid);
            return chatMessages == null ?
                BadRequest("Messages cannot be loaded") : Ok(chatMessages);
        }

        //Get chat pinned messages
        [HttpGet("{chatid}/pinnedmessages")]
        public async Task<IActionResult> GetChatPinnedMessages(int chatid)
        {
            var pinnedMessages = await ChatRepository.GetChatPinnedMessages(chatid);
            return pinnedMessages == null ?
                BadRequest("Pinned messages cannot be loaded") : Ok(pinnedMessages);
        }
    }
}