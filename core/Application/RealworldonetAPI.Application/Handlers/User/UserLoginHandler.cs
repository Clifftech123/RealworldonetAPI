using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Commands.User
{
    public class UserLogin : IRequest<object>
    {
        public required LoginUserDto loginUserDto { get; set; }

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
                var user = await _userManager.FindByNameAsync(request.loginUserDto.Email);
                if (user == null)
                {
                    _logger.LogWarn($"{nameof(UserLogin)}: Login failed for email {request.loginUserDto.Email}. " +
                        "Incorrect credentials.");
                    throw new Exception("Email or Password is incorrect");
                }

                   
                var token = await _tokenService.CreateToken(user);
                var response = new
                {
                    user = new
                    {
                        email = user.Email,
                        username = user.UserName,
                        bio = (string)null, 
                        image = "https://api.realworld.io/images/smiley-cyrus.jpeg",
                        token = token
                    }
                };

                return response;
            }

        }


    }
}



