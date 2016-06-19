using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_BRemove
    {
        [TestMethod]
        public void BRemoveChat_Test()
        {
            //Beheerder maakt een chat invisible voor normale gebruikers
            Assert.AreEqual(true, Database.alterYorN("CHAT", 1, "CHATID", "ISVISIBLE", "N"));

            //Chat wordt weer visible gemaakt
            Assert.AreEqual(true, Database.alterYorN("CHAT", 1, "CHATID", "ISVISIBLE", "Y"));
        }

        [TestMethod]
        public void BRemoveReview_Test()
        {
            //Beheerder maakt een review invisible voor normale gebruikers
            Assert.AreEqual(true, Database.alterYorN("REVIEW", Database.ItemIDSelected, "REVIEWID", "ISVISIBLE", "N"));

            //Review wordt weer visible gemaakt
            Assert.AreEqual(true, Database.alterYorN("REVIEW", Database.ItemIDSelected, "REVIEWID", "ISVISIBLE", "Y"));
        }

        [TestMethod]
        public void BRemoveHulpvraag_Test()
        {
            //Beheerder maakt een hulpvraag invisible voor normale gebruikers
            Assert.AreEqual(true, Database.alterYorN("HULPVRAAG", Database.ItemIDSelected, "HULPVRAAGID", "ISVISIBLE", "N"));

            //Hulpvraag wordt weer visible gemaakt
            Assert.AreEqual(true, Database.alterYorN("HULPVRAAG", Database.ItemIDSelected, "HULPVRAAGID", "ISVISIBLE", "Y"));
        }
    }
}
