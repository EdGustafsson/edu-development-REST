using AutoMapper;
using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using edu_development_REST.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace edu_development_REST.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(CourseViewModel course)
        {
            var newCourse = _mapper.Map<Course>(course);
            _context.Entry(newCourse).State = EntityState.Added;
        }

        public void Delete(Course course)
        {
            _context.Entry(course).State = EntityState.Deleted;
        }

        public async Task<Course> GetCourseByIdAsync(Guid id)
        {
            return await _context.Courses.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public void Update(CourseViewModel updatedCourse, Guid id)
        {
            var course = _mapper.Map<Course>(updatedCourse);
            course.Id = id;
            _context.Entry(course).State = EntityState.Modified;
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
