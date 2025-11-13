using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.PublisherEntities;

namespace Bookstore.Application.Interfaces
{
    public interface IPublishersService
    {
        Task<List<PublisherDto>?> GetAllAsync(int sort);
        Task<PublisherDto?> GetByIdAsync(int id);
        Task<PublisherDto> AddAsync(Publisher publisher);
        Task<PublisherDto> UpdateAsync(int id, Publisher publisher);
        Task DeleteAsync(int id);
    }
}
