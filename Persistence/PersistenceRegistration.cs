using Application.Contracts.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));
            services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(connection));

            services.AddIdentityConfiguration();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IBlogImageRepository, BlogImageRepository>();

            // AddScoped is used to create a new instance of the repository for each request
            // AddTransient is used to create a new instance of the repository for each time it is requested
            // AddSingleton is used to create a single instance of the repository for the lifetime of the application
            return services;
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddDataProtection();

            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("bootcamp")
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            return services;
        }
    }
}
