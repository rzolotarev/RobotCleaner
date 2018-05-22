using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Common
{
    public static class AppSettings
    {      
        public static bool TryGet<T>(string key, out T value)
        {
            var stringValue = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(stringValue))
            {
                value = default(T);
                return false;
            }

            value = (T)Convert.ChangeType(stringValue, typeof(T));
            return true;
        }        

        public static T Get<T>(string key)
        {
            T value;
            if (!TryGet(key, out value))
                throw new Exception($"appSettings '{key}' must be specified in the config file");
            return value;
        }

        public static T SafeGet<T>(string key)
        {
            T value;
            if (!TryGet(key, out value))
                return default(T);

            return value;
        }
    }
}
