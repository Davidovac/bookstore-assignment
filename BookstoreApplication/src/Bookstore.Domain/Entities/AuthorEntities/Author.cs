using Bookstore.Domain.Entities.AwardAuthorEntity;

namespace Bookstore.Domain.Entities.AuthorEntities
{
    public class Author
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<AwardAuthor> AwardAuthors { get; set; } = new List<AwardAuthor>();
    }
}
