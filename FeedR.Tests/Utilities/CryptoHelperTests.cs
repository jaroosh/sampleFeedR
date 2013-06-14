using System.Security.Cryptography;
using FeedR.Commons.Utilities.Crypto;
using NUnit.Framework;

namespace FeedR.Tests.Utilities
{
    [TestFixture]
    public class CryptoHelperTests
    {
        [Test]
        public void Encryp_Should_CorrectlyEncryptAndDecrypt()
        {
            const string text = "Some starting text";

            var encrypted = CryptoHelper.Encrypt<AesManaged>(text, TestRegistry.CryptoTokenPass, TestRegistry.CryptoTokenSalt);
            var decrypted = CryptoHelper.Decrypt<AesManaged>(encrypted, TestRegistry.CryptoTokenPass, TestRegistry.CryptoTokenSalt);

            Assert.AreNotEqual(encrypted, text);
            Assert.AreEqual(decrypted, text);
        }
    }
}
