﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Inmate
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Inmate(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
