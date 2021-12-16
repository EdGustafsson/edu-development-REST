using edu_development_REST.Entities;
using edu_development_REST.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace edu_development_REST.Interfaces
{
    public interface IUserRepository
    {
        void Add(UserViewModel user);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        void Delete(User user);
        Task<bool> SaveAllAsync();
        void Update(UserViewModel updatedUser, Guid id);
    }
}
