using MediatR;

namespace RealworldonetAPI.Application.Commands.Profile
{
    public class UnfollowUserCommand : IRequest<bool>
    {
        public string FollowerUsername { get; }
        public string FollowingUsername { get; }

        public UnfollowUserCommand(string followerUsername, string followingUsername)
        {
            FollowerUsername = followerUsername;
            FollowingUsername = followingUsername;
        }
    }
}
