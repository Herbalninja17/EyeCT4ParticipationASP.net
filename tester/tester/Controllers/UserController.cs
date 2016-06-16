namespace tester.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class UserController : Controller
    {
        // GET: User
        public ActionResult Needy()
        {
            return this.View();
        }

        public ActionResult Volunteer()
        {

            return this.View("VolunteerIntrested");
        }

        public ActionResult Admin()
        {
            return this.View();
        }   
    }

}