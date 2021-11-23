using edu_development_REST.Entities;
using edu_development_REST.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace edu_development_REST.Interfaces
{
    public interface ICourseRepository
    {
        void Add(CourseViewModel course);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByCourseCodeAsync(Guid id);
        void Delete(Course course);
        Task<bool> SaveAllAsync();
        void Update(CourseViewModel updatedCourse, Guid id);
    }
}
