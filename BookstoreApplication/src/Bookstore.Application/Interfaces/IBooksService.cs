using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.BookEntities;

namespace Bookstore.Application.Interfaces
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
