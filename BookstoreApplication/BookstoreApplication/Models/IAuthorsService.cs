namespace BookstoreApplication.Models
{
    public interface IAuthorsService
    {
        Task<List<Author>?> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
