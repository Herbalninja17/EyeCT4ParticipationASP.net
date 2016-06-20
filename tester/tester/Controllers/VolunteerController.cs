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
            Database.interesse(RequestID, Database.acid);
            return this.RedirectToAction("VolunteerIntrested", "Volunteer");
        }

        public ActionResult reportRequest(int RequestID)
        {
            Database.alterYorN("HULPVRAAG", RequestID, "HULPVRAAGID", "ISREPORTED", "Y");
            return this.RedirectToAction("VolunteerIntrested", "Volunteer");
        }

        // GET: Volunteer
        [HttpGet]
        public ActionResult VolunteerIntrested()
        {
            var requests = Database.GetAllVisibleRequests(Database.acid);
            return this.View(requests);
        }

    }
}