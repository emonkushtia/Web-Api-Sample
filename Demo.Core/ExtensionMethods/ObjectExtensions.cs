using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Demo.Core.ExtensionMethods
{
    public static class ObjectExtensions
    {
        private static readonly JsonSerializerSettings ServerJsonSettings =
            CreateServerJsonSettings();

        private static readonly JsonSerializerSettings ClientJsonSettings =
            CreateClientJsonSettings();

        private static readonly Func<string, object, object, object>
            DefaultConfictHandler = (key, oldValue, newValue) => newValue;

        public static IDictionary<string, object> ToDictionary(
            this object instance)
        {
            if (instance == null)
            {
                return new Dictionary<string, object>();
            }

            var result = TypeDescriptor.GetProperties(instance.GetType())
                .Cast<PropertyDescriptor>()
                .ToDictionary(p => p.Name, p => p.GetValue(instance));

            return result;
        }

        public static string ToServerJson(this object instance)
        {
            return instance == null ?
                null :
                JsonConvert.SerializeObject(instance, ServerJsonSettings);
        }

        public static string ToClientJson(this object instance)
        {
            return instance == null ?
                null :
                JsonConvert.SerializeObject(instance, ClientJsonSettings);
        }

        public static object FromServerJson(this string instance, Type type)
        {
            return string.IsNullOrWhiteSpace(instance) ?
                null :
                JsonConvert.DeserializeObject(instance, type, ServerJsonSettings);
        }

        public static T FromServerJson<T>(this string instance)
        {
            return (T)FromServerJson(instance, typeof(T));
        }

        public static object FromClientJson(this string instance, Type type)
        {
            return string.IsNullOrWhiteSpace(instance) ?
                null :
                JsonConvert.DeserializeObject(instance, type, ClientJsonSettings);
        }

        public static T FromClientJson<T>(this string instance)
        {
            return (T)FromClientJson(instance, typeof(T));
        }

        public static TTarget Merge<TTarget, TSource>(
            this TTarget target,
            TSource source)
            where TTarget : class
            where TSource : class
        {
            return Merge(target, source, null, null, DefaultConfictHandler);
        }

        public static TTarget Merge<TTarget, TSource>(
            this TTarget target,
            TSource source,
            string[] excludedProperties)
            where TTarget : class
            where TSource : class
        {
            return Merge(
                target,
                source,
                null,
                excludedProperties,
                DefaultConfictHandler);
        }

        public static TTarget Merge<TTarget, TSource>(
            this TTarget target,
            TSource source,
            string[] excludedProperties,
            Func<string, object, object, object> conflictHandler)
            where TTarget : class
            where TSource : class
        {
            return Merge(
                target,
                source,
                null,
                excludedProperties,
                conflictHandler);
        }

        public static TTarget Merge<TTarget, TSource>(
            this TTarget target,
            TSource source,
            string[] includedProperties,
            string[] excludedProperties)
            where TTarget : class
            where TSource : class
        {
            return Merge(
                target,
                source,
                includedProperties,
                excludedProperties,
                DefaultConfictHandler);
        }

        public static TTarget Merge<TTarget, TSource>(
            this TTarget target,
            TSource source,
            string[] includedProperties,
            string[] excludedProperties,
            Func<string, object, object, object> conflictHandler)
            where TTarget : class
            where TSource : class
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (conflictHandler == null)
            {
                throw new ArgumentNullException("conflictHandler");
            }

            var targetProperties = TypeDescriptor.GetProperties(target)
                .Cast<PropertyDescriptor>()
                .Where(p => !p.IsReadOnly)
                .Where(p =>
                    includedProperties == null ||
                    includedProperties.Contains(
                        p.Name, StringComparer.OrdinalIgnoreCase))
                .Where(p =>
                    excludedProperties == null ||
                    !excludedProperties.Contains(
                        p.Name, StringComparer.OrdinalIgnoreCase))
                .ToDictionary(d => d.Name, d => d);

            var sourceProperties = TypeDescriptor.GetProperties(source)
                .Cast<PropertyDescriptor>()
                .ToDictionary(d => d.Name, d => d);

            foreach (var property in targetProperties
                .Where(p => sourceProperties.ContainsKey(p.Key)))
            {
                var key = property.Key;

                var newValue = sourceProperties[key].GetValue(source);
                var oldValue = targetProperties[key].GetValue(target);

                if (newValue == oldValue)
                {
                    continue;
                }

                var value = conflictHandler(key, oldValue, newValue);

                targetProperties[key].SetValue(target, value);
            }

            return target;
        }

        public static T As<T>(this object instance) where T : class
        {
            return instance as T;
        }

        public static T To<T>(this object instance)
        {
            return (T)instance;
        }

        private static JsonSerializerSettings CreateClientJsonSettings()
        {
            var settings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

           // settings.Converters.Add(new DataOutputFilterJsonConverter());

            return settings;
        }

        private static JsonSerializerSettings CreateServerJsonSettings()
        {
            var settings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

           // settings.Converters.Add(new DataOutputFilterJsonConverter());

            return settings;
        }
    }
}