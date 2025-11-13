using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Entities.Common;

namespace Bookstore.Domain.Interfaces
{
    public interface IAuthorsRepository : IGenericRepository<Author>
    {
        Task<PaginatedList<Author>> GetPagedAsync(int page);
    }
}
