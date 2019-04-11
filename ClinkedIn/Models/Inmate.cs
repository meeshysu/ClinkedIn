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
<<<<<<< HEAD
           
=======
>>>>>>> 5d82fdd755eb0d0ae83302221dc9e49bd2279f3e
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Interest { get; set; }
        public List<int> FriendId { get; set; } = new List<int>();
<<<<<<< HEAD
        public List<int> EnemisIds { get; set; } = new List<int>();
        public string Services { get; set; }
=======
        public List<string> Service { get; set; } = new List<string>();
>>>>>>> 5d82fdd755eb0d0ae83302221dc9e49bd2279f3e
    }
}
