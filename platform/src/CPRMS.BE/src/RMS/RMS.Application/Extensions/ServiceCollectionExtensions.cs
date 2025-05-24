using Rms.Application.Modules.Acedamic.Professions.Queries.GellAllProfessions;
using Rms.Application.Modules.Acedamic.Semesters.Queries;
using Rms.Application.Modules.Specialities.Queries.GellAllSpecialities;
using Rms.Application.Services;

namespace Rms.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<IProfessionRepository, ProfessionRepository>();
            services.AddScoped<ISpecialityRepository, SpecialityRepository>();


            services.AddScoped<UserSystemQueryHandler>();
            services.AddScoped<SemesterQueryHandler>();
            services.AddScoped<GetAllProfessionsQueryHandler>();
            services.AddScoped<GetAllSpecialitiesQueryHandler>();

            services.AddMapster();

            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            services.AddTransient<TokenService>();
            services.AddFluentValidationAutoValidation();
            services.Configure<AccountSettings>(configuration.GetSection("Account"));
            services.Configure<RmsSystemConfig>(configuration.GetSection("RmsSystem"));
        }
    }
}
