using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tester.Models
{
    public class Needy : User
    {
        public int needyID { get; set; }

        public Needy(string name, string address, string city, int phonenumber, int needyID) : base(name, address, city, phonenumber)
        {
            this.needyID = needyID;
        }
    }
}