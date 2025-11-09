using Bookstore.Domain.Entities.BookEntities;

namespace Bookstore.Domain.Interfaces
{
    public interface IBooksRepository
    {
        Task<Book?> GetBookAsync(int id);
        Task<Book?> GetByIdAsync(int id);
        Task<List<Book>?> GetAllAsync(int sort, BookFilterMix filterMix);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task DeleteAsync(Book book);
    }
}
