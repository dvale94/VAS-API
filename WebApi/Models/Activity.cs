using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Activity
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }

        public virtual ICollection<UserActivity> UserActivities { get; set; }
    }
}
