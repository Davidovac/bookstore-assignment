using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task<string> Login(LoginDto data);
    }
}
