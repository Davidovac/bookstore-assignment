using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.Common;

namespace Bookstore.Application.Interfaces
{
    public interface IAuthorsService
    {
        Task<PaginatedList<AuthorDto>?> GetAllAsync(int page);
        Task<List<AuthorNameDto>?> GetAllNamesAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(int id, Author author);
        Task DeleteAsync(int id);
    }
}
