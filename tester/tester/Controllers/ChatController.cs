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
        static int needy;
        static int volunteer;
        static int zender;

        //GET: Chat
        [HttpPost]
        public ActionResult Chatbox(string msg)
        {
            var cuser = Database.chatUser;
            Database.chatsend(needy, volunteer, msg, zender);
            Database.chatbox(needy, volunteer);
            return this.View(cuser);
        }

        [HttpGet]
        public ActionResult Chatbox(int id)
        {
            Database.chatboxlist(Database.acid);
            var cuser = Database.chatUser;
            if (id == 0)
            {
                ViewBag.nochat = "*Choose someone to chat with*";
                return this.View(cuser);
            }
            else if (Database.ac == "Volunteer")
            {
                Database.chatbox(id, Database.acid);
                volunteer = Database.acid;
                needy = id;
                zender = volunteer;
                return this.View(cuser);
            }
            else if (Database.ac == "Needy")
            {
                needy = Database.acid;
                volunteer = id;
                zender = needy;
                Database.chatbox(Database.acid, id);
                return this.View(cuser);
            }
            return this.View(cuser);
        }
    }
}