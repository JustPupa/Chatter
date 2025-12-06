using Cozy_Chatter.DTO;
using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class MessageController(IMessageRepository messageRepository,
        IChatRepository chatRepository) : AbstractController
    {
        private readonly IMessageRepository _messageRepository = messageRepository;
        private readonly IChatRepository _chatRepository = chatRepository;

        [HttpGet("{chatid}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessagesByChat(int chatid, [FromQuery] PaginationRequest request)
        {
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            if (await _chatRepository.GetChatsById(chatid) == null) return NotFound("Chat not found");
            return await GetPagedData(
                request,
                30,
                200,
                _messageRepository,
                await _messageRepository.GetMessagesByChatId(chatid, request.PageNumber, request.PageSize)
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
            if (await _chatRepository.GetChatsById(chatid) == null) return NotFound("Chat not found");
            var totalCount = await _messageRepository.GetPinnedCountByChatAsync(chatid);
            return await GetPagedData(
                request,
                10,
                20,
                _messageRepository,
                await _messageRepository.GetMessagesByChatId(chatid, request.PageNumber, request.PageSize),
                totalCount
            );
        }
    }
}