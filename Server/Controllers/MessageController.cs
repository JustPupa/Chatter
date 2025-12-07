using Cozy_Chatter.DTO;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class MessageController(IChatService chatService) : AbstractController
    {
        private readonly IChatService _chatService = chatService;

        [HttpGet("{chatid}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessagesByChat(int chatid, [FromQuery] PaginationRequest request)
        {
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            if (await _chatService.GetChatsById(chatid) == null) return NotFound("Chat not found");
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