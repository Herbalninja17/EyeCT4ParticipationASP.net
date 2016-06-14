using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tester.Models;

namespace tester.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: Admin
        [HttpPost]
        public ActionResult Chats(string x)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reviews(string x)
        {
            Database.getReviewAdmin();
            return View();
        }

        [HttpPost]
        public ActionResult Requests(string x)
        {
            Database.getRequests();
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
