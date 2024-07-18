using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.User;
using RealworldonetAPI.Application.Queries.User;
using RealworldonetAPI.Domain.Contracts;
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
            var loginUserDto = request.User;

            return Ok(await Mediator.Send(new UserLogin { loginUserDto = loginUserDto }));


        }


        // Register User Endpoint - POST /api/users/register
        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequest request)
        {

            var userDto = request.User;


            var result = await Mediator.Send(new RegisterUser { userdto = userDto });

            // Handle the result of the registration operation
            if (result.Succeeded)
            {
                return Ok(new { Message = "User account created successfully" });
            }
            else
            {
                return BadRequest(new { Errors = result.Errors });
            }
        }



        // Get Current User Endpoint - GET /api/user
        /// <summary>
        ///  Get current user details
        /// </summary>
        /// <returns></returns>

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

        [HttpPut("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            var userDto = request.User;
            var result = await Mediator.Send(new UpdateUserCommand { User = userDto });
            return Ok(result);
        }

    }
}
