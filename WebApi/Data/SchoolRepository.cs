using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly AuthenticationContext _authContext;
        public SchoolRepository(AuthenticationContext authContext)
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

        public async Task<bool> SaveAll()
        {
            return await _authContext.SaveChangesAsync() > 0;
        }
    }
}
