using System.Threading.Tasks;
using BookstoreApplication.Data;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersRepository _publishersRepo;
        private AppDbContext _context;

        public PublishersController(AppDbContext context)
        {
            _publishersRepo = new PublishersRepository(context);
        }

        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _publishersRepo.GetAllAsync());
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
            var publisher = await _publishersRepo.GetByIdAsync(id);
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
                Publisher publisher = new Publisher
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Address = dto.Address,
                    Website = dto.Website
                };
                await _publishersRepo.AddAsync(publisher);
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

                var existingPublisher = await _publishersRepo.GetByIdAsync(id);
                if (existingPublisher == null)
                {
                    return NotFound();
                }
                existingPublisher.Id = dto.Id;
                existingPublisher.Name = dto.Name;
                existingPublisher.Address = dto.Address;
                existingPublisher.Website = dto.Website;

                await _publishersRepo.UpdateAsync(existingPublisher);
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
                var publisher = await _publishersRepo.GetByIdAsync(id);
                if (publisher == null)
                {
                    return NotFound();
                }
                await _publishersRepo.DeleteAsync(publisher);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
