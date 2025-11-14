using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bookstore.Infrastructure.Persistence.MongoDB
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoSettings>(config.GetSection("MongoSettings"));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.Database);
            });

            services.AddSingleton<MongoDbService>();

            return services;
        }
    }
}
