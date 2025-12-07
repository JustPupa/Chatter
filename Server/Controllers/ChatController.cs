using Cozy_Chatter.DTO;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class ChatController(IChatService chatService) : AbstractController
    {
        private readonly IChatService _chatService = chatService;

        [HttpGet("{userid}/chats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChatsByUser(int userid, [FromQuery] PaginationRequest request)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _chatService.GetUserById(userid) == null) return NotFound("User not found");
            return await GetPagedData(
                request,
                15,
                100,
                _chatService,
                await _chatService.GetChatsByUserId(userid, request)
            );
        }

        [HttpGet("{chatid}/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByChat(int chatid, [FromQuery] PaginationRequest request)
        {
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            if (await _chatService.GetChatsById(chatid) == null) return NotFound("Chat not found");
            return await GetPagedData(
                request,
                10,
                30,
                _chatService,
                await _chatService.GetUsersByChatId(chatid, request)
            );
        }
    }
}