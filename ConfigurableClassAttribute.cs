using System;

namespace ContentConfiguring
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurableClassAttribute : Attribute
    {
        public string SectionName { get; init; }

        public ConfigurableClassAttribute(string sectionName)
        {
            SectionName = sectionName ?? throw new ArgumentNullException(nameof(sectionName));
        }
    }
}
