namespace Bookstore.Domain.Entities.BookEntities
{
    public class BookFilterMix
    {
        public string? Title { get; set; }
        public DateTime? FromPublished { get; set; }
        public DateTime? ToPublished { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? FromBirthDate { get; set; }
        public DateTime? ToBirthDate { get; set; }
    }
}
