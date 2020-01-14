using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface ISchoolRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
       
        Task<IEnumerable<School>> GetSchools();
        Task<School> GetSchoolWithID(Guid schoolid);
        
    }
}
