using Bookstore.Domain.Entities.BookEntities;

namespace Bookstore.Domain.Interfaces
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task<List<Book>?> GetPagedAsync(int sort, BookFilterMix filterMix);
        Task UpdateAvgRating(int bookId, double newRating);
    }
}
