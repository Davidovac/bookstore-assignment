using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.ComicVine;
using Bookstore.Domain.ExternalEntities.ComicEntities;

namespace Bookstore.Domain.Interfaces
{
    public interface IExternalComicsService
    {
        Task<ComicVineResponse> Get(string url, bool totalResults = false);
    }
}
