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
        }

        public Inmate DeleteUser(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var deleteUserCommand = connection.CreateCommand();
                deleteUserCommand.CommandText = $@"Delete from [user]
                                              Where id = @id";
                deleteUserCommand.Parameters.AddWithValue("id", id);
                deleteUserCommand.ExecuteNonQuery();


            }
            throw new System.Exception("No user found");
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
            throw new System.Exception("No interest found");
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
            throw new System.Exception("No interest found");
        }

        public UserInterest AddUserInterest(int userid, int interestid)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var addUserInterests = connection.CreateCommand();
            addUserInterests.CommandText = $@"Insert into [userinterests](userid, interestid)
                                       Output inserted.*
                                       Values(@userid, @interestid)";

            addUserInterests.Parameters.AddWithValue("userid", userid);
            addUserInterests.Parameters.AddWithValue("interestid", interestid);

            var reader = addUserInterests.ExecuteReader();

            if (reader.Read())
            {
                var insertedId = (int)reader["Id"];

                var newUserInterest = new UserInterest(userid, interestid) { Id = insertedId };
                connection.Close();
                return newUserInterest;
            }
            throw new System.Exception("No interest added");
        }


        public List<Inmate> GetAll()
        {
            var users = new List<Inmate>();

            var connection = new SqlConnection("Server=localhost;Database=ClinkedIn;Trusted_Connection=True;");
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = @"select username, password, releaseDate, age, id from [user]";

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var username = reader["username"].ToString();
                var password = reader["password"].ToString();
                var releaseDate = (DateTime)reader["releaseDate"];
                var age = (int)reader["age"];
                var user = new Inmate(username, password, releaseDate, age) { Id = id };

                users.Add(user);
            }
            connection.Close();
            return users;
        }

        public List<Interest> GetAllInterest()
        {
            var interests = new List<Interest>();

            var connection = new SqlConnection("Server=localhost;Database=ClinkedIn;Trusted_Connection=True;");
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = $@"select name, id from [interests]";

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var user = new Interest(name) { Id = id };

                interests.Add(user);
            }

            connection.Close();

            return interests;
        }

        public Inmate GetUser(int id)
        {
            var getUser = GetAll();
            var user = (from userz in getUser
                        where userz.Id == id
                        select userz).SingleOrDefault();
            return user;
        }

        public Services AddService(string name, string description, double price)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var addServices = connection.CreateCommand();
            addServices.CommandText = $@"Insert into [services](name, description, price)
                                       Output inserted.*
                                       Values(@name, @description, @price)";

            addServices.Parameters.AddWithValue("name", name);
            addServices.Parameters.AddWithValue("description", description);
            addServices.Parameters.AddWithValue("price", price);

            var reader = addServices.ExecuteReader();

            if (reader.Read())
            {
                var insertedName = reader["name"].ToString();
                var insertedDescription = reader["description"].ToString();
                var insertedPrice = (int)reader["price"];

                var insertedId = (int)reader["Id"];

                var newService = new Services(insertedName, insertedDescription, insertedPrice) { Id = insertedId };
                connection.Close();
                return newService;
            }
            throw new System.Exception("No service added");
        }

        public UserServices AddUserService(int userid, int serviceid)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var addUserServices = connection.CreateCommand();
            addUserServices.CommandText = $@"Insert into [userservices](userid, serviceid)
                                       Output inserted.*
                                       Values(@userid, @serviceid)";
            addUserServices.Parameters.AddWithValue("userid", userid);
            addUserServices.Parameters.AddWithValue("serviceid", serviceid);

            var reader = addUserServices.ExecuteReader();

            if (reader.Read())
            {
                var insertedId = (int)reader["Id"];

                var newUserService = new UserServices(userid, serviceid) { Id = insertedId };
                connection.Close();
                return newUserService;
            }
            throw new System.Exception("No service added");
        }
    }
}
