using Microsoft.AspNetCore.Identity;

namespace Bookstore.Domain.Entities.UserEntities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
