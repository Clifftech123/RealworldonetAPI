using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Commands.User
{

    public class RegisterUser : IRequest<UserDto>
    {
        public UserRegisterDto userdto { get; set; }
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUser, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public RegisterUserHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        // Register a new user and return the user details 
        public async Task<UserDto> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { UserName = request.userdto.Username, Email = request.userdto.Email };
            var result = await _userManager.CreateAsync(user, request.userdto.Password);

            if (!result.Succeeded)
            {

                throw new Exception("User registration failed.");
            }

            // Generate a token for the registered user
            var token = await _tokenService.CreateToken(user);
            return new UserDto
            {
                UserName = user.UserName,
                UserEmail = user.Email,
                Email = user.Email,
                Token = token,
                ImageUrl = "https://api.realworld.io/images/smiley-cyrus.jpeg"
            };
        }
    }
}
