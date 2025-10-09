using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
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
