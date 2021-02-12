using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dateOfBirthday)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirthday.Year;
            if (dateOfBirthday.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}