﻿namespace CourseManagement.WebAPI.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "User";
    }
}
