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

        //TODO - убрать
        public static TimeSpan GetTimeSpan(string key)
        {
            var value = Get<string>(key);
            return TimeSpan.Parse(value);
        }

        //TODO - убрать
        public static TimeSpan GetTimeSpanSafe(string key, TimeSpan defaultValue)
        {
            var value = SafeGet<string>(key);
            if (!string.IsNullOrEmpty(value))
                return TimeSpan.Parse(value);

            return defaultValue;
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
