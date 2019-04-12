using ClinkedIn.DataRepository;
using ClinkedIn.Models;
using ClinkedIn.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Controllers
{
    [Route("api/inmate")]
    [ApiController]
    public class InmateController : ControllerBase
    {
        readonly UserRepository _userRepository;
        readonly CreateUserRequestValidator _validator;

        public InmateController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();
        }

        [HttpPost("register")]
        public ActionResult AddUser(CreateUserRequest createRequest)
        {
            if (_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "users must have a username and password" });
            }

            var newUser = _userRepository.AddUser(createRequest.Username, createRequest.Password);

            return Created($"api/users/{newUser.Id}", newUser);

        }

        [HttpGet("{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            return Ok(user);
        }

<<<<<<< HEAD
        [HttpPost("{id}/addenemies/{enemyId}")]
        public ActionResult AddEnemy(int id, int enemyId)
        {
            var user = _userRepository.GetUser(id);
            if(user.FriendId.Contains(enemyId))
            {
                user.FriendId.Remove(enemyId);
            }
            user.EnemisIds.Add(enemyId);
            return Ok(user);
        }


=======
        //[HttpGet("{service}")]
        //public ActionResult GetUser(int id)
        //{
        //    var user = _userRepository.GetUser(id);
        //    return Ok(user);
        //}

        [HttpGet("allInmates")]
        public ActionResult GetUsers()
        {
            var allUsers = _userRepository.GetUsers();
            return Created($"api/users/{allUsers}", allUsers);
        }

>>>>>>> 5d552a03e8c371f370c99d7820fb8952c8b8617f
        [HttpPost("{id}/addfriend/{friendId}")]
        public ActionResult AddFriend(int id, int friendId)
        {
            var user = _userRepository.GetUser(id);
<<<<<<< HEAD
            if (user.EnemisIds.Contains(friendId))
            {
                user.EnemisIds.Remove(friendId);
            }
=======
>>>>>>> 5d82fdd755eb0d0ae83302221dc9e49bd2279f3e
            user.FriendId.Add(friendId);
            return Ok(user);
        }

<<<<<<< HEAD
<<<<<<< HEAD
        [HttpGet("allUsers")]
=======
=======
        [HttpPost("{id}/addinterest/{interests}/")]
        public ActionResult AddUserInterests(int id, string interests)
        {
            var userInterests = _userRepository.GetUser(id);
            userInterests.Interests.Add(interests);
            return Ok(userInterests);
        }

>>>>>>> 5d552a03e8c371f370c99d7820fb8952c8b8617f
        [HttpPost("{id}/addservices/{service}")]
        public ActionResult AddService(int id, string service)
        {
            var user = _userRepository.GetUser(id);
            user.Service.Add(service);
            return Ok(user);
        }//feven helped me thru this :3 mb
<<<<<<< HEAD

        [HttpGet("{id}")]
>>>>>>> 5d82fdd755eb0d0ae83302221dc9e49bd2279f3e
            public ActionResult GetUsers()
            {
                var allUsers = _userRepository.GetUsers();
                return Created($"api/users/{allUsers}", allUsers);
            }
=======
>>>>>>> 5d552a03e8c371f370c99d7820fb8952c8b8617f
    }
    public class CreateUserRequestValidator
    {
        public bool Validate(CreateUserRequest requestToValidate)
        {
            return string.IsNullOrEmpty(requestToValidate.Username)
                   || string.IsNullOrEmpty(requestToValidate.Password);
        }
    }
}
