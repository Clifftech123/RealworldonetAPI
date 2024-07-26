using MediatR;

namespace RealworldonetAPI.Application.Commands.Profile
{

    public class FollowUserCommand : IRequest<bool>
    {
        public string FollowerUsername { get; }
        public string FollowingUsername { get; }

        public FollowUserCommand(string followerUsername, string followingUsername)
        {
            FollowerUsername = followerUsername;
            FollowingUsername = followingUsername;
        }
    }
}
