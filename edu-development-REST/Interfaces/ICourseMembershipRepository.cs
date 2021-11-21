using edu_development_REST.Entities;
using edu_development_REST.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.Interfaces
{
    public interface ICourseMembershipRepository
    {
        void Add(CourseMembership courseMembership);
        Task<IEnumerable<CourseMembership>> GetCourseMembershipsAsync();
        void Update(CourseMembership updatedCourseMembership, Guid id);
    }
}
