using Bookstore.Domain.Entities.AwardEntities;

namespace Bookstore.Domain.Interfaces
{
    public interface IAwardsRepository
    {
        Task<Award?> GetByIdAsync(int id);
        Task<List<Award>> GetAllAsync();
        Task<Award> AddAsync(Award award);
        Task<Award> UpdateAsync(Award award);
        Task DeleteAsync(Award award);
    }
}
