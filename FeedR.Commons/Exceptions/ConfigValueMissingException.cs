
using System;

namespace FeedR.Commons.Exceptions
{
    public class ConfigValueMissingException : Exception
    {
        private const string ExceptionFormat = "Key {0} not found in config";

        public ConfigValueMissingException(string key)
            : base(String.Format(ExceptionFormat, key))
        {
            
        }

    }
}
