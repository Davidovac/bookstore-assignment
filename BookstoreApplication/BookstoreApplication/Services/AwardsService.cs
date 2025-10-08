using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using NuGet.Protocol.Core.Types;

namespace BookstoreApplication.Services
{
    public class AwardsService : IAwardsService
    {
        private IAwardsRepository _repository;

        public AwardsService(IAwardsRepository repository)
        {
            _repository = repository;
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
