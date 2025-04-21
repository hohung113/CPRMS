using System.Text.RegularExpressions;

namespace Core.Utility.RulesEngine
{
    public static class CommonValidation
    {
        public static bool IsEmail(string? email)
        {
            email = email?.Trim();
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            var firstRegStr = @"^.{1,64}@.{1,135}$";
            var secondRegStr = @"^[a-zA-Z0-9]{1,999}[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]{0,999}(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]{1,999}){0,999}@[A-Za-z0-9]{1,999}((\.|-)[A-Za-z0-9]{1,999}){0,999}\.[A-Za-z0-9]{1,999}";
            var thirdRegStr = @"^.{1,999}\.[a-zA-Z]{1,999}$";
            var firstRegex = new Regex(firstRegStr, RegexOptions.None, new TimeSpan(0, 0, 10));
            var secondRegex = new Regex(secondRegStr, RegexOptions.None, new TimeSpan(0, 0, 10));
            var thirdRegex = new Regex(thirdRegStr, RegexOptions.None, new TimeSpan(0, 0, 10));
            return firstRegex.IsMatch(email) || secondRegex.IsMatch(email) || thirdRegex.IsMatch(email);
        }
        public static bool IsPhone(string? phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return true;
            }
            return new Regex(@"^(?! )[+]{0,1}[0-9 ]{0,999}[0-9]{1,999}$", RegexOptions.None, new TimeSpan(0, 0, 10)).IsMatch(phone);
        }
    }
}
