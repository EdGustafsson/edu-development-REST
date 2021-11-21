using edu_development_REST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        void Delete(User user);
        Task<bool> SaveAllAsync();
        void Update(User updatedUser, Guid id);
    }
}
