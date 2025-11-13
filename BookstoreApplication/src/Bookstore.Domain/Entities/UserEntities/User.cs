using Bookstore.Domain.Entities.ReviewEntities;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Domain.Entities.UserEntities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
