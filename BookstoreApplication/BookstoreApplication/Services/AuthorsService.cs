using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;
        private AuthorsRepository _repository;

        public AuthorsService(AppDbContext context)
        {
            _context = context;
            _repository = new AuthorsRepository(_context);
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
