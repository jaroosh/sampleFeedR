using FeedR.Commons.Model;

namespace FeedR.Commons.Interfaces
{
    /// <summary>
    /// Simple repo of users.
    /// </summary>
    public interface IUserRepository
    {
        UserDto FindUser(string login, string pass);
    }
}
