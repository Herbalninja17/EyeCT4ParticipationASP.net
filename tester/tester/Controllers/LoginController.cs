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
        string naam;
        // GET: Login
        //httpPost werk als iemand iets submit
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            ViewBag.loginfail = "";
            ViewBag.user = username;
            if ("Rechard".Equals(username) == true)
            {
                return RedirectToAction("Register", "Login");
            }
            Models.Database.Login(username, password);
            if (Models.Database.Login(username, password) == true)
            {
                if (Models.Database.ac == "Needy")
                {
                    return RedirectToAction("Needy", "User");
                }
                else if (Models.Database.ac == "Volunteer")
                {
                    return RedirectToAction("Volunteer", "User");
                }
            }
            else if (Models.Database.Login(username, password) == false)
            {
                ViewBag.loginfail = "*Incorrect credentials*";
            }


            return View();
        }

        //httpGet werkt als iemand het pagine view, refresh enzo
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string type, string username, string password, string email, string name, string address, string city, string phone)
        {
            Models.Database.RegesterUser(username, password, type, email, naam, address, city, Convert.ToInt32(phone), "M", "", "N", "N");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}