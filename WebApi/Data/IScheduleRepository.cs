using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IScheduleRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        

        Task<IEnumerable<Outreachschedule>> GetSchedules();
        Task<Outreachschedule> GetOutreachSchedules(Guid Schoolid, string userid, int dayid);
        Task<Dayofweek> GetDayOfWeek(int dayid);

    }
}
