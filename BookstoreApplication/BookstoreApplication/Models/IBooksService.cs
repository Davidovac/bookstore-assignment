using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IBooksService
    {
        Task<List<BookDto>?> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<Book> AddAsync(BookSimpleDto dto, Models.Publisher publisher, Author author);
        Task<Book> UpdateAsync(int id, BookSimpleDto dto);
        Task DeleteAsync(int id);
    }
}
