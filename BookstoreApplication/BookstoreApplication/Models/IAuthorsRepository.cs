using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IAuthorsRepository
    {
        Task<Author?> GetByIdAsync(int id);
        Task<PaginatedList<Author>> GetAllPagedAsync(int page);
        Task<List<AuthorNameDto>> GetAllNamesAsync();
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
