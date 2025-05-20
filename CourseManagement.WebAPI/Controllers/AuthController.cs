using CourseManagement.Application.Interfaces;
using CourseManagement.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var success =await _authService.RegisterAsync(dto.Username,dto.Password,dto.Role);
            if (!success) return BadRequest("نام کاربری تکراری است.");
            return Ok("ثبت نام با موفقیت انجام شد.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto.Username,dto.Password);
            if (token == null) return Unauthorized("نام کاربری یا رمز عبور اشتباه است.");

            return Ok(new { token });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);    
            if (result == null) return Unauthorized("توکن نامعتبر یا منقضی شده است.");

            return Ok(result);
        }


        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var username = User.Identity?.Name; // از ClaimTypes.Name
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Username = username,
                Role = role
            });
        }

    }
}
