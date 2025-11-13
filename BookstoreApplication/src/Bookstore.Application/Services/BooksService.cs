using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Bookstore.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksService> _logger;

        public BooksService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BooksService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BookDto>?> GetAllAsync(int sort, BookFilterMix filterMix)
        {
            _logger.LogInformation($"Check if there are any books");
            var books = await _unitOfWork.Books.GetPagedAsync(sort, filterMix);
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
            var book = await _unitOfWork.Books.GetOneAsync(id);
            if (book == null)
            {
                _logger.LogError($"Book with id {id} does not exist.");
                throw new NotFoundException($"Book with id: {id} not found");
            }
            _logger.LogInformation($"Book with id {id} found.");
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<BookDetailsDto> AddAsync(BookRequestDto dto)
        {
            var author = await _unitOfWork.Authors.GetOneAsync(dto.AuthorId);
            if (author == null) throw new NotFoundException($"Author with id {dto.AuthorId} not found");
            var publisher = await _unitOfWork.Publishers.GetOneAsync(dto.PublisherId);
            if (publisher == null) throw new NotFoundException($"Author with id {dto.PublisherId} not found");

            Book book = _mapper.Map<Book>(dto);
            book.Publisher = publisher;
            book.Author = author;
            _logger.LogInformation($"Check if book is added.");
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Books.AddAsync(book);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            _logger.LogInformation($"Book added.");
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<BookDetailsDto> UpdateAsync(int id, BookRequestDto dto)
        {
            _logger.LogInformation($"Check if sent book's ids match.");
            if (id != dto.Id)
            {
                _logger.LogError($"Sent book's ids do not match.");
                throw new BadRequestException("Identifier value is invalid.");
            }
            _logger.LogInformation($"Book's ids match.");
            _logger.LogInformation($"Check if book with id {id} exists.");
            
            _logger.LogInformation($"Book with id {id} exists.");

            var book = await _unitOfWork.Books.GetOneAsync(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            _mapper.Map(dto, book);

            _logger.LogInformation($"Check if book will update successfully.");

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Books.Update(book);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            _logger.LogInformation($"Book updated successfully.");
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _unitOfWork.Books.GetOneAsync(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Books.Delete(book);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
            _logger.LogInformation($"Book with id {id} deleted successfully.");
        }

    }
}
