using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tester.Models
{
    public class Request
    {
        public int requestID { get; set; }
        public int needyID { get; set; }

        [Required (ErrorMessage = "Voer een omschrijving van Uw hulpvraag in.")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        public bool urgency { get; set; }

        [Required(ErrorMessage = "Voer een locatie van Uw hulpvraag in.")]
        public string location { get; set; }

        [Required(ErrorMessage = "Voer Uw geschatte reistijd in.")]
        public int travelTime { get; set; }

        [Required(ErrorMessage = "Voer een gewenst transport type in.")]
        public string transportType { get; set; }

        [Required(ErrorMessage = "Voer een geldige begintijd in.")]
        [DataType(DataType.Time)]
        public DateTime startDate { get; set; }

        [Required(ErrorMessage = "Voer een geldige eindtijd in.")]
        [DataType(DataType.Time)]
        public DateTime endDate { get; set; }

        [Required(ErrorMessage = "Voer een geldig aantal vrijwilligers in.")]
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