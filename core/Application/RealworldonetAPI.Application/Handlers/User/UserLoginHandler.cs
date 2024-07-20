using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Commands.User
{
    public class UserLogin : IRequest<object>
    {
        public required LoginUserWrapper? loginUserDto { get; set; }

        public class UserLoginHandler : IRequestHandler<UserLogin, object>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILoggerManager _logger;
            private readonly ITokenService _tokenService;

            public UserLoginHandler(UserManager<ApplicationUser> userManager, ILoggerManager logger, ITokenService tokenService)
            {
                _userManager = userManager;
                _logger = logger;
                _tokenService = tokenService;
            }

            public async Task<object> Handle(UserLogin request, CancellationToken cancellationToken)
            {
                try
                {

                    var user = await _userManager.FindByEmailAsync(request.loginUserDto?.User.Email);

                    if (user == null || !await _userManager.CheckPasswordAsync(user, request.loginUserDto?.User.Password))
                    {
                        _logger.LogWarn($"{nameof(UserLogin)}: Login failed for email {request.loginUserDto?.User.Email}. Incorrect credentials.");
                        return new { errors = new { body = new[] { "Email or Password is incorrect" } } };
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
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred in {nameof(UserLoginHandler)}: {ex.Message}");
                    return new { errors = new { body = new[] { "An error occurred while processing your request." } } };
                }
            }
        }
    }
}
