namespace BookstoreApplication.DTOs
{
    public class BookSimpleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; }

        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
    }
}
