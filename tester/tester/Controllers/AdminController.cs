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

        List<string> content = new List<string>();
        //
        // GET: /Admin/
        public ActionResult Admin()
        {


            return View();
        }

        [HttpPost]
        public ActionResult reviewsBTN()
        {
            Models.Database.getReviewAdmin();
            foreach (string item in Database.reviewsListAdmin)
            {
                content.Add(item);
            }
            return View();
        }
	}
}