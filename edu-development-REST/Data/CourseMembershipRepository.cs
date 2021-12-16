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
    public class CourseMembershipRepository : ICourseMembershipRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseMembershipRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CourseMembership>> GetCourseMembershipsAsync()
        {
            return await _context.CourseMemberships.ToListAsync();
        }

        public async Task<CourseMembership> GetCourseMembershipByIdAsync(Guid id)
        {
            return await _context.CourseMemberships.SingleOrDefaultAsync(c => c.Id == id);
        }
        public void Add(CourseMembershipViewModel courseMembership)
        {
            var newCourseMembership = _mapper.Map<CourseMembership>(courseMembership);
            _context.Entry(newCourseMembership).State = EntityState.Added;
        }

        public void Update(CourseMembershipViewModel updatedCourseMembership, Guid id)
        {
            var courseMembership = _mapper.Map<CourseMembership>(updatedCourseMembership);
            courseMembership.Id = id;
            _context.Entry(courseMembership).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
