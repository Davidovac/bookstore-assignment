using System.Security.Claims;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Application.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public GoogleAuthService(UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }
        public async Task<string> LoginWithGoogleAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ExternalLoginException("Invalid Google login, ClaimsPrincipal is null");
            }

            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                throw new ExternalLoginException("Email claim not found from Google login");
            }
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                
                var newUser = new User
                {
                    UserName = email,
                    Email = email,
                    Name = claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value ?? string.Empty,
                    Surname = claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value ?? string.Empty,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newUser);

                if (!result.Succeeded)
                {
                    string errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new ExternalLoginException($"Failed to create user from Google login: {errorMessage}");
                }
                await _userManager.AddToRoleAsync(newUser, "Librarian");
                user = newUser;
            }

            /*var info = new UserLoginInfo("Google", claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? string.Empty, "Google");
            var loginResult = await _userManager.AddLoginAsync(user, info);*/

            return await _authService.GenerateJwt(user);
        }
    }
}