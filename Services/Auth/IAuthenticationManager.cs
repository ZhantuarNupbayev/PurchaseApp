using DataTransfer.DTO.Users;
using System.Threading.Tasks;

namespace Services.Auth
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
