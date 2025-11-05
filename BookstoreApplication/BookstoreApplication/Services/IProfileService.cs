using System.Security.Claims;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IProfileService
    {
        Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal);
    }
}
