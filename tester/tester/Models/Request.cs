using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tester.Models
{
    public class Request
    {
        public int requestID { get; set; }
        public int needyID { get; set; }
        public string description { get; set; }
        public bool urgency { get; set; }
        public string location { get; set; }
        public int travelTime { get; set; }
        public string transportType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int totalVolunteer { get; set; }
        public string reactionList { get; set; }
        public bool reported { get; set; }

        public Request(int ID, int needyID, string description, string location, int traveltime, string transporttype,
            DateTime startdate, DateTime enddate, int totalvolunteers)
        {
            this.requestID = ID;
            this.needyID = needyID;
            this.description = description;
            this.urgency = urgency;
            this.location = location;
            this.travelTime = traveltime;
            this.transportType = transporttype;
            this.startDate = startdate;
            this.endDate = enddate;
            this.totalVolunteer = totalvolunteers;
        }
    }
}