using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_KMV
    {
        [TestMethod]
        public void KMV_Test()
        {
            Assert.AreEqual(true, Database.chatbox(3, 2));
        }
    }
}
