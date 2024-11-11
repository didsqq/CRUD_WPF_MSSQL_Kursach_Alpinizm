using Kursach_Alpinizm;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogInMethod1()
        {
            bool expected = false;
            bool actual = LogIn.logIn("123", "1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestLogInMethod2()
        {
            bool expected = true;
            bool actual = LogIn.logIn("123", "zC6hoKyv5O");
            Assert.AreEqual(expected, actual);
        }
    }
}