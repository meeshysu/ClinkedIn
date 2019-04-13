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

        [HttpPost("{id}/addinterest/{interests}/")]
        public ActionResult AddInterests(int id, string interests)
        {
            var userInterests = _userRepository.GetUser(id);
            userInterests.Interests.Add(interests);
            return Ok(userInterests);
        }

        [HttpPost("{id}/addservices/{service}")]
        public ActionResult AddService(int id, string service)
        {
            var user = _userRepository.GetUser(id);
            user.Service.Add(service);
            return Ok(user);
        }

        [HttpPost("{id}/daysleft/{daysLeft}")]
        public ActionResult AddSentenceLength(int id, int daysLeft)
        {
            _userRepository.GetUser(id).DaysLeft = daysLeft;
           
            return Ok(_userRepository.GetUser(id));
        }

        [HttpGet("allInmates")]
        public ActionResult GetUsers()
        {
            var allUsers = _userRepository.GetUsers();
            return Ok(allUsers);
        }

        [HttpGet("{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            return Ok(user);
        }

        [HttpGet("getinterest/{interests}")]
        public ActionResult GetUsersByInterests(string interests)
        {
            var userInterests = _userRepository.GetUsersByInterests(interests);
            return Ok(userInterests);
        }

        [HttpGet("{id}/getmyfriendsfriends/{friendId}")]
        public ActionResult MyFriendFriend(int id, int friendId)
        {
            var inmates = _userRepository.GetUsers();
            var filterMyFriend = (from inmatez in inmates
                                  where friendId == inmatez.Id
                                  select inmatez).SingleOrDefault();
            var getMyFriendzFriendsList = filterMyFriend.FriendId;

            List<string> name = new List<string>();
            foreach (int getMyFriendzFriend in getMyFriendzFriendsList)
            {
                foreach (var inmateById in inmates)
                {
                    if (getMyFriendzFriend == inmateById.Id)
                    {
                        name.Add(inmateById.Username);
                    }
                }
            }
            return Ok(name);
        }

        [HttpDelete("{id}/deleteservice/{service}")]
        public ActionResult DeleteService(int id, string service)
        {
            var user = _userRepository.GetUser(id);
            user.Service.Remove(service);
            return Ok(user);
        }

        [HttpDelete("{id}/deleteinterest/{interests}")]
        public ActionResult DeleteUserInterest(int id, string interests)
        {
            var userInterests = _userRepository.GetUser(id);
            userInterests.Interests.Remove(interests);
            return Ok(userInterests);
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
