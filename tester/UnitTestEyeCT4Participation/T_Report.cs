using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tester.Models;

namespace UnitTestEyeCT4Participation
{
    [TestClass]
    public class T_Report
    {
        [TestMethod]
        public void ReportRequest_Test()
        {
            //De gebruiker rapporteert een hulpvraag.
            Assert.AreEqual(true, Database.alterYorN("HULPVRAAG", 1, "HULPVRAAGID", "ISREPORTED", "Y"));

            //Hulpvraag wordt weer ongerapporteerd zodat de test vaker kan worden uitgevoerd.
            Assert.AreEqual(true, Database.alterYorN("HULPVRAAG", 1, "HULPVRAAGID", "ISREPORTED", "N"));
        }

        [TestMethod]
        public void ReportChat_Test()
        {
            //De gebruiker rapporteert een chat.
            Assert.AreEqual(true, Database.alterYorN("CHAT", 1, "CHATID", "ISREPORTED", "Y"));

            //Chat wordt weer ongerapporteert zodat de test vaker kan worden uitgevoerd.
            Assert.AreEqual(true, Database.alterYorN("CHAT", 1, "CHATID", "ISREPORTED", "N"));
        }

        [TestMethod]
        public void ReportReview_Test()
        {
            //De gebruiker rapporteert een review.
            Assert.AreEqual(true, Database.alterYorN("REVIEW", 1, "REVIEWID", "ISREPORTED", "Y"));

            //Review wordt weer ongerapporteert zodat de test vaker kan worden uitgevoerd.
            Assert.AreEqual(true, Database.alterYorN("REVIEW", 1, "REVIEWID", "ISREPORTED", "N"));
        }
    }
}
