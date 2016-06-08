using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tester.Models
{
    public class Volunteer : User
    {
        public int volunteerID;

        public Volunteer(string name, string address, string city, int phonenumber, int volunteerID) : base(name, address, city, phonenumber)
        {
            this.volunteerID = volunteerID;
        }
    }
}