using Bookstore.Domain.Entities.AwardEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class AwardsRepository : GenericRepository<Award> ,IAwardsRepository
    {
        public AwardsRepository(AppDbContext context) : base(context) { }
    }
}
