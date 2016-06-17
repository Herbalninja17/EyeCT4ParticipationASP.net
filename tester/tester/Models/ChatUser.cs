using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tester.Models
{
    public class ChatUser
    {
        public ChatUser(int userID, string naam)
        {
            this.userID = userID;
            this.naam = naam;
        }

        public int userID { get; set; }
        public string naam { get; set; }
    }
}