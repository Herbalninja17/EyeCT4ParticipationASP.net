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
            Database.RegesterUser(username, password, type, email, name, address, city, Convert.ToInt32(phone), "M", "", "N", "N");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profile()
        {
            ViewBag.naam = "";
            ViewBag.email = "";
            ViewBag.woonplaats = "";
            ViewBag.adres = "";
            ViewBag.telefoon = "";

            if (Database.acid != 0)
            {
                Database.Profile(Database.acid, true);
                User user = Database.user;
                ViewBag.naam = user.naam;
                ViewBag.email = user.email;
                ViewBag.woonplaats = user.woonplaats;
                ViewBag.adres = user.adres;
                ViewBag.telefoon = user.telefoonnummer;
                Database.getMyReviews(Database.acid);
                if (Database.reviewsProfile.Count == 0)
                {
                    ViewBag.reviews = string.Empty;
                }
                else
                {
                    ViewBag.reviews = "Y";
                }
                var myreviews = Database.reviewsProfile.OrderByDescending(x => x.reviewID);
                return View(myreviews);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Profileb(int acID)
        {

            Database.Profile(acID, false);
            User user = Database.userBekijken;
            ViewBag.naam = user.naam;
            ViewBag.email = user.email;
            ViewBag.woonplaats = user.woonplaats;
            ViewBag.adres = user.adres;
            ViewBag.telefoon = user.telefoonnummer;
            Database.getMyReviews(acID);
            if (Database.reviewsProfile.Count == 0)
            {
                ViewBag.reviews = string.Empty;
            }
            else
            {
                ViewBag.reviews = "Y";
            }
            var myreviews = Database.reviewsProfile.OrderByDescending(x => x.reviewID);
            return View(myreviews);
        }
    }
}