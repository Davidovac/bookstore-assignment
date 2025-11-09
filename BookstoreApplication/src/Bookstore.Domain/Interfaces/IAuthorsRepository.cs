using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.Common;

namespace Bookstore.Domain.Interfaces
{
    public interface IAuthorsRepository
    {
        Task<Author?> GetByIdAsync(int id);
        Task<PaginatedList<Author>> GetAllPagedAsync(int page);
        Task<List<Author>?> GetAllAsync();
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
