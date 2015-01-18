using System.Text.RegularExpressions;

namespace Demo.Core.ExtensionMethods
{
    public static class StringExtenstions
    {
        private static readonly Regex MatchCounterRegex = new Regex(@"([a-z]+)", RegexOptions.Compiled);
        private static readonly Regex MatchCounterRegex2 = new Regex(@"([A-Z]+)", RegexOptions.Compiled);
        private static readonly Regex MatchCounterRegex3 = new Regex(@"([0-9]+)", RegexOptions.Compiled);
        private static readonly Regex MatchCounterRegex4 = new Regex(@"([!\$#%])+", RegexOptions.Compiled);
        private static readonly Regex PhoneNumberRegex = new Regex(@"^\d+$", RegexOptions.Compiled);

        public static bool IsPasswordStrong(this string password)
        {
            if (password == null || password.Length < 6)
            {
                return false;
            }

            var matchCounter = MatchCounterRegex.IsMatch(password) ? 1 : 0;

            matchCounter += MatchCounterRegex2.IsMatch(password) ? 1 : 0;

            matchCounter += MatchCounterRegex3.IsMatch(password) ? 1 : 0;

            matchCounter += MatchCounterRegex4.IsMatch(password) ? 1 : 0;
            
            return matchCounter >= 3;
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber) ||
                phoneNumber.Length != 10)
            {
                return false;
            }

            return PhoneNumberRegex.IsMatch(phoneNumber);
        }
    }
}