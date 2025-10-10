using System.Threading.Tasks;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
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

        public async Task<List<Book>?> GetAllAsync(int sort)
        {
            IQueryable<Book> query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);
            query = ApplySorting(query, sort);
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
    }
}
