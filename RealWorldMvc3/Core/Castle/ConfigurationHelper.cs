using System;
using System.Collections;
using Castle.Core.Configuration;

namespace RealWorldMvc3.Core.Castle
{
    public static class ConfigurationHelper
    {
        public static IConfiguration CreateConfiguration(IConfiguration parent, string name,
                                                         IDictionary dictionary)
        {
            return CreateConfiguration(parent, name, dictionary, "value");
        }

        public static IConfiguration CreateConfiguration(IConfiguration parent, string name,
                                                         IDictionary dictionary, string valueKey)
        {
            string value = null;

            if (dictionary != null && !string.IsNullOrEmpty(valueKey))
            {
                object valueItem = dictionary[valueKey];
                if (valueItem != null)
                {
                    value = valueItem.ToString();
                    dictionary.Remove(valueKey);
                }
            }

            if (!string.IsNullOrEmpty(name))
            {
                parent = CreateChild(parent, name, value);
            }

            if (dictionary != null)
            {
                foreach (DictionaryEntry entry in dictionary)
                {
                    bool useAttribute;
                    string key = ExtractKey(entry.Key, out useAttribute);
                    SetConfigurationValue(parent, key, entry.Value, valueKey, useAttribute);
                }
            }

            return parent;
        }

        public static void SetConfigurationValue(IConfiguration config, string name,
                                                 object value, string valueKey, bool useAttribute)
        {
            bool isAttribute;
            name = ExtractKey(name, out isAttribute);
            useAttribute |= isAttribute;

            if (value is IDictionary)
            {
                CreateConfiguration(config, name, (IDictionary)value, valueKey);
            }
            else
            {
                string valueStr = string.Empty;

                if (value is bool)
                {
                    valueStr = value.ToString().ToLower();
                }
                else if (value is Type)
                {
                    valueStr = ((Type)value).AssemblyQualifiedName;
                }
                else if (value != null)
                {
                    valueStr = value.ToString();
                }

                if (useAttribute)
                {
                    config.Attributes[name] = valueStr;
                }
                else
                {
                    CreateChild(config, name, valueStr);
                }
            }
        }

        public static IConfiguration CreateChild(IConfiguration parent, string name, string value)
        {
            IConfiguration config = new MutableConfiguration(name, value);

            if (parent != null)
            {
                parent.Children.Add(config);
            }

            return config;
        }

        public static string ExtractKey(object key, out bool isAttribute)
        {
            isAttribute = false;
            string keyName = key.ToString();

            if (keyName.StartsWith("@"))
            {
                isAttribute = true;
                keyName = keyName.Substring(1);
            }

            return keyName;
        }
    }
}