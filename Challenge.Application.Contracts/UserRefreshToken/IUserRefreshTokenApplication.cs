using Challenge.Application.Contracts.UserRefreshToken;
using Framework.Application;
using System;
using System.Threading.Tasks;

namespace Challenge.Application.Contracts.User
{
    public interface IUserRefreshTokenApplication
    {
        Task<bool> AnyByUserIdAsync(Guid userid);
        Task<OperationResult> AddAsync(CreateRefreshToken model);
        Task<OperationResult> EditAsync(DetailRefreshToken model);
        Task<DetailRefreshToken> GetByRefreshTekenAsync(Guid refreshtoken);
        Task<DetailRefreshToken> GetByUserIdAsync(Guid userid);
    }
}
