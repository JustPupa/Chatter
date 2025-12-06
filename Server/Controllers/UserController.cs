using Cozy_Chatter.DTO;
using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [Authorize]
    public class UserController(IUserRepository userRepository) : AbstractController
    {
        private readonly IUserRepository _userRepository = userRepository;

        //Get subscribers by user
        [HttpGet("{userid}/subscribers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubscribersByUser(int userid, [FromQuery] PaginationRequest request)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userRepository.GetUserById(userid) == null) return NotFound("User not found");
            return await GetPagedData(
                request,
                30,
                50,
                _userRepository,
                await _userRepository.GetSubscribersByUserId(userid, request.PageNumber, request.PageSize)
            );
        }
    }
}