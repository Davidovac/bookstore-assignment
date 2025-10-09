namespace BookstoreApplication.Models
{
    public interface IAuthorsRepository
    {
        Task<Author?> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
