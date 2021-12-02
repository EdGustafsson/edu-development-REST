using edu_development_REST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using edu_development_REST.ViewModels;

namespace edu_development_REST.Interfaces
{
    public interface IUserSourceRepository
    {
        void Add(UserSourceViewModel userSource);
        Task<IEnumerable<UserSource>> GetUserSourcesAsync();
        Task<UserSource> GetUserSourceByIdAsync(Guid id);
        void Delete(UserSource userSource);
        Task<bool> SaveAllAsync();
        void Update(UserSourceViewModel updatedUserSource, Guid id);
    }
}
