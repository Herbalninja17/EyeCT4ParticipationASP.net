namespace tester.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class User
    {
        public User(string naam, string email, string woonplaats, string adres, int telefoonnummer, string AboutMe)
        {
            this.naam = naam;
            this.email = email;
            this.woonplaats = woonplaats;
            this.adres = adres;
            this.telefoonnummer = telefoonnummer;
            this.AboutMe = AboutMe;
        }

        public string naam { get; set; }
        public string email { get; set; }
        public string woonplaats { get; set; }
        public string adres { get; set; }
        public int telefoonnummer { get; set; }
        public string AboutMe { get; set; }
    }
}