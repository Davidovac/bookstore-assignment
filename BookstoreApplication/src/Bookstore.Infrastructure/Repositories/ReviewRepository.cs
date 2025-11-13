using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;

namespace Bookstore.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {

        public ReviewRepository(AppDbContext context) : base(context) { }
    }
}