using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.PublisherEntities;

namespace Bookstore.Domain.Entities.BookEntities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string ISBN { get; set; }
        public double AvgRating { get; set; } = 0;

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
