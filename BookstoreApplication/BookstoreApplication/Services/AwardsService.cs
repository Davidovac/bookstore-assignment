using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class AwardsService
    {
        private AppDbContext _context;
        private AwardsRepository _repository;

        public AwardsService(AppDbContext context)
        {
            _context = context;
            _repository = new AwardsRepository(_context);
        }

        public async Task<List<Award>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Award?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Award> AddAsync(Award award)
        {
            return await _repository.AddAsync(award);
        }

        public async Task<Award> UpdateAsync(Award award)
        {
            return await _repository.UpdateAsync(award);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
