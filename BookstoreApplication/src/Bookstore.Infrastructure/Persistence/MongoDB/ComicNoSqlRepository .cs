using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Infrastructure.Mappings;
using Bookstore.Infrastructure.Persistence.MongoDB.Models;
using MongoDB.Driver;

namespace Bookstore.Infrastructure.Persistence.MongoDB
{
    public class ComicNoSqlRepository : IComicNoSqlRepository
    {
        private readonly IMongoCollection<ComicIssueDocument> _issues;

        public ComicNoSqlRepository(MongoDbService mongo)
        {
            _issues = mongo.ComicIssues;
        }

        public async Task AddAsync(ComicIssue entry)
        {
            var doc = entry.ToDocument();
            await _issues.InsertOneAsync(doc);
        }

        public async Task<List<ComicIssue>> GetAllAsync()
        {
            var docs = await _issues.Find(_ => true).ToListAsync();
            var result = docs.Select(d => d.ToDomain()).ToList();
            return result;
        }

        public async Task<ComicIssue?> GetByExternalIdAsync(int externalId)
        {
            var doc = await _issues
                .Find(x => x.ExternalIssueId == externalId)
                .FirstOrDefaultAsync();

            return doc?.ToDomain();
        }
    }
}
