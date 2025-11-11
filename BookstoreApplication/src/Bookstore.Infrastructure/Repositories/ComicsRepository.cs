using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;

namespace Bookstore.Infrastructure.Repositories
{
    public class ComicsRepository : IComicsRepository
    {
        private AppDbContext _context;
        public ComicsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddComicIssueAsync(ComicIssue comicIssue)
        {
            _context.ComicIssues.Add(comicIssue);
            await _context.SaveChangesAsync();
        }
    }
}
