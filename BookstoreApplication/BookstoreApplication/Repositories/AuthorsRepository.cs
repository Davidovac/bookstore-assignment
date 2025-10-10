using System.Drawing.Printing;
using System.Threading.Tasks;
using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<List<AuthorNameDto>?> GetAllNamesAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorNameDto
                { 
                    Id = a.Id,
                    FullName = a.FullName
                })
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
