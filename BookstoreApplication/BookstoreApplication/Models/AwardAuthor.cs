namespace BookstoreApplication.Models
{
    public class AwardAuthor
    {
        public int AwardId { get; set; }
        public Award Award { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int Year { get; set; }
    }
}
