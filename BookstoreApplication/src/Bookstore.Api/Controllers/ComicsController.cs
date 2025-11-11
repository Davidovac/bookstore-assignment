using Bookstore.Application.DTOs;
using Bookstore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Editor")]
    public class ComicsController : ControllerBase
    {
        private readonly IComicsService _comicService;
        public ComicsController(IComicsService comicService)
        {
            _comicService = comicService;
        }

        [HttpGet("volumes")]
        public async Task<IActionResult> GetAllVolumesAsync([FromQuery] string volumeName = "")
        {
            return Ok(await _comicService.GetAllVolumesAsync(volumeName));
        }

        [HttpGet("volumes/{volumeId}/issues")]
        public async Task<IActionResult> GetPagedIssuesByVolumeAsync(int volumeId,[FromQuery] int page = 1)
        {
            return Ok(await _comicService.GetPagedIssuesByVolumeAsync(volumeId, page));
        }

        [HttpPost("volumes/{volumeId}/issues")]
        public async Task<IActionResult> AddComicIssueAsync(int volumeId, [FromBody] ComicIssueCreateDto issueDto, [FromQuery] int issueId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _comicService.AddComicIssueAsync(issueId, volumeId, issueDto);
            return Ok();
        }
    }
}
