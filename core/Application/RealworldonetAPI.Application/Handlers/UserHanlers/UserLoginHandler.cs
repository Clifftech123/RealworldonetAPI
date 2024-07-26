using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.DTO.user;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Commands.User
{
    /// <summary>
    /// Command for user login.
    /// </summary>
    public class UserLogin : IRequest<object>
    {
        /// <summary>
        /// Gets or sets the login user DTO.
        /// </summary>
        public required LoginUserWrapper? loginUserDto { get; set; }

        /// <summary>
        /// Handles the user login command.
        /// </summary>
        public class UserLoginHandler : IRequestHandler<UserLogin, object>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILoggerManager _logger;
            private readonly ITokenService _tokenService;

            /// <summary>
            /// Initializes a new instance of the <see cref="UserLoginHandler"/> class.
            /// </summary>
            /// <param name="userManager">The user manager for managing user accounts.</param>
            /// <param name="logger">The logger for logging information and errors.</param>
            /// <param name="tokenService">The service for creating tokens.</param>
            public UserLoginHandler(UserManager<ApplicationUser> userManager, ILoggerManager logger, ITokenService tokenService)
            {
                _userManager = userManager;
                _logger = logger;
                _tokenService = tokenService;
            }

            /// <summary>
            /// Handles the user login command.
            /// </summary>
            /// <param name="request">The user login request.</param>
            /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
            /// <returns>An object containing the user details and token if login is successful, otherwise an error message.</returns>
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
