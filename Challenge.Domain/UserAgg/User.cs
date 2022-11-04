using System;
using System.Collections.Generic;

namespace Challenge.Domain.UserAgg
{
    public class User
    {
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public Guid PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
