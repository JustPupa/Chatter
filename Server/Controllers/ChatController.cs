using Cozy_Chatter.DTO;
using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class ChatController(IChatRepository chatRepository, IUserRepository userRepository) : AbstractController
    {
        private readonly IChatRepository _chatRepository = chatRepository;
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet("{userid}/chats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChatsByUser(int userid, [FromQuery] PaginationRequest request)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userRepository.GetUserById(userid) == null) return NotFound("User not found");
            return await GetPagedData(
                request,
                15,
                100,
                _chatRepository,
                await _chatRepository.GetChatsByUserId(userid, request.PageNumber, request.PageSize)
            );
        }

        [HttpGet("{chatid}/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByChat(int chatid, [FromQuery] PaginationRequest request)
        {
            if (chatid <= 0) return BadRequest("Chat ID must be a positive integer");
            if (await _chatRepository.GetChatsById(chatid) == null) return NotFound("Chat not found");
            return await GetPagedData(
                request,
                10,
                30,
                _chatRepository,
                await _chatRepository.GetUsersByChatId(chatid, request.PageNumber, request.PageSize)
            );
        }
    }
}