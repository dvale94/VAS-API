using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string PantherNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string major { get; set; }
        public bool Caravailable { get; set; }
        public string currentstatus { get; set; }
        public int mdcpsid { get; set; }
       
        public virtual ICollection<Outreachschedule> Outreachschedules { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
