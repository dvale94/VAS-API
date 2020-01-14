using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IVolunteerRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
       /*
        Task<IEnumerable<School>> GetSchools();
        Task<School> GetSchoolWithID(Guid schoolid);
        
        Task<IEnumerable<Outreachschedule>> GetSchedules();
        Task<Outreachschedule> GetOutreachSchedules(Guid Schoolid, string userid, int dayid);

        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeam(Guid teamid);
        */
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUserWithID(string userid);
        /*
        IEnumerable<Team> GetTeamsDapper();
        IEnumerable<ApplicationUser> GetAllUsersDapper();
       
        Task<Dayofweek> GetDayOfWeek(int dayid);
        */
    }
}
