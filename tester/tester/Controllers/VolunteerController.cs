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

        public ActionResult Requests()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult selectRequest(int RequestID)
        {
            Database.intresse(RequestID, 2);
            return RedirectToAction("VolunteerIntrested", "Volunteer");
        }
        // GET: Volunteer
     [HttpGet]
        public ActionResult VolunteerIntrested()
        {
            var requests = Database.GetAllVisibleRequests(2);
            
            return View(requests);
        }

   /*public ActionResult selectChat(string message)
     {
         Database.getSelected("HULPVRAAG", message, "HULPVRAAGID", "BERICHT");
         return RedirectToAction("Chats", "Admin");
     }
    */
    }
}