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

        public Inmate AddUser(string username, string password, string services)
        {
<<<<<<< HEAD
            var newUser = new Inmate(username, password);
            //newUser.FriendId.Add(friendId);
=======
            var newUser = new Inmate(username, password, services);

>>>>>>> 3d047550d92d859e1b9d063541a3b9b07f41ba63
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
        //public AddUserId(int FriendId)
        //{

        //}
    }
}
