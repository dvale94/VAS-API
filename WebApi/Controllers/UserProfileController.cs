using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly IVolunteerRepository _repo;

        private readonly IMapper _mapper;
        public UserProfileController(
            UserManager<ApplicationUser> userManager,
            IVolunteerRepository ivolunteerRepository,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _repo = ivolunteerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            // me roles
            var rolex = await _userManager.GetRolesAsync(user);
            var strRole = rolex.FirstOrDefault();

            return new
            {
                user.PantherNo,
                user.Email,
                user.UserName,
                strRole
            };
        }
        /// <summary>
        /// Get a list of all Users. It will be only for authorize users, but for now is open to anyone.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<UserSimpleDto>>> List()
        {
            var allUsers = await _repo.GetAllUsers();
            var usersToReturn =
               _mapper.Map<List<UserSimpleDto>>(allUsers);

            return Ok(usersToReturn);
        }
        /*
        [HttpGet("alldapper")]
        public ActionResult<IEnumerable<string>> listdapper()
        {
            var allusers = _repo.GetAllUsersDapper();
            return Ok(allusers);
        }
        */
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web method for Admin";
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("ForCustomer")]
        public string GetCustomer()
        {
            return "Web method for Customer";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [Route("ForAdminOrCustomer")]
        public string GetForAdminOrCustomer()
        {
            return "Web method for Admin or Customer";
        }

        [HttpGet]
        [Authorize(Roles = "Volunteer")]
        [Route("ForVolunteer")]
        public string GetFoVolunteerr()
        {
            return "Web method for volunteer only";
        }
    }
}