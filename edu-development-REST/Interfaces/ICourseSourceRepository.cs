using edu_development_REST.Entities;
using edu_development_REST.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace edu_development_REST.Interfaces
{
    public interface ICourseSourceRepository
    {
        void Add(CourseSourceViewModel courseSource);
        Task<IEnumerable<CourseSource>> GetCourseSourcesAsync();
        Task<CourseSource> GetCourseSourceByIdAsync(Guid id);
        void Delete(CourseSource courseSource);
        Task<bool> SaveAllAsync();
        void Update(CourseSourceViewModel updatedCourseSourceSource, Guid id);
    }
}
