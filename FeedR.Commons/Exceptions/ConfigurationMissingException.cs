
using System;

namespace FeedR.Commons.Exceptions
{
    public class ConfigurationMissingException : Exception
    {
        public ConfigurationMissingException()
            : base("Configuration not found")
        {
            
        }
    }
}
