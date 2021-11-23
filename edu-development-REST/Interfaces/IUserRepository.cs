using edu_development_REST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using edu_development_REST.ViewModels;

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
