using Encryption.Models;
using NUnit.Framework;

namespace EncryptionTests
{
    [TestFixture]
    public class SecrecyTests
    {
        [TestCase("alma","aaaa","alma")]
        [TestCase("abcd","bbbb","bcde")]
        [TestCase("helloworld", "abcdefgijkl", "hfnosauzun")]

        public void EncryptingTest(string message, string key, string expectedResult)
        {
            //arrange
            Secrecy secrecy = new Secrecy();

            //act
            string result = secrecy.Encrypting(message, key);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
