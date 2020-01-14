using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Outreachschedule
    {
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Guid SchoolId { get; set; }
        public virtual School School { get; set; }

        public int DayofweekId { get; set; }
        public virtual Dayofweek Dayofweek { get; set; }
        // public int dayoftheweekId { get; set; }

        public string createdby { get; set; }
        public DateTime? starttime { get; set; }
        public DateTime? endtime { get; set; }
        public string classize { get; set; }
        public string comments { get; set; }

      
       

    }
}
