using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IPublishersService
    {
        Task<List<Publisher>?> GetAllAsync();
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher> AddAsync(PublisherDto dto);
        Task<Publisher> UpdateAsync(PublisherDto dto);
        Task DeleteAsync(Publisher publisher);
    }
}
