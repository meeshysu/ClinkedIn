using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.DataRepository
{
    public class UserRepository
    {
        static List<Inmate> _inmates = new List<Inmate>();

        public Inmate AddUser(string username, string password, DateTime releaseDate)
        {
            var newUser = new Inmate(username, password, releaseDate);
            //newUser.FriendId.Add(friendId);
            newUser.Id = _inmates.Count + 1;

            _inmates.Add(newUser);

            return newUser;
        }

        public List<Inmate> GetUsers()
        {
            return _inmates;
        }

        public Inmate GetUser(int id)
        {
            var getUser = GetUsers();
            var user = (from userz in getUser
                       where userz.Id == id
                       select userz).SingleOrDefault();
            return user;
        }

        //public Inmate GetUserDays(int daysleft)
        //{
        //    var getUser = GetUsers();
        //    var user = _inmates.Where(something => something.DaysLeft == daysleft).SingleOrDefault(); 
        //    return user;
        //}

        public Inmate GetUsersByInterests(string interests)
        {

            var getUserInterests = _inmates.Where(something => something.Interests.Contains(interests)).SingleOrDefault();
            return getUserInterests;
        }

    }
}
