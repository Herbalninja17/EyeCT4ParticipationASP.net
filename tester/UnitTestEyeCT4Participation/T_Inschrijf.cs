using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_Inschrijf
    {
        [TestMethod]
        public void Inschrijf_Test()
        {
            //Registreren en inloggen
            Database.RegesterUser("Unittest", "Unittest123", "Needy", "unittest@geslaagd.com", "Unittest", "links", "rechts", 0212151, "M", string.Empty, string.Empty, string.Empty, string.Empty);
            Assert.AreEqual(true, Database.Login("Unittest", "Unittest123"));
        }
    }
}
