using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence.PostgreSQL;
using Bookstore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bookstore.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public IAuthorsRepository Authors { get; private set; }
        public IAwardsRepository Awards { get; private set; }
        public IBooksRepository Books { get; private set; }
        public IComicsRepository Comics { get; private set; }
        public IPublishersRepository Publishers { get; private set; }
        public IReviewRepository Reviews { get; private set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Authors = new AuthorsRepository(dbContext);
            Awards = new AwardsRepository(dbContext);
            Books = new BooksRepository(dbContext);
            Comics = new ComicsRepository(dbContext);
            Publishers = new PublishersRepository(dbContext);
            Reviews = new ReviewRepository(dbContext);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction ??= await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                await _transaction!.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();

            await DisposeTransactionAsync();
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public Task<int> CompleteAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }
    }
}
