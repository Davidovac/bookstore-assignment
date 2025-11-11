using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.DTOs
{
    public class ComicIssueCreateDto
    {
        public int PagesCount { get; set; }
        public double Price { get; set; }
        public int CopiesAvailable { get; set; }
        public string? Description { get; set; }
    }
}
