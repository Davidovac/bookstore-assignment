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
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_publishersRepo.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var publisher = _publishersRepo.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/publishers
        [HttpPost]
        public IActionResult Post(PublisherDto dto)
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
                _publishersRepo.Add(publisher);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, PublisherDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest();
                }

                var existingPublisher = _publishersRepo.GetById(id);
                if (existingPublisher == null)
                {
                    return NotFound();
                }
                existingPublisher.Id = dto.Id;
                existingPublisher.Name = dto.Name;
                existingPublisher.Address = dto.Address;
                existingPublisher.Website = dto.Website;

                _publishersRepo.Update(existingPublisher);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var publisher = _publishersRepo.GetById(id);
                if (publisher == null)
                {
                    return NotFound();
                }
                _publishersRepo.Delete(publisher);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
