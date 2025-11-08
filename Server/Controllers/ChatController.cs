using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController(ChatRepository chatRepository, UserRepository userRepository) : ControllerBase
    {
        private readonly ChatRepository _chatRepository = chatRepository;
        private readonly UserRepository _userRepository = userRepository;

        //Get chats by user
        [HttpGet("{userid}/chats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChatsByUser(int userid)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userRepository.GetUserById(userid) == null) return NotFound("User not found");
            var chats = await _chatRepository.GetChatsByUserId(userid);
            if (chats.Count == 0) return NoContent();
            return Ok(chats);
        }

        //Get users by chat
        [HttpGet("{chatid}/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByChat(int chatid)
        {
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            if (await _chatRepository.GetChatsById(chatid) == null) return NotFound("Chat not found");
            return Ok(await _chatRepository.GetUsersByChatId(chatid));
        }

        //Get messages by chat
        [HttpGet("{chatid}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessagesByChat(int chatid)
        {
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            if (await _chatRepository.GetChatsById(chatid) == null) return NotFound("Chat not found");
            var chatMessages = await _chatRepository.GetMessagesByChatId(chatid);
            if (chatMessages.Count == 0) return NoContent();
            return Ok(chatMessages);
        }

        //Get chat pinned messages
        [HttpGet("{chatid}/pinnedmessages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChatPinnedMessages(int chatid)
        {
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            if (await _chatRepository.GetChatsById(chatid) == null) return NotFound("Chat not found");
            var pinnedMessages = await _chatRepository.GetChatPinnedMessages(chatid);
            if (pinnedMessages.Count == 0) return NoContent();
            return Ok(pinnedMessages);
        }
    }
}