using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Dtos
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<School, SchoolDto>();
            CreateMap<Team, TeamDto>();
            CreateMap<ApplicationUser, UserSimpleDto>(MemberList.Destination);
            CreateMap<Outreachschedule, ScheduleDto>();
            //.ForMember(d => d.Outreachschedules, o => o.Ignore())
            //.ForMember(d => d.UserActivities, o => o.Ignore());


            CreateMap<Activity, ActivityDto>();

            CreateMap<UserActivity, AttendeeDto>()
             .ForMember(d => d.UserName, o => o.MapFrom(s => s.ApplicationUser.UserName))
             .ForMember(d => d.PantherNo, o => o.MapFrom(s => s.ApplicationUser.PantherNo))
              .ForMember(d => d.Email, o => o.MapFrom(s => s.ApplicationUser.Email))
               .ForMember(d => d.Useride, o => o.MapFrom(s => s.ApplicationUser.Id));
        }
    }
}
