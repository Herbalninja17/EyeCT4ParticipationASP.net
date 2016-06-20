using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_LogIn
    {
        [TestMethod]
        public void Login_Test()
        {
            //Code
            //De gebruiker vult zijn of haar gebruikersnaam en wachtwoord 
            //in in het login scherm en drukt op Login.
            string username = "Sjef";
            string password = "Sjefke3";

            Assert.AreEqual(true, Database.Login(username, password));
            Database.ac = string.Empty;
            Database.acid = 0;
            Database.acnaam = string.Empty;
        }
    }
}
