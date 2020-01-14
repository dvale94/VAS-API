using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Teamnumber { get; set; }
        public string Description { get; set; }
       
        public virtual ICollection<ApplicationUser> Volunteer { get; set; }
    }
}
