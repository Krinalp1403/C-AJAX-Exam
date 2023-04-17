using System;
using System.Linq;
using System.Text.RegularExpressions;
using static Employee.Program;

namespace MyValidation
{

    public static class Extention
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.com|\.org|\.in)+)$";

            var regex = new Regex(pattern);
            var match = regex.Match(email);
            return match.Success;
        }
        public static bool IsValidName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            string pattern = @"^[a-zA-Z]+$";

            var regex = new Regex(pattern);
            var match = regex.Match(name);
            return match.Success;
        }
        public static bool IsValidPhoneNumber(this string number)
        {
            return number.ToString().Length == 10;
        }
        public static bool IsValidPostCode(this string number)
        {
            return number.ToString().Length <= 10;
        }
        public static bool IsValidSalary(this double salary)
        {
            return salary >= 10000 && salary <= 50000;
        }
        public static bool IsValidDepartment(this string desig)
        {
            return Enum.GetNames(typeof(Department)).Contains(desig);
        }
    }
}
