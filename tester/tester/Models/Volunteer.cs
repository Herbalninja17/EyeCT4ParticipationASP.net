namespace tester.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Volunteer:User
    {
        public Volunteer(string name,string email, string address, string city, int phonenumber, string aboutme, int ID) :base(name,email,city, address, phonenumber, aboutme)
        {
            this.volunteerID = ID;
        }

        public int volunteerID { get; set; }
        public bool car { get; set; }
        public bool license { get; set; }
        //public List<Appointment> appointmentList { get; set; }
    }
}
