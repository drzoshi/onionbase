using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            var descriptions = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return descriptions.Length > 0 ? descriptions.FirstOrDefault().Description : value.ToString();
        }
    }
}
