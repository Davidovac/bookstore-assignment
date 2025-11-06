using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookstoreApplication.DTOs
{
    public class BookRequestDto
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
