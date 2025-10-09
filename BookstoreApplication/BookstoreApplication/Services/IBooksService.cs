using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IBooksService
    {
        Task<List<BookDto>?> GetAllAsync();
        Task<BookDetailsDto?> GetByIdAsync(int id);
        Task<Book> AddAsync(BookSimpleDto dto, Publisher publisher, Author author);
        Task<Book> UpdateAsync(int id, BookSimpleDto dto);
        Task DeleteAsync(int id);
    }
}
