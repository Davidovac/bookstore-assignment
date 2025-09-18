using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AwardsRepository
    {
        private AppDbContext _context;

        public AwardsRepository(AppDbContext context)
        {
            _context = context;
        }

        public Award? GetById(int id)
        {
            return _context.Awards.Find(id);
        }

        public List<Award> GetAll()
        {
            return _context.Awards.ToList();
        }

        public Award Add(Award award)
        {
            _context.Awards.Add(award);
            _context.SaveChanges();
            return award;
        }

        public Award Update(Award award)
        {
            _context.Awards.Update(award);
            _context.SaveChanges();
            return award;
        }


        public void Delete(Award award)
        {
            _context.Awards.Remove(award);
            _context.SaveChanges();
        }
    }
}
