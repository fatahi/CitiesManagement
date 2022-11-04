using Framework.Application;
using System;
using System.Threading.Tasks;

namespace Challenge.Application.Contracts.User
{
    public interface IUserApplication
    {
        Task<UserViewModel> Authenticate(LoginViewModel command);
        Task<UserViewModel> GetByIdAsync(Guid userid);
    }
}
