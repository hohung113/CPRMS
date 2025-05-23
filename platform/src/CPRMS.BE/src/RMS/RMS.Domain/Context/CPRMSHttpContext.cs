using Microsoft.AspNetCore.Http;

namespace Rms.Domain.Context
{
    public static class CPRMSHttpContext
    {
        private static IHttpContextAccessor? _httpContextAccessor;

        public static void Configure(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        private static HttpContext? HttpContext => _httpContextAccessor?.HttpContext;

        public static string? UserId => HttpContext?.Items["UserId"] as string;
        public static string? UserName => HttpContext?.Items["UserName"] as string;
        public static string? RoleName => HttpContext?.Items["RoleName"] as string;
        public static bool IsAuthenticated => !string.IsNullOrEmpty(UserId);

        public static void SetCurrentUser(string? userId, string? userName, string? roleName)
        {
            if (HttpContext == null) return;

            HttpContext.Items["UserId"] = userId;
            HttpContext.Items["UserName"] = userName;
            HttpContext.Items["RoleName"] = roleName;
        }

        public static void Clear()
        {
            if (HttpContext == null) return;

            HttpContext.Items.Remove("UserId");
            HttpContext.Items.Remove("UserName");
            HttpContext.Items.Remove("RoleName");
        }

        public static bool IsInRole(string role)
        {
            return string.Equals(RoleName, role, StringComparison.OrdinalIgnoreCase);
        }

        public static string GetUserIdOrThrow()
        {
            return UserId ?? throw new UnauthorizedAccessException("User not authenticated");
        }

        public static void EnsureAuthenticated()
        {
            if (!IsAuthenticated)
                throw new UnauthorizedAccessException("User not authenticated");
        }
    }
}