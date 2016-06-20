using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_SendMessage
    {
        [TestMethod]
        public void SendMessage_Test()
        {
            //send chat
            Assert.AreEqual(true, Database.chatsend(3, 2, "unit test", 3));
        }
    }
}
