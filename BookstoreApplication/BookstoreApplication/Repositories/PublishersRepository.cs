using System.Security.Policy;
using System.Threading.Tasks;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Publisher = BookstoreApplication.Models.Publisher;

namespace BookstoreApplication.Repositories
{
    public class PublishersRepository : IPublishersRepository
    {
        private AppDbContext _context;

        public PublishersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<List<Publisher>?> GetAllAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher> UpdateAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }


        public async Task DeleteAsync(Publisher publisher)
        {
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
        }
    }
}
