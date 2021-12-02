using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.Interfaces
{
    public interface IUnitOfWork
    {

        IUserRepository UserRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseMembershipRepository CourseMembershipRepository { get; }
        IUserSourceRepository UserSourceRepository { get; }
        ICourseSourceRepository CourseSourceRepository { get;  }
        Task<bool> Complete();
        bool HasChanges();
    }
}
