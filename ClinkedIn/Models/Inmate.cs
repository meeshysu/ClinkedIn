using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Inmate
    { 
        public Inmate(string username, string password)
        {
            Username = username;
            Password = password;
            
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Interest { get; set; }
        public List<int> FriendId { get; set; } = new List<int>();
    }
}
