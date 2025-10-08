using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories
{
    public interface IBooksRepository
    {
        Task<Book?> GetBookAsync(int id);
        Task<BookDto?> GetByIdAsync(int id);
        Task<List<BookDto>?> GetAllAsync();
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}
