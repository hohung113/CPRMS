using Core.Api.Middlewares;
using Rms.Infrastructure.Persistence;

namespace Rms.API.Middlewares
{
    public class CampusResolutionMiddleware : BaseMiddleware
    {
        public CampusResolutionMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        protected override Task<bool> HandleAsync(HttpContext context)
        {
            var campusProvider = context.RequestServices.GetRequiredService<ICampusProvider>();

#if DEBUG
            campusProvider.CampusName = CprmsCampus.FUDA;
#else
    if (context.Request.Headers.TryGetValue("X-Campus", out var campus))
    {
        campusProvider.CampusName = campus;
        return Task.FromResult(true);
    }

    context.Response.StatusCode = 400;
    return context.Response.WriteAsync("Missing X-Campus header.")
        .ContinueWith(_ => false);
#endif

            return Task.FromResult(true);
        }
    }
}
