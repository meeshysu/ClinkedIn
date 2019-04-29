using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Inmate
    { 
        public Inmate(string username, string password, DateTime releaseDate, int age)
        {
            Username = username;
            Password = password;
            ReleaseDate = releaseDate;
            Age = age;
            
        }
         public Inmate(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Interests { get; set; } = new List<string>();
        public List<int> FriendId { get; set; } = new List<int>();
        public List<int> EnemisIds { get; set; } = new List<int>();
        public List<string> Service { get; set; } = new List<string>();
        public int Sentence { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Age { get; set; }
        public string DaysLeft { get; set; }
    }
}
