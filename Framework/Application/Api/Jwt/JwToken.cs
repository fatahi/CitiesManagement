using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application.Api.Jwt
{
    public class JwToken : IJwToken
    {
        private readonly IConfiguration _configuration;
        public JwToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetJwToken(string username)
        {
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Customer"),
            };
            var jwtSetting = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(jwtSetting.GetSection("key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(jwtSetting.GetSection("Lifetime").Value)),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
