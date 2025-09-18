using System.Threading.Tasks;
using BookstoreApplication.Data;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksRepository _booksRepo;
        private AuthorsRepository _authorsRepo;
        private PublishersRepository _publishersRepo;
        private AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _booksRepo = new BooksRepository(context);
            _authorsRepo = new AuthorsRepository(context);
            _publishersRepo = new PublishersRepository(context);
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _booksRepo.GetAllAsync());
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
            var book = await _booksRepo.GetByIdAsync(id);
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
                var author = await _authorsRepo.GetByIdAsync(dto.AuthorId);
                if (author == null)
                {
                    return BadRequest();
                }

                // kreiranje knjige je moguće ako je izabran postojeći izdavač
                var publisher = await _publishersRepo.GetByIdAsync(dto.PublisherId);
                if (publisher == null)
                {
                    return BadRequest();
                }

                Book book = new Book
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    PageCount = dto.PageCount,
                    PublishedDate = dto.PublishedDate.ToUniversalTime(),
                    ISBN = dto.ISBN,
                    AuthorId = dto.AuthorId,
                    Author = author,
                    PublisherId = dto.PublisherId,
                    Publisher = publisher
                };
                await _booksRepo.AddAsync(book);
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
                var author = await _authorsRepo.GetByIdAsync(dto.AuthorId);
                if (author == null)
                {
                    return BadRequest();
                }

                // izmena knjige je moguća ako je izabran postojeći izdavač
                var publisher = await _publishersRepo.GetByIdAsync(dto.PublisherId);
                if (publisher == null)
                {
                    return BadRequest();
                }

                var book = await _booksRepo.GetBookAsync(id);

                if (book == null)
                    return NotFound();

                book.Title = dto.Title;
                book.PageCount = dto.PageCount;
                book.PublishedDate = dto.PublishedDate.ToUniversalTime();
                book.ISBN = dto.ISBN;
                book.AuthorId = dto.AuthorId;
                book.PublisherId = dto.PublisherId;
                await _booksRepo.UpdateAsync( book);
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
                var book = await _booksRepo.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                await _booksRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
