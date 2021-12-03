using AutoMapper;
using edu_development_REST.Entities;
using edu_development_REST.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserViewModel, User>();

            CreateMap<CourseViewModel, Course>();

            CreateMap<CourseMembershipViewModel, CourseMembership>();

            CreateMap<CourseSourceViewModel, CourseSource>();

            CreateMap<UserSourceViewModel, UserSource>();

        }
    }
}
