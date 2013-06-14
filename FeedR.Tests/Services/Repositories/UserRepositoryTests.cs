using FeedR.Services.Repositories;
using NUnit.Framework;

namespace FeedR.Tests.Services.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public void FindByLoginAndPassword_Should_SearchByEncryptedPassword()
        {
            var repo = TestRegistry.CreateMockedUserRepo();
            var userRepo = new UserRepository(repo);

            var user = userRepo.FindUser(TestRegistry.WellKnownUser.Login, TestRegistry.WellKnownUserUnencryptedPass);

            Assert.IsNotNull(user);
            Assert.AreEqual(TestRegistry.WellKnownUser, user);
        }

    }
}
