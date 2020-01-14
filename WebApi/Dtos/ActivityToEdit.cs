using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ActivityToEdit
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
    }
}
