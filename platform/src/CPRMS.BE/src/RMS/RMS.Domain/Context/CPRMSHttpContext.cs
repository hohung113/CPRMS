namespace Rms.Domain.Context
{
    public static class CPRMSHttpContext
    {
        private static readonly AsyncLocal<CPRMSHttpContent?> _current = new();

        public static CPRMSHttpContent? Current
        {
            get => _current.Value;
            set => _current.Value = value;
        }
        public static string? UserId => Current?.UserId;
        public static string? UserName => Current?.UserName;
        public static string? RoleName => Current?.RoleName;
        public static bool IsAuthenticated => Current?.IsAuthenticated ?? false;
        public static void SetCurrentUser(string? userId, string? userName, string? roleName)
        {
            Current = new CPRMSHttpContent
            {
                UserId = userId,
                UserName = userName,
                RoleName = roleName
            };
        }

        public static void Clear()
        {
            Current = null;
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

    public class CPRMSHttpContent
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);
    }
}