using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class School
    {
        public Guid Id { get; set; }
        public string schoolid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string grade { get; set; }
        public string phonenumber { get; set; }
        
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<ApplicationUser> SchoolPersonnel { get; set; }
        public virtual ICollection<Outreachschedule> Outreachschedules { get; set; }
    }
}
