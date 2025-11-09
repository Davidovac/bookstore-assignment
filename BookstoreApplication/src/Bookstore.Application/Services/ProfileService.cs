using System.Security.Claims;
using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public ProfileService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal)
        {
            // Preuzimanje korisničkog imena iz tokena
            var username = userPrincipal.FindFirstValue("username");

            if (username == null)
            {
                throw new BadRequestException("Token is invalid");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new NotFoundException("User with provided username does not exist");
            }
            var roles = await _userManager.GetRolesAsync(user);
            

            var profile = _mapper.Map<ProfileDto>(user);
            profile.Roles = roles;
            return profile;
        }
    }
}
