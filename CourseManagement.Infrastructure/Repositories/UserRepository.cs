using CourseManagement.Core.Entities;
using CourseManagement.Core.Interfaces;
using CourseManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetByUserNameAsync(string userName)
        {
           return await _context.Users.FirstOrDefaultAsync(u=>u.Username == userName);
        }
        public async Task AddAsync(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(AppUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<AppUser?> GetByRefreshTokenAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>  u.RefreshToken == token);
        }


    }
}
