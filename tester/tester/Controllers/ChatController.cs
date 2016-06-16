namespace tester.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using tester.Models;

    public class ChatController : Controller
    {
        // GET: Chat
        [HttpPost]
        public ActionResult Chatbox(string msg, int i)
        {
            Database.chatsend(3, 2, msg, i);
            return this.View();
        }

        [HttpGet]
        public ActionResult Chatbox()
        {
            Database.chatbox(3, 2);
            return this.View();
        }
    }
}