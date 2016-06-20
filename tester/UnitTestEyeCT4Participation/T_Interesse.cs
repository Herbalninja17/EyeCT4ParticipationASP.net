using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_Interesse
    {
        [TestMethod]
        public void Intresse_Test()
        {
            //Interesse tonen
            Assert.AreEqual(true, Database.interesse(1, 2));

            //Interesse ongedaan maken
            Assert.AreEqual(true, Database.geenInteresse(1, 2));
        }
    }
}
