using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Entities.PublisherEntities;

namespace Bookstore.Domain.Interfaces
{
    public interface IPublishersRepository : IGenericRepository<Publisher>
    {
        Task<List<Publisher>?> GetAllSortedAsync(int sort);
    }
}
