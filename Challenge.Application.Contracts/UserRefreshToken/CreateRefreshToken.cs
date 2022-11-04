using System;

namespace Challenge.Application.Contracts.UserRefreshToken
{
    public class CreateRefreshToken
    {
        public Guid UserId { get; set; }
        public Guid RefreshToken { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
