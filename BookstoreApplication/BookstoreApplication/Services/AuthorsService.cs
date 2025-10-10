using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using Humanizer;

namespace BookstoreApplication.Services
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepository _repository;
        private readonly ILogger<AuthorsService> _logger;

        public AuthorsService(IAuthorsRepository repository, ILogger<AuthorsService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PaginatedList<AuthorDto>?> GetAllAsync(int page)
        {
            if (page < 1)
            {
                page = 1;
            }
            var authors = await _repository.GetAllPagedAsync(page);
            if (authors == null)
            {
                throw new Exception("No authors found");
            }
            return authors;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
            {
                throw new NotFoundException(id);
            }
            return author;
        }

        public async Task<Author> AddAsync(Author author)
        {
            return await _repository.AddAsync(author);
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            if (id != author.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }
            await GetByIdAsync(id); // Ensure the author exists

            return await _repository.UpdateAsync(author);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await GetByIdAsync(id);
            await _repository.DeleteAsync(author);
        }

    }
}
