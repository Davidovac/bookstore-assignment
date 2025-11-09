using Bookstore.Domain.Entities.PublisherEntities;

namespace Bookstore.Application.Interfaces
{
    public interface IPublishersService
    {
        Task<List<Publisher>?> GetAllAsync(int sort);
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher> AddAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
        Task DeleteAsync(int id);
    }
}
