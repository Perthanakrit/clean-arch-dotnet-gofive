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

            return services;
        }
    }
}
