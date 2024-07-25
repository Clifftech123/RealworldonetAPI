using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.User;
using RealworldonetAPI.Application.Queries.User;
using RealworldonetAPI.Domain.Contracts;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.DTO.user.RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.models;
using RealworldonetAPI.Domain.models.User;

namespace RealworldonetAPI.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    public class UserController : BaseApiController
    {
        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="request">The user login request containing login details.</param>
        /// <returns>A response indicating the result of the login operation.</returns>
        [HttpPost("users/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var loginUserWrapper = new Application.DTO.user.LoginUserWrapper { User = request.User };

            return Ok(await Mediator.Send(new UserLogin { loginUserDto = loginUserWrapper }));
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The user registration information.</param>
        /// <returns>A response indicating the result of the registration operation.</returns>
        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterWrapper request)
        {
            var registerDto = request ?? new UserRegisterWrapper { User = new NewUser() };

            var result = await Mediator.Send(request: new RegisterUserCommand { userdto = registerDto });

            return Ok(result);
        }

        /// <summary>
        /// Gets the current user details.
        /// </summary>
        /// <returns>A response containing the current user details.</returns>

        [HttpGet("user")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(await Mediator.Send(new GetCurrentUserQuery()));
        }

        /// <summary>
        /// Updates the current user details.
        /// </summary>
        /// <param name="request">The user update request containing updated user details.</param>
        /// <returns>A response indicating the result of the update operation.</returns>

        [HttpPut("user")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            if (request.User == null)
            {
                return BadRequest("User details cannot be null.");
            }

            var userDto = request.User;
            var updateUserWrapper = new UpdateUserWrapper { User = userDto };

            var result = await Mediator.Send(new UpdateUserCommand { User = updateUserWrapper });
            return Ok(result);
        }
    }
}
