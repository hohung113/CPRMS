using Rms.Domain.Modules.Academic.Interface;
using Rms.Infrastructure.Modules.Academic.Repository;

namespace Rms.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICampusProvider, CampusProvider>();
            services.AddDbContext<RmsDbContext>();
            //var connectionString = configuration.GetConnectionString("EntityFrameworkConnectionString");
            //services.AddDbContext<RmsDbContext>(options =>
            //    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).EnableSensitiveDataLogging());
            //services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            // Seeder
        }
    }
}


