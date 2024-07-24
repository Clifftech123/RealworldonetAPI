using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Commands.User
{

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public RegisterUserHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }


        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = request.userdto?.User;
            if (userDto == null)
            {
                throw new ArgumentException("User registration data is missing.");
            }

            // Validate the password
            if (!userDto.Password.Any(char.IsDigit))
            {
                throw new ArgumentException("Password must include at least one number");
            }

            // Check if the username or email already exists
            var existingUserByUsername = await _userManager.FindByNameAsync(userDto.Username);
            var existingUserByEmail = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUserByUsername != null)
            {
                throw new ArgumentException("Username already exists.");
            }
            if (existingUserByEmail != null)
            {
                throw new ArgumentException("Email already exists.");
            }

            // Set the default image URL
            var defaultImageUrl = "https://api.realworld.io/images/smiley-cyrus.jpeg";

            var user = new ApplicationUser
            {
                UserName = userDto.Username,
                Email = userDto.Email,
                Image = defaultImageUrl
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                // Aggregate IdentityResult errors into a single exception message
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errors}");
            }

            // Generate a token for the registered user
            var token = await _tokenService.CreateToken(user);

            // Return the UserDto including the default image URL
            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = token,
                Image = user.Image
            };
        }


    }
}
