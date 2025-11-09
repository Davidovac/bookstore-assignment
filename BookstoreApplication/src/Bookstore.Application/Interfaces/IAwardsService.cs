using Bookstore.Domain.Entities.AwardEntities;

namespace Bookstore.Application.Interfaces
{
    public interface IAwardsService
    {
        Task<List<Award>> GetAllAsync();
        Task<Award?> GetByIdAsync(int id);
        Task<Award> AddAsync(Award award);
        Task<Award> UpdateAsync(int id, Award award);
        Task DeleteAsync(int id);
    }
}
