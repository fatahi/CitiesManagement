﻿using System;

namespace Challenge.Domain.UserAgg
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsValid { get; set; }
        public virtual User User { get; set; }
    }
}
