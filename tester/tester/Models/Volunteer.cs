using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tester.Models
{

    public class Volunteer:User
    {
        public int volunteerID { get; set; }
        public bool car { get; set; }
        public bool license { get; set; }
        //public List<Appointment> appointmentList { get; set; }
        public Volunteer(string name,string email, string address, string city, int phonenumber, int ID) :base(name,email,city, address, phonenumber)
        {
            this.volunteerID = ID;
        }
        
    }
}
