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
    public class TeamController : ControllerBase
    {
        private readonly IVolunteerRepository _volunteerRepo;
        private readonly IMapper _mapper;
        //
        private readonly ITeamRepository _teamRepo;
        private readonly ISchoolRepository _schoolRepo;
        public TeamController(
            IVolunteerRepository volunteerRepository,
            ITeamRepository teamRepository,
            ISchoolRepository schoolRepository,
           IMapper mapper
            )
        {
            _volunteerRepo = volunteerRepository;
            _teamRepo = teamRepository;
            _schoolRepo = schoolRepository;
            _mapper = mapper;
        }
       
        [HttpGet]
        public async Task<ActionResult<List<TeamDto>>> List()
        {
            var teams = await _teamRepo.GetTeams();
            var teamsToReturn =
               _mapper.Map<List<TeamDto>>(teams);

            return Ok(teamsToReturn);
        }
       

        [HttpPost]
        public async Task<ActionResult<bool>> create(Team teamToCreate)
        {
            var newteam = new Team()
            {
                Teamnumber = teamToCreate.Teamnumber,
                Description = teamToCreate.Description
            };
            _teamRepo.Add(newteam);

            return await _teamRepo.SaveAll();
        }
        //
        [HttpPut("{teamid}")]
        public async Task<ActionResult<bool>> Update(Guid teamid, TeamDto teamDto)
        {
            try
            {
                var teamx = await _teamRepo.GetTeam(teamid);
                if (teamx == null)
                    return BadRequest($"could not find team with id: {teamid}");

                teamx.Description = teamDto.Description ?? teamx.Description;
                teamx.Teamnumber = teamDto.Teamnumber ?? teamx.Teamnumber;

                var res = await _teamRepo.SaveAll();

                var teamxToReturn = _mapper.Map<TeamDto>(teamx);

                return Ok(teamxToReturn);


            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }


        }
        //
        [HttpDelete("{teamid}")]
        public async Task<ActionResult<bool>> Delete(Guid teamid)
        {
            var teamx = await _teamRepo.GetTeam(teamid);
           
            if (teamx == null)
                return BadRequest($"could not find team with id: {teamid}");

            var volunteers = teamx.Volunteer.Count();
            if (volunteers > 0)
                return BadRequest("team must be empty to perform delete");

            _teamRepo.Delete(teamx);

            return await _teamRepo.SaveAll();
        }
        //
        [HttpPost("addteamtoschool")]
        public async Task<ActionResult<SchoolDto>> addTeamToSchool(TeamSchoolDto teamSchoolDto)
        {
            try
            {

                var teamx = await _teamRepo.GetTeam(teamSchoolDto.teamid);
                
                var schoolx = await _schoolRepo.GetSchoolWithID(teamSchoolDto.schoolid);

                schoolx.Teams.Add(teamx);
                var result = await _teamRepo.SaveAll();

                var schoolToReturn =
               _mapper.Map<SchoolDto>(schoolx);
                return Ok(schoolToReturn);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db no good");
            }

        }
        //
        [HttpPost("removeteamfromschool")]
        public async Task<ActionResult<bool>> removeTeamFromSchool(TeamSchoolDto teamSchoolDto)
        {
            try
            {

                var teamx = await _teamRepo.GetTeam(teamSchoolDto.teamid);
                var schoolx = await _schoolRepo.GetSchoolWithID(teamSchoolDto.schoolid);

                schoolx.Teams.Remove(teamx);
               
                return await _teamRepo.SaveAll();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db no good");
            }

        }
        //

        [HttpPost("addusertoteam")]
        public async Task<ActionResult<TeamDto>> addVolunteerToTeam(UsertoteamDto usertoteamDto)
        {
            try
            {

                var teamx = await _teamRepo.GetTeam(usertoteamDto.teamid);
                var userx = await _volunteerRepo.GetUserWithID(usertoteamDto.userid);


                teamx.Volunteer.Add(userx);
                var result = await _teamRepo.SaveAll();

                var teamxToReturn = _mapper.Map<TeamDto>(teamx);

                return Ok(teamxToReturn);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db no good");
            }

        }

        [HttpPost("removeuserfromteam")]
        public async Task<ActionResult<TeamDto>> removeVolunteerFromTeam(UsertoteamDto usertoteamDto)
        {
            try
            {

                var teamx = await _teamRepo.GetTeam(usertoteamDto.teamid);
                var userx = await _volunteerRepo.GetUserWithID(usertoteamDto.userid);


                teamx.Volunteer.Remove(userx);
                var result = await _teamRepo.SaveAll();

                var teamxToReturn = _mapper.Map<TeamDto>(teamx);

                return Ok(teamxToReturn);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db no good");
            }

        }


     
    }
}