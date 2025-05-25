using Core.Api.Middlewares;
using Rms.API.Middlewares;

namespace Rms.API.Middlewares
{
    public class CprmsApiKeyMiddleware : BaseMiddleware
    {
        private readonly IConfiguration _config;

        public CprmsApiKeyMiddleware(RequestDelegate next, IConfiguration config) : base(next)
        {
            _config = config;
        }

        protected override Task<bool> HandleAsync(HttpContext context)
        {
            var expectedKey = _config["ApiKey"];
            if (context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey)
                && providedKey == expectedKey)
            {
                return Task.FromResult(true);
            }

            context.Response.StatusCode = 401;
            return context.Response.WriteAsync("Invalid or missing API Key.")
                .ContinueWith(_ => false);
        }
    }

}