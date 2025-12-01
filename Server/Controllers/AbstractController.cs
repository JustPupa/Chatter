using Cozy_Chatter.DTO;
using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController : ControllerBase
    {
        public async Task<IActionResult> GetPagedData<T, K>(PaginationRequest request, 
            int minPageSize, int maxPageSize, T repository, List<K> data, int uniqueTotalCount = -1) where T : AbstractRepository
        {
            int pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            int pageSize = request.PageSize switch
            {
                var x when x <= 0 => minPageSize,
                var x when x > maxPageSize => maxPageSize,
                _ => request.PageSize
            };
            int totalItems = (uniqueTotalCount == -1) ? await repository.GetCountAsync() : uniqueTotalCount;
            if (data.Count == 0) return NoContent();
            return Ok(new
            {
                pageNumber,
                pageSize,
                totalItems,
                totalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                data
            });
        }
    }
}