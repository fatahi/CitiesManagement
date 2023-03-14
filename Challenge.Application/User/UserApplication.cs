using AutoMapper;
using Challenge.Domain.CityAgg;
using Challenge.Domain.UserAgg;
using Framework.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Application.Contracts.User
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserApplication(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Authenticate(LoginViewModel model)
        {
            var user=await _userRepository.FindAsync(x => x.UserName == model.Username);
            return _mapper.Map<Domain.UserAgg.User, UserViewModel>(user);
        }

        public async Task<UserViewModel> GetByIdAsync(Guid userid)
        {
            var user = await _userRepository.FindAsync(x => x.Id == userid);
            return _mapper.Map<Domain.UserAgg.User, UserViewModel>(user);
        }
    }
}
