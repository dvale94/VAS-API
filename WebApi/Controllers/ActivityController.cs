using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IVolunteerRepository _repo;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly AuthenticationContext _authContext;

        public ActivityController(
            IVolunteerRepository volunteerRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
             AuthenticationContext authContext
            )
        {
            _repo = volunteerRepository;
            _userManager = userManager;
            _mapper = mapper;
            _authContext = authContext;
        }
       /*
        [HttpGet]
        public async Task<ActionResult<List<ActivityDto>>> List()
        {
            var activities = await _repo.GetActivities();

            var activitiesToReturn =
                 _mapper.Map<List<Activity>, List<ActivityDto>>(activities.ToList());
            return Ok(activitiesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> Details(Guid id)
        {
            var activityx = await _repo.GetActivity(id);
            if (activityx == null)
                return NotFound();

            var activityToReturn = _mapper.Map<Activity, ActivityDto>(activityx);
            return Ok(activityToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create(Activity activityToCreate)
        {
            var activity = new Activity()
            {
                Title = activityToCreate.Title,
                Description = activityToCreate.Description,
                StartDate = activityToCreate.StartDate,
                EndDate = activityToCreate.EndDate,
                SchoolAddress = activityToCreate.SchoolAddress,
                SchoolName = activityToCreate.SchoolName
            };
            _repo.Add(activity);

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            // me roles
            var rolex = await _userManager.GetRolesAsync(user);
            var strRole = rolex.FirstOrDefault();

            var attendee = new UserActivity
            {
                ApplicationUser = user,
                Activity = activity,
                IsHost = true,
                DateJoined = DateTime.Now,
                Urole = strRole,
                ableToCheckInOut = false
            };
            _repo.Add(attendee);

            return await _repo.SaveAll();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Edit(Guid id, ActivityToEdit activityToEdit)
        {
            if (activityToEdit == null)
                BadRequest(ModelState);

            var activityx = await _repo.GetActivity(id);
            if (activityx == null)
                return NoContent();

            activityx.Title = activityToEdit.Title ?? activityx.Title;
            activityx.Description = activityToEdit.Description ?? activityx.Description;
            activityx.StartDate = activityToEdit.StartDate ?? activityx.StartDate;
            activityx.EndDate = activityToEdit.EndDate ?? activityx.EndDate;
            activityx.SchoolName = activityToEdit.SchoolName ?? activityx.SchoolName;
            activityx.SchoolAddress = activityToEdit.SchoolAddress ?? activityx.SchoolAddress;


            return await _repo.SaveAll();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var activityx = await _repo.GetActivity(id);
            if (activityx == null)
                return NoContent();

            _repo.Delete(activityx);
            return await _repo.SaveAll();
        }

        [HttpPost("{actid}/attend")]
        public async Task<ActionResult<bool>> Attend(Guid actid)
        {
            var activityx = await _repo.GetActivity(actid);
           
            if (activityx == null)
                return NotFound();

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            // me roles
            var rolex = await _userManager.GetRolesAsync(user);
            var strRole = rolex.FirstOrDefault();
            // TODO move _authContext to repo 
            var attendance = await _authContext.UserActivities
               .SingleOrDefaultAsync(x => x.ActivityId == actid &&
                        x.ApplicationUserId == userId);

            if (attendance != null)
                return BadRequest();

            attendance = new UserActivity
            {
                ApplicationUser = user,
                Activity = activityx,
                IsHost = false,
                DateJoined = DateTime.Now,
                Urole = strRole,
                ableToCheckInOut = false
            };

            _authContext.UserActivities.Add(attendance);

            return await _repo.SaveAll();
        }

        [HttpDelete("{actid}/attend")]
        public async Task<ActionResult<bool>> Unattend(Guid actid)
        {
            var activityx = await _repo.GetActivity(actid);

            if (activityx == null)
                return NotFound();

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            // me roles
            var rolex = await _userManager.GetRolesAsync(user);
            var strRole = rolex.FirstOrDefault();
            // TODO move _authContext to repo 
            var attendance = await _authContext.UserActivities
               .SingleOrDefaultAsync(x => x.ActivityId == actid &&
                        x.ApplicationUserId == userId);

            if (attendance == null)
                return Ok();
            if (attendance.IsHost)
                return BadRequest();

            _authContext.UserActivities.Remove(attendance);

            return await _repo.SaveAll();

        }
        */
    }
}