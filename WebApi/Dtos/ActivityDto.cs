using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }

        [JsonProperty("attendees")]
        public ICollection<AttendeeDto> UserActivities { get; set; }
    }
}
