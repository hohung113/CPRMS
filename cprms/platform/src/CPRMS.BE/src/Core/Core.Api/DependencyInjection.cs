using Core.Api.MediatRCustom;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Đăng ký các service liên quan đến Dispatcher và MediatR.
        /// </summary>
        /// <param name="services">Collection của các service trong DI container.</param>
        /// <param name="assemblyMarker">Assembly chứa các handler (thường là assembly của Program).</param>
        /// <returns>ServiceCollection đã được cấu hình.</returns>
        //public static IServiceCollection AddCPRMSServiceComponents(this IServiceCollection services, params Type[] assemblyMarker)
        //{

        //    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblyMarker));
        //    services.AddScoped<IDispatcher, Dispatcher>();

        //    return services;
        //}
    }
}

