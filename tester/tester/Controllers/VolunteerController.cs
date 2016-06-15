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
    
        // GET: Volunteer
     [HttpGet]
        public ActionResult VolunteerIntrested()
        {
            List<Request> requests = Database.GetAllVisibleRequests();
            return View();
        }

   /*  public ActionResult selectChat(string message)
     {
         Database.getSelected("HULPVRAAG", message, "HULPVRAAGID", "BERICHT");
         return RedirectToAction("Chats", "Admin");
     }
    */
    }
}