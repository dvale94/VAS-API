using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }

        public string Pantherid { get; set; }
        public DateTime? date { get; set; }

        public DateTime? SignInTime { get; set; }
        public DateTime? SignOutTime { get; set; }

        public string Notes { get; set; }
    }
}
