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
            Database.chatsend(needy, volunteer, msg, zender);
            Database.chatbox(needy, volunteer);
            return this.View();
        }

        [HttpGet]
        public ActionResult Chatbox(int id)
        {
            if(id == 0)
            {
                ViewBag.nochat = "*Choose someone to chat with*";
            }
            else if (Database.ac == "Volunteer")
            {
                Database.chatbox(id, Database.acid);
                volunteer = Database.acid;
                needy = id;
                zender = volunteer;
                return this.View();
            }
            else if (Database.ac == "Needy")
            {
                needy = Database.acid;
                volunteer = id;
                zender = needy;
                Database.chatbox(Database.acid, id);
                return this.View();
            }
            return this.View();
        }
    }
}