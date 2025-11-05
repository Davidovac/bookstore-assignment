using BookstoreApplication.Models;

namespace BookstoreApplication.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
