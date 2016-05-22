using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tester.Models;

namespace tester.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //httpPost werk als iemand iets submit
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            if ("Rechard".Equals(username) == true)
            {                
                return RedirectToAction("Home", "Login");
            }

            Models.Database.Login(username, password);
            if (Models.Database.Login(username, password) == true)
            {
                return RedirectToAction("Home", "Login");
            }

            return View();
        }

        //httpGet werkt als iemand het pagine view, refresh enzo
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {            
            return View();
        }
    }
}