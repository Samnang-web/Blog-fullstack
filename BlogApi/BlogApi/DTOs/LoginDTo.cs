﻿namespace BlogApi.DTOs
{
    public class LoginDTo
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
