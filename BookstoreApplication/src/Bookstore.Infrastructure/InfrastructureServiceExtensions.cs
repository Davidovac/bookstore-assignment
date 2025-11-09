using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistence;
using Bookstore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IPublishersRepository, PublishersRepository>();
        services.AddScoped<IAwardsRepository, AwardsRepository>();

        return services;
    }
}

