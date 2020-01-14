using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IVolunteerRepository _volunteerRepo;
        private readonly IMapper _mapper;
        //
        private readonly IScheduleRepository _scheduleRepo;
        private readonly ISchoolRepository _schoolRepo;
        public ScheduleController(
            IVolunteerRepository volunteerRepository,
            IScheduleRepository scheduleRepository,
            ISchoolRepository   schoolRepository,
             IMapper mapper
            )
        {
            _volunteerRepo = volunteerRepository;
            _scheduleRepo = scheduleRepository;
            _schoolRepo = schoolRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScheduleDto>>> List()
        {
            var schedules = await _scheduleRepo.GetSchedules();
            var scheduleToReturn =
                _mapper.Map<List<ScheduleDto>>(schedules.ToList());

            return Ok(scheduleToReturn);
        }
        //
        [HttpPost("create")]
        public async Task<ActionResult<ScheduleDto>> createSchedule(UserSchoolDto userSchoolDto)
        {
            try
            {
                var user = await _volunteerRepo.GetUserWithID(userSchoolDto.userid);
                if (user == null)
                    return BadRequest($"user with id {userSchoolDto.userid} not found");

                var schoolx = await _schoolRepo.GetSchoolWithID(userSchoolDto.schoolid);
                if (schoolx == null)
                    return BadRequest($"School with id {userSchoolDto.schoolid} not found");

                var dayOfWeekx = await _scheduleRepo.GetDayOfWeek(userSchoolDto.dayoftheweek);

                if (dayOfWeekx == null)
                {
                    Dayofweek dw = new Dayofweek()
                    {
                        Id = userSchoolDto.dayoftheweek
                    };

                    _scheduleRepo.Add(dw);
                    await _scheduleRepo.SaveAll();
                }

                var dayw = userSchoolDto.dayoftheweek;

                Outreachschedule schedulex = new Outreachschedule()
                {
                    School = schoolx,
                    ApplicationUser = user,
                    DayofweekId = dayw,

                    starttime = userSchoolDto.startime,
                    endtime = userSchoolDto.endtime,
                    comments = userSchoolDto.comments,
                    classize = userSchoolDto.classize
                };

                _scheduleRepo.Add(schedulex);


                var result = await _scheduleRepo.SaveAll();

                var schedulexToReturn =
                        _mapper.Map<ScheduleDto>(schedulex);

                return Ok(schedulexToReturn);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

        }
        //
       
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(OutreachSchoolDayDto outreachSchoolDayDto)
        {
            try
            {
                var oschedulex = await _scheduleRepo
                    .GetOutreachSchedules(
                        outreachSchoolDayDto.schoolid,
                        outreachSchoolDayDto.userid,
                        outreachSchoolDayDto.dayid);


                if (oschedulex == null)
                    return NotFound("Schedule no found");

                _scheduleRepo.Delete(oschedulex);

                return await _scheduleRepo.SaveAll();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        //
        [HttpPut]
        public async Task<ActionResult<bool>> Update(SchdeduleToUpdateDto schdeduleToUpdateDto)
        {
            try
            {
                var user = await _volunteerRepo.GetUserWithID(schdeduleToUpdateDto.userid);
                if (user == null)
                    return BadRequest($"user with id {schdeduleToUpdateDto.userid} not found");

                var schoolx = await _schoolRepo.GetSchoolWithID(schdeduleToUpdateDto.schoolid);
                if (schoolx == null)
                    return BadRequest($"School with id {schdeduleToUpdateDto.schoolid} not found");

                var dayOfWeekx = await _scheduleRepo.GetDayOfWeek(schdeduleToUpdateDto.dayoftheweek);
                if (dayOfWeekx == null)
                    return BadRequest($"Day {schdeduleToUpdateDto.dayoftheweek} not Existent");

                var oschedulex = await _scheduleRepo
                    .GetOutreachSchedules(
                        schdeduleToUpdateDto.schoolid,
                        schdeduleToUpdateDto.userid,
                        schdeduleToUpdateDto.dayoftheweek);
                if (oschedulex == null)
                    return BadRequest($"it seems that day {schdeduleToUpdateDto.dayoftheweek} not scheduled in week");
                // TODO update schedule ids
                oschedulex.classize = schdeduleToUpdateDto.classize ?? oschedulex.classize;
                oschedulex.starttime = schdeduleToUpdateDto.startime ?? oschedulex.starttime;
                oschedulex.endtime = schdeduleToUpdateDto.endtime ?? oschedulex.endtime;
                oschedulex.classize = schdeduleToUpdateDto.classize ?? oschedulex.classize;
                oschedulex.createdby = schdeduleToUpdateDto.createdby ?? oschedulex.createdby;
                oschedulex.comments = schdeduleToUpdateDto.comments ?? oschedulex.comments;

               //
                var res = await _schoolRepo.SaveAll();
                var schedulexToReturn =
                        _mapper.Map<ScheduleDto>(oschedulex);
                return Ok(schedulexToReturn);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }


        }
        //
    }
}