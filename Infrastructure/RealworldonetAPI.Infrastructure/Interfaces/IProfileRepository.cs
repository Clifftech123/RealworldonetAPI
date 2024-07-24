
using RealworldonetAPI.Application.DTO.user;

namespace RealworldonetAPI.Infrastructure.Interfaces
{
    public interface IProfileRepository
    {
        Task<UserProfile> GetProfileAsync(string username);
        Task<bool> FollowUserAsync(string followerUsername, string followingUsername);
        Task<bool> UnfollowUserAsync(string followerUsername, string followingUsername);
    }
}
