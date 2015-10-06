using System;
using System.Globalization;

namespace Gorilla.Utilities
{
    /// <summary>
    /// Extension Methods for Enums
    /// </summary>
    public static class ExtensionMethodsDatetime
    {
        /// <summary>
        /// Verfiy if DateTime is not null and Converts the value of the current System.DateTime object to its equivalent short time string representation.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>A string that contains the short time string representation of the current</returns>
        public static string ToShortTimeString(this DateTime? dateTime)
        {
            return dateTime?.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
        }

        /// <summary>
        /// Verfiy if DateTime is not null and converts the value of the current System.DateTime object to its equivalent short date string representation.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>A string that contains the short date string representation of the current</returns>
        public static string ToShortDateString(this DateTime? dateTime)
        {
            return dateTime?.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this DateTime? dateTime, string format = null)
        {
            return dateTime?.ToString(format);
        }
    }
}
