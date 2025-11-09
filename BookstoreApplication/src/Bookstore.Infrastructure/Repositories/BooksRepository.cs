using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private AppDbContext _context;
        public BooksRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>?> GetAllAsync(int sort, BookFilterMix filterMix)
        {
            IQueryable<Book> query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);
            query = ApplySorting(query, sort);
            query = ApplyFilters(query, filterMix);
            return await query.ToListAsync();
        }

        public async Task<Book?> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }


        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        private static IQueryable<Book> ApplySorting(IQueryable<Book> query, int sort)
        {
            return sort switch
            {
                (int)BookSortTypes.TITLE_DESC => query.OrderByDescending(b => b.Title),
                (int)BookSortTypes.DATE_ASC => query.OrderBy(b => b.PublishedDate),
                (int)BookSortTypes.DATE_DESC => query.OrderByDescending(b => b.PublishedDate),
                (int)BookSortTypes.AUTHOR_NAME_ASC => query.OrderBy(b => b.Author.FullName),
                (int)BookSortTypes.AUTHOR_NAME_DESC => query.OrderByDescending(b => b.Author.FullName),
                _ => query.OrderBy(b => b.Title),
            };
        }

        private static IQueryable<Book> ApplyFilters(IQueryable<Book> books, BookFilterMix filterMix)
        {
            if (!string.IsNullOrEmpty(filterMix.Title))
            {
                books = books.Where(b => b.Title.ToLower().Contains(filterMix.Title.ToLower()));
            }

            if (filterMix.FromPublished != null)
            {
                books = books.Where(b => b.PublishedDate >= filterMix.FromPublished);
            }

            if (filterMix.ToPublished != null)
            {
                books = books.Where(b => b.PublishedDate <= filterMix.ToPublished);
            }

            if (filterMix.AuthorId != null)
            {
                books = books.Where(b => b.AuthorId == filterMix.AuthorId);
            }

            if (!string.IsNullOrEmpty(filterMix.AuthorName))
            {
                books = books.Where(b => b.Author.FullName.ToLower().Contains(filterMix.AuthorName.ToLower()));
            }

            if (filterMix.FromBirthDate != null)
            {
                books = books.Where(b => b.Author.DateOfBirth >= filterMix.FromBirthDate);
            }

            if (filterMix.ToBirthDate != null)
            {
                books = books.Where(b => b.Author.DateOfBirth <= filterMix.ToBirthDate);
            }

            return books;
        }
    }
}
