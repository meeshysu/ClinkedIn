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
<<<<<<< HEAD
            
=======
            Services = services;
>>>>>>> 3d047550d92d859e1b9d063541a3b9b07f41ba63
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
<<<<<<< HEAD
        public string Interest { get; set; }
        public List<int> FriendId { get; set; } = new List<int>();
=======
        public string Services { get; set; }
>>>>>>> 3d047550d92d859e1b9d063541a3b9b07f41ba63
    }
}
