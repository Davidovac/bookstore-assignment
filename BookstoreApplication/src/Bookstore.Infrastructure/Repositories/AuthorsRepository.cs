using AutoMapper;
using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.Common;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private AppDbContext _context;

        public AuthorsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<PaginatedList<Author>> GetAllPagedAsync(int page)
        {
            int pageSize = 1;
            int pageIndex = page - 1;

            var count = await _context.Authors.CountAsync();

            var authors = await _context.Authors
              .Skip(pageIndex * pageSize)
              .Take(pageSize)
              .ToListAsync();

            PaginatedList<Author> result = new PaginatedList<Author>(authors, count, pageIndex, pageSize);
            return result;
        }

        public async Task<List<Author>?> GetAllAsync()
        {
            return await _context.Authors
                .ToListAsync();
        }

        public async Task<Author> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }


        public async Task DeleteAsync(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
