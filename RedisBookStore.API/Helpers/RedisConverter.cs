using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisBookStore.API.Helpers
{
    public static class RedisConverter
    {
        public static HashEntry[] ToHashEntries(object obj)
        {
            var properties = obj.GetType().GetProperties();
            return properties
                .Where(x => x.GetValue(obj) != null)
                .Select
                (
                    property =>
                    {
                        var propertyValue = property.GetValue(obj);
                        string hashValue;

                        hashValue = propertyValue is IEnumerable<object> ? JsonConvert.SerializeObject(propertyValue) : propertyValue.ToString();

                        return new HashEntry(property.Name, hashValue);
                    }
                )
                .ToArray();
        }
        
        public static T ConvertFromRedis<T>(HashEntry[] hashEntries)
        {
            var properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
                if (entry.Equals(new HashEntry())) continue;
                if (property.PropertyType == typeof(System.String[]))
                {
                    String[] blah = new []{entry.Value.ToString()};
                    property.SetValue(obj, Convert.ChangeType(blah, property.PropertyType));
                }
                else 
                { 
                    property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
                }
            }
            return (T)obj;
        }
    }
}