using AutoMapper;
using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.Common;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        public AuthorsRepository(AppDbContext context) : base(context) { }

        public async Task<PaginatedList<Author>> GetPagedAsync(int page)
        {
            int pageSize = 1;
            int pageIndex = page - 1;

            var count = await _dbContext.Authors.CountAsync();

            var authors = await _dbContext.Authors
              .Skip(pageIndex * pageSize)
              .Take(pageSize)
              .ToListAsync();

            PaginatedList<Author> result = new PaginatedList<Author>(authors, count, pageIndex, pageSize);
            return result;
        }
    }
}
