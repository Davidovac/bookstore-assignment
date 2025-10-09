using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IAuthorsService
    {
        Task<List<Author>?> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(int id, Author author);
        Task DeleteAsync(int id);
    }
}
