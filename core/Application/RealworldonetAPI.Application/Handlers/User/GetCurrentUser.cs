using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Queries.User
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUser;

        public GetCurrentUserQueryHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        // Get current user details based on the user ID from the token in the request header
        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return new UserDto
            {
                UserName = user.UserName,
                UserEmail = user.Email,
                ImageUrl = "https://api.realworld.io/images/smiley-cyrus.jpeg",
                Token = user.Token

            };
        }
    }
}

