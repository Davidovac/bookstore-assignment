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
            return Ok(await _awardsService.GetAllAsync());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _awardsService.GetByIdAsync(id));
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> PostAsync(Award award)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _awardsService.AddAsync(award));
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Award award)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _awardsService.UpdateAsync(id, award));
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _awardsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
