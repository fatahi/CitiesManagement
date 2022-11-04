using AutoMapper;
using Challenge.Application.Contracts.UserRefreshToken;
using Challenge.Domain.CityAgg;
using Challenge.Domain.UserAgg;
using Framework.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Application.Contracts.User
{
    public class UserRefreshTokenApplication : IUserRefreshTokenApplication
    {
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IMapper _mapper;
        public UserRefreshTokenApplication(IUserRefreshTokenRepository userRefreshTokenRepository, IMapper mapper)
        {
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult> AddAsync(CreateRefreshToken model)
        {
            var resullt = new OperationResult();
            var refreshToken = _mapper.Map<CreateRefreshToken, Domain.UserAgg.UserRefreshToken>(model);
            resullt.ReturnValue = await _userRefreshTokenRepository.AddAsync(refreshToken, true);
            return resullt;
        }
        public async Task<OperationResult> EditAsync(DetailRefreshToken model)
        {
            var resullt = new OperationResult();
            try
            {
                var refreshToken = _mapper.Map<CreateRefreshToken, Domain.UserAgg.UserRefreshToken>(model);
                await _userRefreshTokenRepository.UpdateAsync(refreshToken, true);
                resullt.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                resullt.Failed("error in update refresh token.");
                resullt.IsSucceeded = false;
            }
            
            return resullt;
        }
        public async Task<bool> AnyByUserIdAsync(Guid userid)
        {
            return await _userRefreshTokenRepository.ExistsAsync(x => x.UserId == userid);
        }

        public async Task<DetailRefreshToken> GetByRefreshTekenAsync(Guid refreshtoken)
        {
            var refreshToken= await _userRefreshTokenRepository.FindAsync(x => x.RefreshToken == refreshtoken);
            return _mapper.Map<Domain.UserAgg.UserRefreshToken, DetailRefreshToken>(refreshToken);
        }

        public async Task<DetailRefreshToken> GetByUserIdAsync(Guid userid)
        {
            var refreshToken = await _userRefreshTokenRepository.FindAsync(x => x.UserId == userid);
            return _mapper.Map<Domain.UserAgg.UserRefreshToken, DetailRefreshToken>(refreshToken);
        }
    }
}
