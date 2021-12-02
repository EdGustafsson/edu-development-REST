using AutoMapper;
using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using edu_development_REST.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.Data
{
    public class CourseSourceRepository : ICourseSourceRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseSourceRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(CourseSourceViewModel courseSource)
        {
            var newCourseSource = _mapper.Map<Course>(courseSource);
            _context.Entry(newCourseSource).State = EntityState.Added;
        }

        public void Delete(CourseSource courseSource)
        {
            _context.Entry(courseSource).State = EntityState.Deleted;
        }

        public async Task<CourseSource> GetCourseSourceByIdAsync(Guid id)
        {
            return await _context.CourseSources.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CourseSource>> GetCourseSourcesAsync()
        {
            return await _context.CourseSources.ToListAsync();
        }

        public void Update(CourseSourceViewModel updatedCourseSource, Guid id)
        {
            var courseSource = _mapper.Map<CourseSource>(updatedCourseSource);
            courseSource.Id = id;
            _context.Entry(courseSource).State = EntityState.Modified;
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
