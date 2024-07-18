﻿namespace AuthMicroservice.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsEmailСonfirm { get; set; }
        public List<UserRoles> UserRoles { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
