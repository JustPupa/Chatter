using Cozy_Chatter.DTO;
using Cozy_Chatter.Models;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController : ControllerBase
    {
        public async Task<IActionResult> GetPagedData<T, K>(PaginationRequest request,
            int minPageSize, int maxPageSize, T service, List<K> data, int uniqueTotalCount = -1)
            where T : IService where K : IEntity
        {
            int pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            int pageSize = request.PageSize switch
            {
                var x when x <= 0 => minPageSize,
                var x when x > maxPageSize => maxPageSize,
                _ => request.PageSize
            };
            int totalItems = (uniqueTotalCount == -1) ? await service.GetCountAsync<K>() : uniqueTotalCount;
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