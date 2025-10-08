namespace BookstoreApplication.Models
{
    public interface IAwardsService
    {
        Task<List<Award>> GetAllAsync();
        Task<Award?> GetByIdAsync(int id);
        Task<Award> AddAsync(Award award);
        Task<Award> UpdateAsync(Award award);
        Task DeleteAsync(int id);
    }
}
