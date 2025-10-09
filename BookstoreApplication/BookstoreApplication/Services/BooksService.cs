using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _repository;
        private readonly IAuthorsService _authorsService;
        private readonly IPublishersService _publishersService;
        private readonly IMapper _mapper;

        public BooksService(IBooksRepository repository, IAuthorsService authorsService, IPublishersService publishersService, IMapper mapper)
        {
            _repository = repository;
            _authorsService = authorsService;
            _publishersService = publishersService;
            _mapper = mapper;
        }
        
        public async Task<List<BookDto>?> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();
            if (books == null)
            {
                throw new Exception("No books found");
            }
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<BookDetailsDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                throw new NotFoundException(id);
            }
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var book = await _repository.GetBookAsync(id);
            if (book == null)
            {
                throw new NotFoundException(id);
            }
            return book;
        }

        public async Task<BookDetailsDto> CreateAndLinkAsync(BookSimpleDto dto)
        {
            var author = await _authorsService.GetByIdAsync(dto.AuthorId);
            var publisher = await _publishersService.GetByIdAsync(dto.PublisherId);

            Book book = _mapper.Map<Book>(dto);
            book.Publisher = publisher;
            book.Author = author;
            await _repository.AddAsync(book);
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<BookDetailsDto> UpdateAsync(int id, BookSimpleDto dto)
        {
            if (id != dto.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }
            var bookCheck = await GetBookAsync(id);

            Book book = _mapper.Map<Book>(dto);

            await _repository.UpdateAsync(book);
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task DeleteAsync(int id)
        {
            Book? book = await GetBookAsync(id);
            await _repository.DeleteAsync(book);
        }

    }
}
