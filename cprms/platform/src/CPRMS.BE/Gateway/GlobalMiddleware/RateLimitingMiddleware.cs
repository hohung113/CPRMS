using Microsoft.Extensions.Caching.Memory;

namespace Gateway.GlobalMiddleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly int _limit = 5;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(60);

        public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(ipAddress))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Unable to determine IP address");
                return;
            }

            var cacheKey = $"rl_{ipAddress}";
            var timestamps = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _period;
                return new List<DateTime>();
            });

            timestamps.RemoveAll(ts => ts < DateTime.UtcNow - _period);

            if (timestamps.Count >= _limit)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            timestamps.Add(DateTime.UtcNow);
            _cache.Set(cacheKey, timestamps, _period);

            await _next(context);
        }
    }
}