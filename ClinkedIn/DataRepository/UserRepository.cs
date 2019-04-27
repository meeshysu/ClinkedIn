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

        //public List<Inmate> GetUsers()
        //{
        //    return _inmates;
        //}

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
                return newService;
            }
            throw new System.Exception("No service added");

        }

        //public Inmate GetUsersByInterests(string interests)
        //{

        //    var getUserInterests = _inmates.Where(something => something.Interests.Contains(interests)).SingleOrDefault();
        //    return getUserInterests;
        //}

    }
}

