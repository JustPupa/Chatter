using Cozy_Chatter.DTO;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cozy_Chatter.Controllers
{
    public class MessageController(IChatService chatService) : AbstractController
    {
        private readonly IChatService _chatService = chatService;

        [HttpGet("{chatid}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessagesByChat(int chatid, [FromQuery] PaginationRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            var chat = await _chatService.GetChatsById(chatid);
            if (chat == null) return NotFound("Chat not found");
            if (!chat.ChatUsers.Select(u => u.UserId).Contains(userId)) return Forbid();
            return await GetPagedData(
                request,
                30,
                200,
                _chatService,
                await _chatService.GetMessagesByChatId(chatid, request)
            );
        }

        [HttpGet("{chatid}/pinnedmessages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChatPinnedMessages(int chatid, [FromQuery] PaginationRequest request)
        {
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            if (await _chatService.GetChatsById(chatid) == null) return NotFound("Chat not found");
            var totalCount = await _chatService.GetPinnedCountByChatAsync(chatid);
            return await GetPagedData(
                request,
                10,
                20,
                _chatService,
                await _chatService.GetMessagesByChatId(chatid, request),
                totalCount
            );
        }
    }
}