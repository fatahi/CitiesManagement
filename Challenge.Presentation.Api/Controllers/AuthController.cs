using Challenge.Application.Contracts.User;
using Challenge.Application.Contracts.UserRefreshToken;
using Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IConfiguration configuration;
        private readonly IUserApplication _userApplication;
        private readonly IUserRefreshTokenApplication _userRefreshTokenApplication;
        public AuthController(IConfiguration configuration, IUserApplication userApplication,
            IUserRefreshTokenApplication userRefreshTokenApplication)
        {
            this.configuration = configuration;
            _userApplication = userApplication;
            _userRefreshTokenApplication = userRefreshTokenApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user =await _userApplication.Authenticate(model);

            #region Validate User

            if (user == null) return BadRequest("User information is incorrect.");
            if (!user.IsActive)
            {
                return BadRequest("Your account is inactive.");
            }
            var hashPassword = Encryption.HashPasswordWithSalt(model.Password, user.PasswordSalt);
            if (user.Password != hashPassword)
            {
                return BadRequest("Username and password are incorrect.");
            }

            #endregion

            var result = await GetTokenWithRefreshToken(user);

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> GetNewToken(RefreshTokenViewModel model)
        {
            //calims from token => userId
            var userRefreshToken = await _userRefreshTokenApplication.GetByRefreshTekenAsync(model.RefreshToken);

            if (userRefreshToken == null) return BadRequest("The refresh of the sent token is not correct.");

            if (!userRefreshToken.IsValid) return BadRequest("Refresh token sending is not valid.");


            var refreshTokenTimeout = configuration.GetValue<int>("RefreshTokenTimeOut");
            if (DateTime.Now.CompareTo(userRefreshToken.CreateDate.AddMinutes(refreshTokenTimeout)) != -1)
                return BadRequest("The token refresh time has expired.");

            //create new token
            //update new refresh token or update refresh token createDate
            var user = await _userApplication.GetByIdAsync(userRefreshToken.UserId);
            var result = await GetTokenWithRefreshToken(user, false);

            return Ok(result);
        }


        private async Task<AuthenticateViewModel> GetTokenWithRefreshToken(UserViewModel user, bool callFromAuthenticate = true)
        {
            //read token time & key from appsetting.json
            var secretKey = configuration.GetValue<string>("TokenKey");
            var tokenTimeOut = configuration.GetValue<int>("TokenTimeOut");
            var refreshTokenTimeout = configuration.GetValue<int>("RefreshTokenTimeOut");

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.UserName),
                }),

                Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            var refreshToken = Guid.NewGuid();

            var hasUserRefreshTokenRow = await _userRefreshTokenApplication.AnyByUserIdAsync(user.Id);
            if (callFromAuthenticate && !hasUserRefreshTokenRow)
            {
                //insert new refresh token
                //insert refreshToken + userId in db
                var userRefreshToken = new CreateRefreshToken
                {
                    CreateDate = DateTime.Now,
                    IsValid = true,
                    RefreshToken = refreshToken,
                    UserId = user.Id,
                };
                await _userRefreshTokenApplication.AddAsync(userRefreshToken);
            }
            else
            {
                //update refresh token
                var userRefreshTokenItem = await _userRefreshTokenApplication.GetByUserIdAsync(user.Id);
                userRefreshTokenItem.RefreshToken = refreshToken;
                userRefreshTokenItem.CreateDate = DateTime.Now;
                userRefreshTokenItem.IsValid = true;
                await _userRefreshTokenApplication.EditAsync(userRefreshTokenItem);
            }

            var result = new AuthenticateViewModel()
            {
                RefreshToken = refreshToken.ToString(),
                Token = token,
                FullName = user.UserName,
            };
            return result;
        }
    }
}
