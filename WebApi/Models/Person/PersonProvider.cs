using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Person
{
    public class PersonProvider : IPersonProvider
    {

        private IConfiguration _configuration { get; }
        public PersonProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Person> GetPersons()
        {
            IEnumerable<Person> personas = null;
            using (var sqlConnection = new SqlConnection(_configuration["OutreachConnectionString"]))
            {
                personas = sqlConnection.Query<Person>("select * from Person");
            }
            return personas;
        }
    }
}
