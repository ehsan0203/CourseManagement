using CourseManagement.Application.DTOs;
using CourseManagement.Application.Interfaces;
using CourseManagement.Core.Entities;
using CourseManagement.Core.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly PasswordHasher<AppUser> _hasher = new();
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo , IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<AuthResult?> LoginAsync(string username, string password)
        {
            var user = await _userRepo.GetByUserNameAsync(username);
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed) return null;

            // تولید JWT
            var jwtToken = GenerateJwtToken(user);

            // تولید Refresh Token
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepo.UpdateAsync(user); // این متد باید در Repo پیاده‌سازی شده باشه

            return new AuthResult
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };
        }


        public async Task<bool> RegisterAsync(string username, string password, string role)
        {
            var existing = await _userRepo.GetByUserNameAsync(username);
            if (existing != null) return false;

            var user = new AppUser
            {
                Username = username,
                PasswordHash = _hasher.HashPassword(null!, password),
                Role = role
            };
            await _userRepo.AddAsync(user);
            return true;
        }

        public async Task<AuthResult?> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepo.GetByRefreshTokenAsync(refreshToken);
            if(user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow) return null;

            var newJwt = GenerateJwtToken(user);
            var newRefresh = Guid.NewGuid().ToString();

            user.RefreshToken = newRefresh;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepo.UpdateAsync(user);

            return new AuthResult
            {
                Token = newJwt,
                RefreshToken = newRefresh
            };
        }

        private string GenerateJwtToken(AppUser user)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name , user.Username),
                new Claim(ClaimTypes.Role , user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims:claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials:creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
