using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task RegisterAsync(RegistrationDto data)
        {
            var user = _mapper.Map<User>(data);
            var result = await _userManager.CreateAsync(user, data.Password);
            if (!result.Succeeded)
            {
                string errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errorMessage);
            }
        }

        public async Task<string> Login(LoginDto data)
        {
            var user = await _userManager.FindByNameAsync(data.Username);
            if (user == null)
            {
                throw new BadRequestException("Invalid credentials.");
            }

            // provera da li lozinka odgovara nađenom korisniku
            var passwordMatch = await _userManager.CheckPasswordAsync(user, data.Password);
            if (!passwordMatch)
            {
                throw new BadRequestException("Invalid credentials.");
            }

            var token = await GenerateJwt(user);
            return token;
        }

        private async Task<string> GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("username", user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              issuer: _configuration["Jwt:Issuer"],
              audience: _configuration["Jwt:Audience"],
              claims: claims,
              expires: DateTime.UtcNow.AddDays(1),
              signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
