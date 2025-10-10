namespace BookstoreApplication.Models
{
    public interface IPublishersRepository
    {
        Task<Publisher?> GetByIdAsync(int id);
        Task<List<Publisher>?> GetAllAsync(int sort);
        Task<Publisher> AddAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(Publisher publisher);
        Task DeleteAsync(Publisher publisher);
    }
}
