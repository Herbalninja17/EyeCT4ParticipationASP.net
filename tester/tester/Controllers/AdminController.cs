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
        //-- Het wijzigen van de zichtbaarheid van de content. De admin kan content zelf reporten en verwijderen --- //
        // Alter Chats
        [HttpPost]
        public ActionResult Chats(string command)
        {
            if (command.Equals("Remove selected"))
            {          
                Database.alterYorN("CHAT", Database.ItemIDSelected, "CHATID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("CHAT", Database.ItemIDSelected, "CHATID", "ISREPORTED", "N");
            }
                return View();
        }

        // Alter Reviews
        [HttpPost]
        public ActionResult Reviews(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("REVIEW", Database.ItemIDSelected, "REVIEWID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("REVIEW", Database.ItemIDSelected, "REVIEWID", "ISREPORTED", "N");
            }
            return View();
        }

        // Alter Requests
        [HttpPost]
        public ActionResult Requests(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("HULPVRAAG", Database.ItemIDSelected, "HULPVRAAGID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("HULPVRAAG", Database.ItemIDSelected, "HULPVRAAGID", "ISREPORTED", "N");
            }
            return View();
        }

        // Alter reported Chats
        [HttpPost]
        public ActionResult reportedChats(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("CHAT", Database.ItemIDSelected, "CHATID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("CHAT", Convert.ToInt32(Database.ItemIDSelected), "CHATID", "ISREPORTED", "N");
            }
            return View();
        }

        // Alter reported Reviews
        [HttpPost]
        public ActionResult reportedReviews(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("REVIEW", Database.ItemIDSelected, "REVIEWID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("REVIEW", Database.ItemIDSelected, "REVIEWID", "ISREPORTED", "N");
            }
            return View();
        }

        // Alter Reported Requests
        [HttpPost]
        public ActionResult reportedRequests(string command)
        {
            if (command.Equals("Remove selected"))
            {
                Database.alterYorN("HULPVRAAG", Database.ItemIDSelected, "HULPVRAAGID", "ISVISIBLE", "N");
            }
            else if (command.Equals("Ignore selected"))
            {
                Database.alterYorN("HULPVRAAG", Database.ItemIDSelected, "HULPVRAAGID", "ISREPORTED", "N");
            }
            return View();
        }

        // ---Get relevant content--- //

        // Get Chats
        [HttpGet]
        public ActionResult Chats()
        {
            Database.getChat();
            return View();
        }

        // Get Reviews
        [HttpGet]
        public ActionResult Reviews()
        {
            Database.getReviewAdmin();
            return View();
        }

        // Get Requests
        [HttpGet]
        public ActionResult Requests()
        {
            Database.getRequests();
            return View();
        }

        // Get Reported Chats
        [HttpGet]
        public ActionResult reportedChats()
        {
            Database.getreportedChat();
            return View();
        }

        // Get Reported Reviews
        [HttpGet]
        public ActionResult reportedReviews()
        {
            Database.getReportedReviews();
            return View();
        }

        // Get Reported Request
       [HttpGet]
        public ActionResult reportedRequest()
        {
            Database.getReportedRequests();
            return View();
        }

        // --- Select ContentID to alter visibility/reported status --- //

        // Select Chat
        public ActionResult selectChat(string message)
       {
           Database.getSelected("CHAT", message, "CHATID", "BERICHT");
           return RedirectToAction("Chats", "Admin");
       }

        // Select Review
        public ActionResult selectReview(string message)
        {
            Database.getSelected("REVIEW", message, "REVIEWID", "OPMERKINGEN");
            return RedirectToAction("Reviews", "Admin");
        }
        
        // Select Request
        public ActionResult selectRequest(string message)
        {
            Database.getSelected("HULPVRAAG", message, "HULPVRAAGID", "OMSCHRIJVING");
            return RedirectToAction("Requests", "Admin");
        }
    }
}
