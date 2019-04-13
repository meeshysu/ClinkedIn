﻿using ClinkedIn.DataRepository;
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

        [HttpGet("{id}/getmutualfriends/{friendId}")]
        public ActionResult MyFriendFriend(int id, int friendId)
        {
            var inmate = _userRepository.GetUser(id);
            var inmates = _userRepository.GetUsers();
            var filterFriend = (from inmatez in inmates
                                where friendId == inmatez.Id
                                select inmatez).SingleOrDefault();
            var getMutualFriend = (filterFriend.FriendId);
           
            List<string> name = new List<string>();
            foreach (int i in getMutualFriend)
            {
                foreach (var inmateId in inmates)
                {
                    if (i == inmateId.Id)
                    {
                        name.Add(inmateId.Username);
                        //return name;
                    }
                    //var friendsNames = (from myfriendId in i
                    //                    where myfriendId == inmate.Id
                    //                    select myfriendId.)
                }
            }
            return Ok(name);
        }

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


        [HttpPost("{id}/addfriend/{friendId}")]
        public ActionResult AddFriend(int id, int friendId)
        {
            var user = _userRepository.GetUser(id);

            if (user.EnemisIds.Contains(friendId))
            {
                user.EnemisIds.Remove(friendId);
            }
            user.FriendId.Add(friendId);
            return Ok(user);
        }

        [HttpGet("allUsers")]
        [HttpPost("{id}/addservices/{service}")]
        public ActionResult AddService(int id, string service)
        {
            var user = _userRepository.GetUser(id);
            user.Service.Add(service);
            return Ok(user);
        }//feven helped me thru this :3 mb

        [HttpGet("{id}")]
            public ActionResult GetUsers()
            {
                var allUsers = _userRepository.GetUsers();
                return Created($"api/users/{allUsers}", allUsers);
            }
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
