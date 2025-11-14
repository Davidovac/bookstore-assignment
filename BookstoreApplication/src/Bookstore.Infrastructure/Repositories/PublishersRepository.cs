using Bookstore.Domain.Entities.PublisherEntities;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class PublishersRepository : GenericRepository<Publisher>, IPublishersRepository
    {
        public PublishersRepository(AppDbContext context) : base(context) { }

        public async Task<List<Publisher>?> GetAllSortedAsync(int sort)
        {
            IQueryable<Publisher> query = _dbContext.Publishers;
            query = ApplySorting(query, sort);
            return await query.ToListAsync();
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
