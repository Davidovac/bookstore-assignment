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
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_booksRepo.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        // GET api/books/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var book = _booksRepo.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public IActionResult Post(BookSimpleDto dto)
        {
            try
            {
                // kreiranje knjige je moguće ako je izabran postojeći autor
                var author = _authorsRepo.GetById(dto.AuthorId);
                if (author == null)
                {
                    return BadRequest();
                }

                // kreiranje knjige je moguće ako je izabran postojeći izdavač
                var publisher = _publishersRepo.GetById(dto.PublisherId);
                if (publisher == null)
                {
                    return BadRequest();
                }

                Book book = new Book
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    PageCount = dto.PageCount,
                    PublishedDate = dto.PublishedDate,
                    ISBN = dto.ISBN,
                    AuthorId = dto.AuthorId,
                    Author = author,
                    PublisherId = dto.PublisherId,
                    Publisher = publisher
                };
                _booksRepo.Add(book);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, BookSimpleDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest();
                }

                // izmena knjige je moguća ako je izabran postojeći autor
                var author = _authorsRepo.GetById(dto.AuthorId);
                if (author == null)
                {
                    return BadRequest();
                }

                // izmena knjige je moguća ako je izabran postojeći izdavač
                var publisher = _publishersRepo.GetById(dto.PublisherId);
                if (publisher == null)
                {
                    return BadRequest();
                }

                var book = _booksRepo.GetBook(id);

                if (book == null)
                    return NotFound();

                book.Title = dto.Title;
                book.PageCount = dto.PageCount;
                book.PublishedDate = dto.PublishedDate;
                book.ISBN = dto.ISBN;
                book.AuthorId = dto.AuthorId;
                book.PublisherId = dto.PublisherId;
                _booksRepo.Update(id, book);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var book = _booksRepo.GetById(id);
                if (book == null)
                {
                    return NotFound();
                }
                _booksRepo.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
