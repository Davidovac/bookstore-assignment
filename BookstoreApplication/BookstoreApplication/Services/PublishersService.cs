using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public class PublishersService : IPublishersService
    {
        private IPublishersRepository _repository;

        public PublishersService(IPublishersRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Publisher>?> GetAllAsync(string sort)
        {
            var publishers = await _repository.GetAllAsync(sort);
            if (publishers == null)
            {
                throw new Exception("No publishers found");
            }
            return publishers;
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null)
            {
                throw new NotFoundException(id);
            }
            return publisher;
        }

        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            return await _repository.AddAsync(publisher);
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }

            await GetByIdAsync(id); // Ensure the publisher exists

            return await _repository.UpdateAsync(publisher);
        }

        public async Task DeleteAsync(int id)
        {
            var publisher = await GetByIdAsync(id);
            await _repository.DeleteAsync(publisher);
        }


    }
}
