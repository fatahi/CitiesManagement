using System;

namespace Challenge.Application.Contracts.UserRefreshToken
{
    public class RefreshTokenViewModel
    {
        public Guid RefreshToken { get; set; }
        public string Token { get; set; }
    }
}
