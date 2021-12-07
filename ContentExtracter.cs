using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ContentConfiguring
{
    public class ContentExtracter
    {
        private const string _errorMsg1 = "Configurable class must have a ConfigurableClassAttribute";
        private const string _errorMsg2 = "Configurable property must have a string type";
        
        private readonly IConfiguration _configuration;

        public ContentExtracter(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public T ExtractContent<T>() where T : class, new()
        {
            Type type = typeof(T);
            
            ConfigurableClassAttribute classAttribute = type.GetCustomAttribute<ConfigurableClassAttribute>();
            if(classAttribute == null) 
            {
                throw new Exception(_errorMsg1);
            }

            string sectionName = classAttribute.SectionName;
            IConfigurationSection section = _configuration.GetSection(sectionName);

            T contentModel = new();

            foreach (PropertyInfo property in type.GetProperties()) 
            {
                if(property.PropertyType != typeof(string)) 
                {
                    throw new Exception(_errorMsg2);
                }

                var attribute = property.GetCustomAttribute<ConfigurablePropertyAttribute>();
                
                if (attribute != null) 
                {
                    string key = attribute.Key;
                    string value = section?[key] ?? attribute.DefaultValue;
                    value = value.Replace("    ", "");
                    property.SetValue(contentModel, value);
                }
            }

            return contentModel;
        }
    }
}
