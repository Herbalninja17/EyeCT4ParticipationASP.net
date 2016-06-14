using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tester.Models
{
    public class Review
    {
        public int reviewID { get; set; }
        public string beoordeling { get; set; }
        public string opmerkingen { get; set; }
        public int needyID { get; set; }
        public string needyName { get; set; }
        public int volunteerID { get; set; }
        public string volunteerName { get; set; }

        public Review()
        {

        }

        public Review(int reviewID, string beoordeling, string opmerkingen, int needyID, string needyName, int volunteerID, string volunteerName)
        {
            this.reviewID = reviewID;
            this.beoordeling = beoordeling;
            this.opmerkingen = opmerkingen;
            this.needyID = needyID;
            this.needyName = needyName;
            this.volunteerID = volunteerID;
            this.volunteerName = volunteerName;
        }
    }
}