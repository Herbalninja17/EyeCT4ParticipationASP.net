using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tester.Controllers
{
    public class NeedyController : Controller
    {
        [HttpPost]
        public ActionResult Chats(int x)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reviews(int x)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Requests(string description, string location, int totalVolunteer, string transportType, int travelTime, string startDate, string endDate, string urgent)
        {
            Models.Database.placeARequest(tester.Models.Database.acid, description, location, travelTime, transportType, startDate, endDate, "Y", totalVolunteer);
            return View();
        }

        [HttpGet]
        public ActionResult Requests()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Chats()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Reviews()
        {
            return View();
        }

    }
}