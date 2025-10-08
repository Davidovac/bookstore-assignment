using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories
{
    public interface IPublishersRepository
    {
        Task<Publisher?> GetByIdAsync(int id);
        Task<List<Publisher>?> GetAllAsync();
        Task<Publisher> AddAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(Publisher publisher);
        Task DeleteAsync(Publisher publisher);
    }
}
