using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class AttendeeDto
    {
        public string PantherNo { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsHost { get; set; }
        public string Useride { get; set; }
    }
}
