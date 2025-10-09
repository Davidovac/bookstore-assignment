using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _repository;
        private readonly IMapper _mapper;

        public BooksService(IBooksRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BookDto>?> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();
            if (books == null)
            {
                throw new Exception("No books found");
            }
            try
            {
                return _mapper.Map<List<BookDto>>(books);
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping books", ex);
            }
        }

        public async Task<BookDetailsDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            return _mapper.Map<BookDetailsDto>(book);
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
            Book? book = await _repository.GetBookAsync(id);
            if (book == null)
            {
                throw new ArgumentException($"Book with id {id} not found");
            }
            await _repository.DeleteAsync(book);
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _repository.GetBookAsync(id);
        }
    }
}
