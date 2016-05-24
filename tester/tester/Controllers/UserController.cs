using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tester.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Needy()
        {
            return View();
        }

        public ActionResult Volunteer()
        {
            return View();
        }
    }
}