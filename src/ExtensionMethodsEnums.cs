using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Gorilla.Utilities
{
    /// <summary>
    /// Extension Methods for Enums
    /// </summary>
    public static class ExtensionMethodsEnums
    {
        /// <summary>
        /// Get the display attribute of enum
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum source)
        {
            var field = source.GetType().GetRuntimeField(source.ToString());
            var attribute = field.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();

            if (attribute == null)
            {
                return source.ToString();
            }

            if (attribute.ResourceType == null)
            {
                return attribute.Name ?? attribute.Description;
            }

            var resource = new ResourceManager(attribute.ResourceType);
            return resource.GetString(attribute.Name) ?? attribute.Name ?? attribute.Description;
        }


    }
}
