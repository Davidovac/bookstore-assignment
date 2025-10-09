namespace BookstoreApplication.Models
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
