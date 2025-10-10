using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IBooksService
    {
        Task<List<BookDto>?> GetAllAsync(int sort, BookFilterMix filterMix);
        Task<BookDetailsDto?> GetByIdAsync(int id);
        Task<BookDetailsDto> CreateAndLinkAsync(BookSimpleDto dto);
        Task<BookDetailsDto> UpdateAsync(int id, BookSimpleDto dto);
        Task DeleteAsync(int id);
    }
}
