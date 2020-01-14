using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Dtos
{
    public class ScheduleDto
    {

        public string ApplicationUserId { get; set; }
        public virtual UserSimpleDto ApplicationUser { get; set; }
       

        public Guid SchoolId { get; set; }
        public virtual SchoolDto School { get; set; }

        public int DayofweekId { get; set; }

        public string createdby { get; set; }
        public DateTime? starttime { get; set; }
        public DateTime? endtime { get; set; }
        public string classize { get; set; }
        public string comments { get; set; }
        
    }
}
