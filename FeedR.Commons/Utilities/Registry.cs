using System;
using System.Collections.Specialized;
using System.Configuration;
using FeedR.Commons.Exceptions;

namespace FeedR.Commons.Utilities
{
    public static class Registry
    {
        #region Members.

        private static bool _isInitialized;
        private static NameValueCollection _settings;

        #endregion

        #region Values.

        public static class Validation
        {
            public const string EmailRegex =
                @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";

            public const string PasswordRegex = @"^(?=.*\d)(?=.*[a-zA-Z]).{8,16}$";
        }

        public static class Messages
        {
            public const string PasswordTooWeak = "Password is too weak";
            public const string InvalidEmailFormat = "Invalid email format '{0}'";
            public const string UserAlreadyExists = "User {0} already exists!";
        }

        public static class Keys
        {
            public const string DefaultGatorIntervalKey = "DefaultFeedGatorInterval";
            public const string ToasterErrorKey = "_toastrError";
            public const string ToasterSuccessKey = "_toastrSuccess";
        }

        public const string FeedDivName = "feedList";

        public static class Security
        {
            public const string CryptoTokenPass = "KNM&Ihnb69Kr%hbGk^*";  // This might be taken from app config.
            public const string CryptoTokenSalt = "vw44235"; // This might be taken from app config.
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Injecting this makes Registry and other code more testable.
        /// </summary>
        /// <param name="nvm"></param>
        public static void Initialize(NameValueCollection nvm)
        {
            if(nvm == null)
                throw new ConfigurationMissingException();

            _settings = nvm;
            _isInitialized = true;
        }

        public static T GetValue<T>(string key)
        {
            if(!_isInitialized)
                throw new ConfigurationMissingException();

            string val = _settings[key];
            if (val == null)
                throw new ConfigValueMissingException(key);

            return (T) Convert.ChangeType(val, typeof (T));
        }

        #endregion
    }
}
