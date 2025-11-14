using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence.MongoDB.Models;

namespace Bookstore.Infrastructure.Persistence.MongoDB
{
    public class ComicNoSqlService : IComicNoSqlService
    {
        private readonly IComicNoSqlRepository _repository;

        public ComicNoSqlService(IComicNoSqlRepository repository)
        {
            _repository = repository;
        }

        public async Task AddIssueAsync(ComicIssue issueInc)
        {
            try
            {
                await _repository.AddAsync(issueInc);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ComicIssue>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ComicIssue?> GetOneAsync(int externalId)
        {
            try
            {
                return await _repository.GetByExternalIdAsync(externalId);
            }
            catch
            {
                throw;
            }
        }
    }
}
