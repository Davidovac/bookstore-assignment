using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Infrastructure.Persistence.MongoDB.Models;
using MongoDB.Bson;

namespace Bookstore.Infrastructure.Mappings
{
    public static class ComicIssueMapper
    {
        public static ComicIssueDocument ToDocument(this ComicIssue entity)
        {
            return new ComicIssueDocument
            {
                Id = ObjectId.GenerateNewId(),
                ExternalIssueId = entity.ExternalIssueId,
                CreatedAt = entity.CreatedAt,
                PagesCount = entity.PagesCount,
                Price = entity.Price,
                CopiesAvailable = entity.CopiesAvailable,
                Description = entity.Description,
                Name = entity.Name,
                Image = entity.Image,
                CoverDate = entity.CoverDate,
                IssueNumber = entity.IssueNumber
            };
        }

        public static ComicIssue ToDomain(this ComicIssueDocument document)
        {
            return new ComicIssue
            {
                Id = document.Id != ObjectId.Empty ? 0 : 0,
                ExternalIssueId = document.ExternalIssueId,
                CreatedAt = document.CreatedAt,
                PagesCount = document.PagesCount,
                Price = document.Price,
                CopiesAvailable = document.CopiesAvailable,
                Description = document.Description,
                Name = document.Name,
                Image = document.Image,
                CoverDate = document.CoverDate,
                IssueNumber = document.IssueNumber
            };
        }
    }
}
