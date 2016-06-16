namespace tester.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using tester.Models;

    public class LoginController : Controller
    {
        private static int profileb;

        // GET: Login
        //httpPost werk als iemand iets submit
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            ViewBag.loginfail = string.Empty;
            ViewBag.user = username;
            if ("Rechard".Equals(username) == true)
            {
                return this.RedirectToAction("Register", "Login");
            }
            Models.Database.Login(username, password);
            if (Database.Login(username, password) == true)
            {
                if (Database.ac == "Needy")
                {
                    return this.RedirectToAction("Needy", "User");
                }
                else if (Database.ac == "Volunteer")
                {
                    return this.RedirectToAction("VolunteerIntrested", "Volunteer");
                }
            }
            else if (Database.Login(username, password) == false)
            {
                ViewBag.loginfail = "*Incorrect credentials*";
            }
            return this.View();
        }

        //httpGet werkt als iemand het pagine view, refresh enzo
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Register(string type, string username, string password, string email, string name, string address, string city, string phone)
        {
            Database.RegesterUser(username, password, type, email, name, address, city, Convert.ToInt32(phone), "M", string.Empty, "N", "N");
            return this.View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return this.View();
        }

        [HttpGet]
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public ActionResult Profile()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            ViewBag.naam = string.Empty;
            ViewBag.email = string.Empty;
            ViewBag.woonplaats = string.Empty;
            ViewBag.adres = string.Empty;
            ViewBag.telefoon = string.Empty;

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
                return this.View(myreviews);
            }
            else
            {
                return this.View();
            }
        }

        [HttpGet]
        public ActionResult Profileb(int acID)
        {
            Database.Profile(acID, false);
            User user = Database.userBekijken;
            profileb = acID;
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
            return this.View(myreviews);
        }

        [HttpGet]
        public ActionResult ReportReview(int revID, bool profile)
        {
            if (profile == false)
            {
                Database.alterYorN("Review", revID, "REVIEWID", "ISREPORTED", "Y");
                return this.RedirectToAction("Profile", "Login");
            }
            else
            {
                Database.alterYorN("Review", revID, "REVIEWID", "ISREPORTED", "Y");
                return this.RedirectToAction("Profileb", "Login", new { acID = profileb });
            }

        }
    }
}