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
        private readonly ILogger<BooksService> _logger;

        public BooksService(IBooksRepository repository, IAuthorsService authorsService, IPublishersService publishersService, IMapper mapper, ILogger<BooksService> logger)
        {
            _repository = repository;
            _authorsService = authorsService;
            _publishersService = publishersService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BookDto>?> GetAllAsync(int sort, BookFilterMix filterMix)
        {
            _logger.LogInformation($"Check if there are any books");
            var books = await _repository.GetAllAsync(sort, filterMix);
            if (books == null)
            {
                _logger.LogError($"No books found.");
                throw new Exception("No books found");
            }
            _logger.LogInformation($"Books found.");
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<BookDetailsDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Check if book with id {id} exists.");
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                _logger.LogError($"Book with id {id} does not exist.");
                throw new NotFoundException(id);
            }
            _logger.LogInformation($"Book with id {id} found.");
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<Book> GetBookAsync(int id)
        {
            _logger.LogInformation($"Check if book with id {id} exists.");
            var book = await _repository.GetBookAsync(id);
            if (book == null)
            {
                _logger.LogError($"Book with id {id} does not exist.");
                throw new NotFoundException(id);
            }
            _logger.LogInformation($"Book with id {id} found.");
            return book;
        }

        public async Task<BookDetailsDto> CreateAndLinkAsync(BookSimpleDto dto)
        {
            var author = await _authorsService.GetByIdAsync(dto.AuthorId);
            var publisher = await _publishersService.GetByIdAsync(dto.PublisherId);

            Book book = _mapper.Map<Book>(dto);
            book.Publisher = publisher;
            book.Author = author;
            _logger.LogInformation($"Check if book is added.");
            await _repository.AddAsync(book);
            _logger.LogInformation($"Book added.");
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<BookDetailsDto> UpdateAsync(int id, BookSimpleDto dto)
        {
            _logger.LogInformation($"Check if sent book's ids match.");
            if (id != dto.Id)
            {
                _logger.LogError($"Sent book's ids do not match.");
                throw new BadRequestException("Identifier value is invalid.");
            }
            _logger.LogInformation($"Book's ids match.");
            _logger.LogInformation($"Check if book with id {id} exists.");
            await GetBookAsync(id);
            _logger.LogInformation($"Book with id {id} exists.");

            Book book = _mapper.Map<Book>(dto);


            _logger.LogInformation($"Check if book will update successfully.");
            await _repository.UpdateAsync(book);
            _logger.LogInformation($"Book updated successfully.");
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task DeleteAsync(int id)
        {
            Book? book = await GetBookAsync(id);
            await _repository.DeleteAsync(book);
            _logger.LogInformation($"Book with id {id} deleted successfully.");
        }

    }
}
