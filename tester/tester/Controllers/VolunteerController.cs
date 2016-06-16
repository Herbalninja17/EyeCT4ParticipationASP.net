namespace tester.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using tester.Models;

    public class VolunteerController : Controller
    {

        [HttpPost]
        public ActionResult Requests()
        {
            
            return this.View();
        }

        [HttpGet]
        public ActionResult selectRequest(int RequestID)
        {
            Database.intresse(RequestID, 2);
            return this.RedirectToAction("VolunteerIntrested", "Volunteer");
        }
        // GET: Volunteer
     [HttpGet]
        public ActionResult VolunteerIntrested()
        {
            var requests = Database.GetAllVisibleRequests(2);
            return this.View(requests);
        }

   /*public ActionResult selectChat(string message)
     {
         Database.getSelected("HULPVRAAG", message, "HULPVRAAGID", "BERICHT");
         return this.RedirectToAction("Chats", "Admin");
     }
    */
    }
}