using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.DTOs
{
    public class ReviewCreateRequestDto
    {
        public int BookId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be a number between 1 and 5")]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
