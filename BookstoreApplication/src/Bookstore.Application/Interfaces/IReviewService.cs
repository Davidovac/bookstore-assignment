using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.UserEntities;

namespace Bookstore.Application.Interfaces
{
    public interface IReviewService
    {
        Task AddAsync(ClaimsPrincipal claimsPrincipal, ReviewCreateRequestDto reviewDto);
    }
}
