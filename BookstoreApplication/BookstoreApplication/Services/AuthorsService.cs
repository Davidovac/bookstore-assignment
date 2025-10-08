using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepository _repository;

        public AuthorsService(IAuthorsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Author>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Author> AddAsync(Author author)
        {
            return await _repository.AddAsync(author);
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            return await _repository.UpdateAsync(author);
        }

        public async Task DeleteAsync(Author author)
        {
            await _repository.DeleteAsync(author);
        }

    }
}
