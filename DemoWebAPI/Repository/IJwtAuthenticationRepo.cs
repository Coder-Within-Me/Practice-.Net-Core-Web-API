using System.Threading.Tasks;

namespace DemoWebAPI.Repository
{
    public interface IJwtAuthenticationRepo
    {
         string GenerateJWT(string UserId, string UserName, string Role);
        Task<(string, bool)> UserLogin(string UserName, string Password);
    }
}