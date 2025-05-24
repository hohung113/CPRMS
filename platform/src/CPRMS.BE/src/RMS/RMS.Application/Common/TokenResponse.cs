﻿namespace Rms.Application.Common
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt => IssuedAt.AddSeconds(ExpiresIn);
    }
}
