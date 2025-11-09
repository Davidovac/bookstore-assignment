using System.Security.Claims;
using Bookstore.Application.DTOs;

namespace Bookstore.Application.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal);
    }
}
