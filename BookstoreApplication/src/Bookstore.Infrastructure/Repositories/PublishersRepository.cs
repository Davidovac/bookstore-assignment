using Bookstore.Domain.Entities.PublisherEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
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

        public async Task<List<Publisher>?> GetAllAsync(int sort)
        {
            IQueryable<Publisher> query = _context.Publishers;
            query = ApplySorting(query, sort);
            return await query.ToListAsync();
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

        private static IQueryable<Publisher> ApplySorting(IQueryable<Publisher> query, int sort)
        {
            return sort switch
            {
                (int)PublisherSortTypes.NAME_DESC => query.OrderByDescending(p => p.Name),
                (int)PublisherSortTypes.DATE_ASC => query.OrderBy(p => p.Address),
                (int)PublisherSortTypes.DATE_DESC => query.OrderByDescending(p => p.Address),
                _ => query.OrderBy(p => p.Name),
            };
        }
    }
}
