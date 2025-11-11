using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.DTOs
{
    public class ComicIssueSummaryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CoverDate { get; set; } //IssueDate
        public string? IssueNumber { get; set; }
        public string? Image { get; set; }
    }
}
