using Microsoft.AspNetCore.Identity;

namespace RealworldonetAPI.Application.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(IdentityUser user);
    }
}
