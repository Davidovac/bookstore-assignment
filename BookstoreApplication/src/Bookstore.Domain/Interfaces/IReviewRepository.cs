using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.ReviewEntities;

namespace Bookstore.Domain.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
    }
}
