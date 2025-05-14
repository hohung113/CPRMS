using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rms.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("EntityFrameworkConnectionString");
            //services.AddDbContext<RmsDbContext>(options => 
            //    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).EnableSensitiveDataLogging());
            //services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            // Seeder
        }
    }
}
