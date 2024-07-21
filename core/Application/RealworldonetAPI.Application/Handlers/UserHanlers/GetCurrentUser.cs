using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Queries.User
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, object>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUser;
        private readonly ITokenService _tokenService;

        public GetCurrentUserQueryHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUser, ITokenService tokenService)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"User with {user} not found.");
            }

            var token = await _tokenService.CreateToken(user);


            var response = new
            {
                user = new
                {
                    id = user.Id,
                    email = user.Email,
                    username = user.UserName,
                    bio = user.Bio,
                    image = user.Image,
                    token = token
                }
            };

            return response;
        }
    }
}
