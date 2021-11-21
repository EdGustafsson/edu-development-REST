using System;

namespace edu_development_REST.ViewModels
{
    public class CourseMembershipViewModel
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrolledDate { get; set; }
    }

}