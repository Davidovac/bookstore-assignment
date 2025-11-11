using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Infrastructure.Exceptions
{
    public class UnauthorizedApiAccessException : Exception
    {
        public UnauthorizedApiAccessException()
            : base("Unauthorized access to the external API.")
        {
        }
    }
}
