namespace BookstoreApplication.Models
{
    public class Award
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int YearBegan { get; set; }
        public ICollection<AwardAuthor> AwardAuthors { get; set; } = new List<AwardAuthor>();
    }
}
