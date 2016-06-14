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
        public ActionResult Chatbox(int n, int v, string msg, int i)
        {
            tester.Models.Database.chatsend(n, v, msg, i);
            return View();
        }

        [HttpGet]
        public ActionResult Chatbox()
        {
            tester.Models.Database.chatbox(3, 2);
            return View();
        }
    }
}