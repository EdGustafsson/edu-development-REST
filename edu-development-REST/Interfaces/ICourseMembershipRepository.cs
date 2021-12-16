using edu_development_REST.Entities;
using edu_development_REST.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace edu_development_REST.Interfaces
{
    public interface ICourseMembershipRepository
    {
        void Add(CourseMembershipViewModel courseMembership);
        Task<IEnumerable<CourseMembership>> GetCourseMembershipsAsync();
        void Update(CourseMembershipViewModel updatedCourseMembership, Guid id);

        Task<bool> SaveAllAsync();

        Task<CourseMembership> GetCourseMembershipByIdAsync(Guid id);
    }
}
