using Bookstore.Application.Interfaces;
using Bookstore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorsService, AuthorsService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAwardsService, AwardsService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPublishersService, PublishersService>();
            services.AddScoped<IComicsService, ComicsService>();

            return services;
        }
    }
}

