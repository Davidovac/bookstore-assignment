using System.Security.Claims;
using Google.Apis.Auth;

namespace BookstoreApplication.Services
{
    public interface IGoogleAuthService
    {
        Task<string> LoginWithGoogleAsync(ClaimsPrincipal claimsPrincipal);
    }
}