using BookstoreApplication.Models;

namespace BookstoreApplication.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string ISBN { get; set; }

        public int AuthorId { get; set; }
        public AuthorDto Author { get; set; }
        public int PublisherId { get; set; }
        public PublisherDto Publisher { get; set; }
    }
}
