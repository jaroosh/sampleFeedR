using System;

namespace FeedR.Commons.Exceptions
{
    /// <summary>
    /// Represents an exception during data storage.
    /// </summary>
    public sealed class DataStoreException : Exception
    {
        private const string ExceptionFormat = "Error storing '{0}'";
        private readonly string _entityName;

        public DataStoreException(string entityName)
        {
            _entityName = entityName;
        }

        public override string Message
        {
            get
            {
                return String.Format(ExceptionFormat, _entityName);
            }
        }


    }
}
