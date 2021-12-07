using System;

namespace ContentConfiguring
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurablePropertyAttribute : Attribute
    {
        public string Key { get; init; }
        public string DefaultValue { get; init; }

        public ConfigurablePropertyAttribute(string key, string defaultValue = "NaN")
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            DefaultValue = defaultValue;
        }
    }
}
