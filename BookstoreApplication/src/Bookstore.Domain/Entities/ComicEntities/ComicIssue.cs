using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities.ComicEntities
{
    public class ComicIssue
    {
        public int Id { get; set; }
        public int ExternalIssueId { get; set; }

        public DateTime CreatedAt { get; set; }
        public int PagesCount { get; set; }
        public double Price { get; set; }
        public int CopiesAvailable { get; set; }
        public string? Description { get; set; }

        public string Name { get; set; }
        public string? Image { get; set; }
        public DateTime CoverDate { get; set; } //IssueDate
        public string IssueNumber { get; set; }
    }
}
