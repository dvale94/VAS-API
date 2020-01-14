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
    public class AttendanceController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IAttendanceRepository _attendanceRepo;

        private readonly IMapper _mapper;
        public AttendanceController(
            UserManager<ApplicationUser> userManager,
            IAttendanceRepository iattendanceRepository,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _attendanceRepo = iattendanceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfileWithAttendance()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            // me roles
            var rolex = await _userManager.GetRolesAsync(user);
            var strRole = rolex.FirstOrDefault();

            return new
            {
                user.Id,
                user.PantherNo,
                user.Email,
                user.UserName,
                user.Attendances
            };
        }

        [HttpPost("signintime")]
        [Authorize]
        public async Task<ActionResult<Attendance>> SigninTime(SigninCreateDto signinCreateDto)
        {
            try
            {
                DateTime localDate = DateTime.UtcNow;

                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);
                var attend = new Attendance()
                {
                    date = localDate,
                    Pantherid = user.PantherNo,
                    SignInTime = localDate,
                    Notes = signinCreateDto.notes
                };

                _attendanceRepo.Add(attend);
                await _attendanceRepo.SaveAll();

                user.Attendances.Add(attend);
                await _attendanceRepo.SaveAll();

                return Ok(attend);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

        }

        [HttpPut("signout/{attendanceid}")]
        [Authorize]
        public async Task<ActionResult<Attendance>> checkOut(Guid attendanceid)
        {
            try
            {
                var attendancex = await _attendanceRepo.GetAttendanceWithID(attendanceid);
                if (attendancex == null)
                    return BadRequest($"could not find attendance with id: {attendanceid}");

                DateTime localDate = DateTime.UtcNow;

                attendancex.SignOutTime = localDate;

                var res = await _attendanceRepo.SaveAll();
                return Ok(attendancex);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

        }

        [HttpDelete("{attendanceid}")]
        [Authorize]
        public async Task<ActionResult<bool>> Delete(Guid attendanceid)
        {
            try
            {
                var attendancex = await _attendanceRepo.GetAttendanceWithID(attendanceid);
                if (attendancex == null)
                    return BadRequest($"could not find attendance with id: {attendanceid}");
                _attendanceRepo.Delete(attendancex);

                var result = await _attendanceRepo.SaveAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

        }
    }
}