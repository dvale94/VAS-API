using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserActivity
    {
        public string ApplicationUserId { get; set; }
        public  virtual ApplicationUser ApplicationUser { get; set; }
        public Guid ActivityId { get; set; }
        public  virtual Activity Activity { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
        public string Urole { get; set; }
        public bool? ableToCheckInOut { get; set; }
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public  float? hoursSubmitted { get; set; }
        public float? hoursApproved { get; set; }

    }
}
