using Microsoft.AspNetCore.Http;

namespace Rms.Domain.Context
{
    public static class CPRMSHttpContext
    {
        private const string ContextKey = "CPRMS_Context";

        public static void Set(HttpContext httpContext, CPRMSHttpContextData data)
        {
            httpContext.Items[ContextKey] = data;
        }

        public static CPRMSHttpContextData? Get(HttpContext httpContext)
        {
            return httpContext.Items.TryGetValue(ContextKey, out var value)
                ? value as CPRMSHttpContextData
            : null;
        }

        public static void Clear(HttpContext httpContext)
        {
            httpContext.Items.Remove(ContextKey);
        }
    }

    public class CPRMSHttpContextData
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);
    }
}
