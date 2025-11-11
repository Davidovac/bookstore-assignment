using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Infrastructure.Exceptions
{
    public class ApiCommunicationException : Exception
    {
        public ApiCommunicationException(string message) : base(message)
        {
        }
    }
}
