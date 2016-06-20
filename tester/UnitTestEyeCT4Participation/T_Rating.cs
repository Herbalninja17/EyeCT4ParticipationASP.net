using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_Rating
    {
        [TestMethod]
        public void Rating_Test()
        {
            Assert.AreEqual(true, Database.placeReview(5, "unit test", 3, 2));
        }
    }
}
