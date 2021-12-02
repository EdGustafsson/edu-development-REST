using AutoMapper;
using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using edu_development_REST.ViewModels;

namespace edu_development_REST.Data
{
    public class UserSourceRepository : IUserSourceRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserSourceRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserSource> GetUserSourceByIdAsync(Guid id)
        {
            return await _context.UserSources.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<UserSource>> GetUserSourcesAsync()
        {
            return await _context.UserSources.ToListAsync();
        }
        public void Add(UserSourceViewModel userSource)
        {
            var newUserSource = _mapper.Map<UserSource>(userSource);
            _context.Entry(newUserSource).State = EntityState.Added;
        }
        public void Delete(UserSource user)
        {
            _context.Entry(user).State = EntityState.Deleted;
        }
        public void Update(UserSourceViewModel updatedUserSource, Guid id)
        {
            var userSource = _mapper.Map<UserSource>(updatedUserSource);
            userSource.Id = id;
            _context.Entry(userSource).State = EntityState.Modified;
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
