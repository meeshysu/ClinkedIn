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

        [HttpPost("{id}/addfriend/{friendId}")]
        public ActionResult AddFriend(int id, int friendId)
        {
            var user = _userRepository.GetUser(id);
            //List<int>  = new List<int>();
            user.FriendId.Add(friendId);
            return Ok(user);
        }

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
