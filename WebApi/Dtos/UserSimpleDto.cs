using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Dtos
{
    public class UserSimpleDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PantherNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string major { get; set; }
        public bool Caravailable { get; set; }
        public string currentstatus { get; set; }
        public int mdcpsid { get; set; }

    
        
    }
}
