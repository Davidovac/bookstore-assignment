using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Entities.UserEntities;

namespace Bookstore.Domain.Entities.ReviewEntities
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
