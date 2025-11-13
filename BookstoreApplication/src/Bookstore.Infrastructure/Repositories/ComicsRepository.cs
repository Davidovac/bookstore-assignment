using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;

namespace Bookstore.Infrastructure.Repositories
{
    public class ComicsRepository : GenericRepository<ComicIssue>, IComicsRepository
    {
        public ComicsRepository(AppDbContext context) : base(context) { }
        public async Task AddComicIssueAsync(ComicIssue comicIssue)
        {
            await _dbContext.ComicIssues.AddAsync(comicIssue);
        }
    }
}
