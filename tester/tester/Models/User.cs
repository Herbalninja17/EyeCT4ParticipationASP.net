﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tester.Models
{
    public class User
    {
        public int id { get; set; }
        public string type { get; set; }
        public string naam { get; set; }
        public string email { get; set; }
        public string woonplaats { get; set; }
        public string adres { get; set; }
        public int telefoonnummer { get; set; }

        public User(string naam, string email, string woonplaats, string adres, int telefoonnummer)
        {
            this.id = id;
            this.type = type;
            this.naam = naam;
            this.email = email;
            this.woonplaats = woonplaats;
            this.adres = adres;
            this.telefoonnummer = telefoonnummer;
        }
    }
}