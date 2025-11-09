using Bookstore.Application.DTOs;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.BookEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        // GET: api/books

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] BookFilterMix filterMix, int sort = 0)
        {
            return Ok(await _booksService.GetAllAsync(sort, filterMix));
        }

        // GET api/books/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _booksService.GetByIdAsync(id));
        }

        // POST api/books
        [Authorize(Roles = "Librarian")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(BookRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _booksService.CreateAndLinkAsync(dto));
        }

        // PUT api/books/5
        [Authorize(Roles = "Editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, BookRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _booksService.UpdateAsync(id, dto));
        }

        // DELETE api/books/5
        [Authorize(Roles = "Editor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _booksService.DeleteAsync(id);
            return NoContent();
        }
    }
}
