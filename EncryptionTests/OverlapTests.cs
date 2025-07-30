using Encryption.Models;
using NUnit.Framework;

namespace EncryptionTests
{
    [TestFixture]
    public class OverlapTests
    {
        [TestCase("alma","beka",new[] { "klux"})]
        [TestCase("elm", "mey",new[] { "arm", "uyu", "jlu"})]

        public void FindKeyTest(string encrypted1, string encrypted2, string[] expectedResult)
        {
            //arrange
            Overlap overlap = new Overlap();

            //act
            string[] result = overlap.FindKey(encrypted1, encrypted2);

            //arrange
            Assert.That(result, Is.EqualTo(expectedResult));
        }

    }
}
