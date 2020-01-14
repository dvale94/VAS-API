using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class SchoolDto
    {
        public Guid Id { get; set; }
        public string schoolid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string grade { get; set; }
        public string phonenumber { get; set; }

        public virtual ICollection<TeamDto> Teams { get; set; }
        public virtual ICollection<UserSimpleDto> SchoolPersonnel { get; set; }
        //[JsonProperty("personnel")]
        // public ICollection<AttendeeDto> UserPersonnel { get; set; }
    }
}
