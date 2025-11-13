using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.AwardEntities;

namespace Bookstore.Application.Interfaces
{
    public interface IAwardsService
    {
        Task<List<AwardDto>> GetAllAsync();
        Task<AwardDto?> GetByIdAsync(int id);
        Task<AwardDto> AddAsync(Award award);
        Task<AwardDto> UpdateAsync(int id, Award award);
        Task DeleteAsync(int id);
    }
}
