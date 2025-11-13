using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.AwardEntities;

namespace Bookstore.Application.DTOs
{
    public class AwardAuthorDto
    {
        public int AwardId { get; set; }
        public AwardDto? Award { get; set; }

        public int AuthorId { get; set; }
        public AuthorDto? Author { get; set; }

        public int Year { get; set; }
    }
}