using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace WebApi.Data
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly AuthenticationContext _authContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration { get; }
        public VolunteerRepository(
            AuthenticationContext authContext,
            UserManager<ApplicationUser> userManager,
             IConfiguration configuration
            )
        {
            _authContext = authContext;
            _userManager = userManager;
            _configuration = configuration;
        }
        public void Add<T>(T entity) where T : class
        {
            _authContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _authContext.Remove(entity);
        }
        public async Task<bool> SaveAll()
        {
            return await _authContext.SaveChangesAsync() > 0;
        }
       /*
        public async Task<IEnumerable<School>> GetSchools()
        {
            var schools =
                await _authContext.Schools
                .Include(sch => sch.Teams)
                .ToListAsync();
            return schools;
        }
        public async Task<School> GetSchoolWithID(Guid schoolid)
        {
            var schoolx =
                await _authContext.Schools
                .Include(s => s.Teams)
                .FirstOrDefaultAsync(s => s.Id == schoolid);
            return schoolx;
        }
       
        public async Task<IEnumerable<Team>> GetTeams()
        {
            var teams = await _authContext.Teams.ToListAsync();
            return teams;
        }
        public async Task<Team> GetTeam(Guid teamid)
        {
            var teamx =
                await _authContext.Teams.FirstOrDefaultAsync(t => t.Id == teamid);
            return teamx;
        }
        public  IEnumerable<Team> GetTeamsDapper()
        {

            IEnumerable<Team> teams = null;
            using (var sqlConnection = new SqlConnection(_configuration["OutreachConnectionString"]))
            {
                teams = sqlConnection.Query<Team>("select * from Teams");
            }
            return teams;

            
        }
        public IEnumerable<ApplicationUser> GetAllUsersDapper()
        {
            IEnumerable<ApplicationUser> allusers = null;
            using (var sqlConnection = new SqlConnection(_configuration["OutreachConnectionString"]))
            {
                allusers = sqlConnection.Query<ApplicationUser>("select * from AspNetUsers");
            }
            return allusers;
        }
        */
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            var allusers = await _authContext
                .ApplicationUsers
                .ToListAsync();
            return allusers;
        }
        public async Task<ApplicationUser> GetUserWithID(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);

            return user;
        }
        /*
        public async Task<IEnumerable<Outreachschedule>> GetSchedules()
        {
            var schedules = await _authContext.Outreachschedules.ToListAsync();

            return schedules;
        }

        public async Task<Outreachschedule> GetOutreachSchedules(Guid schoolid, string userid, int dayid)
        {

            var oSchedulex = await _authContext.Outreachschedules
                .FirstOrDefaultAsync(os =>
                       os.SchoolId == schoolid && os.ApplicationUserId == userid && os.DayofweekId == dayid);

            return oSchedulex;
        }

        public async Task<Dayofweek> GetDayOfWeek(int dayid)
        {
            var dayweek = await _authContext.Dayofweeks.FirstOrDefaultAsync(d => d.Id == dayid);

            return dayweek;
        }
        */
    }
}
