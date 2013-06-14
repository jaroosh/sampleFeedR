using System.Linq;
using System.Security.Cryptography;
using FeedR.Commons.Interfaces;
using FeedR.Commons.Model;
using FeedR.Commons.Utilities;
using FeedR.Commons.Utilities.Crypto;

namespace FeedR.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Members.

        private readonly IRepository<UserDto> _userRepo; 

        #endregion

        /// <summary>
        /// CTOR.
        /// </summary>
        /// <param name="userRepo"></param>
        public UserRepository(IRepository<UserDto> userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Find user by login and pass.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public UserDto FindUser(string login, string pass)
        {
            // Use one way hash.
            var encrypted = CryptoHelper.Encrypt<AesManaged>(pass, Registry.Security.CryptoTokenPass, Registry.Security.CryptoTokenSalt); 
            var users = _userRepo.Query(u => u.Login == login && u.Password == encrypted && u.IsActive);
            return users == null ? null : users.SingleOrDefault();
        }
    }
}
