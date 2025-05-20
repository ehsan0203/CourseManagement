using CourseManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult?> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string password, string role);
        Task<AuthResult?> RefreshTokenAsync(string refreshToken);

    }
}
