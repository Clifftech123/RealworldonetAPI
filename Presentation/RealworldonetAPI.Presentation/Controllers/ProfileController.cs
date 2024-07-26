using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Profile;
using RealworldonetAPI.Application.Queries.Profile;

namespace RealworldonetAPI.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing user profiles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
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

        /// <summary>
        /// Gets the profile of a user.
        /// </summary>
        /// <param name="username">The username of the user whose profile is to be retrieved.</param>
        /// <returns>An IActionResult containing the user profile or a not found message.</returns>
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

        /// <summary>
        /// Unfollows a user.
        /// </summary>
        /// <param name="username">The username of the user to unfollow.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
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
