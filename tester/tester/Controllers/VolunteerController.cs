using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tester.Models;

namespace tester.Controllers
{
    public class VolunteerController : Controller
    {

        [HttpPost]

        //public ActionResult Requests()
        //{
        //    //List<Request> requests = Database.GetAllVisibleRequests();
        //    //return View(requests);
        //}

        // GET: Volunteer
    [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}