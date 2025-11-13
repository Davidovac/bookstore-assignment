using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.AwardEntities;
using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Entities.UserEntities;
using Bookstore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Application.Services
{
    public class AwardsService : IAwardsService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AwardsService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AwardDto>> GetAllAsync()
        {
            var awards = await _unitOfWork.Awards.GetAllAsync();
            if (awards == null)
            {
                throw new Exception("No awards found");
            }
            return _mapper.Map<List<AwardDto>>(awards) ?? new List<AwardDto>();
        }

        public async Task<AwardDto?> GetByIdAsync(int id)
        {
            var award = await _unitOfWork.Awards.GetOneAsync(id);
            if (award == null)
            {
                throw new NotFoundException($"Award with id: {id} not found");
            }
            return _mapper.Map<AwardDto>(award);
        }

        public async Task<AwardDto> AddAsync(Award award)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Awards.AddAsync(award);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
            return _mapper.Map<AwardDto>(award);
        }

        public async Task<AwardDto> UpdateAsync(int id, Award award)
        {
            if (id != award.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }
            await GetByIdAsync(id); // Ensure the award exists

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Awards.Update(award);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            return _mapper.Map<AwardDto>(award);
        }

        public async Task DeleteAsync(int id)
        {
            var award = await _unitOfWork.Awards.GetOneAsync(id);
            if (award == null) throw new NotFoundException($"Award with id: {id} not found");
            
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Awards.Delete(award);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
