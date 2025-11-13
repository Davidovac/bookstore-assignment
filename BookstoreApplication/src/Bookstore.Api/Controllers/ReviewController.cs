using Bookstore.Application.DTOs;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Services;
using Bookstore.Domain.Entities.AuthorEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ReviewCreateRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _reviewService.AddAsync(User, request);
            return Ok();
        }
    }
}
