using BookstoreApplication.Exceptions;
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
            var awards = await _repository.GetAllAsync();
            if (awards == null)
            {
                throw new Exception("No awards found");
            }
            return awards;
        }

        public async Task<Award?> GetByIdAsync(int id)
        {
            var award = await _repository.GetByIdAsync(id);
            if (award == null)
            {
                throw new NotFoundException(id);
            }
            return award;
        }

        public async Task<Award> AddAsync(Award award)
        {
            return await _repository.AddAsync(award);
        }

        public async Task<Award> UpdateAsync(int id, Award award)
        {
            if (id != award.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }
            await GetByIdAsync(id); // Ensure the award exists

            return await _repository.UpdateAsync(award);
        }

        public async Task DeleteAsync(int id)
        {
            var award = await GetByIdAsync(id);
            await _repository.DeleteAsync(award);
        }
    }
}
