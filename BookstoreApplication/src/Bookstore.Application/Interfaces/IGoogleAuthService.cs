using System.Security.Claims;

namespace Bookstore.Application.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<string> LoginWithGoogleAsync(ClaimsPrincipal claimsPrincipal);
    }
}