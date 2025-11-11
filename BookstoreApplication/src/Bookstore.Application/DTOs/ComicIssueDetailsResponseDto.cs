using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.DTOs
{
    public class ComicIssueDetailsResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CoverDate { get; set; } //IssueDate
        public string IssueNumber { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public int ExternalIssueId { get; set; }
        public int PagesCount { get; set; }
        public double Price { get; set; }
        public int CopiesAvailable { get; set; }

        public ComicVolumeResponseDto? Volume { get; set; }
    }
}
