using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicGameLibrary.Service
{
    public static class GetValueByPropertyName
    {
        public static dynamic GetValueByName<T>(this T obj, string key)
        {
            var property = obj.GetType().GetProperty(key);
            var type = property.PropertyType;
            return Convert.ChangeType(property.GetValue(obj),type);
        }
    }
}
