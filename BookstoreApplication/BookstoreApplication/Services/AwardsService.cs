using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;
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
            Award? award = await _repository.GetByIdAsync(id);
            if (award == null)
            {
                throw new ArgumentException($"Award with id {id} not found");
            }
            await _repository.DeleteAsync(award);
        }
    }
}
