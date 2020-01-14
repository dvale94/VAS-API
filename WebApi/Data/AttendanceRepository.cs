using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public class AttendanceRepository: IAttendanceRepository
    {
        private readonly AuthenticationContext _authContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration { get; }

        public AttendanceRepository(
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

        public async Task<Attendance> GetAttendanceWithID(Guid attendanceid)
        {
            var attendancex =
                await _authContext.Attendances
                .FirstOrDefaultAsync(at => at.Id == attendanceid);
            return attendancex;

        }
    }
}
