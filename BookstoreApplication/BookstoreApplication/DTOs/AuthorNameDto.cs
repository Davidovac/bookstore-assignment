using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.DTOs
{
    public class AuthorNameDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
