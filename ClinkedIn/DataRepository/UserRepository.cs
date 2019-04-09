﻿using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.DataRepository
{
    public class UserRepository
    {
        static List<Inmate> _inmates = new List<Inmate>();

        public Inmate AddUser(string username, string password)
        {
            var newUser = new Inmate(username, password);

            newUser.Id = _inmates.Count + 1;

            _inmates.Add(newUser);

            return newUser;
        }
    }
}