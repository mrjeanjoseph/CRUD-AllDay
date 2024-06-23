using System.Configuration;

namespace PlatformServices.Infrastructure
{
    public class Place : ConfigurationElement
    {
        [ConfigurationProperty("code", IsRequired = true)]
        public string Code
        {
            get { return (string)this["code"]; }
            set { this["code"] = value; }
        }

        [ConfigurationProperty("city", IsRequired = true)]
        public string City
        {
            get { return (string)this["city"]; }
            set { this["city"] = value; }
        }

        [ConfigurationProperty("country", DefaultValue = "USA")]
        public string Country
        {
            get { return (string)this["country"]; }
            set { this["country"] = value; }
        }

    }
}