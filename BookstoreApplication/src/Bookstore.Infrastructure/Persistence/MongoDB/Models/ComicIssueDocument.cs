using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bookstore.Infrastructure.Persistence.MongoDB.Models
{
    public class ComicIssueDocument
    {
        public ObjectId Id { get; set; }
        public int ExternalIssueId { get; set; }
        public string Name { get; set; }
        public string IssueNumber { get; set; }
        public DateTime CoverDate { get; set; }
        public int PagesCount { get; set; }
        public double Price { get; set; }
        public int CopiesAvailable { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
