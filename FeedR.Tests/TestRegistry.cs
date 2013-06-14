using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using FeedR.Commons.Interfaces;
using FeedR.Commons.Model;
using FeedR.Commons.Utilities.Crypto;
using Moq;

namespace FeedR.Tests
{
    public class TestRegistry
    {
        public static UserDto WellKnownUser;
        public static string WellKnownUserUnencryptedPass;

        public const string CryptoTokenPass = "KNM&Ihnb69Kr%hbGk^*";
        public const string CryptoTokenSalt = "vw44235";

        static TestRegistry()
        {
            SetupUsers();
        }

        public static IRepository<UserDto> CreateMockedUserRepo()
        {
            var repoMock = new Mock<IRepository<UserDto>>();
            repoMock.Setup(m => m.Query(It.IsAny<Expression<Func<UserDto, bool>>>())).Returns(
                (Expression<Func<UserDto, bool>> pred) =>
                pred.Compile()(WellKnownUser) ? new List<UserDto>() {WellKnownUser} : null);

            return repoMock.Object;
        }

        private static void SetupUsers()
        {
            WellKnownUserUnencryptedPass = "myPass";
            WellKnownUser = new UserDto()
                {
                    CreatedDate = DateTime.Now,
                    Email = "sampleEmail@wp.pl",
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    LastLogOn = DateTime.Now,
                    Login = "myLogin",
                    Password = CryptoHelper.Encrypt<AesManaged>("myPass", CryptoTokenPass, CryptoTokenSalt)
                };
        }
    }
}
