namespace tester.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using tester.Models;

    public class NeedyController : Controller
    {
        private static int volID;

        [HttpPost]
        public ActionResult Chats(int x)
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Reviews(int x)
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Requests(string description, string location, int totalVolunteer, string transportType, int travelTime, string startDate, string endDate, string urgency)
        {
            if (urgency == "true")
            {
                urgency = "Y";
            }
            else if (urgency == "false")
            {
                urgency = "N";
            }
            Database.placeARequest(Database.acid, description, location, travelTime, transportType, startDate, endDate, urgency, totalVolunteer);
            return this.View();
        }

        [HttpGet]
        public ActionResult Requests()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Chats()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Review(string volnaam, int volid)
        {
            ViewBag.volnaam = volnaam;
            volID = volid;
            return this.View();
        }

        [HttpPost]
        public ActionResult Review(int beoordeling, string opmerkingen)
        {
            Database.placeReview(beoordeling, opmerkingen, Database.acid, volID);
            volID = 0;
            return RedirectToAction("Requests", "Needy");
        }
    }
}