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
        // GET: Admin
        //Het wijzigen van de zichtbaarheid van de content. De admin kan content zelf reporten en verwijderen
        // Chats
        [HttpPost]
        public ActionResult Chats(string command)
        {
            if (command.Equals("Remove selected"))
            {          
                Database.alterYorN("CHAT", 2, "CHATID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("CHAT", 2, "CHATID", "ISREPORTED", "N");
            }
                return View();
        }

        // Reviews
        [HttpPost]
        public ActionResult Reviews(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("REVIEW", 2, "REVIEWID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("REVIEW", 2, "REVIEWID", "ISREPORTED", "N");
            }
            return View();
        }

        // Requests
        [HttpPost]
        public ActionResult Requests(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("HULPVRAAG", 2, "HULPVRAAGID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("HULPVRAAG", 2, "HULPVRAAGID", "ISREPORTED", "N");
            }
            return View();
        }

        // reported Chats
        [HttpPost]
        public ActionResult reportedChats(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("CHAT", 2, "CHATID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("CHAT", 2, "CHATID", "ISREPORTED", "N");
            }
            return View();
        }

        // reported Reviews
        [HttpPost]
        public ActionResult reportedReviews(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("REVIEW", 2, "REVIEWID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("REVIEW", 2, "REVIEWID", "ISREPORTED", "N");
            }
            return View();
        }

        // Reported Requests
        [HttpPost]
        public ActionResult reportedRequests(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("HULPVRAAG", 2, "HULPVRAAGID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("HULPVRAAG", 2, "HULPVRAAGID", "ISREPORTED", "N");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Chats()
        {
            Database.getChat();
            return View();
        }

        [HttpGet]
        public ActionResult Reviews()
        {
            Database.getReviewAdmin();
            return View();
        }

        [HttpGet]
        public ActionResult Requests()
        {
            Database.getRequests();
            return View();
        }

        [HttpGet]
        public ActionResult reportedChats()
        {
            Database.getreportedChat();
            return View();
        }

        [HttpGet]
        public ActionResult reportedReviews()
        {
            Database.getReportedReviews();
            return View();
        }

       [HttpGet]
        public ActionResult reportedRequest()
        {
            Database.getReportedRequests();
            return View();
        }

    }
}
