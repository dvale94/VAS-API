using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class UserSchoolDto
    {
        public Guid schoolid;
        public string userid;

        public int dayoftheweek;

        public DateTime startime;
        public DateTime endtime { get; set; }
        public string classize { get; set; }
        public string comments { get; set; }
    }
}
