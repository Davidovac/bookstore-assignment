using System.Net;
using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        public BooksRepository(AppDbContext context) : base(context) { }

        public async Task<List<Book>?> GetPagedAsync(int sort, BookFilterMix filterMix)
        {
            IQueryable<Book> query = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);
            query = ApplySorting(query, sort);
            query = ApplyFilters(query, filterMix);
            query = query.Take(40);
            return await query.ToListAsync();
        }

        public async Task UpdateAvgRating(int bookId, double newRating)
        {
            await _dbContext.Books
                .Where(b => b.Id == bookId)
                .ExecuteUpdateAsync(s => s.SetProperty(b => b.AvgRating, newRating));
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
