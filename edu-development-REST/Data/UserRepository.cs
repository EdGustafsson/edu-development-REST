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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public void Add(UserViewModel user)
        {
            var newUser = _mapper.Map<User>(user);
            _context.Entry(newUser).State = EntityState.Added;
        }
        public void Delete(User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
        }
        public void Update(UserViewModel updatedUser, Guid id)
        {
            var user = _mapper.Map<User>(updatedUser);
            user.Id = id;
            _context.Entry(user).State = EntityState.Modified;
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
