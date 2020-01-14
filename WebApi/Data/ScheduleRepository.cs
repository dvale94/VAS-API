using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AuthenticationContext _authContext;
        public ScheduleRepository(AuthenticationContext authContext)
        {
            _authContext = authContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _authContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _authContext.Remove(entity);
        }

        public async Task<Dayofweek> GetDayOfWeek(int dayid)
        {
            var dayweek = await _authContext.Dayofweeks.FirstOrDefaultAsync(d => d.Id == dayid);

            return dayweek;
        }

        public async Task<Outreachschedule> GetOutreachSchedules(Guid schoolid, string userid, int dayid)
        {
            var oSchedulex = await _authContext.Outreachschedules
                .FirstOrDefaultAsync(os =>
                       os.SchoolId == schoolid && os.ApplicationUserId == userid && os.DayofweekId == dayid);

            return oSchedulex;
        }

        public async Task<IEnumerable<Outreachschedule>> GetSchedules()
        {
            var schedules = await _authContext.Outreachschedules.ToListAsync();

            return schedules;
        }

        public async Task<bool> SaveAll()
        {
            return await _authContext.SaveChangesAsync() > 0;
        }
    }
}
