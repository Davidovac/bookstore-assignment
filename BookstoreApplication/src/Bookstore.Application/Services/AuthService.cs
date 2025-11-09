using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoMapper.Configuration;
using Bookstore.Application.DTOs;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Bookstore.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthService(UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
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
            await _userManager.AddToRoleAsync(user, "Librarian");
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

        public async Task<string> GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("username", user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim("role", role)));

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
