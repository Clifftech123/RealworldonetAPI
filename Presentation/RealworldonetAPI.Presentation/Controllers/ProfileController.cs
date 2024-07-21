using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Profile;
using RealworldonetAPI.Application.Queries.Profile;

namespace RealworldonetAPI.Presentation.Controllers
{
    public class ProfileController : BaseApiController
    {



        /// <summary>
        /// Follows a user.
        /// </summary>
        /// <param name="username">The username of the user to follow.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("follow/{username}")]
        public async Task<IActionResult> FollowUser(string username)
        {

            var followerUsername = User.Identity.Name;

            var command = new FollowUserCommand(followerUsername, username);
            var result = await Mediator.Send(command);

            if (result)
            {
                return Ok(new { Message = $"Successfully followed {username}." });
            }
            else
            {
                return BadRequest(new { Message = $"Failed to follow {username}." });
            }
        }


        [HttpGet("profiles/{username}/follow")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var query = new GetProfileQuery(username);
            var userProfile = await Mediator.Send(query);

            if (userProfile != null)
            {
                return Ok(userProfile);
            }
            else
            {
                return NotFound(new { Message = $"Profile for {username} not found." });
            }
        }



        [HttpDelete("profiles/{username}/unfollow")]
        public async Task<IActionResult> UnfollowUser(string username)
        {
            var followerUsername = User.Identity.Name;

            var command = new UnfollowUserCommand(followerUsername, username);
            var result = await Mediator.Send(command);

            if (result)
            {
                return Ok(new { Message = $"Successfully unfollowed {username}." });
            }
            else
            {
                return BadRequest(new { Message = $"Failed to unfollow {username}." });
            }
        }

    }


}

