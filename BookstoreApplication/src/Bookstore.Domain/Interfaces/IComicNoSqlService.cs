using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.ComicEntities;

namespace Bookstore.Domain.Interfaces
{
    public interface IComicNoSqlService
    {
        Task AddIssueAsync(ComicIssue issueInc);
        Task<List<ComicIssue>> GetAllAsync();
        Task<ComicIssue?> GetOneAsync(int externalId);
    }
}
