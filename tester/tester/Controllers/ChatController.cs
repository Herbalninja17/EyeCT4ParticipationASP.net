using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tester.Models;

namespace tester.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        [HttpPost]
        public ActionResult Chatbox(int n, int v)
        {
            tester.Models.Database.chatbox(n, v);
            return View();
        }

        [HttpGet]
        public ActionResult Chatbox()
        {
            return View();
        }
    }
}