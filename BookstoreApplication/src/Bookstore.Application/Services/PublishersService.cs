using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.PublisherEntities;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Entities.UserEntities;
using Bookstore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Application.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublishersService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PublisherDto>> GetAllAsync(int sort)
        {
            var publishers = await _unitOfWork.Publishers.GetAllSortedAsync(sort);
            if (publishers == null)
            {
                throw new Exception("No publishers found");
            }
            return _mapper.Map<List<PublisherDto>>(publishers);
        }

        public async Task<PublisherDto?> GetByIdAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetOneAsync(id);
            if (publisher == null)
            {
                throw new NotFoundException($"Publisher with id: {id} not found");
            }
            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task<PublisherDto> AddAsync(Publisher publisher)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Publishers.AddAsync(publisher);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task<PublisherDto> UpdateAsync(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }

            await GetByIdAsync(id);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Publishers.Update(publisher);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task DeleteAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetOneAsync(id);
            if (publisher == null)
            {
                throw new NotFoundException($"Publisher with id: {id} not found");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Publishers.Delete(publisher);
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
