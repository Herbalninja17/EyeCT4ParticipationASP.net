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
            //RFID rfid = new RFID();
            //if (rfid.Attached == true)
            //{
            //    rfid = new RFID();
            //    rfid.openCmdLine(rfid);
            //    rfid.Tag += new TagEventHandler(rfid_Tag);
            //    rfid.Attach += new AttachEventHandler(rfid_Attach);
            //    rfid.TagLost += new TagEventHandler(rfid_TagLost);
            //    rfid.Detach += new DetachEventHandler(rfid_Detach);
            //}
            //ViewBag.rfid = rfid.tag;
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
                    Database.online = true;
                    return this.RedirectToAction("Needy", "User");
                }
                else if (Database.ac == "Volunteer")
                {
                    Database.online = true;
                    return this.RedirectToAction("Volunteer", "User");
                }
                else if (Database.ac == "Admin")
                {
                    Database.online = true;
                    return this.RedirectToAction("Chats", "Admin");
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
        public ActionResult Register(string type, string username, string password, string email, string name, string address, string city, string phone, string aboutme)
        {
            //RFID rfid = new RFID();
            //if (rfid.Attached == true)
            //{
            //    rfid = new RFID();
            //    rfid.openCmdLine(rfid);
            //    rfid.Tag += new TagEventHandler(rfid_Tag);
            //    rfid.Attach += new AttachEventHandler(rfid_Attach);
            //    rfid.TagLost += new TagEventHandler(rfid_TagLost);
            //    rfid.Detach += new DetachEventHandler(rfid_Detach);
            //}
            //ViewBag.rfid = rfid.tag;
            Database.RegesterUser(username, password, type, email, name, address, city, Convert.ToInt32(phone), "M", string.Empty, "N", "N", aboutme);
            return this.RedirectToAction("Index", "Login");
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
            ViewBag.aboutme = string.Empty;

            if (Database.acid != 0)
            {
                Database.Profile(Database.acid, true);
                User user = Database.user;
                ViewBag.naam = user.naam;
                ViewBag.email = user.email;
                ViewBag.woonplaats = user.woonplaats;
                ViewBag.adres = user.adres;
                ViewBag.telefoon = user.telefoonnummer;
                ViewBag.aboutme = user.AboutMe;
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
            ViewBag.aboutme = user.AboutMe;
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

        [HttpGet]
        public ActionResult Uitloggen()
        {
            Database.ac = string.Empty;
            Database.acid = 0;
            Database.acnaam = string.Empty;
            Database.user = null;
            Database.userBekijken = null;
            Database.online = false;
            return this.View();
        }
    }
}