using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Infrastructure.Persistence.MongoDB.Models;

namespace Bookstore.Infrastructure.Persistence.MongoDB
{
    public interface IComicNoSqlRepository
    {
        Task AddAsync(ComicIssue entry);
        Task<List<ComicIssue>> GetAllAsync();
        Task<ComicIssue?> GetByExternalIdAsync(int externalId);
    }
}
