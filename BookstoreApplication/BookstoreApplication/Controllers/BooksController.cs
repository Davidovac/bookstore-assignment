using System.Threading.Tasks;
using BookstoreApplication.DTOs;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private IBooksService _booksService;
        private IAuthorsService _authorsService;
        private IPublishersService _publishersService;

        public BooksController(IBooksService booksService, IAuthorsService authorsService, IPublishersService publishersService)
        {
            _booksService = booksService;
            _authorsService = authorsService;
            _publishersService = publishersService;
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
