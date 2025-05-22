namespace Rms.Application.Helper
{
    public static class PermissionChecker
    {
        public static bool IsAdmin(string email, string adminEmail)
        {
            return email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase);
        }
    }
}
