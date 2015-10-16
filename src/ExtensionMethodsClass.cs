using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Gorilla.Utilities
{
    /// <summary>
    /// Extension Methods 
    /// </summary>
    public static class ExtensionMethodsClass
    {
        /// <summary>
        /// Convert string to DateTime
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        public static DateTime ToDateTime(this string value)
        {
            return DateTime.Parse(value);
        }

        /// <summary>
        /// Convert string to Decimal
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        public static decimal ToDecimal(this string value)
        {
            return decimal.Parse(value);
        }

        /// <summary>
        /// Convert a string to Int
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        /// <summary>
        /// Convert an object to Int, used for enum
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        public static int ToInt(this object value)
        {
            var retorno = 0;

            if (value != null)
            {
                retorno = Convert.ToInt32(value);
            }

            return retorno;
        }


        /// <summary>
        /// Convert a string to Short
        /// </summary>
        /// <param name="value"></param>
        public static short ToShort(this string value)
        {
            return short.Parse(value);
        }

        /// <summary>
        /// Convert a string to Byte
        /// </summary>
        public static byte ToByte(this string value)
        {
            return byte.Parse(value);
        }

        /// <summary>
        /// Convert a Stream to array of bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ToArray(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Return only the numbers part of string
        /// Ex: 99999-999 => 99999999
        /// </summary>
        [DebuggerStepThrough]
        public static string OnlyNumbers(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ?
                string.Empty :
                Regex.Replace(value, @"\D+", "");
        }

        /// <summary>
        /// Map all properties of the source object them find a properties with same name and type on destiny and copy the value
        /// Use [IgnorableProperty] attr to ignore a prop
        /// </summary>
        /// <param name="destiny"></param>
        /// <param name="source">An object to read and get values</param>
        [DebuggerStepThrough]
        public static void LoadFrom(this object destiny, object source)
        {
            var properties = destiny.GetType().GetRuntimeProperties().ToList();

            foreach (var t in properties)
            {
                //Encontranto propriedades no objeto
                var propertyOrigem = source.GetType().GetRuntimeProperty(t.Name);

                if (propertyOrigem == null || !propertyOrigem.CanRead || t.CanWrite)
                    continue;

                //Verificando se a propriedade deve ser ignorada
                var atributos = propertyOrigem.GetCustomAttributes(typeof(Attributes.IgnorableProperty), false);

                if (atributos != null && atributos.Any())
                    continue;

                if (propertyOrigem.PropertyType == t.PropertyType || Nullable.GetUnderlyingType(propertyOrigem.PropertyType) == t.PropertyType)
                {
                    t.SetValue(destiny, propertyOrigem.GetValue(source, null), null);
                }
            }
        }

        /// <summary>
        /// Convert all objects to T, use the extension method LoadFrom
        /// </summary>
        /// <typeparam name="T">Tipo de objeto de saída</typeparam>
        /// <param name="items">Lista de itens de origem</param>
        [DebuggerStepThrough]
        public static List<T> ConvertAll<T>(this IEnumerable<object> items)
        {
            var retorno = new List<T>();

            foreach (var item in items)
            {
                var adicionar = Activator.CreateInstance<T>();
                adicionar.LoadFrom(item);

                retorno.Add(adicionar);
            }

            return retorno;
        }

        /// <summary>
        /// Convert a class to another using the extension method LoadFrom
        /// </summary>
        /// <typeparam name="T">Class to convert</typeparam>
        /// <param name="item">Source</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static T ConvertTo<T>(this object item)
        {
            var retorno = Activator.CreateInstance<T>();
            retorno.LoadFrom(item);

            return retorno;
        }

        /// <summary>
        /// Gets the next date according to the day of week
        /// </summary>
        public static DateTime FindNextDayOfWeek(this DateTime startDate, DayOfWeek dayOfWeek)
        {
            var result = startDate;

            while (result.DayOfWeek != dayOfWeek)
            {
                result = result.AddDays(1);
            }

            return result;
        }

        /// <summary>
        /// replace last occurence of the string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="find"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(this string source, string find, string replace)
        {
            var place = source.LastIndexOf(find, StringComparison.Ordinal);
            var result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }

        /// <summary>
        /// replace last occurence of the string using Regexp
        /// </summary>
        /// <param name="source"></param>
        /// <param name="regexFind"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceRegLastOccurrence(this string source, string regexFind, string replace)
        {
            return Regex.Replace(source, regexFind, match => match.NextMatch().Index == 0 ? replace : match.Value);
        }

        /// <summary>
        /// Convert the string to slug
        /// Gorilla Geek => gorilla-geek
        /// </summary>
        /// <param name="source"></param>
        /// <param name="limit">limit of caracteres</param>
        /// <returns></returns>
        public static string ToSlug(this string source, short? limit = null)
        {

            if (null == source)
            {
                return "";
            }

            //Remove os acentos
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(source);
            var value = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Converte "GooglePlus" em "-google-plus"
            value = Regex.Replace(value, @"([A-Z]+)", "-$1").ToLowerInvariant();

            // Remove caracteres inválidos
            value = Regex.Replace(value, @"[^a-z0-9]+", "-");

            // Remove hífens do início e do fim da string
            value = value.Trim('-');

            // Se houver um length e a string for maior do que ele, trunca a string
            if (limit.HasValue && value.Length > limit)
            {
                value = value.Substring(0, limit.Value).Trim('-');
            }

            return value;
        }

        /// <summary>
        /// Transform the first letter to lower
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FirstLetterToLower(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            return char.ToLower(source[0]) + source.Substring(1);
        }
    }
}
