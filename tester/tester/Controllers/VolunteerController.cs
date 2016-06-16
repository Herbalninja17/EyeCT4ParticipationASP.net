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

        
        public ActionResult showInterest(int RequestID)
        {
            Database.intresse(RequestID, tester.Models.Database.acid);
            return this.RedirectToAction("VolunteerIntrested", "Volunteer");
        }
        // GET: Volunteer
     [HttpGet]
        public ActionResult VolunteerIntrested()
        {
            var requests = Database.GetAllVisibleRequests(tester.Models.Database.acid);
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