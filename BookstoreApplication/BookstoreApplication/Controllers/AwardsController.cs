using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Services;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private IAwardsService _awardsService;

        public AwardsController(IAwardsService awardsService)
        {
            _awardsService = awardsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _awardsService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var award = await _awardsService.GetByIdAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return Ok(award);
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> PostAsync(Award award)
        {
            var existingAward = await _awardsService.GetByIdAsync(award.Id);
            return Ok(award);
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Award award)
        {
            if (id != award.Id)
            {
                return BadRequest();
            }

            var existingAward = await _awardsService.GetByIdAsync(id);
            if (existingAward == null)
            {
                return NotFound();
            }

            await _awardsService.UpdateAsync(award);
            return Ok(award);
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var award = await _awardsService.GetByIdAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            await _awardsService.DeleteAsync(id);

            return NoContent();
        }
    }
}
