using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Person
{
    public interface IPersonProvider
    {
        IEnumerable<Person> GetPersons();
    }
}
