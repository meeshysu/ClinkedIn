using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Inmate
    { 
        public Inmate(string username, string password, string services)
        {
            Username = username;
            Password = password;
            Services = services;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Services { get; set; }
    }
}
