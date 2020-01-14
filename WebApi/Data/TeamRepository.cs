using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AuthenticationContext _authContext;
        public TeamRepository(AuthenticationContext authContext)
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

        public async Task<Team> GetTeam(Guid teamid)
        {
            var teamx =
                await _authContext.Teams.FirstOrDefaultAsync(t => t.Id == teamid);
            return teamx;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            var teams = await _authContext.Teams.ToListAsync();
            return teams;
        }

        public async Task<bool> SaveAll()
        {
            return await _authContext.SaveChangesAsync() > 0;
        }
    }
}
