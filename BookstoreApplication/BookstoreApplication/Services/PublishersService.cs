using BookstoreApplication.Data;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        private PublishersRepository _repository;

        public PublishersService(AppDbContext context)
        {
            _context = context;
            _repository = new PublishersRepository(_context);
        }

        public async Task<List<Publisher>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Publisher> AddAsync(PublisherDto dto)
        {
            Publisher publisher = new Publisher
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Website = dto.Website
            };
            return await _repository.AddAsync(publisher);
        }

        public async Task<Publisher> UpdateAsync(PublisherDto dto)
        {
            Publisher publisher = new Publisher
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Website = dto.Website
            };
            return await _repository.UpdateAsync(publisher);
        }

        public async Task DeleteAsync(Publisher publisher)
        {
            await _repository.DeleteAsync(publisher);
        }


    }
}
