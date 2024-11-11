using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kursach_Alpinizm;
namespace UnitTestProject1
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
            bool actual = LogIn.logIn("123", "123");
            Assert.AreEqual(expected, actual);
        }
    }
}
