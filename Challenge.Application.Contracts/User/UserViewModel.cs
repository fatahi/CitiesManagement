using System;

namespace Challenge.Application.Contracts.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
    }
}
