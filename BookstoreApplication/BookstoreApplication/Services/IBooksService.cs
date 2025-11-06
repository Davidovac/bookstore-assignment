using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IBooksService
    {
        Task<List<BookDto>?> GetAllAsync(int sort, BookFilterMix filterMix);
        Task<BookDetailsDto?> GetByIdAsync(int id);
        Task<BookDetailsDto> CreateAndLinkAsync(BookRequestDto dto);
        Task<BookDetailsDto> UpdateAsync(int id, BookRequestDto dto);
        Task DeleteAsync(int id);
    }
}
