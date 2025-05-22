using Autofac.Core;
using FluentValidation.AspNetCore;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rms.Application.Modules.Acedamic.Profession.Queries.GellAllProfessions;
using Rms.Application.Modules.Acedamic.QueryHandler;
using Rms.Application.Modules.Acedamic.Validator;
using Rms.Application.Modules.UserManagement.QueryHandler;
using Rms.Domain.Modules.Academic.Interface;
using Rms.Infrastructure.Modules.Academic.Repository;
using Rms.Infrastructure.Modules.UserSystem.Repository;
using Rms.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
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
