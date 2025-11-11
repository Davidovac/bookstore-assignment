using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Infrastructure.Exceptions
{
    public class RateLimitException : Exception
    {
        public RateLimitException() : base("Rate limit exceeded. Please try again later.")
        {
        }
    }
}
