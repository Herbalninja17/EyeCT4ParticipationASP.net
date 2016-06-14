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
        public ActionResult Requests(string omschrijving, string locatie, int aantalVrijwilligers, string vervoerstype, string reistijd, string begintijd, string eindtijd, char urgent)
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

        [HttpGet]
        public ActionResult Requests()
        {
            return View();
        }
    }
}