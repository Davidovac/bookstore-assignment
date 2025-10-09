using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IPublishersService
    {
        Task<List<Publisher>?> GetAllAsync();
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher> AddAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
        Task DeleteAsync(int id);
    }
}
