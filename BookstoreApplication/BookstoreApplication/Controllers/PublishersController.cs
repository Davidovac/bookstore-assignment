using System.Threading.Tasks;
using BookstoreApplication.Data;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private IPublishersService _publishersService;

        public PublishersController(IPublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int sort = 0)
        {
            return Ok(await _publishersService.GetAllAsync(sort));
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _publishersService.GetByIdAsync(id));
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> PostAsync(Publisher publisher)
        {
            return Ok(await _publishersService.AddAsync(publisher));
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _publishersService.UpdateAsync(id, publisher));
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _publishersService.DeleteAsync(id);
            return NoContent();
        }
    }
}
