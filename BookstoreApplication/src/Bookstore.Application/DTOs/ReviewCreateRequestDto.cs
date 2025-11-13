using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.DTOs
{
    public class ReviewCreateRequestDto
    {
        public int BookId { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
