using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimplePageFactory.Helpers
{
    public static class DateHelper
    {
        /// <summary>
        /// Reverting string date. For example calling on "2017-12-31" method return "31-12-2017" and vice versa
        /// </summary>
        public static string RevertDate(this string date)
        {
            if ((Regex.IsMatch(date, "[0-9]{4}-[0-9]{2}-[0-9]{2}")) || (Regex.IsMatch(date, "[0-9]{2}-[0-9]{2}-[0-9]{4}")))
                return String.Join("-", date.Split('-').Reverse());
            else
                throw new ArgumentException("Parameter in wrong format", "date");
        }
    }
}
