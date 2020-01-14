using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class OutreachSchoolDayDto
    {
        public string userid { get; set; }
       

        public Guid schoolid { get; set; }
        

        public int dayid { get; set; }
    }
}
