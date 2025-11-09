using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.AwardEntities;

namespace Bookstore.Domain.Entities.AwardAuthorEntity
{
    public class AwardAuthor
    {
        public int AwardId { get; set; }
        public Award? Award { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public int Year { get; set; }
    }
}
