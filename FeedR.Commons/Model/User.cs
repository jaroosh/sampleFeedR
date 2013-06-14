using System;

namespace FeedR.Commons.Model
{
    /// <summary>
    ///  Represents a user in the system.
    /// </summary>
    public class UserDto : DtoObject
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLogOn { get; set; }
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Encrypted password.
        /// </summary>
        public string Password { get; set; }
    }
}
