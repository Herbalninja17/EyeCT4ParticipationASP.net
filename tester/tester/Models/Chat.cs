using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tester.Models
{
    public class Chat
    {
        public static object refreshchat()
        {
            tester.Models.Database.chatbox(3, 2);
            return "";
        }

    }
}