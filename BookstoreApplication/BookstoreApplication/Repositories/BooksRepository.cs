using System.Threading.Tasks;
using BookstoreApplication.Data;
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

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PageCount = b.PageCount,
                    PublishedDate = b.PublishedDate,
                    ISBN = b.ISBN,
                    AuthorId = b.AuthorId,
                    Author = new AuthorDto
                    {
                        Id = b.Author.Id,
                        FullName = b.Author.FullName,
                        Biography = b.Author.Biography,
                        DateOfBirth = b.Author.DateOfBirth
                    },
                    PublisherId = b.PublisherId,
                    Publisher = new PublisherDto
                    {
                        Id = b.Publisher.Id,
                        Name = b.Publisher.Name,
                        Address = b.Publisher.Address,
                        Website = b.Publisher.Website
                    }
                })
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<BookDto>?> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PageCount = b.PageCount,
                    PublishedDate = b.PublishedDate,
                    ISBN = b.ISBN,
                    AuthorId = b.AuthorId,
                    Author = new AuthorDto
                    {
                        Id = b.Author.Id,
                        FullName = b.Author.FullName,
                        Biography = b.Author.Biography,
                        DateOfBirth = b.Author.DateOfBirth
                    },
                    PublisherId = b.PublisherId,
                    Publisher = new PublisherDto
                    {
                        Id = b.Publisher.Id,
                        Name = b.Publisher.Name,
                        Address = b.Publisher.Address,
                        Website = b.Publisher.Website
                    }
                })
                .ToListAsync();
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


        public async Task DeleteAsync(int id)
        {
            Book? book = _context.Books.Find(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
