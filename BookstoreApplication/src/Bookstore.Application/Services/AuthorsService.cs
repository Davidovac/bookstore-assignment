using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.AwardEntities;
using Bookstore.Domain.Entities.Common;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Entities.UserEntities;
using Bookstore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bookstore.Application.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthorsService> _logger;
        private readonly IMapper _mapper;

        public AuthorsService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper, ILogger<AuthorsService> logger)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedList<AuthorDto>?> GetPagedAsync(int page)
        {
            if (page < 1)
            {
                page = 1;
            }
            var response = await _unitOfWork.Authors.GetPagedAsync(page);
            if (response.Items == null)
            {
                throw new Exception("No authors found");
            }
            var authorsDto = _mapper.Map<List<AuthorDto>>(response.Items);
            PaginatedList<AuthorDto> result = new PaginatedList<AuthorDto>(authorsDto, response.Count, response.PageIndex, response.PageSize);
            return result;
        }

        public async Task<List<AuthorNameDto>?> GetAllNamesAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            if (authors == null)
            {
                throw new Exception("No authors found");
            }
            return _mapper.Map<List<AuthorNameDto>>(authors);
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            
            var author = await _unitOfWork.Authors.GetOneAsync(id);
            if (author == null)
            {
                throw new NotFoundException($"Author with id: {id} not found");
            }
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> AddAsync(Author author)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var response = await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> UpdateAsync(int id, Author author)
        {
            if (id != author.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }
            await GetByIdAsync(id); // Ensure the author exists
            
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Authors.Update(author);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetOneAsync(id);
            if (author == null) throw new NotFoundException("Author nije pronadjen");

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Authors.Delete(author);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

    }
}
