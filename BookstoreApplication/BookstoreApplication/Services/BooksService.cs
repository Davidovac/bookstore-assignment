using System.Security.Policy;
using BookstoreApplication.Data;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Humanizer;
using NuGet.Protocol.Core.Types;

namespace BookstoreApplication.Services
{
    public class BooksService : IBooksService
    {
        private IBooksRepository _repository;

        public BooksService(IBooksRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookDto>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Book> AddAsync(BookSimpleDto dto, Models.Publisher publisher, Author author)
        {
            Book book = new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                PageCount = dto.PageCount,
                PublishedDate = dto.PublishedDate.ToUniversalTime(),
                ISBN = dto.ISBN,
                AuthorId = dto.AuthorId,
                Author = author,
                PublisherId = dto.PublisherId,
                Publisher = publisher
            };
            await _repository.AddAsync(book);
            return book;
        }

        public async Task<Book> UpdateAsync(int id, BookSimpleDto dto)
        {
            var book = await GetBookAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            book.Title = dto.Title;
            book.PageCount = dto.PageCount;
            book.PublishedDate = dto.PublishedDate.ToUniversalTime();
            book.ISBN = dto.ISBN;
            book.AuthorId = dto.AuthorId;
            book.PublisherId = dto.PublisherId;
            await _repository.UpdateAsync(book);
            return book;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _repository.GetBookAsync(id);
        }
    }
}
