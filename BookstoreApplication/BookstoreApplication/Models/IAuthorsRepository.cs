using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IAuthorsRepository
    {
        Task<Author?> GetByIdAsync(int id);
        Task<PaginatedList<AuthorDto>> GetAllPagedAsync(int page);
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
