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
    public class SchoolController : ControllerBase
    {

        private readonly IVolunteerRepository _volunteerRepo;
        private readonly IMapper _mapper;
        //
        private readonly ISchoolRepository _schoolRepo;
        public SchoolController(
            IVolunteerRepository volunteerRepository,
            ISchoolRepository schoolRepository,
           IMapper mapper
            )
        {
            _volunteerRepo = volunteerRepository;
            _schoolRepo = schoolRepository;
            _mapper = mapper;
        }
        //
        [HttpGet]
        public async Task<ActionResult<List<SchoolDto>>> List()
        {
            var schools = await _schoolRepo.GetSchools();
            var schoolsToReturn =
                 _mapper.Map<List<SchoolDto>>(schools);

            return Ok(schoolsToReturn);
        }
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDto>> Details(Guid id)
        {
            var schoolx = await _schoolRepo.GetSchoolWithID(id);
            if (schoolx == null)
                return NotFound();

            var schoolxToReturn =
                 _mapper.Map<SchoolDto>(schoolx);

            return Ok(schoolxToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> create(School SchoolToCreate)
        {
            var newschool = new School()
            {
                schoolid = SchoolToCreate.schoolid,
                name = SchoolToCreate.name,
                address = SchoolToCreate.address,
                phonenumber = SchoolToCreate.phonenumber,
                grade = SchoolToCreate.grade
            };
            _schoolRepo.Add(newschool);

            return await _schoolRepo.SaveAll();
        }
        //
        [HttpPut("{schoolid}")]
        public async Task<ActionResult<bool>> Update(Guid schoolid, SchoolDto schoolDto)
        {
            try
            {
                var schoolx = await _schoolRepo.GetSchoolWithID(schoolid);
                if (schoolx == null)
                    return BadRequest($"could not find school with id: {schoolid}");

                schoolx.schoolid = schoolDto.schoolid ?? schoolx.schoolid;
                schoolx.name = schoolDto.name ?? schoolx.name;
                schoolx.address = schoolDto.address ?? schoolx.address;
                schoolx.phonenumber = schoolDto.phonenumber ?? schoolx.phonenumber;
                schoolx.grade = schoolDto.grade ?? schoolx.grade;
                
                var res = await _schoolRepo.SaveAll();

                var schoolxToReturn = _mapper.Map<SchoolDto>(schoolx);

                return Ok(schoolxToReturn);


            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }


        }
        //
        //
        [HttpDelete("{schoolid}")]
        public async Task<ActionResult<bool>> Delete(Guid schoolid)
        {

            var schoolx = await _schoolRepo.GetSchoolWithID(schoolid);

            if (schoolx == null)
                return BadRequest($"could not find School with id: {schoolid}");

            var noPersonels = schoolx.SchoolPersonnel.Count();

            if (noPersonels > 0)
                return BadRequest($"There are {noPersonels} personnels associated to this school");

            var noTeams = schoolx.Teams.Count();
            if (noTeams > 0)
                return BadRequest($"There are {noTeams} Teams associated to this school");

            var noSchedules = schoolx.Outreachschedules.Count();
            if (noSchedules > 0)
                return BadRequest($"There are {noSchedules} scheduless associated to this school");

            _schoolRepo.Delete(schoolx);

            var result = await _schoolRepo.SaveAll();
            return Ok(result);
        }
        // 
        [HttpPost("addpersonneltoschool")]
        public async Task<ActionResult<SchoolDto>> addPersonnelToSchool(PersonnelToSchoolDto personnelToSchoolDto)
        {
            try
            {

                var schoolx = await _schoolRepo.GetSchoolWithID(personnelToSchoolDto.schoolid);
                var userx = await _volunteerRepo.GetUserWithID(personnelToSchoolDto.personnelid);


                schoolx.SchoolPersonnel.Add(userx);
                var result = await _schoolRepo.SaveAll();

                var schoolxToReturn = _mapper.Map<SchoolDto>(schoolx);

                return Ok(schoolxToReturn);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db no good");
            }

        }

    }
}