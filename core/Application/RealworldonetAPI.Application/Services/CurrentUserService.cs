using Microsoft.AspNetCore.Http;
using RealworldonetAPI.Application.Interface;
using System.Security.Claims;

namespace RealworldonetAPI.Application.Services
{

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            // Assuming the user's ID is stored as the NameIdentifier claim
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
