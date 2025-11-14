using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Infrastructure.Persistence.MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bookstore.Infrastructure.Persistence.MongoDB
{
    public class MongoDbService
    {
        public IMongoDatabase Database { get; }

        public MongoDbService(IOptions<MongoSettings> options)
        {
            var settings = options.Value;

            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.Database);
        }

        public IMongoCollection<ComicIssueDocument> ComicIssues =>
            Database.GetCollection<ComicIssueDocument>("ComicIssues");
    }
}
