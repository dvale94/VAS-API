using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string Teamnumber { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserSimpleDto> Volunteer { get; set; }
    }
}
