using Encryption.Exceptions;
using Encryption.Models;
using NUnit.Framework;

namespace EncryptionTests
{
    [TestFixture]
    public class SecrecyTests
    {
        [TestCase("alma", "aaaa", "alma")]
        [TestCase("abcd", "bbbb", "bcde")]
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

        [TestCase("aaaa", "alma", "alma")]
        [TestCase("bbbb", "bcde", "abcd")]
        [TestCase("abcdefgijkl", "hfnosauzun", "helloworld")]

        public void DecodingTest(string key, string encrypted, string expectedResult)
        {
            //arrange
            Secrecy secrecy = new Secrecy();

            //act
            string result = secrecy.Decoding(key, encrypted);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("úúúú")]
        [TestCase("BOLDOG")]
        [TestCase("?")]

        public void ExceptionTest(string message)
        {
            //arrange
            Secrecy secrecy = new Secrecy();

            //act-assert
            Assert.Throws(typeof(InvalidFormatException), () => secrecy.ValidFormat(message));
        }
    }
}
