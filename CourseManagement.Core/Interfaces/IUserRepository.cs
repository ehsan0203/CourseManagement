using CourseManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByUserNameAsync(string userName);
        Task AddAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task<AppUser?> GetByRefreshTokenAsync(string token);
    }
}
