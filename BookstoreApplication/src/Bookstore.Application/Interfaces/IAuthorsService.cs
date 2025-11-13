using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.Common;

namespace Bookstore.Application.Interfaces
{
    public interface IAuthorsService
    {
        Task<PaginatedList<AuthorDto>?> GetPagedAsync(int page);
        Task<List<AuthorNameDto>?> GetAllNamesAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task<AuthorDto> AddAsync(Author author);
        Task<AuthorDto> UpdateAsync(int id, Author author);
        Task DeleteAsync(int id);
    }
}
