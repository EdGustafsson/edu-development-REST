﻿using AutoMapper;
using edu_development_REST.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);
        public ICourseRepository CourseRepository => new CourseRepository(_context, _mapper);
        public ICourseMembershipRepository CourseMembershipRepository => new CourseMembershipRepository(_context, _mapper);
        public IUserSourceRepository UserSourceRepository => new UserSourceRepository(_context, _mapper);
        public ICourseSourceRepository CourseSourceRepository => new CourseSourceRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
