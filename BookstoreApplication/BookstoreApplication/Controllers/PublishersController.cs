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
        private PublishersService _publishersService;
        private AppDbContext _context;

        public PublishersController(AppDbContext context)
        {
            _publishersService = new PublishersService(context);
        }

        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _publishersService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var publisher = await _publishersService.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> PostAsync(PublisherDto dto)
        {
            try
            {
                await _publishersService.AddAsync(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, PublisherDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest();
                }

                var existingPublisher = await _publishersService.GetByIdAsync(id);
                if (existingPublisher == null)
                {
                    return NotFound();
                }

                await _publishersService.UpdateAsync(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var publisher = await _publishersService.GetByIdAsync(id);
                if (publisher == null)
                {
                    return NotFound();
                }
                await _publishersService.DeleteAsync(publisher);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
