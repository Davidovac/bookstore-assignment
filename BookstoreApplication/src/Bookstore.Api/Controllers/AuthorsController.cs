using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.AuthorEntities;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        {
            return Ok(await _authorsService.GetAllAsync(page));
        }

        [HttpGet]
        [Route("names")]
        public async Task<IActionResult> GetNameListAsync()
        {
            return Ok(await _authorsService.GetAllNamesAsync());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _authorsService.GetByIdAsync(id));
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> PostAsync(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authorsService.AddAsync(author));
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_authorsService.UpdateAsync(id, author));
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
