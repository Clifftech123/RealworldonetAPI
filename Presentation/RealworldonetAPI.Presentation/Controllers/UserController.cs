using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.User;
using RealworldonetAPI.Application.Queries.User;
using RealworldonetAPI.Domain.Contracts;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.DTO.user.RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.models.User;

namespace RealworldonetAPI.Presentation.Controllers
{
    public class UserController : BaseApiController
    {

        // Login User Endpoint - POST /api/users/login

        /// <summary>
        ///   exiting user  Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("users/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var loginUserWrapper = new LoginUserWrapper { User = request.User };

            return Ok(await Mediator.Send(new UserLogin { loginUserDto = loginUserWrapper }));
        }


        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The user registration information.</param>
        /// <returns>A response indicating the result of the registration operation.</returns>
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterWrapper request)
        {
            var registerDto = request ?? new UserRegisterWrapper { User = new UserRegisterDto() };

            var result = await Mediator.Send(new RegisterUserCommand { userdto = registerDto });

            return Ok(result);
        }


        // Get Current User Endpoint - GET /api/user
        /// <summary>
        ///  Get current user details
        /// </summary>
        /// <returns></returns>

        [Authorize]
        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]

        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(await Mediator.Send(new GetCurrentUserQuery()));
        }



        // Update User Endpoint - PUT /api/user
        /// <summary>
        ///  Update  current user 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [Authorize]
        [HttpPut("user")]

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
