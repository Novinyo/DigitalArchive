using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Attributes;

namespace Application.Common
{
    public static class EnumHelper
    {
         public static string GetAttributeStringValue(this Enum enumValue)
        {
            var attribute = enumValue.GetAttributeOfType<StringValueAttribute>();

            return attribute == null ? String.Empty : attribute.StringValue;
        }

         public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}