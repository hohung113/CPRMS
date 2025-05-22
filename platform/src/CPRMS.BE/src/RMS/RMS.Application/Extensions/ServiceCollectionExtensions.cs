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


            services.AddScoped<UserSystemQueryHandler>();
            services.AddScoped<SemesterQueryHandler>();
            services.AddScoped<GetAllProfessionsQueryHandler>();
            services.AddMapster();

            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            services.AddFluentValidationAutoValidation();
        }
    }
}
