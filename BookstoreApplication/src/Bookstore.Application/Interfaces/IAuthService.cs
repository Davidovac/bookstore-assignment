using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.UserEntities;

namespace Bookstore.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task<string> Login(LoginDto data);
        Task<string> GenerateJwt(User user);
    }
}
