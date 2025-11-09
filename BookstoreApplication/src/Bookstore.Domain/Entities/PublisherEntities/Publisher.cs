using Bookstore.Domain.Entities.BookEntities;

namespace Bookstore.Domain.Entities.PublisherEntities
{
    public class Publisher
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Website { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
