using System.Threading.Tasks;

namespace Framework.Application.Api.Jwt
{
    public interface IJwToken
    {
        Task<string> GetJwToken(string username);
    }
}
