using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Data;
using BookstoreApplication.Models;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        /*[HttpGet]
        public IActionResult GetAll()
        {
            return Ok(DataStore.Awards);
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var award = DataStore.Awards.FirstOrDefault(a => a.Id == id);
            if (award == null)
            {
                return NotFound();
            }
            return Ok(award);
        }

        // POST api/authors
        [HttpPost]
        public IActionResult Post(Author award)
        {
            award.Id = DataStore.GetNewAuthorId();
            DataStore.Awards.Add(award);
            return Ok(award);
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Author award)
        {
            if (id != award.Id)
            {
                return BadRequest();
            }

            var existingAward = DataStore.Awards.FirstOrDefault(a => a.Id == id);
            if (existingAward == null)
            {
                return NotFound();
            }

            int index = DataStore.Awards.IndexOf(existingAward);
            if (index == -1)
            {
                return NotFound();

            }

            DataStore.Awards[index] = award;
            return Ok(award);
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var award = DataStore.Awards.FirstOrDefault(a => a.Id == id);
            if (award == null)
            {
                return NotFound();
            }
            DataStore.Authors.Remove(award);

            // kaskadno brisanje svih knjiga obrisanog autora
            DataStore.Awards.RemoveAll(a => a.AwardId == id);

            return NoContent();
        }*/
    }
}
