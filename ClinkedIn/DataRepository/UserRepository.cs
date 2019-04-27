using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.DataRepository
{
    public class UserRepository
    {
        const string ConnectionString = "Server=localhost;Database=ClinkedIn;Trusted_Connection=True;";
        public Inmate AddUser(string username, string password, DateTime releaseDate, int age)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into [user](username, password, releaseDate, age)
                                              Output inserted.*
                                              Values(@username, @password, @releaseDate, @age)";

                insertUserCommand.Parameters.AddWithValue("username", username); //username from our AddUser parameter.
                insertUserCommand.Parameters.AddWithValue("password", password);
                insertUserCommand.Parameters.AddWithValue("releaseDate", releaseDate);
                insertUserCommand.Parameters.AddWithValue("age", age);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUsername = reader["username"].ToString();
                    var insertedPassword = reader["password"].ToString();
                    var insertedReleaseDate = (DateTime)reader["releaseDate"];
                    var insertedAge = (int)reader["age"];

                    var insertedId = (int)reader["Id"];

                    var newUser = new Inmate(insertedUsername, insertedPassword, insertedReleaseDate, insertedAge) { Id = insertedId };
                    return newUser;
                }
            }
            throw new System.Exception("No user found");

            //static List<Inmate> _inmates = new List<Inmate>();

            //public Inmate AddUser(string username, string password, DateTime releaseDate)
            //{
            //    var newUser = new Inmate(username, password, releaseDate);
            //    newUser.Id = _inmates.Count + 1;

            //    _inmates.Add(newUser);

            //    return newUser;
        }

        public Interest AddInterest(string name, int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into [interests](name)
                                              Output inserted.*
                                              Values(@name)";

                insertUserCommand.Parameters.AddWithValue("name", name); //username from our AddUser parameter.

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedInterestName = reader["name"].ToString();


                    var insertedId = (int)reader["Id"];

                    var newInterest = new Interest(insertedInterestName) { Id = insertedId };
                    return newInterest;
                }
            }
            throw new System.Exception("No user found");
        }

        public Interest UserInterestId(int userid, int interestid)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into [interests](userid)
                                              Output inserted.*
                                              Values(@userid)";

                insertUserCommand.Parameters.AddWithValue("userid", userid); //username from our AddUser parameter.

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedInterestName = reader["userid"].ToString();


                    var insertedId = (int)reader["Id"];

                    var newInterest = new Interest(insertedInterestName) { Id = insertedId };
                    return newInterest;
                }
            }
            throw new System.Exception("No user found");
        }

        public List<Inmate> GetAll()
        {
            //create a list of users
            var users = new List<Inmate>();

            //connect to your local host to get your database
            var connection = new SqlConnection("Server=localhost;Database=ClinkedIn;Trusted_Connection=True;");
            connection.Open();

            //create the command that gets all users: all the information you want from the user
            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = @"select username, password, releaseDate, age, id from [user]";

            //send command thru connection
            var reader = getAllUsersCommand.ExecuteReader();

            //read the results, map it to a type and repository list
            while (reader.Read())//direct cast
            {
                var id = (int)reader["Id"];
                var username = reader["username"].ToString();
                var password = reader["password"].ToString();
                var releaseDate = (DateTime)reader["releaseDate"];
                var age = (int)reader["age"];
                var user = new Inmate(username, password, releaseDate, age) { Id = id };

                users.Add(user);
            }

            //close the conection
            connection.Close();

            //return the list
            return users;
        }

        public List<Interest> GetAllInterest()
        {
            var interests = new List<Interest>();

            var connection = new SqlConnection("Server=localhost;Database=ClinkedIn;Trusted_Connection=True;");
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = @"select name, id from [interest]";

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var user = new Interest(name) { Id = id };

                interests.Add(user);
            }

            //close the conection
            connection.Close();

            //return the list
            return interests;
        }
        //public List<Inmate> GetUsers()
        //{
        //    return _inmates;
        //}

        public Inmate GetUser(int id)
        {
            var getUser = GetAll();
            var user = (from userz in getUser
                       where userz.Id == id
                       select userz).SingleOrDefault();
            return user;
        }

        //public Inmate GetUsersByInterests(string interests)
        //{

        //    var getUserInterests = _inmates.Where(something => something.Interests.Contains(interests)).SingleOrDefault();
        //    return getUserInterests;
        //}

    }
}

