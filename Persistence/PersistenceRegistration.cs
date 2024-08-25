using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Repositories;

namespace Persistence {
    public static class PersistenceRegistration {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, string connection) {


            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IBlogImageRepository, BlogImageRepository>();
            // AddScoped is used to create a new instance of the repository for each request
            // AddTransient is used to create a new instance of the repository for each time it is requested
            // AddSingleton is used to create a single instance of the repository for the lifetime of the application
            
            return services;
        }
    }
}
