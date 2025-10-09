using System.Threading.Tasks;
using BookstoreApplication.DTOs;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _booksService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var book = await _booksService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> PostAsync(BookSimpleDto dto)
        {
            try
            {
                // kreiranje knjige je moguće ako je izabran postojeći autor
                var author = await _authorsService.GetByIdAsync(dto.AuthorId);
                if (author == null)
                {
                    return BadRequest();
                }

                // kreiranje knjige je moguće ako je izabran postojeći izdavač
                var publisher = await _publishersService.GetByIdAsync(dto.PublisherId);
                if (publisher == null)
                {
                    return BadRequest();
                }

                await _booksService.AddAsync(dto, publisher, author);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, BookSimpleDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest();
                }

                // izmena knjige je moguća ako je izabran postojeći autor
                var author = await _authorsService.GetByIdAsync(dto.AuthorId);
                if (author == null)
                {
                    return BadRequest();
                }

                // izmena knjige je moguća ako je izabran postojeći izdavač
                var publisher = await _publishersService.GetByIdAsync(dto.PublisherId);
                if (publisher == null)
                {
                    return BadRequest();
                }

                await _booksService.UpdateAsync(id, dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var book = await _booksService.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                await _booksService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
