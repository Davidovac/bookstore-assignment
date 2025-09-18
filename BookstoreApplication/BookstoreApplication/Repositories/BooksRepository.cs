using BookstoreApplication.Data;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class BooksRepository
    {
        private AppDbContext _context;
        public BooksRepository(AppDbContext context)
        {
            _context = context;
        }

        public Book GetBook(int id)
        {
            return _context.Books.Find(id);
        }

        public BookDto GetById(int id)
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PageCount = b.PageCount,
                    PublishedDate = b.PublishedDate,
                    ISBN = b.ISBN,
                    Author = new AuthorDto
                    {
                        Id = b.Author.Id,
                        FullName = b.Author.FullName,
                        Biography = b.Author.Biography,
                        DateOfBirth = b.Author.DateOfBirth
                    },
                    Publisher = new PublisherDto
                    {
                        Id = b.Publisher.Id,
                        Name = b.Publisher.Name,
                        Address = b.Publisher.Address,
                        Website = b.Publisher.Website
                    }
                })
                .FirstOrDefault(b => b.Id == id);
        }

        public List<BookDto> GetAll()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PageCount = b.PageCount,
                    PublishedDate = b.PublishedDate,
                    ISBN = b.ISBN,
                    Author = new AuthorDto
                    {
                        Id = b.Author.Id,
                        FullName = b.Author.FullName,
                        Biography = b.Author.Biography,
                        DateOfBirth = b.Author.DateOfBirth
                    },
                    Publisher = new PublisherDto
                    {
                        Id = b.Publisher.Id,
                        Name = b.Publisher.Name,
                        Address = b.Publisher.Address,
                        Website = b.Publisher.Website
                    }
                })
                .ToList();
        }

        public Book Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book Update(int id, Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }


        public void Delete(int id)
        {
            Book? book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
