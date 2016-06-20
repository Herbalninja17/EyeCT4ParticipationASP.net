using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_Hulp
    {
        [TestMethod]
        public void Hulpvraag_Test()
        {
            //Plaats een hulpvraag de "Y" staat voor urgent
            Assert.AreEqual(true, Database.placeARequest(1, "unittest", "links", 4, "kaas", "12:00", "12:01", "Y", 6));
        }
    }
}
